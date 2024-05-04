using CsvHelper;
using CsvHelper.Configuration;
using RandomUsers.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace RandomUsers.Utility
{
    public static class CsvFormatter
    {
        public static string Format(List<UserData> userData)
        {
            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteHeader<UserData>();
                csv.NextRecord();
                csv.WriteRecords(userData);
                return writer.ToString();
            }
        }
    }
}