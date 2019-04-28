// <copyright file="MemberExtensions.cs" company="Mark van de Veerdonk">
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

namespace MailChimpSync.Misc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MailChimp.Net.Models;

    /// <summary>
    /// Extension methods for the <see cref="Member"/> class
    /// </summary>
    public static class MemberExtensions
    {
        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns>the full name</returns>
        public static string GetFullName(this Member member)
        {
            var dict = member.MergeFields;
            var firstName = GetStringFromDict(dict, "FNAME");
            var tussenVoegsel = GetStringFromDict(dict, "TV");
            var lastName = GetStringFromDict(dict, "LNAME");

            var fullName = ($"{firstName} {tussenVoegsel}".Trim() + " " + lastName).Trim();
            return fullName;
        }

        /// <summary>
        /// Sets the name fields.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        /// <param name="lastName">The last name.</param>
        public static void SetName(this Member member, string firstName, string middleName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                member.MergeFields["FNAME"] = "-";
            }
            else
            {
                member.MergeFields["FNAME"] = firstName;
            }

            if (!string.IsNullOrWhiteSpace(middleName))
            {
                member.MergeFields["TV"] = middleName;
            }

            member.MergeFields["LNAME"] = lastName;
        }

        /// <summary>
        /// Returns whether or not the full name of <paramref name="o"/> equals the full name of this instance.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="o">The o.</param>
        /// <returns>true only if the names are equal</returns>
        public static bool EqualFullName(this Member member, Member o)
        {
            var result = member.MergeFields["FNAME"].Equals(o.MergeFields["FNAME"]);
            result &= member.MergeFields["LNAME"].Equals(o.MergeFields["LNAME"]);
            result &= (member.MergeFields["TV"].ToString().Length == 0 && !o.MergeFields.Keys.Contains("TV"))
                || (member.MergeFields["TV"].ToString() == "-" && !o.MergeFields.Keys.Contains("TV"))
                || (o.MergeFields["TV"].ToString() == "-" && !member.MergeFields.Keys.Contains("TV"))
                || member.MergeFields["TV"].Equals(o.MergeFields["TV"]);
            return result;
        }

        private static string GetStringFromDict(IDictionary<string, object> dict, string key)
        {
            return dict.ContainsKey(key) ? dict[key].ToString() : string.Empty;
        }
    }
}
