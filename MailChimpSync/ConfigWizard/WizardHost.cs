// <copyright file="WizardHost.cs" company="Mark van de Veerdonk">
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
    /// The form that hosts the wizard pages
    /// </summary>
    /// <seealso cref="Form" />
    public partial class WizardHost : Form
    {
        private List<WizardPage> pages = new List<WizardPage>();
        private int currentPageIndex;
        private WizardPage currentPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardHost"/> class.
        /// </summary>
        public WizardHost()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the shared data.
        /// </summary>
        /// <value>
        /// The shared data.
        /// </value>
        internal SharedData SharedData { get; set; }

        /// <summary>
        /// Adds a wizard page.
        /// </summary>
        /// <param name="page">The page.</param>
        public void AddPage(WizardPage page)
        {
            page.SharedData = SharedData;
            pages.Add(page);
        }

        private async void WizardHost_Shown(object sender, EventArgs e)
        {
            currentPageIndex = 0;
            await ShowPage();
        }

        private async Task ShowPage()
        {
            if (currentPageIndex < pages.Count)
            {
                currentPage = pages[currentPageIndex];
                Cursor = Cursors.WaitCursor;
                try
                {
                    await currentPage.PreShow();
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
                panelWizardPage.Controls.Clear();
                panelWizardPage.Controls.Add(currentPage);
                currentPage.Dock = DockStyle.Fill;
                currentPage.Show();

                if (currentPageIndex == pages.Count - 1)
                {
                    btnFinish.Text = "&Finish";
                }
                else
                {
                    btnFinish.Text = "&Last >>";
                }
                btnNext.Enabled = currentPageIndex < pages.Count - 1;
                btnFirst.Enabled = btnPrev.Enabled = currentPageIndex > 0;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        private async void BtnNext_Click(object sender, EventArgs e)
        {
            bool ready = false;
            await ShowHourglass(async () =>
            {
                ready = await currentPage.ReadyForNextPage();
            });

            if (ready)
            {
                currentPageIndex++;
                await ShowPage();
            }
        }

        private async void ButtonFinish_Click(object sender, EventArgs e)
        {
            if (currentPageIndex == pages.Count - 1)
            {
                if (await currentPage.ReadyForNextPage())
                {
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                var result = true;
                int i;
                for (i = currentPageIndex; i < pages.Count; ++i)
                {
                    var page = pages[i];
                    if (i > currentPageIndex)
                    {
                        await page.PreShow();
                    }
                    result &= await page.ReadyForNextPage();
                    if (!result)
                    {
                        break;
                    }
                }
                currentPageIndex = Math.Min(pages.Count - 1, i);
                await ShowPage();
            }
        }

        private async void BtnPrev_Click(object sender, EventArgs e)
        {
            currentPageIndex--;
            await ShowPage();
        }

        private async void BtnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 0;
            await ShowPage();
        }

        private async Task ShowHourglass(Func<Task> action)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                await action();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
