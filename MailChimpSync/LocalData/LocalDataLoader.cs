// <copyright file="LocalDataLoader.cs" company="Mark van de Veerdonk">
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

namespace MailChimpSync.LocalData
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ExcelDataReader;

    /// <summary>
    /// Loads the local data, the data to sync to MailChimp
    /// </summary>
    internal class LocalDataLoader
    {
        /// <summary>
        /// Gets the data to synchronize.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The data to sync</returns>
        /// <exception cref="System.Exception">Unsupported data protocol: {protocol}</exception>
        public static DataSet GetData(string connectionString)
        {
            DataSet dataSet = null;
            (string protocol, string location) = ConnectionString.DissectConnectionString(connectionString);
            if (protocol == ConnectionString.ProtocolExcel)
            {
                dataSet = ReadExcelData(location);
            }
            else
            {
                throw new Exception($"Unsupported data protocol: {protocol}");
            }

            return dataSet;
        }

        private static DataSet ReadExcelData(string filePath)
        {
            DataSet dataSet;
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    dataSet = reader.AsDataSet();
                }
            }

            return dataSet;
        }
    }
}
