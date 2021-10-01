using misa.timesheet.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.core.Interfaces.IRepositories
{
    public interface IRestPermissionRepository : IBaseRepository<RestPermission>
    {
        /// <summary>
        /// Lấy dữ liệu đơn nghỉ lọc và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchTerms"></param>
        /// <param name="status"></param>
        /// <param name="unitIds"></param>
        /// <returns></returns>
        PagingResult<RestPermission> GetFilter(int pageSize, int pageNumber, string searchTerms, int? status, string unitIds);

        /// <summary>
        /// từ chối tất cả các đơn nghỉ 
        /// </summary>
        /// <param name="approvedIds"></param>
        /// <param name="waitIds"></param>
        /// <returns></returns>
        int RefuseBatch(List<Guid> approvedIds, List<Guid> waitIds);

        /// <summary>
        /// duyệt tất cả các đơn nghỉ
        /// </summary>
        /// <param name="restPermissions"></param>
        /// <returns></returns>
        int ApproveBatch(List<RestPermission> restPermissions);

        /// <summary>
        /// lọc các bản ghi theo bộ lọc đã định nghĩa
        /// </summary>
        /// <param name="listFilter"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        PagingResult<RestPermission> GetFilterPro( List<FilterItem> listFilter ,int pageSize, int pageNumber);
    }
}
