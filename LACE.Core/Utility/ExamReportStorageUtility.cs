using LACE.Core.ExtensionMethods;
using LACE.Core.Models;
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

        public static void GetFiles(List<ExamReport> reports)
        {
            foreach (ExamReport rpt in reports)
            {
                string file = GenerateFilePath(rpt);

                using (StreamReader sr = new StreamReader(file))
                {
                    rpt.FileSource = sr.ReadToEnd();
                }
            }
        }

        private static string GenerateFilePath(ExamReport report)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(report.UploadDate.ToString("yyMMdd"));
            sb.Append(report.CreatedOn.ToString("yyMMdd"));
            sb.Append(report.PatientCPF.RemoveDotsAndDashes());
            sb.Append(report.PatientRG.RemoveDotsAndDashes());
            sb.Append(report.ExamName);
            sb.Append($".{report.FileExtension}");
            return sb.ToString();
        }
    }
}
