// <copyright file="RemoteDataGetter.cs" company="Mark van de Veerdonk">
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
    using System.Text;
    using System.Threading.Tasks;
    using MailChimp.Net;
    using MailChimp.Net.Core;
    using MailChimp.Net.Models;
    using MailChimpSync.Config;

    /// <summary>
    /// Gets all mailing list members from the remote.
    /// </summary>
    internal class RemoteDataGetter
    {
        /// <summary>
        /// Gets the members.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>All members</returns>
        public static Task<IEnumerable<Member>> GetMembers(SyncConfig config)
        {
            var manager = new MailChimpManager(config.MailChimpApiKey);
            return manager.Members.GetAllAsync(config.MailingListId, new MemberRequest() { Limit = 10000 });
        }
    }
}
