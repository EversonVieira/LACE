using LACE.Core.Models;
using LACE.Core.Repository;
using Microsoft.Extensions.Logging;
using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;
using Nedesk.Extensions;

namespace LACE.Core.Validators
{
    public class ExamReportValidator
    {
        private readonly ExamReportRepository _examReportRepository;
        private readonly ILogger _logger;

        public ExamReportValidator(ExamReportRepository examReportRepository, ILogger<ExamReportValidator> logger)
        {
            _examReportRepository = examReportRepository;
            _logger = logger;
        }

        public void ValidateInsert(NDResponse NDResponse, ExamReport report)
        {
            ValidateCommon(NDResponse, report);
        }

        public void ValidateUpdate(NDResponse NDResponse, ExamReport report)
        {
            ValidateCommon(NDResponse, report);

        }
        public void ValidateDelete(NDResponse NDResponse, ExamReport report)
        {
            ValidateCommon(NDResponse, report);
        }

        private void ValidateCommon(NDResponse NDResponse, ExamReport report)
        {
            if (report.PatientSUS.Length > 20)
            {
                NDResponse.AddValidationMessage("911", "O tamanho máximo do campo PatientSUS é de 20 caracteres.");
            }
            if (report.PatientRG.Length > 20)
            {
                NDResponse.AddValidationMessage("911", "O tamanho máximo do campo PatientRG é de 20 caracteres.");
            }
            if (report.PatientSUS.Length > 20)
            {
                NDResponse.AddValidationMessage("911", "O tamanho máximo do campo PatientSUS é de 20 caracteres.");
            }

            if (report.PatientSUS.IsNullOrEmpty() && report.PatientCPF.IsNullOrEmpty() && report.PatientRG.IsNullOrEmpty())
            {
                NDResponse.AddValidationMessage("911", "Informe ao menos um dos campos a seguir: PatientSUS, PatientCPF, PatientRG.");
            }
        }

    }
}
