// <copyright file="ConnectionString.cs" company="Mark van de Veerdonk">
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

#pragma warning disable SA1008 // Opening parenthesis must be spaced correctly. Using tuples causes this warning to popup.

namespace MailChimpSync.LocalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Connection string capable of specifiying a protocol and target (like an URL)
    /// </summary>
    internal class ConnectionString
    {
        /// <summary>
        /// The protocol excel
        /// </summary>
        public const string ProtocolExcel = "excel";

        /// <summary>
        /// Creates the connection string.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="location">The location.</param>
        /// <returns>connection string</returns>
        public static string CreateConnectionString(string protocol, string location)
        {
            return $"{protocol}|{location}";
        }

        /// <summary>
        /// Extracts the protocol from the given <paramref name="connectionString"/>.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>the protocol</returns>
        public static string GetProtocol(string connectionString)
        {
            return GetParts(connectionString)[0];
        }

        /// <summary>
        /// Extracts the location from the given <paramref name="connectionString"/>.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The location</returns>
        public static string GetLocation(string connectionString)
        {
            return GetParts(connectionString)[1];
        }

        /// <summary>
        /// Dissects the connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>the protocol and the location</returns>
        public static (string protocol, string location) DissectConnectionString(string connectionString)
        {
            var parts = GetParts(connectionString);
            return (parts[0], parts[1]);
        }

        private static string[] GetParts(string connectionString)
        {
            var parts = connectionString.Split('|');
            if (parts.Length != 2)
            {
                throw new Exception("Invalid connection string");
            }

            return parts;
        }
    }
}
