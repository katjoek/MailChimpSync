// <copyright file="ConfigWizardFactory.cs" company="Mark van de Veerdonk">
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
    using MailChimp.Net.Interfaces;
    using MailChimpSync.Config;

    /// <summary>
    /// Factory for the configuration wizard
    /// </summary>
    internal class ConfigWizardFactory
    {
        /// <summary>
        /// Creates the wizard that will allow changing the given <paramref name="sharedData"/> .
        /// </summary>
        /// <param name="sharedData">The shared data.</param>
        /// <returns>The wizard</returns>
        public static WizardHost Create(SharedData sharedData)
        {
            var wizard = new WizardHost()
            {
                SharedData = sharedData
            };
            wizard.AddPage(new Page1());
            wizard.AddPage(new Page2());
            wizard.AddPage(new Page3());
            wizard.AddPage(new Page4());
            wizard.AddPage(new Page5());
            return wizard;
        }
    }
}
