// <copyright file="Page3.cs" company="Mark van de Veerdonk">
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
    /// Page 3 of the configuration wizard
    /// </summary>
    /// <seealso cref="WizardPage" />
    public partial class Page3 : WizardPage
    {
        private DataTable table;

        /// <summary>
        /// Initializes a new instance of the <see cref="Page3"/> class.
        /// </summary>
        public Page3()
        {
            InitializeComponent();
        }

        private int ColumnNamesRowIdx => (int)eColunmNamesRow.Value - 1;

        private int FirstDataRowIdx => (int)eFirstDataRow.Value - 1;

        /// <inheritdoc/>
        internal override Task PreShow()
        {
            table = SharedData.Data.Tables[0];
            eColunmNamesRow.Maximum = table.Rows.Count;
            eFirstDataRow.Maximum = table.Rows.Count + 1;

            cbColumnNamesPresent.Checked = SharedData.SyncConfig.HeaderRow.HasValue;
            if (cbColumnNamesPresent.Checked)
            {
                eColunmNamesRow.Value = (decimal)SharedData.SyncConfig.HeaderRow.Value + 1;
            }
            else
            {
                eColunmNamesRow.Value = 1;
            }

            eFirstDataRow.Value = (decimal)SharedData.SyncConfig.FirstDataRow + 1;

            UpdateExtractedData();

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        internal override Task<bool> ReadyForNextPage()
        {
            if (cbColumnNamesPresent.Checked)
            {
                if (ColumnNamesRowIdx >= FirstDataRowIdx)
                {
                    MessageBox.Show("Column name row cannot be greater or equal to first data row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return Task.FromResult(false);
                }
                SharedData.SyncConfig.HeaderRow = ColumnNamesRowIdx;
            }
            else
            {
                SharedData.SyncConfig.HeaderRow = null;
            }

            SharedData.SyncConfig.FirstDataRow = FirstDataRowIdx;

            return Task.FromResult(true);
        }

        private void ColumnNamesPresent_CheckedChanged(object sender, EventArgs e)
        {
            eColunmNamesRow.Enabled = cbColumnNamesPresent.Checked;
            EnsureDataComesAfterHeaderRow();
            UpdateExtractedData();
        }

        private void EnsureDataComesAfterHeaderRow()
        {
            if (cbColumnNamesPresent.Checked)
            {
                eFirstDataRow.Minimum = eColunmNamesRow.Value + 1;
            }
            else
            {
                eFirstDataRow.Minimum = 1;
            }
        }

        private void UpdateExtractedData()
        {
            if (cbColumnNamesPresent.Checked)
            {
                eHeaderRowContents.Text = GetRowString(ColumnNamesRowIdx);
            }
            else
            {
                eHeaderRowContents.Clear();
            }

            eFirstDataRowContents.Text = GetRowString(FirstDataRowIdx);
        }

        private string GetRowString(int rowIdx)
        {
            var sb = new StringBuilder();
            if (rowIdx < table.Rows.Count)
            {
                var row = table.Rows[rowIdx];
                for (int j = 0; j < table.Columns.Count; ++j)
                {
                    sb.Append($"{row[j]}, ");
                }
                sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            EnsureDataComesAfterHeaderRow();
            UpdateExtractedData();
        }
    }
}
