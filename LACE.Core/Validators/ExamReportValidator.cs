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
            if (report.PatientCPF.IsNullOrEmpty())
            {
                response.AddValidationMessage("911", "'PatientCpf' é obrigatório.");
            }

            if (report.PatientRG.IsNullOrEmpty())
            {
                response.AddValidationMessage("911", "'PatientRG' é obrigatório.");
            }
        }

    }
}
