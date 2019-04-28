// <copyright file="Page1.cs" company="Mark van de Veerdonk">
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
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using MailChimp.Net;
    using MailChimp.Net.Core;
    using MailChimpSync.LocalData;

    /// <summary>
    /// Page 1 of the configuration wizard
    /// </summary>
    /// <seealso cref="WizardPage" />
    public partial class Page1 : WizardPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Page1"/> class.
        /// </summary>
        public Page1()
        {
            InitializeComponent();
        }

        /// <inheritdoc/>
        internal override async Task<bool> ReadyForNextPage()
        {
            if (SharedData == null)
            {
                return false;
            }
            SharedData.SyncConfig.LocalDataConnectionString = ConnectionString.CreateConnectionString(ConnectionString.ProtocolExcel, eDataFileName.Text);
            SharedData.SyncConfig.MailChimpApiKey = eApiKey.Text;
            SharedData.Manager = new MailChimpManager(SharedData.SyncConfig.MailChimpApiKey);

            bool result = false;
            try
            {
                if (SharedData.SyncConfig.MailChimpApiKey.Length > 0
                    && SharedData.SyncConfig.LocalDataConnectionString.Length > 0
                    && File.Exists(SharedData.SyncConfig.ExcelFileName))
                {
                    await AssertThatApiKeyWorks();

                    SharedData.Data = LocalDataLoader.GetData(SharedData.SyncConfig.LocalDataConnectionString);
                    if (SharedData.Data.Tables.Count == 0 || SharedData.Data.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("No data found in local data.");
                    }
                    result = true;
                }
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
#pragma warning restore CA1031 // Do not catch general exception types

            if (!result)
            {
                MessageBox.Show("Please specify a valid API key and a file that contains the data to sync.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return result;
        }

        /// <inheritdoc/>
        internal override Task PreShow()
        {
            eApiKey.Text = SharedData.SyncConfig.MailChimpApiKey;
            eDataFileName.Clear();
            if (!string.IsNullOrWhiteSpace(SharedData.SyncConfig.LocalDataConnectionString))
            {
                eDataFileName.Text = ConnectionString.GetLocation(SharedData.SyncConfig.LocalDataConnectionString);
                eDataFileName.Select(eDataFileName.Text.Length, 0);
            }

            return Task.CompletedTask;
        }

        private async Task AssertThatApiKeyWorks()
        {
            await SharedData.Manager.Lists.GetAllAsync(new ListRequest() { Limit = 1 });
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                eDataFileName.Text = openFileDialog.FileName;
            }
        }
    }
}
