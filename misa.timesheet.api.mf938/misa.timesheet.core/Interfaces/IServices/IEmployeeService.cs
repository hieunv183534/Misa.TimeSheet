using misa.timesheet.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.core.Interfaces.IServices
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// cập nhật số NP của nhân viên có id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="numberOfNP"></param>
        /// <returns></returns>
        ServiceResult UpdateNP(Guid employeeId, double numberOfNP);

        /// <summary>
        /// tìm kiếm theo mã, tên, vị trí công việc
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        ServiceResult GetFilter(string searchTerms);
    }
}
