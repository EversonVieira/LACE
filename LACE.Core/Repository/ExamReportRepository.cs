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
$@"SELECT Id, UserId, SourcePatientId, SourceExamId, PatientCPF, PatientRG, ExamName, FileExtension, FilePath, ExamDate, UploadDate, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn FROM ExamReport";

        private string INSERT_SQL =
$@"INSERT INTO ExamReport(UserId, RegisterId, SourcePatientId, SourceExamId, PatientCpf, PatientRG, ExamName, FileExtension, FilePath, ExamDate, UploadDate, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn)
VALUES(@UserId, @RegisterId, @SourcePatientId, @SourceExamId, @PatientCpf, @PatientRG, @ExamName, @FileExtension, @FilePath, @ExamDate, @UploadDate, @CreatedBy, @ModifiedBy, @CreatedOn, @ModifiedOn); SELECT LAST_INSERT_ID()";

        private string UPDATE_SQL = 
$@"UPDATE ExamReport SET UserId = @UserId, SourcePatientId = @SourcePatientId, SourceExamId = @SourceExamId, PatiendCpf = @PatientCpf, PatientRg = @PatientRg, 
ExamName = @ExamName, FileExtension = @FileExtension, FilePath = @FilhePath, ExamDate = @ExamDate, UploadDate = @UploadDate,
CreatedBy = @CreatedBy, ModifiedBy = @ModifiedBy, CreatedOn = @CreatedOn, ModifiedOn = @ModifiedOn
WHERE Id = @Id";


        public ExamReportRepository(ILogger<ExamReportRepository> logger) : base(logger)
        {

        }

        public NDResponse<long> Insert(DbConnection connection, ExamReport exam)
        {
            NDResponse<long> NDResponse = new NDResponse<long>();

            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add($@"@{nameof(ExamReport.UserId)}", exam.UserId);
                parameters.Add($@"@{nameof(ExamReport.RegisterId)}", exam.RegisterId);
                parameters.Add($@"@{nameof(ExamReport.PatientCPF)}", exam.PatientCPF);
                parameters.Add($@"@{nameof(ExamReport.PatientRG)}", exam.PatientRG);
                parameters.Add($@"@{nameof(ExamReport.SourcePatientId)}", exam.SourcePatientId);
                parameters.Add($@"@{nameof(ExamReport.SourceExamId)}", exam.SourceExamId);
                parameters.Add($@"@{nameof(ExamReport.ExamName)}", exam.ExamName);
                parameters.Add($@"@{nameof(ExamReport.FileExtension)}", exam.FileExtension);
                parameters.Add($@"@{nameof(ExamReport.FilePath)}", exam.FilePath);
                parameters.Add($@"@{nameof(ExamReport.ExamDate)}", exam.ExamDate);
                parameters.Add($@"@{nameof(ExamReport.UploadDate)}", exam.UploadDate);

                base.AddBaseModelParameters(parameters, exam);

                using (DbCommand cmd = CreateCommand(connection, INSERT_SQL, parameters))
                {
                    NDResponse.ResponseData = ExecuteScalar<long>(cmd);
                    NDResponse.StatusCode = HttpStatusCode.Created;
                }

            }
            catch (Exception ex)
            {
                base.HandleWithException(NDResponse, ex);
            }

            return NDResponse;
        }

        public NDResponse<bool> Update(DbConnection connection, ExamReport exam)
        {
            NDResponse<bool> NDResponse = new();

            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add($@"@{nameof(ExamReport.Id)}", exam.Id);
                parameters.Add($@"@{nameof(ExamReport.UserId)}", exam.UserId);
                parameters.Add($@"@{nameof(ExamReport.PatientCPF)}", exam.ExamName);
                parameters.Add($@"@{nameof(ExamReport.PatientRG)}", exam.PatientRG);
                parameters.Add($@"@{nameof(ExamReport.SourcePatientId)}", exam.SourcePatientId);
                parameters.Add($@"@{nameof(ExamReport.SourceExamId)}", exam.SourceExamId);
                parameters.Add($@"@{nameof(ExamReport.ExamName)}", exam.ExamName);
                parameters.Add($@"@{nameof(ExamReport.FileExtension)}", exam.FilePath);
                parameters.Add($@"@{nameof(ExamReport.FilePath)}", exam.FilePath);
                parameters.Add($@"@{nameof(ExamReport.ExamDate)}", exam.ExamDate);
                parameters.Add($@"@{nameof(ExamReport.UploadDate)}", exam.UploadDate);
                base.AddBaseModelParameters(parameters, exam);

                using (DbCommand cmd = CreateCommand(connection, UPDATE_SQL, parameters))
                {
                    ExecuteNonQuery(cmd);
                    NDResponse.ResponseData = true;
                    NDResponse.StatusCode = HttpStatusCode.OK;
                }

            }
            catch (Exception ex)
            {
                base.HandleWithException(NDResponse, ex);
            }

            return NDResponse;
        }

        public NDListResponse<ExamReport> FindByRequest(DbConnection connection, NDListRequest request)
        {
            NDListResponse<ExamReport> NDResponse = new();
            NDResponse.ResponseData ??= new();

            try
            {
                string sql = $"{SELECT_SQL} {RetrieveFilterWhereClause(request.Filters)} ORDER BY Id DESC";

                using (DbCommand cmd = CreateCommand(connection, sql.ToString(), RetrieveFilterParameters(request.Filters)))
                {
                    using (DbDataReader reader = ExecuteReader(cmd).ResponseData)
                    {
                        while (reader.Read())
                        {
                            NDResponse.ResponseData.Add(ModelUtility.FillObject<ExamReport>(reader));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                base.HandleWithException(NDResponse, ex);
            }


            return NDResponse;
        }

        public NDListResponse<ExamReport> FindAll(DbConnection connection)
        {
            NDListResponse<ExamReport> NDResponse = new();
            NDResponse.ResponseData ??= new();

            try
            {
                using (DbCommand cmd = CreateCommand(connection, SELECT_SQL))
                {
                    using (DbDataReader reader = ExecuteReader(cmd).ResponseData)
                    {
                        while (reader.Read())
                        {
                            NDResponse.ResponseData.Add(ModelUtility.FillObject<ExamReport>(reader));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                base.HandleWithException(NDResponse, ex);
            }


            return NDResponse;
        }
    }
}
