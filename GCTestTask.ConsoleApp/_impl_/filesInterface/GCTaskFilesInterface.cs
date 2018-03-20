using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GCTestTask.ConsoleApp
{
    public class GCTaskFilesInterface
    {
        public static string Path => System.IO.Path.GetTempPath();

        public static string StatusesFileName => "GCTask_statuses.txt";

        public static string ScheduleFileName => "GCTask_schedule.txt";

        public static void SaveStatuses(
                                IReadOnlyCollection<GCTaskDriverInfo> statuses
        ){
            string[] statusFileLines = new string[statuses.Count + 2];

            ushort mnc = 20, // module name col. width in chars
                   dnc = 60, // display name col. width in chars
                   blc = 11; // boolean col. width in chars

            uint maxModuleNameChars = 0,
                 maxDisplayNameChars = 0;

            foreach (GCTaskDriverInfo driverInfo in statuses) {
                if (driverInfo.ModuleName.Length > maxModuleNameChars)
                    maxModuleNameChars = (uint)driverInfo.ModuleName.Length;

                if (driverInfo.DisplayName.Length > maxDisplayNameChars)
                    maxDisplayNameChars = (uint)driverInfo.DisplayName.Length;
            }

            statusFileLines[0]
                = GetColumnString(  "Module Name",
                                    maxModuleNameChars, mnc   )
                + GetColumnString(  "Display Name",
                                    maxDisplayNameChars, dnc   )
                + GetColumnString(  "Activated", blc, blc   )
                + GetColumnString(  "Can Disable", blc, blc   );

            statusFileLines[1]
                = new string('-', mnc + dnc + 2 * blc + 4 * gapWidthChars);

            uint lineIndex = 2;
            foreach (GCTaskDriverInfo driverInfo in statuses) {
                string lineString
                    = GetColumnString(  driverInfo.ModuleName,
                                        maxModuleNameChars, mnc   )
                    + GetColumnString(  driverInfo.DisplayName,
                                        maxDisplayNameChars, dnc   )
                    + GetColumnString(  driverInfo.IsActivated.ToString(),
                                        blc, blc                             )
                    + GetColumnString(  driverInfo.SupportsDisabling.ToString(),
                                        blc, blc     
                    );

                statusFileLines[lineIndex] = lineString;
                lineIndex++;
            }

            string filePathAndName = Path + StatusesFileName;
            try {
                System.IO.File.WriteAllLines(filePathAndName, statusFileLines);
            }
            catch (Exception e) {
                Console.WriteLine(string.Format(
                          "[error] Could not save statuses in file \"{0}\". \n"
                        + "        Reason: {1}"
                    , filePathAndName, e.Message
                ));
            }
        }

        public static string GetNameOfModuleToDisable() {
            string scheduleLine = GetScheduleLine();

            if (string.IsNullOrEmpty(scheduleLine))
                return null;

            string moduleName = null;
            Match match = Regex.Match(scheduleLine, @"^\s*(\w+)");
            if (match.Success)
                moduleName = match.Groups[1].ToString();

            return moduleName;
        }

        public static IEnumerable<DateTime> GetDisablementSchedule() {
            string scheduleLine = GetScheduleLine();

            if (scheduleLine == null)
                return null;

            MatchCollection matches = Regex.Matches(
                scheduleLine,
            @"([0-9]{4})-([0-9]{2})-([0-9]{2})_([0-9]{2}):([0-9]{2}):([0-9]{2})"
            );

            var schedule = new List<DateTime>();
            try {
                foreach (Match match in matches) {
                    GroupCollection groups = match.Groups;
                    ushort year = ushort.Parse(groups[1].ToString()),
                           month = ushort.Parse(groups[2].ToString()),
                           monthDay = ushort.Parse(groups[3].ToString()),
                           hour = ushort.Parse(groups[4].ToString()),
                           minute = ushort.Parse(groups[5].ToString()),
                           second = ushort.Parse(groups[6].ToString());

                    var time = new DateTime(year, month, monthDay,
                                                hour, minute, second    );
                    schedule.Add(time);
                }
            }
            catch (Exception) {
                return null;
            }

            return schedule;
        }


        private static string GetColumnString(
                                                string columnText,
                                                uint fullColumnWidthChars,
                                                ushort columnWidthLimitChars
        ) {
            ushort columnWidth = (ushort)Math.Min(  fullColumnWidthChars,
                                                    columnWidthLimitChars   );

            string gapAtEnd = new string(' ', gapWidthChars);

            if (columnWidth < 4)
                throw new InvalidOperationException("columnWidth < 4");

            if (columnText.Length == columnWidth)
                return columnText + gapAtEnd;

            if (columnText.Length > columnWidth)
                return   columnText.Substring(0, columnWidth - 3)
                       + "..." + gapAtEnd;

            return  columnText
                  + new string(' ', columnWidth - columnText.Length)
                  + gapAtEnd;
        }

        private static string GetScheduleLine() {
            string filePathAndName = Path + ScheduleFileName;
            try {
                IEnumerable<string> lines
                    = System.IO.File.ReadLines(filePathAndName);

                string scheduleLine = "";
                foreach (string line in lines) {
                    scheduleLine = line;
                    break;
                }

                return scheduleLine;
            }
            catch (Exception e) {
                Console.WriteLine(string.Format(
                          "[error] Could not use schedule file \"{0}\". \n"
                        + "        Reason: {1}"
                    , filePathAndName, e.Message
                ));
                return null;
            }
        }

        private const ushort gapWidthChars = 4;
    }
}