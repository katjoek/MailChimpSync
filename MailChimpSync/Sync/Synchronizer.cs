// <copyright file="Synchronizer.cs" company="Mark van de Veerdonk">
//     MailChimpSync - Synchronize a local data source with a MailChimp Audience
//     Copyright (C) 2019  Mark van de Veerdonk
//
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
//
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
//
//     You should have received a copy of the GNU General Public License
//     along with this program. If not, see &lt;https://www.gnu.org/licenses/&gt;
// </copyright>

namespace MailChimpSync.Sync
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using MailChimp.Net;
    using MailChimp.Net.Interfaces;
    using MailChimp.Net.Models;
    using MailChimpSync.Config;
    using MailChimpSync.LocalData;
    using MailChimpSync.Misc;

    /// <summary>
    /// The class capable of actually performing the synchronization task.
    /// </summary>
    internal class Synchronizer
    {
        private IMailChimpManager manager;
        private SyncConfig config;
        private List<Member> members;
        private DataSet data;

        /// <summary>
        /// Initializes a new instance of the <see cref="Synchronizer"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="members">The members.</param>
        public Synchronizer(SyncConfig config, List<Member> members)
        {
            manager = new MailChimpManager(config.MailChimpApiKey);
            this.config = config;
            this.members = members;
            data = LocalDataLoader.GetData(config.LocalDataConnectionString);
        }

        /// <summary>
        /// Synchronizes the local data with MailChimp.
        /// </summary>
        /// <returns>an awaitable task</returns>
        public async Task Sync()
        {
            var table = data.Tables[0];
            Dictionary<string, List<int>> emailAddressToRowsDict = GetEmailAddressRowsLookup(table);
            Dictionary<string, string> emailAddressNamesDict = GetNamesForEmailaddresses(table, emailAddressToRowsDict);
            Dictionary<string, HashSet<string>> emailAddressInterestsDict = GetInterestsForEmailAddress(table, emailAddressToRowsDict);

            var currentEmailAddresses = new HashSet<string>(members.Select(m => m.EmailAddress.ToUpperInvariant()));
            var goalEmailAddresses = new HashSet<string>(emailAddressToRowsDict.Keys);

            var addressesToDelete = new HashSet<string>(currentEmailAddresses);
            addressesToDelete.ExceptWith(goalEmailAddresses);

            if (!await DeleteAddresses(addressesToDelete))
            {
                Logger.Log("Aborting synchronization");
                return;
            }

            if (!await AddOrUpdateAddresses(goalEmailAddresses, emailAddressNamesDict, emailAddressInterestsDict))
            {
                Logger.Log("Aborting synchronization");
                return;
            }

            Logger.Log("Done!");
        }

        private async Task<bool> AddOrUpdateAddresses(
            HashSet<string> addressesToAddOrUpdate,
            Dictionary<string, string> emailAddressNamesDict,
            Dictionary<string, HashSet<string>> emailAddressInterestsDict)
        {
            foreach (var address in addressesToAddOrUpdate)
            {
                var member = new Member()
                {
                    EmailAddress = address,
                    StatusIfNew = Status.Subscribed,
                    ListId = config.MailingListId,
                };

                var remote = members.Find(m => string.Equals(m.EmailAddress, member.EmailAddress, StringComparison.InvariantCultureIgnoreCase));
                if (remote != null)
                {
                    foreach (var interest in remote.Interests.Where(i => i.Value))
                    {
                        member.Interests[interest.Key] = false;
                    }
                }

                foreach (var interest in emailAddressInterestsDict[address])
                {
                    member.Interests[interest] = true;
                }

                member.SetName(string.Empty, string.Empty, emailAddressNamesDict[address]);
                try
                {
                    if (RemoteIsMissingOrDifferent(member))
                    {
                        Logger.Log($"Add or update email address {address} for {emailAddressNamesDict[address]}");
                        await manager.Members.AddOrUpdateAsync(config.MailingListId, member);
                    }
                    else
                    {
                        Logger.Log($"No update needed for email addres {address}");
                    }
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch (Exception e)
                {
                    Logger.Log(e.Message);
                    Logger.Log("Continuing with next member.");
                }
#pragma warning restore CA1031 // Do not catch general exception types
            }

            return true;
        }

        private bool RemoteIsMissingOrDifferent(Member member)
        {
            var remote = members.Find(m => string.Equals(m.EmailAddress, member.EmailAddress, StringComparison.InvariantCultureIgnoreCase));
            if (remote == null)
            {
                return true;
            }

            if (!remote.EqualFullName(member))
            {
                return true;
            }

            var activeInterests = remote.Interests
                .Where(t => t.Value)
                .Select(t => t.Key)
                .OrderBy(i => i);
            if (!activeInterests.SequenceEqual(member.Interests.Keys.OrderBy(i => i)))
            {
                return true;
            }

            return false;
        }

        private async Task<bool> DeleteAddresses(HashSet<string> addressesToDelete)
        {
            if (addressesToDelete.Count > 0)
            {
                Logger.Log("Deleting no longer present members");
            }

            foreach (var address in addressesToDelete)
            {
                var member = members.Find(m => string.Equals(m.EmailAddress, address, StringComparison.InvariantCultureIgnoreCase));
                if (member != null)
                {
                    var msg = $"Delete email address {member.EmailAddress} for {member.GetFullName()}";
                    var res = MessageBox.Show(msg, "Delete?", MessageBoxButtons.YesNoCancel);
                    if (res == DialogResult.Cancel)
                    {
                        return false;
                    }

                    if (res == DialogResult.No)
                    {
                        continue;
                    }

                    Logger.Log($"Deleting {member.EmailAddress}");
                    await manager.Members.DeleteAsync(config.MailingListId, member.EmailAddress);
                }
                else
                {
                    Logger.Log($"Unexpectedly did not find email address '{address}' in members. Cannot delete.");
                }
            }

            return true;
        }

        private Dictionary<string, HashSet<string>> GetInterestsForEmailAddress(DataTable table, Dictionary<string, List<int>> emailAddressToRowsDict)
        {
            var emailAddressInterestsDict = new Dictionary<string, HashSet<string>>();
            foreach (var emailAddress in emailAddressToRowsDict.Keys)
            {
                var interests = new HashSet<string>();
                foreach (var rowIdx in emailAddressToRowsDict[emailAddress])
                {
                    var row = table.Rows[rowIdx];
                    foreach (var interestConfig in config.InterestConfigs)
                    {
                        if (row[interestConfig.LocalColumn].ToString() == interestConfig.MembershipValue)
                        {
                            interests.Add(interestConfig.MailChimpInterest.Id);
                        }
                    }
                }
                emailAddressInterestsDict[emailAddress] = interests;
            }

            return emailAddressInterestsDict;
        }

        private Dictionary<string, string> GetNamesForEmailaddresses(DataTable table, Dictionary<string, List<int>> emailAddressToRowsDict)
        {
            Dictionary<string, string> emailAddressNamesDics = new Dictionary<string, string>();
            foreach (var emailAddress in emailAddressToRowsDict.Keys)
            {
                var names = new HashSet<string>();
                foreach (var rowIdx in emailAddressToRowsDict[emailAddress])
                {
                    var name = GetFullName(table, rowIdx);
                    names.Add(name);
                }
                emailAddressNamesDics[emailAddress] = string.Join(", ", names.ToArray());
            }

            return emailAddressNamesDics;
        }

        private Dictionary<string, List<int>> GetEmailAddressRowsLookup(DataTable table)
        {
            var emailAddressDict = new Dictionary<string, List<int>>();
            for (int rowIdx = config.FirstDataRow; rowIdx < table.Rows.Count; ++rowIdx)
            {
                var emailAddressList = table.Rows[rowIdx][config.EmailAddressColumn].ToString().Trim();
                if (config.EmailAddressColumn2 != -1)
                {
                    var email2 = table.Rows[rowIdx][config.EmailAddressColumn2].ToString().Trim();
                    if (email2.Length > 0)
                    {
                        emailAddressList += ";" + email2;
                    }
                }
                IEnumerable<string> emailAddresses = emailAddressList.Split(';');
                if (emailAddresses.Count() == 1)
                {
                    emailAddresses = emailAddressList.Split(' ');
                }
                emailAddresses = emailAddresses.Distinct();
                emailAddresses = emailAddresses.Select(s => s.Trim()).Where(s => s.Length > 0);

                foreach (var emailAddress in emailAddresses)
                {
                    var emailAddressLower = emailAddress.ToUpperInvariant();
                    if (!emailAddressDict.ContainsKey(emailAddressLower))
                    {
                        emailAddressDict[emailAddressLower] = new List<int>();
                    }

                    emailAddressDict[emailAddressLower].Add(rowIdx);
                }
            }

            for (var i = 1; i <= 5; ++i)
            {
                Logger.Log($"Number of addresses used for {i} person{(i > 1 ? "s" : string.Empty)}: {emailAddressDict.Values.Count(l => l.Count == i)}");
            }

            return emailAddressDict;
        }

        private string GetFullName(DataTable table, int rowIdx)
        {
            var nameParts = new List<string>();
            foreach (var nameColumnIdx in config.NameColumns)
            {
                nameParts.Add(table.Rows[rowIdx][nameColumnIdx].ToString());
            }
            var fullName = NameHelper.GetFullName(nameParts);
            return fullName;
        }
    }
}
