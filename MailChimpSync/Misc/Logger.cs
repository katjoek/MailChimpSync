// <copyright file="Logger.cs" company="Mark van de Veerdonk">
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
    /// A simple logger class. Make sure to set <see cref="OnLogEvent"/> to actually perform the logging.
    /// </summary>
    internal static class Logger
    {
        /// <summary>
        /// Occurs when <see cref="Log(string)"/> is called.
        /// </summary>
        public static event Action<string> OnLogEvent;

        /// <summary>
        /// Logs the specified log line.
        /// </summary>
        /// <param name="logLine">The log line.</param>
        public static void Log(string logLine)
        {
            OnLogEvent?.Invoke(logLine);
        }
    }
}
