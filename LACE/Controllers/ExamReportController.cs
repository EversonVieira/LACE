using LACE.Core.Auth;
using LACE.Core.Business;
using LACE.Core.Controller;
using LACE.Core.Helper;
using LACE.Core.Models;
using LACE.Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Nedesk.Core.Attributes;
using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;
using Nedesk.Core.Security.Factory;
using Nedesk.Core.Security.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LACE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [NDAuthenticate]
    public class ExamReportController : LACEBaseController
    {
        private readonly AuthUserBusiness _authUserBusiness;
        private readonly ExamReportBusiness _examReportBusiness;

        private readonly LoginHelper _loginHelper;

        public ExamReportController(AuthUserBusiness authUserBusiness,
                                    ExamReportBusiness examReportBusiness,
                                    LoginHelper loginHelper,
                                    NDITokenFactory<TokenPayload> tokenFactory,
                                    IHttpContextAccessor httpContextAccessor,
                                    ILogger<LoginController> logger) : base(logger, tokenFactory, httpContextAccessor)
        {
            this._authUserBusiness = authUserBusiness;
            this._examReportBusiness = examReportBusiness;
            this._loginHelper = loginHelper;
        }

        [HttpGet]
        public ActionResult<NDListResponse<DTO_ExamReport>> GetByLoggedUser()
        {
            var currentUser = this.RetrieveCurrentUser();
            return Ok(_examReportBusiness.FindByUserId(currentUser));
        }

        [HttpPost]
        public ActionResult<NDListResponse<DTO_ExamReport>> Create(DTO_ExamReport report)
        {
            var currentUser = this.RetrieveCurrentUser();
            return Ok(_examReportBusiness.Insert(report));
        }

    }
}
