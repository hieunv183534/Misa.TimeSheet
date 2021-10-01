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
    public class RestPermissionService: BaseService<RestPermission>, IRestPermissionService
    {

        #region Declare

        IRestPermissionRepository _restPermissionRepository;
        static List<Unit> _units;

        #endregion

        #region Constructor

        public RestPermissionService(IRestPermissionRepository restPermissionRepository, IBaseRepository<RestPermission> baseRepository, IBaseRepository<Unit> baseRepositoryUnit) : base(baseRepository)
        {
            _restPermissionRepository = restPermissionRepository;
            _units = baseRepositoryUnit.GetAll();
        }

        #endregion

        /// <summary>
        /// xử lí nghiệp vụ lấy ds đơn nghỉ lọc và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchTerms"></param>
        /// <param name="status"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ServiceResult GetFilter(int pageSize, int pageNumber, string searchTerms, int? status, Guid? unitId)
        {
            try
            {
                string unitIds;
                //Xử lí nghiệp vụ
                if (unitId != null)
                {
                    unitIds = string.Empty;
                    unitIds = GetUnitIdList(unitId);
                }
                else
                {
                    unitIds = null;
                }
                //Lấy dữ liệu từ db
                PagingResult<RestPermission> pagingResult = _restPermissionRepository.GetFilter(pageSize, pageNumber, searchTerms, status, unitIds);
                if (pagingResult.Data.Count() > 0)
                {
                    _serviceResult.Data = pagingResult;
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

        /// <summary>
        /// lấy ds các id trong cơ cấu của unitId
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        private string GetUnitIdList(Guid? unitId)
        {
            string unitIds = string.Empty;
            for(int i=0; i< _units.Count ; i++)
            {
                var unit = _units[i];
                if (IsChild(unit.UnitId, unitId))
                {
                    unitIds += unit.UnitId + " ";
                }
            }
            unitIds += unitId;
            return unitIds;
        }

        /// <summary>
        /// kiểm tra 1 đợn vị có thuộc đơn vị kia không
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        static Boolean IsChild(Guid unitId, Guid? targetId)
        {
            if(Parent(unitId, targetId) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// hàm đệ quy tìm về tổ tiên của 1 đơn vị
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        static Guid? Parent(Guid? unitId, Guid? targetId)
        {
            if (find(unitId).ParentId == null)
                return null;
            else if(find(unitId).ParentId.ToString() == targetId.ToString())
            {
                return targetId;
            }
            else
            {
                return Parent(find(unitId).ParentId, targetId);
            }
        }

        /// <summary>
        /// tìm đơn vị trong list dựa và id
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        static Unit find(Guid? unitId)
        {
            foreach(var unit in _units)
            {
                if(unit.UnitId.ToString() == unitId.ToString())
                {
                    return unit;
                }
            }
            return null;
        }

        /// <summary>
        /// xử lí nghiệp vụ từ chối hàng loạt
        /// </summary>
        /// <param name="approvedIds"></param>
        /// <param name="waitIds"></param>
        /// <returns></returns>
        public ServiceResult RefuseBatch(List<Guid> approvedIds, List<Guid> waitIds)
        {
            try
            {
                var rowAffect = _restPermissionRepository.RefuseBatch(approvedIds, waitIds);
                if (rowAffect == (approvedIds.Count() + waitIds.Count()))
                {
                    _serviceResult.Data = rowAffect;
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 500;
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

        /// <summary>
        /// xử lí nghiệp vụ duyệt hàng loạt
        /// </summary>
        /// <param name="restPermissions"></param>
        /// <returns></returns>
        public ServiceResult ApproveBatch(List<RestPermission> restPermissions)
        {
            try
            {
                //xử lí nghiệp vụ

                // tương tác vs db
                var rowAffect = _restPermissionRepository.ApproveBatch(restPermissions);
                if (rowAffect == (restPermissions.Count()))
                {
                    _serviceResult.Data = rowAffect;
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 500;
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

        /// <summary>
        /// xử lí nghiệp vụ lọc đơn nghỉ theo bộ lọc đã định nghĩa
        /// </summary>
        /// <param name="listFilter"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public ServiceResult GetFilterPro(List<FilterItem> listFilter, int pageSize, int pageNumber)
        {
            try
            {
                // xử lí nghiệp vụ
                //Lấy dữ liệu từ db
                PagingResult<RestPermission> pagingResult = _restPermissionRepository.GetFilterPro(listFilter, pageSize,  pageNumber);
                if (pagingResult.Data.Count() > 0)
                {
                    _serviceResult.Data = pagingResult;
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
    }
}
