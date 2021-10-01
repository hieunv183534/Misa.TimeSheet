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
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {

        #region Declare

        protected IBaseRepository<MISAEntity> _baseRepository;
        public ServiceResult _serviceResult;

        #endregion

        #region Constructor

        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
        }

        #endregion

        /// <summary>
        /// Xử lí nghiệp vụ thêm
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author HieuNv
        public virtual ServiceResult Add(MISAEntity entity)
        {
            try
            {
                // xử lí nghiệp vụ thêm
                var validatemsg = Validate(entity, "add");

                if (validatemsg.devMsg != "-1")
                {
                    _serviceResult.Data = validatemsg;
                    _serviceResult.StatusCode = 400;
                    return _serviceResult;
                }

                // thêm dữ liệu vào db
                var rowAffect = _baseRepository.Add(entity);
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
        /// Xử lsi nghiệp vụ xóa
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        public ServiceResult Delete(Guid entityId)
        {
            try
            {
                // xử lí nghiệp vụ xóa
                // xóa dữ liệu khỏi db
                var rowAffect = _baseRepository.Delete(entityId);
                if (rowAffect > 0)
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
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        public ServiceResult DeleteBatch(List<Guid> entityIds)
        {
            try
            {
                // xử lí nghiệp vụ xóa
                // xóa dữ liệu khỏi db
                var rowAffect = _baseRepository.DeleteBatch(entityIds);
                if (rowAffect > 0)
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
        /// Xử lsi nghiệp vụ lấy dữ liệu
        /// </summary>
        /// <returns></returns>
        /// Author HieuNv
        public ServiceResult GetAll()
        {
            try
            {
                // xử lí nghiệp vụ lấy dữ liệu
                // lấy tất cả dữ liệu từ db
                var entities = _baseRepository.GetAll();
                if (entities.Count() > 0)
                {
                    _serviceResult.Data = entities;
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
        /// Xử lí nghiệp vụ lấy bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        public ServiceResult GetById(Guid entityId)
        {
            try
            {
                // xử lí nghiệp vụ lấy 1 dữ liệu
                // lấy bản ghi theo id
                var entity = _baseRepository.GetById(entityId);
                if (entity != null)
                {
                    _serviceResult.Data = entity;
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
        /// Xử lí nghiệp vụ lấy ds bản ghi theo ds id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        public ServiceResult GetByIds(string entityIds)
        {
            try
            {
                // xử lí nghiệp vụ lấy dữ liệu
                // lấy tất cả dữ liệu từ db
                var entities = _baseRepository.GetByIds(entityIds);
                if (entities.Count() > 0)
                {
                    _serviceResult.Data = entities;
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
        /// Xử lsi nghiệp vụ cập nhật bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        public virtual ServiceResult Update(MISAEntity entity, Guid entityId)
        {
            try
            {
                // xử lí nghiệp vụ thêm
                var validateMsg = Validate(entity, "update");

                if (validateMsg.devMsg != "-1")
                {
                    _serviceResult.Data = validateMsg;
                    _serviceResult.StatusCode = 400;
                    return _serviceResult;
                }

                // thêm dữ liệu vào db
                var rowAffect = _baseRepository.Update(entity, entityId);
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
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="mode"></param>
        /// <returns> messenger lỗi tương ứng </returns>
        /// Author HieuNv
        private MessengerResult Validate(MISAEntity entity, string mode)
        {
            var props = entity.GetType().GetProperties();

            foreach (var prop in props)
            {
                // kiểm tra trường bắt buộc nhập reqiued !!!! 1
                if (prop.IsDefined(typeof(Requied), false))
                {
                    var propValue = prop.GetValue(entity);
                    if (propValue == null||(propValue.ToString() == String.Empty))
                    {
                        return new MessengerResult(
                                MyResources.ResourcesController.GetMessengerErrorUser(prop.Name, "Requied", " "),
                                MyResources.ResourcesController.GetMessengerErrorDev(prop.Name, "Requied", " "));
                    }
                }
            }
            return new MessengerResult("-1", "-1");
        }
    }
}
