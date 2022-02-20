using Nedesk.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Models
{
    public class ExamReport
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long SourcePatientId { get; set; }
        public long SourceExamId { get; set; }
        public string PatientCpf { get; set; }
        public string PatientRG { get; set; }
        public string ExamName { get; set; }
        public string FileExtension { get; set; }
        public string FileSource { get; set; }
        public DateTime ExamDate { get; set; }
        public DateTime UploadDate { get; set; }

        // Helper Properties
        public bool HasRegisterPatient => UserId != 0;

        public string FilePath
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (PatientCpf.IsNotNullOrEmpty())
                    sb.Append($"{PatientCpf}_");

                if (PatientRG.IsNotNullOrEmpty())
                    sb.Append($"{PatientRG}_");

                sb.Append(@"/");

                sb.Append($"{ExamName}-{ExamDate.ToShortDateString()}.{FileExtension}");


                return sb.ToString();
            }
        }
    }
}
