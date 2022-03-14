using LACE.Core.Business;
using LACE.Core.Models;
using LACE.Core.Models.DTO;
using LACE.Core.Utility;
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
        public ExamReportController(ExamReportBusiness examReportBusiness, ILogger<NDBaseController> logger, IAuth AuthService, IHttpContextAccessor httpContextAccessor) : base(logger, AuthService, httpContextAccessor)
        {
            _examReportBusiness = examReportBusiness;
        }

        [HttpGet]
        public ActionResult<IResponse<List<DTO_ExamReport>>> GetBySession()
        {
            ListResponse<DTO_ExamReport> response = new ListResponse<DTO_ExamReport>();

            var userResponse = _authService.GetSessionUser();
            if (userResponse.InError)
            {
                response.Merge(userResponse);
                return response;
            }

            if (!userResponse.HasResponseData)
            {
                response.AddWarningMessage("911", "Dados do usuário não foram encontrados.");
                return response;
            }
            return GetResponse(() => _examReportBusiness.FindByUserId(userResponse.ResponseData.Id));
        }

        [HttpPost]
        public ActionResult<IResponse<long>> Create(DTO_ExamReport report)
        {
            return GetResponse(() => _examReportBusiness.Insert(report));
        }

    }
}
