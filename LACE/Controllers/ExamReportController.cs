using LACE.Core.Business;
using LACE.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nedesk.Core.API;
using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;

namespace LACE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamReportController : NDBaseController
    {
        private readonly ExamReportBusiness _examReportBusiness;
        public ExamReportController(ExamReportBusiness examReportBusiness ,ILogger<NDBaseController> logger, IAuth AuthService, IHttpContextAccessor httpContextAccessor) : base(logger, AuthService, httpContextAccessor)
        {
            _examReportBusiness = examReportBusiness; 
        }

        [HttpGet]
        public ActionResult<IResponse<List<ExamReport>>> GetBySession()
        {
            var userResponse = GetResponse(() => _authService.GetSessionUser());
            if(userResponse.Value.InError)
            {
                ListResponse<ExamReport> response = new ListResponse<ExamReport>();
                response.Merge(userResponse.Value);
                return response;
            }
            return GetResponse(() => _examReportBusiness.FindByUserId(userResponse.Value.ResponseData.Id));
        }

        [HttpPost]
        public ActionResult<IResponse<long>> Create(ExamReport report)
        {
            return GetResponse(() => _examReportBusiness.Insert(report));
        }

    }
}
