using LACE.Core.Models;
using LACE.Core.Models.DTO;
using LACE.Core.Repository;
using LACE.Core.Utility;
using LACE.Core.Validators;
using Microsoft.Extensions.Logging;
using Nedesk.Core.DataBase.Factory;
using Nedesk.Core.Enums;
using Nedesk.Core.Models;
using Nedesk.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Business
{
    public class ExamReportBusiness
    {
        private readonly ExamReportRepository _examReportRepository;
        private readonly ExamReportValidator _validator;
        private readonly NDIDBConnectionFactory _connectionFactory;
        private readonly ILogger _logger;

        public ExamReportBusiness(ExamReportValidator validator, 
                                  ExamReportRepository examReportRepository,
                                  NDIDBConnectionFactory connectionFactory,
                                  ILogger<ExamReportBusiness> logger)
        {
            _validator = validator;
            _connectionFactory = connectionFactory;
            _examReportRepository = examReportRepository;
            _logger = logger;
        }

        public NDResponse<long> Insert(DTO_ExamReport report)
        {
            NDResponse<long> NDResponse = new NDResponse<long>();

            ExamReport endReport = ModelUtility.FromObj<DTO_ExamReport, ExamReport>(report);
            _validator.ValidateInsert(NDResponse, endReport);

            if (!NDResponse.IsValid)
                return NDResponse;

            ExamReportStorageUtility.SaveFile(endReport);


            using (DbConnection connection = _connectionFactory.GetReadWriteConnection())
            {
                NDResponse = _examReportRepository.Insert(connection, endReport);
            }


            return NDResponse;
        }

        public NDResponse<bool> Update(DTO_ExamReport report)
        {
            NDResponse<bool> NDResponse = new NDResponse<bool>();

            ExamReport endReport = ModelUtility.FromObj<DTO_ExamReport, ExamReport>(report);

            _validator.ValidateUpdate(NDResponse, endReport);
            if (!NDResponse.IsValid)
                return NDResponse;

            using (DbConnection connection = _connectionFactory.GetReadWriteConnection())
            {
                NDResponse = _examReportRepository.Update(connection, endReport);
            }

            return NDResponse;
        }

        public NDListResponse<DTO_ExamReport> FindAll()
        {
            NDListResponse<DTO_ExamReport> NDResponse = new NDListResponse<DTO_ExamReport>();

            using (DbConnection connection = _connectionFactory.GetReadWriteConnection())
            {
                var examReportNDResponse =  _examReportRepository.FindAll(connection);

                ExamReportStorageUtility.GetFiles(examReportNDResponse.ResponseData, _logger);

                foreach (ExamReport rpt in examReportNDResponse.ResponseData)
                {
                    NDResponse = new NDListResponse<DTO_ExamReport>();
                    NDResponse.ResponseData.Add(ModelUtility.FromObj<ExamReport, DTO_ExamReport>(rpt));
                }


                return NDResponse;
            }

        }

        public NDListResponse<DTO_ExamReport> FindByUserId(AuthUser user)
        {
            NDListResponse<DTO_ExamReport> NDResponse = new NDListResponse<DTO_ExamReport>();


            NDListRequest request = new NDListRequest();
            request.Filters.AddRange(new List<NDFilter>
            {
                new NDFilter
                {
                    Target1 = "PatientCpf",
                    OperationType = NDFilterOperationTypeEnum.Equals,
                    AggregateType = NDFilterAggregateTypeEnum.OR,
                    Value1 = user.Cpf
                },
                new NDFilter
                {
                    Target1 = "PatientRg",
                    OperationType = NDFilterOperationTypeEnum.Equals,
                    AggregateType = NDFilterAggregateTypeEnum.OR,
                    Value1 = user.Rg
                },
                new NDFilter
                {
                    Target1 = "PatientSUS",
                    OperationType = NDFilterOperationTypeEnum.Equals,
                    AggregateType = NDFilterAggregateTypeEnum.OR,
                    Value1 = user.Sus
                },
            });

            using (DbConnection connection = _connectionFactory.GetReadOnlyConnection())
            {
                var examReportNDResponse = _examReportRepository.FindByRequest(connection, request);

                if (!NDResponse.IsValid)
                    return NDResponse;

                ExamReportStorageUtility.GetFiles(examReportNDResponse.ResponseData, _logger);

                NDResponse = new NDListResponse<DTO_ExamReport>();
                NDResponse.ResponseData ??= new List<DTO_ExamReport>();
                foreach (ExamReport rpt in examReportNDResponse.ResponseData)
                {
                    NDResponse.ResponseData.Add(ModelUtility.FromObj<ExamReport, DTO_ExamReport>(rpt));
                }

                NDResponse.ResponseData = NDResponse.ResponseData.OrderByDescending(x => x.UploadDate).ToList();
                return NDResponse;

            }
        }

        public NDListResponse<DTO_ExamReport> FindByRegisterId(string registerID)
        {
            NDListResponse<DTO_ExamReport> NDResponse = new NDListResponse<DTO_ExamReport>();

            NDListRequest request = new NDListRequest();
            request.Filters.AddRange(new List<NDFilter>
            {
                new NDFilter
                {
                    Target1 = "RegisterId",
                    OperationType = NDFilterOperationTypeEnum.Equals,
                    AggregateType = NDFilterAggregateTypeEnum.AND,
                    Value1 = registerID
                },
            });

            using (DbConnection connection = _connectionFactory.GetReadOnlyConnection())
            {
                var examReportNDResponse = _examReportRepository.FindByRequest(connection, request);

                if (!examReportNDResponse.IsValid)
                {
                    NDResponse.Merge(examReportNDResponse);
                    return NDResponse; 
                }

                if (!examReportNDResponse.ResponseData.Any())
                {
                    NDResponse.AddValidationMessage("911", "Não foi encontrado nenhum laudo com o código informado");
                    return NDResponse;
                }

                ExamReportStorageUtility.GetFiles(examReportNDResponse.ResponseData, _logger);

                NDResponse = new NDListResponse<DTO_ExamReport>();
                NDResponse.ResponseData ??= new List<DTO_ExamReport>();
                foreach (ExamReport rpt in examReportNDResponse.ResponseData)
                {
                    NDResponse.ResponseData.Add(ModelUtility.FromObj<ExamReport, DTO_ExamReport>(rpt));
                }

                NDResponse.ResponseData = NDResponse.ResponseData.OrderByDescending(x => x.UploadDate).ToList();
                return NDResponse;
            }
        }
    }
}
