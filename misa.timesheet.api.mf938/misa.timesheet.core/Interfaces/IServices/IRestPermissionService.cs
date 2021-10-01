using misa.timesheet.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.core.Interfaces.IServices
{
    public interface IRestPermissionService : IBaseService<RestPermission>
    {
        /// <summary>
        /// lấy ds đơn nghỉ lọc và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchTerms"></param>
        /// <param name="status"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        ServiceResult GetFilter(int pageSize, int pageNumber, string searchTerms,int? status, Guid? unitId);

        /// <summary>
        /// xử lí nghiệp vụ từ chối các đơn nghỉ trong ds
        /// </summary>
        /// <param name="approvedIds"></param>
        /// <param name="waitIds"></param>
        /// <returns></returns>
        ServiceResult RefuseBatch(List<Guid> approvedIds,  List<Guid> waitIds);

        /// <summary>
        /// xử lí nghiệp vụ duyệt tất cả các bản ghi trong ds
        /// </summary>
        /// <param name="restPermissions"></param>
        /// <returns></returns>
        ServiceResult ApproveBatch(List<RestPermission> restPermissions);

        /// <summary>
        /// xử lí nghiệp vụ lọc đơn nghỉ theo bộ lọc đã định nghĩa
        /// </summary>
        /// <param name="listFilter"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        ServiceResult GetFilterPro(List<FilterItem> listFilter, int pageSize, int pageNumber);
    }
}
