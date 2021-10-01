using misa.timesheet.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.core.Interfaces.IRepositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Cập nhật số nghỉ phép của nhân viên được duyệt nghỉ
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="numberOfNP"></param>
        /// <returns></returns>
        int UpdateNP(Guid employeeId, double numberOfNP);

        /// <summary>
        /// tìm kiếm nhân viên theo tên, mã , vị trí công việc
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        List<Employee> GetFilter(string searchTerms);
    }
}
