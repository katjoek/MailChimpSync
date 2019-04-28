// <copyright file="AddRuleDialog.cs" company="Mark van de Veerdonk">
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
    using MailChimp.Net.Interfaces;
    using MailChimpSync.Config;

    /// <summary>
    /// Dialog for adding a new rule
    /// </summary>
    /// <seealso cref="Form" />
    public partial class AddRuleDialog : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRuleDialog"/> class.
        /// </summary>
        public AddRuleDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            cbInterest.SelectedIndex = 0;
            eValue.Clear();
        }

        /// <summary>
        /// Gets the interest synchronization configuration.
        /// </summary>
        /// <returns>the interest synchronization configuration</returns>
        public InterestSyncConfig GetInterestSyncConfig()
        {
            var columnItem = (ColumnItem)cbColumn.Items[cbColumn.SelectedIndex];
            var interest = (TitleIdTuple)cbInterest.Items[cbInterest.SelectedIndex];
            var result = new InterestSyncConfig()
            {
                LocalColumn = columnItem.ColumnIdx,
                MailChimpInterest = new MailChimpInterest() { Id = interest.Id, Name = interest.Title },
                MembershipValue = eValue.Text
            };

            return result;
        }

        /// <summary>
        /// Fills the ComboBox items.
        /// </summary>
        /// <param name="sharedData">The shared data.</param>
        /// <returns>An awaitable task</returns>
        internal Task FillComboBoxItems(SharedData sharedData)
        {
            Task t1 = FillColumnComboBox(sharedData.Data, sharedData.SyncConfig);
            Task t2 = FillInterestComboBox(sharedData.Manager, sharedData.SyncConfig);
            return Task.WhenAll(t1, t2);
        }

        private async Task FillInterestComboBox(IMailChimpManager manager, SyncConfig syncConfig)
        {
            cbInterest.Items.Clear();
            var interestCategories = await manager.InterestCategories.GetAllAsync(syncConfig.MailingListId);
            foreach (var interestCategory in interestCategories)
            {
                var interests = await manager.Interests.GetAllAsync(syncConfig.MailingListId, interestCategory.Id);
                foreach (var interest in interests)
                {
                    var item = new TitleIdTuple()
                    {
                        Title = interest.Name,
                        Id = interest.Id
                    };

                    if (interestCategories.Count() > 1)
                    {
                        item.Title = $"{interestCategory.Title}.{item.Title}";
                    }
                    cbInterest.Items.Add(item);
                }
            }
        }

        private Task FillColumnComboBox(DataSet data, SyncConfig syncConfig)
        {
            cbColumn.Items.Clear();
            FillItems(data, syncConfig, (item) => cbColumn.Items.Add(item));
            return Task.CompletedTask;
        }

        private void FillItems(DataSet data, SyncConfig syncConfig, Func<object, int> add)
        {
            var table = data.Tables[0];
            var headerRowIdx = syncConfig.HeaderRow;
            for (int i = 0; i < table.Columns.Count; ++i)
            {
                string s;
                if (headerRowIdx.HasValue)
                {
                    s = table.Rows[headerRowIdx.Value][i].ToString();
                }
                else
                {
                    s = $"Column {i}";
                }

                add(new ColumnItem() { ColumnIdx = i, Text = s });
            }
        }

        private void AddRuleDialog_Shown(object sender, EventArgs e)
        {
            cbColumn.Focus();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (cbColumn.SelectedIndex != -1 && cbInterest.SelectedIndex != -1)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Error", "Please select a column, set a value to match and a interest to set incase of a match.");
            }
        }
    }
}
