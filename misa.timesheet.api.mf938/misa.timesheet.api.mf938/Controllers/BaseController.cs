using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using misa.timesheet.core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace misa.timesheet.api.mf938.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<MISAEntity> : ControllerBase
    {
        #region Delare

        protected IBaseService<MISAEntity> _baseService;

        #endregion

        #region Consstructor

        public BaseController(IBaseService<MISAEntity> baseService)
        {
            _baseService = baseService;
        }

        #endregion

        /// <summary>
        /// Lấy toàn bộ ds
        /// </summary>
        /// <returns></returns>
        /// Author HieuNv
        [HttpGet]
        public IActionResult GetAll()
        {
            var serviceResult = _baseService.GetAll();
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            var serviceResult = _baseService.GetById(entityId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        [HttpGet("getByIds/{entityIds}")]
        public IActionResult GetByIds(String entityIds)
        {
            var serviceResult = _baseService.GetByIds(entityIds);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        /// <summary>
        /// thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author HieuNv
        [HttpPost]
        public virtual IActionResult Post([FromBody] MISAEntity entity)
        {
            var serviceResult = _baseService.Add(entity);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        /// <summary>
        /// cập nhật 1 bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author HieuNv
        [HttpPut("{entityId}")]
        public virtual IActionResult Put(Guid entityId, [FromBody] MISAEntity entity)
        {
            var serviceResult = _baseService.Update(entity, entityId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            var serviceResult = _baseService.Delete(entityId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        [HttpPost("deleteBatch")]
        public IActionResult DeleteBatch([FromBody] List<Guid> entityIds)
        {
            var serviceResult = _baseService.DeleteBatch(entityIds);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }
    }
}
