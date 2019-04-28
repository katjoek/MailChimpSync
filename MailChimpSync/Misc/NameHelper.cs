// <copyright file="NameHelper.cs" company="Mark van de Veerdonk">
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

namespace MailChimpSync.Misc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Name handling methods
    /// </summary>
    internal class NameHelper
    {
        /// <summary>
        /// Creates a nice full name based on different name parts, ensuring just the right amount spaces.
        /// </summary>
        /// <param name="nameParts">The name parts.</param>
        /// <returns>the full name</returns>
        public static string GetFullName(IEnumerable<string> nameParts)
        {
            var fullName = default(string);
            foreach (var namePart in nameParts)
            {
                if (fullName == null)
                {
                    fullName = namePart.Trim();
                }
                else
                {
                    fullName = $"{fullName} {namePart}".Trim();
                }
            }

            return fullName;
        }
    }
}
