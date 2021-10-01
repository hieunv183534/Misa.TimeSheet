using Dapper;
using misa.timesheet.core.Entities;
using misa.timesheet.core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.infrastructure.Repositories
{
    public class RestPermissionRepository : BaseRepository<RestPermission>, IRestPermissionRepository
    {
        /// <summary>
        /// Lấy ds đơn nghỉ lọc và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchTerms"></param>
        /// <param name="status"></param>
        /// <param name="unitIds"></param>
        /// <returns></returns>
        public PagingResult<RestPermission> GetFilter(int pageSize, int pageNumber, string searchTerms, int? status, string unitIds)
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@SearchTerms", searchTerms);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@Status", status);
                parameters.Add("@UnitIds", unitIds);
                parameters.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var procName = $"Proc_GetFilterPagingRestPermission";

                var restPermissions = dbConnection.Query<RestPermission>(procName, param: parameters, commandType: CommandType.StoredProcedure);
                var TotalRecord = parameters.Get<int>("TotalRecord");
                return new PagingResult<RestPermission>(TotalRecord, (List<RestPermission>)restPermissions);
            }
        }

        /// <summary>
        /// lấy ds đơn nghỉ theo giá trị cả bộ lọc và phân trang
        /// </summary>
        /// <param name="listFilter"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public PagingResult<RestPermission> GetFilterPro(List<FilterItem> listFilter, int pageSize, int pageNumber)
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var sql = $"select * from view_restpermission vr ";
                var sqlCondition = $"where 1=1 ";

                foreach( var item in listFilter)
                {
                    if (item.Enable)
                    {
                        if(item.FilterType == "FilterText")
                        {
                            switch (item.FilterOption)
                            {
                                // chứa
                                case "1":
                                    sqlCondition += $" and vr.{item.DataField} LIKE CONCAT('%', '{item.FilterValue}' , '%') ";
                                    break;
                                // không chứa
                                case "2":
                                    sqlCondition += $" and vr.{item.DataField} NOT LIKE CONCAT('%', '{item.FilterValue}' , '%') ";
                                    break;
                                // bằng
                                case "3":
                                    sqlCondition += $" and vr.{item.DataField} LIKE '{item.FilterValue}' ";
                                    break;
                                // bắt đầu bằng
                                case "4":
                                    sqlCondition += $" and vr.{item.DataField} LIKE CONCAT('{item.FilterValue}' , '%') ";
                                    break;
                                // kết thúc bằng
                                case "5":
                                    sqlCondition += $" and vr.{item.DataField} LIKE CONCAT('%', '{item.FilterValue}') ";
                                    break;
                                // trống
                                case "6":
                                    sqlCondition += $" and vr.{item.DataField} IS NULL or  vr.{item.DataField} = '' ";
                                    break;
                                // không trống
                                case "7":
                                    sqlCondition += $" and vr.{item.DataField} IS NOT NULL and  vr.{item.DataField} != '' ";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                // lấy tổng số bản ghi 
                var sqlCount = $"select count(RestPermissionId) as TotalRecord from view_restpermission vr " + sqlCondition;
                int TotalRecord = (int)dbConnection.QueryFirstOrDefault(sqlCount).TotalRecord;
                // Xử lsi điều kiện limit, offset
                var limit = pageSize;
                var offset = (pageNumber - 1) * pageSize;
                sql += sqlCondition;
                sql += $" limit {offset} , {limit} ";

                var restPermissions = dbConnection.Query<RestPermission>(sql);

                return new PagingResult<RestPermission>(TotalRecord, (List<RestPermission>)restPermissions);
            }
        }

        /// <summary>
        /// từ chối hàng loạt đơn nghỉ trong ds
        /// </summary>
        /// <param name="approvedIds"></param>
        /// <param name="waitIds"></param>
        /// <returns></returns>
        public int RefuseBatch(List<Guid> approvedIds, List<Guid> waitIds)
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var rowAffects = 0;
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    // từ chối những th đang chờ
                    foreach(var waitId in waitIds)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add($"@Status", 2);
                        parameters.Add($"@RestPermissionId", waitId);
                        rowAffects += dbConnection.Execute("Proc_UpdateFieldStatus", param: parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                    }
                    // từ chối những th đã duyệt
                    foreach(var approvedId in approvedIds)
                    {
                        DynamicParameters parameters1 = new DynamicParameters();
                        parameters1.Add($"@RestPermissionId", approvedId);
                        var applicant = dbConnection.QueryFirstOrDefault<RestPermission>("Proc_GetRestPermissionById", param: parameters1, commandType: CommandType.StoredProcedure, transaction: transaction);
                        var applicantId = applicant.ApplicantId;
                        var numberOfNP = getNumberOfNP(applicant.StartTime, applicant.EndTime);
                        DynamicParameters parameters2 = new DynamicParameters();
                        parameters2.Add("@EmployeeId", applicantId);
                        parameters2.Add("@NumberOfNP", -numberOfNP);
                        int a = dbConnection.Execute("Proc_UpdateNumberOfNP", param: parameters2, commandType: CommandType.StoredProcedure, transaction: transaction);
                        if (a == 1)
                        {
                            DynamicParameters parameters3 = new DynamicParameters();
                            parameters3.Add($"@Status", 2);
                            parameters3.Add($"@RestPermissionId", approvedId);
                            rowAffects += dbConnection.Execute("Proc_UpdateFieldStatus", param: parameters3, commandType: CommandType.StoredProcedure, transaction: transaction);
                        }
                    }
                    if(rowAffects == (approvedIds.Count()+ waitIds.Count()))
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                return rowAffects;
            }
        }

        /// <summary>
        /// duyệt hàng loạt đơn nghỉ trong ds
        /// </summary>
        /// <param name="restPermissions"></param>
        /// <returns></returns>
        int IRestPermissionRepository.ApproveBatch(List<RestPermission> restPermissions)
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var rowAffects = 0;
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    foreach (var restPermission in restPermissions)
                    {
                        var numberOfNP = getNumberOfNP(restPermission.StartTime, restPermission.EndTime);
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add($"@EmployeeId", restPermission.ApplicantId);
                        var employee = dbConnection.QueryFirstOrDefault<Employee>("Proc_GetEmployeeById", param: parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                        if (employee.RemainingNP < numberOfNP)
                        {
                            return 0;
                        }
                        else
                        {
                            DynamicParameters parameters2 = new DynamicParameters();
                            parameters2.Add("@EmployeeId", restPermission.ApplicantId);
                            parameters2.Add("@NumberOfNP", numberOfNP);
                            int a = dbConnection.Execute("Proc_UpdateNumberOfNP", param: parameters2, commandType: CommandType.StoredProcedure, transaction: transaction);
                            if(a == 1)
                            {
                                DynamicParameters parameters3 = new DynamicParameters();
                                parameters3.Add($"@Status", 1);
                                parameters3.Add($"@RestPermissionId", restPermission.RestPermissionId);
                                rowAffects += dbConnection.Execute("Proc_UpdateFieldStatus", param: parameters3, commandType: CommandType.StoredProcedure, transaction: transaction);
                            }
                        }
                    }
                    if (rowAffects == (restPermissions.Count()))
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                return rowAffects;
            }
        }

        /// <summary>
        /// trả về số ngày nghỉ phép từ startTime và endTime
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        double getNumberOfNP(DateTime startTime, DateTime endTime)
        {
            TimeSpan span = endTime - startTime;
            int ms =(int)span.TotalSeconds;
            double numDay = (ms)*1.0 / (24 * 60 * 60);
            return (double)Math.Round((double)numDay, 2);
        }
    }
}
