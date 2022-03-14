using LACE.Core.Models;
using LACE.Core.Repository;
using LACE.Core.Utility;
using LACE.Core.Validators;
using Microsoft.Extensions.Logging;
using Nedesk.Core.Models;
using Nedesk.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Business
{
    public class ExamReportBusiness
    {
        private readonly ExamReportRepository _examReportRepository;
        private readonly AuthUserBusiness _authUserBusiness;
        private readonly ExamReportValidator _validator;
        private readonly ILogger _logger;

        public ExamReportBusiness(ExamReportValidator validator, ExamReportRepository examReportRepository, AuthUserBusiness authUserBusiness,ILogger<ExamReportBusiness> logger)
        {
            _validator = validator;
            _examReportRepository = examReportRepository;
            _authUserBusiness = authUserBusiness;
            _logger = logger;
        }

        public Response<long> Insert(ExamReport report)
        {
            Response<long> response = new Response<long>();

            _validator.ValidateInsert(response, report);
            if (!response.IsValid)
                return response;

            ExamReportStorageUtility.SaveFile(report);

            return _examReportRepository.Insert(report);
        }

        public Response<bool> Update(ExamReport report)
        {
            Response<bool> response = new Response<bool>();

            _validator.ValidateUpdate(response, report);
            if (!response.IsValid)
                return response;

            return _examReportRepository.Update(report);
        }

        public ListResponse<ExamReport> FindAll()
        {
            return _examReportRepository.FindAll();
        }

        public ListResponse<ExamReport> FindByUserId(long userId)
        {

            ListResponse<ExamReport> response = new ListResponse<ExamReport>();


            ListResponse<AuthUser> userResponse = _authUserBusiness.FindById(userId);

            if (!userResponse.IsValid)
            {
                response.Merge(userResponse);
                return response;
            }

            AuthUser user = userResponse.ResponseData.First();

            BaseListRequest request = new BaseListRequest();
            request.Filters.AddRange(new List<Filter>
            {
                new Filter
                {
                    Target1 = "PatientCpf",
                    OperationType = FilterOperationType.Equals,
                    AggregateType = FilterAggregateType.OR,
                    Value1 = user.Cpf
                },
                new Filter
                {
                    Target1 = "PatientRg",
                    OperationType = FilterOperationType.Equals,
                    AggregateType = FilterAggregateType.OR,
                    Value1 = user.Rg
                },
            });

            response = _examReportRepository.FindByRequest(request);

            if (!response.IsValid)
                return response;

            ExamReportStorageUtility.GetFiles(response.ResponseData);

            return response;
        }
    }
}
