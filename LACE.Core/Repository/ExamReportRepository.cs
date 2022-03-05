using LACE.Core.Models;
using LACE.Core.Utility;
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
    public class ExamReportRepository : BaseRepository
    {
        private string SELECT_SQL =
$@"SELECT Id, UserId, SourcePatientId, SourceExamId, PatientCpf, PatientRG, FileName, FileExtension, FilePath, ExamDate, UploadDate, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn FROM ExamReport";

        private string INSERT_SQL =
$@"INSERT INTO ExamReport(UserId, SourcePatientId, SourceExamId, PatientCpf, PatientRG, FileName, FileExtension, FilePath, ExamDate, UploadDate, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn)
VALUES(@UserId, @SourcePatientId, @SourceExamId, @PatientCpf, @PatientRG, @FileName, @FileExtension, @FilePath, @ExamDate, @UploadDate, @CreatedBy, @ModifiedBy, @CreatedOn, @ModifiedOn)";

        private string UPDATE_SQL = 
$@"UPDATE ExamReport SET UserId = @UserId, SourcePatientId = @SourcePatientId, SourceExamId = @SourceExamId, PatiendCpf = @PatientCpf, PatientRg = @PatientRg, 
FileName = @FileName, FileExtension = @FileExtension, FilePath = @FilhePath, ExamDate = @ExamDate, UploadDate = @UploadDate,
CreatedBy = @CreatedBy, ModifiedBy = @ModifiedBy, CreatedOn = @CreatedOn, ModifiedOn = @ModifiedOn
WHERE Id = @Id";


        public ExamReportRepository(IDBConnectionFactory dBConnectionFactory, ILogger<ExamReportRepository> logger) : base(dBConnectionFactory, logger)
        {

        }

        public Response<int> Insert(AuthUser user)
        {
            Response<int> response = new Response<int>();

            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add($@"@{nameof(AuthUser.Cpf)}", user.Cpf);
                parameters.Add($@"@{nameof(AuthUser.Rg)}", user.Rg);
                parameters.Add($@"@{nameof(AuthUser.Name)}", user.Name);
                parameters.Add($@"@{nameof(AuthUser.Email)}", user.Email);
                parameters.Add($@"@{nameof(AuthUser.Password)}", user.Password);
                parameters.Add($@"@{nameof(AuthUser.IsActive)}", user.IsActive);
                parameters.Add($@"@{nameof(AuthUser.IsLocked)}", user.IsLocked);
                base.AddBaseModelParameters(parameters, user);

                using (DbCommand cmd = CreateCommand(INSERT_SQL, parameters))
                {
                    response.ResponseData = ExecuteScalar(cmd);
                    response.StatusCode = HttpStatusCode.Created;
                }

            }
            catch (Exception ex)
            {
                base.HandleWithException(response, ex);
            }

            return response;
        }

        public Response<bool> Update(AuthUser user)
        {
            Response<bool> response = new();

            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add($@"@{nameof(AuthUser.Cpf)}", user.Cpf);
                parameters.Add($@"@{nameof(AuthUser.Rg)}", user.Rg);
                parameters.Add($@"@{nameof(AuthUser.Name)}", user.Name);
                parameters.Add($@"@{nameof(AuthUser.Email)}", user.Email);
                parameters.Add($@"@{nameof(AuthUser.Password)}", user.Password);
                parameters.Add($@"@{nameof(AuthUser.IsActive)}", user.IsActive);
                parameters.Add($@"@{nameof(AuthUser.IsLocked)}", user.IsLocked);
                base.AddBaseModelParameters(parameters, user);

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

        public ListResponse<AuthUser> FindByRequest(BaseListRequest request)
        {
            ListResponse<AuthUser> response = new();
            response.ResponseData ??= new();

            try
            {
                string sql = $"{SELECT_SQL} {RetrieveFilterWhereClause(request.Filters)} ORDER BY Id DESC";

                using (DbCommand cmd = CreateCommand(sql.ToString(), RetrieveFilterParameters(request.Filters)))
                {
                    using (DbDataReader reader = ExecuteReader(cmd).ResponseData)
                    {
                        while (reader.Read())
                        {
                            response.ResponseData.Add(ModelUtility.FillObject<AuthUser>(reader));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                base.HandleWithException(response, ex);
            }


            return response;
        }

        public ListResponse<AuthUser> FindAll()
        {
            ListResponse<AuthUser> response = new();
            response.ResponseData ??= new();

            try
            {
                using (DbCommand cmd = CreateCommand(SELECT_SQL))
                {
                    using (DbDataReader reader = ExecuteReader(cmd).ResponseData)
                    {
                        while (reader.Read())
                        {
                            response.ResponseData.Add(ModelUtility.FillObject<AuthUser>(reader));
                        }
                    }
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
