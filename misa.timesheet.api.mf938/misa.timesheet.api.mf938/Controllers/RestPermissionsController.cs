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
    public class RestPermissionsController : BaseController<RestPermission>
    {
        #region Declare

        IRestPermissionService _restPermissionService;

        #endregion

        #region Constructor

        public RestPermissionsController(IBaseService<RestPermission> baseService, IRestPermissionService restPermissionService) : base(baseService)
        {
            _restPermissionService = restPermissionService;
        }

        #endregion

        /// <summary>
        /// lọc đơn nghỉ theo 1 số trường và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchTerms"></param>
        /// <param name="status"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public IActionResult GetFitler([FromQuery] int pageSize, [FromQuery] int pageNumber,
            [FromQuery] string searchTerms, [FromQuery] int? status, [FromQuery] Guid? unitId)
        {
            var serviceResult = _restPermissionService.GetFilter(pageSize, pageNumber, searchTerms, status, unitId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        /// <summary>
        /// từ chối tất cả cac bản ghi có trong ds
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("refuseBatch")]
        public IActionResult RefuseBatch([FromBody] RefuseBatchEntity data) 
        {
            var serviceResult = _restPermissionService.RefuseBatch(data.ApprovedIds, data.WaitIds);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        /// <summary>
        /// duyệt tất cả các bản ghi có trong ds
        /// </summary>
        /// <param name="restPermissions"></param>
        /// <returns></returns>
        [HttpPut("approveBatch")]
        public IActionResult ApproveBatch([FromBody] List<RestPermission> restPermissions)
        {
            var serviceResult = _restPermissionService.ApproveBatch(restPermissions);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        /// <summary>
        /// lọc đơn nghỉ theo bộ lọc và phân trang
        /// </summary>
        /// <param name="listFilter"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpPost("getFilter")]
        public IActionResult GetFitlerPro([FromBody] List<FilterItem> listFilter, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var serviceResult = _restPermissionService.GetFilterPro(listFilter, pageSize , pageNumber);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }
    }
}
