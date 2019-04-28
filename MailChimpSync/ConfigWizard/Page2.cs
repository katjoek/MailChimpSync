// <copyright file="Page2.cs" company="Mark van de Veerdonk">
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
    /// Page 2 of the configuration wizard
    /// </summary>
    /// <seealso cref="WizardPage" />
    public partial class Page2 : WizardPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Page2"/> class.
        /// </summary>
        public Page2()
        {
            InitializeComponent();
        }

        /// <inheritdoc/>
        internal override Task<bool> ReadyForNextPage()
        {
            if (cbListSelector.Items.Count == 0)
            {
                MessageBox.Show("No lists found on MailChimp. Please create via MailChimp's website.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Task.FromResult(false);
            }
            else if (cbListSelector.SelectedIndex == -1)
            {
                MessageBox.Show("Please select one of the mailing lists retrieved from MailChimp.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Task.FromResult(false);
            }

            SharedData.SyncConfig.MailingListId = ((ListComboItem)cbListSelector.SelectedItem).Id;
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        internal override async Task PreShow()
        {
            var lists = await SharedData.Manager.Lists.GetAllAsync();
            cbListSelector.Items.Clear();
            foreach (var list in lists)
            {
                cbListSelector.Items.Add(
                    new ListComboItem()
                    {
                        Id = list.Id,
                        Name = list.Name
                    });
            }

            if (cbListSelector.Items.Count > 0
                && !string.IsNullOrEmpty(SharedData.SyncConfig.MailingListId))
            {
                foreach (var item in cbListSelector.Items)
                {
                    var list = (ListComboItem)item;
                    if (list.Id == SharedData.SyncConfig.MailingListId)
                    {
                        cbListSelector.SelectedItem = list;
                        break;
                    }
                }
            }
        }

        private class ListComboItem
        {
            public string Name { get; set; }

            public string Id { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
