using LACE.Core.Models;
using Microsoft.Extensions.Logging;
using Nedesk.Core.DataBase.Factory;
using Nedesk.Core.Models;
using Nedesk.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Repository
{
    public class AuthSessionRepository : BaseRepository
    {
        private string INSERT_SQL =
$@"INSERT INTO AuthSession(UserId, SessionKey, LastRenewDate) VALUES(@UserId, @SessionKey, @LastRenewDate); SELECT LAST_INSERT_ID();";

        private string UPDATE_SQL =
$@"UPDATE AuthSession SET UserId = @UserId, SessionKey = @SessionKey, LastRenewDate = @LastRenewDate WHERE Id = @Id;";

        private string SELECT_SQL =
$@"SELECT Id, UserId, SessionKey, LastRenewDate FROM AuthSession ";

        private string DELETE_SQL =
$@"DELETE FROM AuthSession WHERE Id = @Id";

        public AuthSessionRepository(IDBConnectionFactory dBConnectionFactory, ILogger<BaseRepository> logger) : base(dBConnectionFactory, logger)
        {
        }

        public Response<int> Insert(AuthSession session)
        {
            Response<int> response = new Response<int>();

            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add($"{nameof(AuthSession.UserId)}", session.UserId);
                parameters.Add($"{nameof(AuthSession.SessionKey)}", session.SessionKey);
                parameters.Add($"{nameof(AuthSession.LastRenewDate)}", session.LastRenewDate);
                using (DbCommand cmd = CreateCommand(INSERT_SQL, parameters))
                {
                    response.ResponseData =  ExecuteScalar(cmd);
                    response.StatusCode = HttpStatusCode.Created;
                }

            }
            catch (Exception ex)
            {
                base.HandleWithException(response, ex);
            }

            return response;
        }

        public Response<bool> Update(AuthSession session)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add($"{nameof(AuthSession.Id)}", session.Id);
                parameters.Add($"{nameof(AuthSession.UserId)}", session.UserId);
                parameters.Add($"{nameof(AuthSession.SessionKey)}", session.SessionKey);
                parameters.Add($"{nameof(AuthSession.LastRenewDate)}", session.LastRenewDate);

                using (DbCommand cmd = CreateCommand(UPDATE_SQL, parameters))
                {
                    ExecuteNonQuery(cmd);
                    response.ResponseData = true;
                    response.StatusCode = HttpStatusCode.OK;
                }

            }
            catch (Exception ex)
            {
                base.HandleWithException(response, ex);
            }

            return response;
        }

        public Response<bool> DeleteById(long id)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add($"{nameof(AuthSession.Id)}", id);

                using (DbCommand cmd = CreateCommand(DELETE_SQL, parameters))
                {
                    ExecuteNonQuery(cmd);
                    response.ResponseData = true;
                    response.StatusCode = HttpStatusCode.OK;
                }

            }
            catch (Exception ex)
            {
                base.HandleWithException(response, ex);
            }

            return response;

        }

        public ListResponse<AuthSession> FindByRequest(BaseListRequest request)
        {
            ListResponse<AuthSession> response = new ListResponse<AuthSession>();

            try
            {
                Dictionary<string, object> parameters = RetrieveFilterParameters(request.Filters);
                string sql = $"{SELECT_SQL} {RetrieveFilterWhereClause(request.Filters)}";

                using (DbCommand cmd = CreateCommand(SELECT_SQL, parameters))
                {
                    ExecuteNonQuery(cmd);
                    response.StatusCode = HttpStatusCode.OK;
                }

            }
            catch (Exception ex)
            {
                base.HandleWithException(response, ex);
            }

            return response;
        }

    }
}
