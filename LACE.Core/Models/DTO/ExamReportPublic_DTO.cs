using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Models.DTO
{
    public class ExamReportPublic_DTO
    {
        public string SourcePatientId { get; set; }
        public string SourceExamId { get; set; }
        public string PatientCpf { get; set; }
        public string PatientRG { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileSource { get; set; }
        public DateTime ExamDate { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
