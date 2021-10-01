using misa.timesheet.core.Entities;
using misa.timesheet.core.Interfaces.IRepositories;
using misa.timesheet.core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {

        #region Declare

        IEmployeeRepository _employeeRepository;

        #endregion

        #region Constructor

        public EmployeeService(IEmployeeRepository employeeRepository, IBaseRepository<Employee> baseRepository) : base(baseRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Xử lí nghiệp vụ lọc nhân viên
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        public ServiceResult GetFilter(string searchTerms)
        {
            try
            {
                //Xử lí nghiệp vụ
                //Lấy dữ liệu từ db
                List<Employee> employees = _employeeRepository.GetFilter(searchTerms);
                if (employees.Count() > 0)
                {
                    _serviceResult.Data = employees;
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MyResources.ResourceErrorTypeVN.User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        #endregion


        /// <summary>
        /// Xử lí nghiệp vụ cập nhật số nghỉ phép cho nhân viên có id đi kèm
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="numberOfNP"></param>
        /// <returns></returns>
        public ServiceResult UpdateNP(Guid employeeId, double numberOfNP)
        {
            try
            {
                // xử lí nghiệp vụ
                Employee employee = _employeeRepository.GetById(employeeId);

                if((employee.RemainingNP - numberOfNP) < 0)
                {
                    var errorObj = new
                    {
                        devMsg = MyResources.ResourceErrorTypeVN.Dev_OutRangeOfNP,
                        userMsg = MyResources.ResourceErrorTypeVN.User_OutRangeOfNP,
                    };
                    _serviceResult.Data = errorObj;
                    _serviceResult.StatusCode = 500;
                    return _serviceResult;
                }
                else
                {
                    // tương tác với db
                    int rowAffect = _employeeRepository.UpdateNP(employeeId, numberOfNP);
                    if (rowAffect > 0)
                    {
                        _serviceResult.Data = rowAffect;
                        _serviceResult.StatusCode = 201;
                        return _serviceResult;
                    }
                    else
                    {
                        _serviceResult.StatusCode = 500;
                        return _serviceResult;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MyResources.ResourceErrorTypeVN.User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }
    }
}
