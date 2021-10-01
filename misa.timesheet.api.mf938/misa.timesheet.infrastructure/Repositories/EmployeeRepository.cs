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
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// tìm kiếm nhân viên theo mã, tên, vị trí công việc
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        public List<Employee> GetFilter(string searchTerms)
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@SearchTerms", searchTerms);
                var procName = $"Proc_GetFilterEmployee";

                var employees = dbConnection.Query<Employee>(procName, param: parameters, commandType: CommandType.StoredProcedure);
                return (List<Employee>)employees;
            }
        }

        /// <summary>
        /// Cập nhật số nghỉ phép của nhân viên được duyệt nghỉ
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="numberOfNP"></param>
        /// <returns></returns>
        public int UpdateNP(Guid employeeId, double numberOfNP)
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", employeeId);
                parameters.Add("@NumberOfNP", numberOfNP);
                var procName = $"Proc_UpdateNumberOfNP";
                var rowAffect = dbConnection.Execute(procName, param: parameters, commandType: CommandType.StoredProcedure);
                return rowAffect;
            }
        }
    }
}
