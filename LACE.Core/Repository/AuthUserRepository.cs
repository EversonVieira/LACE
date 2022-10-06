using Dapper;
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
    public class AuthUserRepository : BaseRepository
    {
        private string SELECT_SQL = 
@$"SELECT Id, Cpf, Rg, Sus, Name, Email, IsActive, IsLocked, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn FROM AuthUser ";

        private string INSERT_SQL =
$@"INSERT INTO AuthUser(Cpf, Rg, Sus, Name, Email, Password, IsActive, IsLocked, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn)
VALUES(@Cpf, @Rg, @Sus, @Name, @Email, @Password, @IsActive, @IsLocked, @CreatedBy, @ModifiedBy, @CreatedOn, @ModifiedOn); SELECT LAST_INSERT_ID();";

        private string UPDATE_SQL =
$@"UPDATE AuthUser SET Name = @Name, Password = @Password, IsActive = @IsActive, IsLocked = @IsLocked,
CreatedBy = @CreatedBy, ModifiedBy = @ModifiedBy, CreatedOn = @CreatedOn, ModifiedOn = @ModifiedOn
WHERE Id = @Id";


        public AuthUserRepository(ILogger<BaseRepository> logger) : base(logger)
        {

        }

        public NDResponse<long> Insert(DbConnection connection,AuthUser user)
        {
            NDResponse<long> NDResponse = new NDResponse<long>();

            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add($@"@{nameof(AuthUser.Cpf)}", user.Cpf);
                parameters.Add($@"@{nameof(AuthUser.Rg)}", user.Rg);
                parameters.Add($@"@{nameof(AuthUser.Sus)}", user.Sus);
                parameters.Add($@"@{nameof(AuthUser.Name)}", user.Name);
                parameters.Add($@"@{nameof(AuthUser.Email)}", user.Email);
                parameters.Add($@"@{nameof(AuthUser.Password)}", user.Password);
                parameters.Add($@"@{nameof(AuthUser.IsActive)}", user.IsActive);
                parameters.Add($@"@{nameof(AuthUser.IsLocked)}", user.IsLocked);
                base.AddBaseModelParameters(parameters, user);
                
                using (DbCommand cmd = CreateCommand(connection, INSERT_SQL, parameters))
                {
                    NDResponse.ResponseData = ExecuteScalar(cmd);
                    NDResponse.StatusCode = HttpStatusCode.Created;
                }

            }
            catch (Exception ex)
            {
                base.HandleWithException(NDResponse, ex);
            }

            return NDResponse;
        }

        public NDResponse<bool> Update(DbConnection connection, AuthUser user)
        {
            NDResponse<bool> NDResponse = new();

            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add($@"@{nameof(AuthUser.Id)}", user.Id);
                parameters.Add($@"@{nameof(AuthUser.Name)}", user.Name);
                parameters.Add($@"@{nameof(AuthUser.Email)}", user.Email);
                parameters.Add($@"@{nameof(AuthUser.Password)}", user.Password);
                parameters.Add($@"@{nameof(AuthUser.IsActive)}", user.IsActive);
                parameters.Add($@"@{nameof(AuthUser.IsLocked)}", user.IsLocked);
                base.AddBaseModelParameters(parameters, user);

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

        public NDListResponse<AuthUser> FindByRequest(DbConnection connection, NDListRequest request)
        {
            NDListResponse<AuthUser> NDResponse = new();
            NDResponse.ResponseData ??= new();

            try
            {
                string sql = $"{SELECT_SQL} {RetrieveFilterWhereClause(request.Filters)}";

                using (DbCommand cmd = CreateCommand(connection, sql.ToString(), RetrieveFilterParameters(request.Filters)))
                {
                    using (DbDataReader reader = ExecuteReader(cmd).ResponseData)
                    {
                        while (reader.Read())
                        {
                            NDResponse.ResponseData.Add(ModelUtility.FillObject<AuthUser>(reader));
                        }
                    }
                }

                NDResponse.StatusCode = HttpStatusCode.OK;

            }
            catch(Exception ex)
            {
                base.HandleWithException(NDResponse, ex);
            }


            return NDResponse;
        }

        public NDListResponse<AuthUser> FindAll(DbConnection connection)
        {
            NDListResponse<AuthUser> NDResponse = new();
            NDResponse.ResponseData ??= new();

            try
            {
                using (DbCommand cmd = CreateCommand(connection, SELECT_SQL))
                {
                    using (DbDataReader reader = ExecuteReader(cmd).ResponseData)
                    {
                        while (reader.Read())
                        {
                            NDResponse.ResponseData.Add(ModelUtility.FillObject<AuthUser>(reader));
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
