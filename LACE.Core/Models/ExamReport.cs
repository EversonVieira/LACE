using Nedesk.Core.Models;
using Nedesk.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Models
{
    public class ExamReport : BaseModel
    {
        public long UserId { get; set; }
        public string RegisterId { get; set; }
        public string SourcePatientId { get; set; }
        public string SourceExamId { get; set; }
        public string PatientCPF { get; set; }
        public string PatientRG { get; set; }
        public string PatientSUS { get; set; }
        public string ExamName { get; set; }
        public string FileExtension { get; set; }
        public string FileSource { get; set; }
        public string FilePath { get; set; }
        public DateTime ExamDate { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
