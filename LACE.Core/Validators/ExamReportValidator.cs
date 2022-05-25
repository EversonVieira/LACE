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
        private readonly AuthUserRepository _userRepository;
        private readonly ILogger _logger;

        public ExamReportValidator(AuthUserRepository userRepository, ILogger<AuthUserValidator> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public void ValidateInsert(IBaseResponse response, ExamReport report)
        {
            ValidateCommon(response, report);
        }

        public void ValidateUpdate(IBaseResponse response, ExamReport report)
        {
            ValidateCommon(response, report);

        }
        public void ValidateDelete(IBaseResponse response, ExamReport report)
        {
            ValidateCommon(response, report);
        }

        private void ValidateCommon(IBaseResponse response, ExamReport report)
        {
            if (report.PatientSUS.Length > 20)
            {
                response.AddValidationMessage("911", "O tamanho máximo do campo PatientSUS é de 20 caracteres.");
            }
            if (report.PatientRG.Length > 20)
            {
                response.AddValidationMessage("911", "O tamanho máximo do campo PatientRG é de 20 caracteres.");
            }
            if (report.PatientSUS.Length > 20)
            {
                response.AddValidationMessage("911", "O tamanho máximo do campo PatientSUS é de 20 caracteres.");
            }

            if (report.PatientSUS.IsNullOrEmpty() && report.PatientCPF.IsNullOrEmpty() && report.PatientRG.IsNullOrEmpty())
            {
                response.AddValidationMessage("911", "Informe ao menos um dos campos a seguir: PatientSUS, PatientCPF, PatientRG.");
            }
        }

    }
}
