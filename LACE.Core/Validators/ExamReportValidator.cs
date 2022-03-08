using LACE.Core.Models;
using LACE.Core.Repository;
using Microsoft.Extensions.Logging;
using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;

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

        public void ValidateInsert(IResponse<ExamReport> response)
        {

        }

        public void ValidateUpdate(IResponse<ExamReport> response)
        {

        }

        public void ValidateDelete(IResponse<ExamReport> response)
        {

        }

        public void ValidateSelect(IResponse<ExamReport> response, BaseListRequest request)
        {

        }

        private void ValidateCommon(IResponse<ExamReport> response)
        {

        }

    }
}
