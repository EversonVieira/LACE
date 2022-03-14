using LACE.Core.Models;
using LACE.Core.Models.DTO;
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

        public Response<long> Insert(DTO_ExamReport report)
        {
            Response<long> response = new Response<long>();

            ExamReport endReport = ModelUtility.FromObj<DTO_ExamReport, ExamReport>(report);
            _validator.ValidateInsert(response, endReport);

            if (!response.IsValid)
                return response;

            ExamReportStorageUtility.SaveFile(endReport);

            return _examReportRepository.Insert(endReport);
        }

        public Response<bool> Update(DTO_ExamReport report)
        {
            Response<bool> response = new Response<bool>();

            ExamReport endReport = ModelUtility.FromObj<DTO_ExamReport, ExamReport>(report);

            _validator.ValidateUpdate(response, endReport);
            if (!response.IsValid)
                return response;

            return _examReportRepository.Update(endReport);
        }

        public ListResponse<DTO_ExamReport> FindAll()
        {
            ListResponse<DTO_ExamReport> response = new ListResponse<DTO_ExamReport>();

            var examReportResponse =  _examReportRepository.FindAll();
            foreach (ExamReport rpt in examReportResponse.ResponseData)
            {
                response = new ListResponse<DTO_ExamReport>();
                response.ResponseData.Add(ModelUtility.FromObj<ExamReport, DTO_ExamReport>(rpt));
            }

            return response;

        }

        public ListResponse<DTO_ExamReport> FindByUserId(long userId)
        {

            ListResponse<DTO_ExamReport> response = new ListResponse<DTO_ExamReport>();


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

            var examReportResponse = _examReportRepository.FindByRequest(request);

            if (!response.IsValid)
                return response;

            ExamReportStorageUtility.GetFiles(examReportResponse.ResponseData, _logger);

            foreach(ExamReport rpt in examReportResponse.ResponseData)
            {
                response = new ListResponse<DTO_ExamReport>();
                response.ResponseData.Add(ModelUtility.FromObj<ExamReport, DTO_ExamReport>(rpt));
            }

            return response;
        }
    }
}
