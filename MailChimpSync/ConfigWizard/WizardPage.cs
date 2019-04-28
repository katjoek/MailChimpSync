// <copyright file="WizardPage.cs" company="Mark van de Veerdonk">
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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using MailChimp.Net.Interfaces;
    using MailChimpSync.Config;

    /// <summary>
    /// Base class for wizard pages
    /// </summary>
    /// <seealso cref="UserControl" />
    public class WizardPage : UserControl
    {
        /// <summary>
        /// Gets or sets the shared data (containing the configuration to edit with the wizard).
        /// </summary>
        internal SharedData SharedData { get; set; }

        /// <summary>
        /// The method that will be called just before a wizard page is shown
        /// </summary>
        /// <returns>an awaitable task</returns>
        internal virtual Task PreShow()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Asks the wizard page whether it is okay to move on to the next page.
        /// </summary>
        /// <returns>true only when it is okay to continue to the next page</returns>
        internal virtual Task<bool> ReadyForNextPage()
        {
            return Task.FromResult(true);
        }
    }
}
