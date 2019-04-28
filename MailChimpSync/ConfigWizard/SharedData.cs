// <copyright file="SharedData.cs" company="Mark van de Veerdonk">
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

namespace MailChimpSync.ConfigWizard
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MailChimp.Net.Interfaces;
    using MailChimpSync.Config;

    /// <summary>
    /// Data shared between wizard and application
    /// </summary>
    internal class SharedData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedData"/> class.
        /// </summary>
        public SharedData()
        {
            SyncConfig = new SyncConfig();
        }

        /// <summary>
        /// Gets or sets the synchronization configuration.
        /// </summary>
        public SyncConfig SyncConfig { get; set; }

        /// <summary>
        /// Gets or sets the MailChimp manager which allows communication with MailChimp.
        /// </summary>
        public IMailChimpManager Manager { get; set; }

        /// <summary>
        /// Gets or sets the local data to synchronize.
        /// </summary>
        public DataSet Data { get; set; }
    }
}
