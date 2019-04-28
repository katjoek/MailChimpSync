// <copyright file="InterestSyncConfig.cs" company="Mark van de Veerdonk">
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
    /// Configuration items for synchonizing a membership of a MailChimp group/interest.
    /// </summary>
    public class InterestSyncConfig
    {
        /// <summary>
        /// Gets or sets the mail chimp interest.
        /// </summary>
        public MailChimpInterest MailChimpInterest { get; set; }

        /// <summary>
        /// Gets or sets the index of the column of the local data to use to sync this interest.
        /// </summary>
        public int LocalColumn { get; set; }

        /// <summary>
        /// Gets or sets the value the local data needs to have to indicate this person should have this interest.
        /// </summary>
        public string MembershipValue { get; set; }
    }
}
