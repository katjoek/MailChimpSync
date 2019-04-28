// <copyright file="FormMain.cs" company="Mark van de Veerdonk">
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

namespace MailChimpSync
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
    using System.Windows.Threading;
    using MailChimp.Net.Models;
    using MailChimpSync.Config;
    using MailChimpSync.ConfigWizard;
    using MailChimpSync.Misc;
    using MailChimpSync.Sync;

    /// <summary>
    /// The main form for the MailChimpSync application
    /// </summary>
    /// <seealso cref="Form" />
    public partial class FormMain : Form
    {
        private const string ConfigFilePath = "config.json";
        private SyncConfig config;
        private Dispatcher dispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            Logger.OnLogEvent += (line) =>
            {
                eLog.AppendText(line);
                eLog.AppendText(Environment.NewLine);
            };
            dispatcher = Dispatcher.CurrentDispatcher;
            LoadConfig();
        }

        private void LoadConfig()
        {
            if (File.Exists(ConfigFilePath))
            {
                var json = File.ReadAllText(ConfigFilePath);
                config = Newtonsoft.Json.JsonConvert.DeserializeObject<SyncConfig>(json);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (config == null)
            {
                EditConfig();
            }
        }

        private void EditConfig()
        {
            var sharedData = new SharedData();
            if (config != null)
            {
                sharedData.SyncConfig.MailChimpApiKey = config.MailChimpApiKey;
                sharedData.SyncConfig.LocalDataConnectionString = config.LocalDataConnectionString;
                sharedData.SyncConfig.HeaderRow = config.HeaderRow;
                sharedData.SyncConfig.FirstDataRow = config.FirstDataRow;
                sharedData.SyncConfig.MailingListId = config.MailingListId;
                sharedData.SyncConfig.EmailAddressColumn = config.EmailAddressColumn;
                sharedData.SyncConfig.EmailAddressColumn2 = config.EmailAddressColumn2;
                sharedData.SyncConfig.NameColumns = config.NameColumns;
                sharedData.SyncConfig.InterestConfigs.Clear();
                sharedData.SyncConfig.InterestConfigs.AddRange(config.InterestConfigs);
            }
            var wizard = ConfigWizardFactory.Create(sharedData);
            var result = wizard.ShowDialog();
            if (result == DialogResult.OK)
            {
                config = wizard.SharedData.SyncConfig;
                SaveConfig();
            }
        }

        private void SaveConfig()
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(config);
            File.WriteAllText(ConfigFilePath, json);
        }

        private void Log(string s)
        {
            Debug.WriteLine(s);
        }

        private void ConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditConfig();
        }

        private async void MenuItemSynchronize_Click(object sender, EventArgs e)
        {
            await PreventRecurrentExecution(async () =>
            {
#if true
                var members = (await RemoteDataGetter.GetMembers(config)).ToList();
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(members);
                File.WriteAllText("members.json", json);
#else
            var json = File.ReadAllText("members.json");
            var members = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Member>>(json);
#endif
                var syncer = new Synchronizer(config, members);
                await syncer.Sync();
            });
        }

        private async Task PreventRecurrentExecution(Func<Task> action)
        {
            miSynchronize.Enabled = false;
            try
            {
                await action();
            }
            finally
            {
                miSynchronize.Enabled = true;
            }
        }
    }
}
