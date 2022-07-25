using LACE.Core.ExtensionMethods;
using LACE.Core.Models;
using Microsoft.Extensions.Logging;
using Nedesk.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Utility
{
    public static class ExamReportStorageUtility
    {
        private static readonly string storagePath = $@"Files";

        public static NDResponse<bool> SaveFile(ExamReport report)
        {
            var NDResponse = new NDResponse<bool>();

            report.FilePath = $@"{storagePath}/{GenerateFilePath(report)}";

            bool creating = false;
            while (!Directory.Exists(storagePath) && !creating)
            {
                creating = true;
                Directory.CreateDirectory(storagePath);
            }

            byte[] buffer = Convert.FromBase64String(report.FileSource.Split(",")[1]);

            using (FileStream stream = File.Create(report.FilePath))
            {
                stream.Write(buffer, 0, buffer.Length);
            }

            return NDResponse;
        }

        public static void GetFiles(List<ExamReport> reports, ILogger logger)
        {
            foreach (ExamReport rpt in reports)
            {
                try
                {

                    byte[] buffer = File.ReadAllBytes(rpt.FilePath);
                    rpt.FileSource = Convert.ToBase64String(buffer);

                }
                catch(Exception ex)
                {
                    logger.LogWarning($"File not found: {ex.Message}");
                    logger.LogError(ex, ex.Message);
                }
            }
        }

        private static string GenerateFilePath(ExamReport report)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(report.UploadDate.ToString("yyMMddhhmmss."));
            sb.Append($"{ report.PatientCPF.RemoveDotsAndDashes()}.");
            sb.Append($"{report.PatientRG.RemoveDotsAndDashes()}.");
            sb.Append($"{report.ExamName}");
            sb.Append($".{report.FileExtension}");
            return sb.ToString();
        }
    }
}
