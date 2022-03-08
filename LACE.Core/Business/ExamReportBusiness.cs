using LACE.Core.Models;
using LACE.Core.Repository;
using Microsoft.Extensions.Logging;
using Nedesk.Core.Models;
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
        private readonly ILogger _logger;

        public ExamReportBusiness(ExamReportRepository examReportRepository, ILogger<ExamReportBusiness> logger)
        {
            _examReportRepository = examReportRepository;
            _logger = logger;
        }

        public Response<long> Insert(ExamReport report)
        {
            return _examReportRepository.Insert(report);
        }

        public Response<bool> Update(ExamReport report)
        {
            return _examReportRepository.Update(report);
        }

        public ListResponse<ExamReport> FindAll()
        {
            return _examReportRepository.FindAll();
        }

        public ListResponse<ExamReport> FindByUserId(long userId)
        {

            BaseListRequest request = new BaseListRequest();

            request.Filters.Add(new Filter()
            {
                Target1 = "UserId",
                OperationType = FilterOperationType.Equals,
                Value1 = userId
            });

            return _examReportRepository.FindByRequest(request);
        }
    }
}
