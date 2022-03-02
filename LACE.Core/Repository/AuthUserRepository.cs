using LACE.Core.Models;
using Microsoft.Extensions.Logging;
using Nedesk.Core.DataBase.Factory;
using Nedesk.Core.Models;
using Nedesk.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Repository
{
    public class AuthUserRepository : BaseRepository
    {
        public AuthUserRepository(IDBConnectionFactory dBConnectionFactory, ILogger<BaseRepository> logger) : base(dBConnectionFactory, logger)
        {

        }

        public async Task<ListResponse<AuthUser>> FindByRequest(BaseListRequest request)
        {
            ListResponse<AuthUser> response = new();

            try
            {

            }
            catch(Exception ex)
            {
                base.HandleWithException(response, ex);
            }


            return response;
        }
    }
}
