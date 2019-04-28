// <copyright file="Page5.cs" company="Mark van de Veerdonk">
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
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using MailChimpSync.Config;

    /// <summary>
    /// Page 5 of the configuration wizard
    /// </summary>
    /// <seealso cref="WizardPage" />
    public partial class Page5 : WizardPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Page5"/> class.
        /// </summary>
        public Page5()
        {
            InitializeComponent();
            addRuleDialog = new AddRuleDialog();
        }

        /// <inheritdoc/>
        internal override async Task PreShow()
        {
            await addRuleDialog.FillComboBoxItems(SharedData);
            UpdateRules(SharedData.SyncConfig.InterestConfigs);
        }

        /// <inheritdoc/>
        internal override Task<bool> ReadyForNextPage()
        {
            UpdateRules(SharedData.SyncConfig.InterestConfigs);
            return Task.FromResult(true);
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            addRuleDialog.Clear();
            if (addRuleDialog.ShowDialog() == DialogResult.OK)
            {
                var cfg = addRuleDialog.GetInterestSyncConfig();
                SharedData.SyncConfig.InterestConfigs.Add(cfg);
                UpdateRules(SharedData.SyncConfig.InterestConfigs);
            }
        }

        private void UpdateRules(List<InterestSyncConfig> interestConfigs)
        {
            lvRules.Items.Clear();
            foreach (var cfg in interestConfigs)
            {
                var item = new ListViewItem(new string[] { GetColumnName(cfg.LocalColumn), cfg.MembershipValue, cfg.MailChimpInterest.Name });
                lvRules.Items.Add(item);
            }
        }

        private string GetColumnName(int idx)
        {
            if (SharedData.SyncConfig.HeaderRow.HasValue)
            {
                var headerRow = SharedData.SyncConfig.HeaderRow.Value;
                return SharedData.Data.Tables[0].Rows[headerRow][idx].ToString();
            }
            else
            {
                return $"Column {idx + 1}";
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (lvRules.SelectedIndices.Count > 0)
            {
                var indices = new List<int>();
                for (int i = 0; i < lvRules.SelectedIndices.Count; ++i)
                {
                    indices.Insert(0, lvRules.SelectedIndices[i]);
                }
                indices.Sort();
                foreach (var idx in indices.Reverse<int>())
                {
                    SharedData.SyncConfig.InterestConfigs.RemoveAt(idx);
                }
                UpdateRules(SharedData.SyncConfig.InterestConfigs);
            }
        }

        private void ButtonClearAll_Click(object sender, EventArgs e)
        {
            SharedData.SyncConfig.InterestConfigs.Clear();
            lvRules.Items.Clear();
        }
    }
}
