using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using misa.timesheet.core.Entities;
using misa.timesheet.core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace misa.timesheet.api.mf938.Controllers
{
    public class EmployeesController : BaseController<Employee>
    {
        #region Declare

        IEmployeeService _employeeService;

        #endregion

        #region Constructor

        public EmployeesController(IBaseService<Employee> baseService, IEmployeeService employeeService) : base(baseService)
        {
            _employeeService = employeeService;
        }

        #endregion

        /// <summary>
        /// cập nhật số nghỉ phép của một nhân viên
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="numberOfNP"></param>
        /// <returns></returns>
        [HttpPut("updateNP")]
        public IActionResult PutNP([FromQuery] Guid employeeId, [FromQuery] double numberOfNP)
        {
            var serviceResult = _employeeService.UpdateNP(employeeId, numberOfNP);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        /// <summary>
        /// lọc nhân viên theo tên|mã nhân viên| vị trí
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public IActionResult GetFitler([FromQuery] string searchTerms)
        {
            var serviceResult = _employeeService.GetFilter(searchTerms);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }
    }
}
