// <copyright file="Page4.cs" company="Mark van de Veerdonk">
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

    /// <summary>
    /// Page 4 of the configuration wizard
    /// </summary>
    /// <seealso cref="WizardPage" />
    public partial class Page4 : WizardPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Page4"/> class.
        /// </summary>
        public Page4()
        {
            InitializeComponent();
        }

        /// <inheritdoc/>
        internal override Task PreShow()
        {
            cbEmailAddress.Items.Clear();
            cbEmailAddress2.Items.Clear();
            lbAvailableColumns.Items.Clear();
            lbSelectedNameColumns.Items.Clear();
            FillItems(cbEmailAddress.Items.Add);
            FillItems(cbEmailAddress2.Items.Add);
            FillItems(lbAvailableColumns.Items.Add);

            var nameColumns = SharedData.SyncConfig.NameColumns;
            int i = 0;
            while (i < lbAvailableColumns.Items.Count)
            {
                var item = (ColumnItem)lbAvailableColumns.Items[i];
                if (nameColumns.Contains(item.ColumnIdx))
                {
                    lbAvailableColumns.SelectedIndex = i;
                    SelectNameColumn();
                }
                else
                {
                    ++i;
                }
            }

            for (i = 0; i < cbEmailAddress.Items.Count; ++i)
            {
                var item = (ColumnItem)cbEmailAddress.Items[i];
                if (item.ColumnIdx == SharedData.SyncConfig.EmailAddressColumn)
                {
                    cbEmailAddress.SelectedIndex = i;
                    break;
                }
            }

            for (i = 0; i < cbEmailAddress2.Items.Count; ++i)
            {
                var item = (ColumnItem)cbEmailAddress2.Items[i];
                if (item.ColumnIdx == SharedData.SyncConfig.EmailAddressColumn2)
                {
                    cbEmailAddress2.SelectedIndex = i;
                    break;
                }
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        internal override Task<bool> ReadyForNextPage()
        {
            if (cbEmailAddress.SelectedItem == null || lbSelectedNameColumns.Items.Count == 0)
            {
                MessageBox.Show("Please select a column for the email address and at least one for the full name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Task.FromResult(false);
            }

            SharedData.SyncConfig.EmailAddressColumn = ((ColumnItem)cbEmailAddress.SelectedItem).ColumnIdx;
            SharedData.SyncConfig.EmailAddressColumn2 = ((ColumnItem)cbEmailAddress2.SelectedItem).ColumnIdx;

            SharedData.SyncConfig.NameColumns.Clear();
            foreach (var o in lbSelectedNameColumns.Items)
            {
                var item = (ColumnItem)o;
                SharedData.SyncConfig.NameColumns.Add(item.ColumnIdx);
            }

            return Task.FromResult(true);
        }

        private void FillItems(Func<object, int> add)
        {
            var table = SharedData.Data.Tables[0];
            var headerRowIdx = SharedData.SyncConfig.HeaderRow;
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

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            SelectNameColumn();
        }

        private void SelectNameColumn()
        {
            if (lbAvailableColumns.SelectedItem != null)
            {
                lbSelectedNameColumns.Items.Add(lbAvailableColumns.SelectedItem);
                lbAvailableColumns.Items.RemoveAt(lbAvailableColumns.SelectedIndex);

                UpdateExampleFullName();
            }
        }

        private void BtnDeselect_Click(object sender, EventArgs e)
        {
            DeselectNameColumn();
        }

        private void DeselectNameColumn()
        {
            if (lbSelectedNameColumns.SelectedItem != null)
            {
                var item = (ColumnItem)lbSelectedNameColumns.SelectedItem;
                var added = false;
                for (int i = 0; i < lbAvailableColumns.Items.Count; ++i)
                {
                    var availableItem = (ColumnItem)lbAvailableColumns.Items[i];
                    if (availableItem.ColumnIdx > item.ColumnIdx)
                    {
                        lbAvailableColumns.Items.Insert(i, lbSelectedNameColumns.SelectedItem);
                        added = true;
                        break;
                    }
                }

                if (!added)
                {
                    lbAvailableColumns.Items.Add(lbSelectedNameColumns.SelectedItem);
                }
                lbSelectedNameColumns.Items.RemoveAt(lbSelectedNameColumns.SelectedIndex);

                UpdateExampleFullName();
            }
        }

        private void ListBoxAvailableColumns_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectNameColumn();
        }

        private void LisBoxSelectedFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeselectNameColumn();
        }

        private void UpdateExampleFullName()
        {
            var fullName = string.Empty;
            foreach (var it in lbSelectedNameColumns.Items)
            {
                var item = (ColumnItem)it;
                var namePart = SharedData.Data.Tables[0].Rows[SharedData.SyncConfig.FirstDataRow][item.ColumnIdx];
                fullName = $"{fullName} {namePart}".Trim();
            }

            if (fullName.Length > 0)
            {
                lblFullName.Text = fullName;
            }
            else
            {
                lblFullName.Text = "-";
            }
        }

        private void CheckBoxEmailAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idx = cbEmailAddress.SelectedIndex;
            if (idx == -1)
            {
                lblEmailAddress.Text = "-";
            }
            else
            {
                var item = (ColumnItem)cbEmailAddress.Items[idx];
                lblEmailAddress.Text = SharedData.Data.Tables[0].Rows[SharedData.SyncConfig.FirstDataRow][item.ColumnIdx].ToString();
            }
        }

        private void CheckBoxEmailAddress2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idx = cbEmailAddress2.SelectedIndex;
            if (idx == -1)
            {
                lblEmailAddress2.Text = "-";
            }
            else
            {
                var item = (ColumnItem)cbEmailAddress2.Items[idx];
                lblEmailAddress2.Text = SharedData.Data.Tables[0].Rows[SharedData.SyncConfig.FirstDataRow][item.ColumnIdx].ToString();
            }
        }
    }
}
