// <copyright file="SyncConfig.cs" company="Mark van de Veerdonk">
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

namespace MailChimpSync.Config
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The configuration needed to sync local tabular data with a MailChimp mailing list
    /// </summary>
    public class SyncConfig
    {
        private static readonly SimpleAES SimpleAes = new SimpleAES();

        /// <summary>
        /// Gets or sets the encrypted MailChimp API key.
        /// </summary>
        public string EncryptedMailChimpApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(MailChimpApiKey))
                {
                    return string.Empty;
                }
                else
                {
                    return SimpleAes.EncryptToString(MailChimpApiKey);
                }
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MailChimpApiKey = string.Empty;
                }
                else
                {
                    MailChimpApiKey = SimpleAes.DecryptString(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the identifier of the MailChimp mailing list to sync.
        /// </summary>
        public string MailingListId { get; set; }

        /// <summary>
        /// Gets or sets the local data source from which to retrieve data to sync with MailChimp.
        /// </summary>
        public string LocalDataConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the index of the first row that contains data to sync.
        /// </summary>
        public int FirstDataRow { get; set; } = 1;

        /// <summary>
        /// Gets or sets the index of the row that contains the column names.
        /// </summary>
        public int? HeaderRow { get; set; } = 0;

        /// <summary>
        /// Gets or sets the indices of the columns that when concatenated (deleting superfluous spaces)
        /// will yield the full name of a person.
        /// </summary>
        public List<int> NameColumns { get; set; } = new List<int>();

        /// <summary>
        /// Gets or sets the email address column index.
        /// </summary>
        public int EmailAddressColumn { get; set; } = -1;

        /// <summary>
        /// Gets or sets the email address column index.
        /// </summary>
        public int EmailAddressColumn2 { get; set; } = -1;

        /// <summary>
        /// Gets or sets the configuration needed to sync interests.
        /// </summary>
        public List<InterestSyncConfig> InterestConfigs { get; set; } = new List<InterestSyncConfig>();

        /// <summary>
        /// Gets or sets the interest identifiers that specify that when a mailing list entry has that interest,
        /// the entry should not be marked for deletion, eventhough the local data may not contain data for this
        /// entry.
        /// </summary>
        public List<MailChimpInterest> LeaveUsersAloneInterests { get; set; } = new List<MailChimpInterest>();

        /// <summary>
        /// Gets or sets the encrypted MailChimp API key.
        /// </summary>
        internal string MailChimpApiKey { get; set; }

        /// <summary>
        /// Gets the name of the excel file.
        /// </summary>
        internal string ExcelFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LocalDataConnectionString))
                {
                    return string.Empty;
                }
                var parts = LocalDataConnectionString.Split('|');
                if (parts.Length == 2)
                {
                    return parts[1];
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
