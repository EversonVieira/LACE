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

        public static Response<bool> SaveFile(ExamReport report)
        {
            var response = new Response<bool>();

            report.FilePath = $@"{storagePath}/{GenerateFilePath(report)}";

            bool creating = false;
            while (!Directory.Exists(storagePath) && !creating)
            {
                creating = true;
                Directory.CreateDirectory(storagePath);
            }
            
            using (StreamWriter wr = new StreamWriter(report.FilePath))
            {
                wr.Write(report.FileSource);
            }

            return response;
        }

        public static void GetFiles(List<ExamReport> reports, ILogger logger)
        {
            foreach (ExamReport rpt in reports)
            {
                string file = $@"{storagePath}/{GenerateFilePath(rpt)}";

                try
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        rpt.FileSource = sr.ReadToEnd();
                    }

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

            sb.Append(report.UploadDate.ToString("yyMMddhhmmss"));
            sb.Append(report.CreatedOn.ToString("yyMMddhhmmss"));
            sb.Append(report.PatientCPF.RemoveDotsAndDashes());
            sb.Append(report.PatientRG.RemoveDotsAndDashes());
            sb.Append(report.ExamName);
            sb.Append($".{report.FileExtension}");
            return sb.ToString();
        }
    }
}
