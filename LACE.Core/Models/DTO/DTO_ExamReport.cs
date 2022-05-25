using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Models.DTO
{
    public class DTO_ExamReport
    {
        public string RegisterId { get; set; }
        public string SourcePatientId { get; set; }
        public string SourceExamId { get; set; }
        public string PatientCPF { get; set; }
        public string PatientRG { get; set; }
        public string PatientSUS { get; set; }
        public string ExamName { get; set; }
        public string FileExtension { get; set; }
        public string FileSource { get; set; }
        public DateTime ExamDate { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
