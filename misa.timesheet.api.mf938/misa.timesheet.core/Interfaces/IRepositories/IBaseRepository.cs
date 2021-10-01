using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.core.Interfaces.IRepositories
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Lấy toàn bộ ds
        /// </summary>
        /// <returns></returns>
        /// Author HieuNv
        List<MISAEntity> GetAll();

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        MISAEntity GetById(Guid entityId);

        /// <summary>
        /// Lấy theo ds id
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        /// Author HieuNv
        List<MISAEntity> GetByIds(String entityIds);

        /// <summary>
        /// Thêm
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author HieuNv
        int Add(MISAEntity entity);

        /// <summary>
        /// Sửa 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        int Update(MISAEntity entity, Guid entityId);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        int Delete(Guid entityId);

        /// <summary>
        /// Lấy theo một prop
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        /// Author HieuNv
        MISAEntity GetByProp(string propName, object propValue);

        /// <summary>
        /// Xóa tất cả các bản ghi có id trong mảng
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        int DeleteBatch(List<Guid> entityIds);
    }
}
