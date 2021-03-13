using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BestOil.Entities;
using Newtonsoft.Json;

namespace BestOil.Helpers
{
    public static class FileHelper
    {
        private static JsonSerializer Serializer;

        static FileHelper()
        {
            Serializer = new JsonSerializer();
        }

        public static void WriteToJsonFile(Bill bill, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    using (var jw = new JsonTextWriter(sw))
                    {
                        jw.Formatting = Formatting.Indented;

                        Serializer.Serialize(jw, bill);
                    }
                }
            }
        }

        public static void WriteToPdf(Bill bill, string filePath)
        {

        }

        public static string CreateNewFileName(Guid billId)
        {
            var now = DateTime.Now;

            return $"{now.Day}-{now.Month}-{now.Year}-{now.Hour}-{now.Minute}-{billId.ToString().Substring(0, 8)}";
        }
    }
}