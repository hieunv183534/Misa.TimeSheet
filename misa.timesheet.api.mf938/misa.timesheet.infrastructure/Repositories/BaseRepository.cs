using Dapper;
using misa.timesheet.core.Entities;
using misa.timesheet.core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.infrastructure.Repositories
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {

        #region Declare

        string _tableName;

        #endregion

        #region Consrtuctor

        public BaseRepository()
        {
            _tableName = typeof(MISAEntity).Name;
        }

        #endregion

        /// <summary>
        /// insert 1 bản ghi vào db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author HieuNv
        public int Add(MISAEntity entity)
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var props = entity.GetType().GetProperties();
                DynamicParameters parameters = new DynamicParameters();
                foreach (var prop in props)
                {
                    if (!prop.IsDefined(typeof(NotMap), false))
                    {
                        var propName = prop.Name;
                        var propValue = prop.GetValue(entity);
                        parameters.Add($"@{propName}", propValue);
                    }
                }
                var procName = $"Proc_Insert{_tableName}";
                var rowAffect = dbConnection.Execute(procName, param: parameters, commandType: CommandType.StoredProcedure);
                return rowAffect;
            }
        }

        /// <summary>
        /// Xóa một bả ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        public int Delete(Guid entityId)
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var procName = $"Proc_Delete{_tableName}ById";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{_tableName}Id", entityId);
                var rowAffect = dbConnection.Execute(procName, param: parameters, commandType: CommandType.StoredProcedure);
                return rowAffect;
            }
        }

        /// <summary>
        /// Xóa tất cả các bản ghi có id trong mảng
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        public int DeleteBatch(List<Guid> entityIds)
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var rowAffects = 0;
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    var procName = $"Proc_Delete{_tableName}ById";
                    foreach (var entityId in entityIds)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add($"@{_tableName}Id", entityId);
                        rowAffects +=
                            dbConnection.Execute(procName, param: parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                    }
                    transaction.Commit();
                }
                if (rowAffects < entityIds.Count())
                    rowAffects = 0;
                return rowAffects;
            }
        }

        /// <summary>
        /// select toàn bộ ds
        /// </summary>
        /// <returns></returns>
        /// Author HieuNv
        public List<MISAEntity> GetAll()
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var procName = $"Proc_Get{_tableName}";
                var entities = dbConnection.Query<MISAEntity>(procName, commandType: CommandType.StoredProcedure);
                return (List<MISAEntity>)entities;
            }
        }

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        public MISAEntity GetById(Guid entityId)
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var procName = $"Proc_Get{_tableName}ById";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{_tableName}Id", entityId);
                var entity = dbConnection.QueryFirstOrDefault<MISAEntity>(procName, param: parameters, commandType: CommandType.StoredProcedure);
                return entity;
            }
        }

        public List<MISAEntity> GetByIds(string entityIds)
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var procName = $"Proc_Get{_tableName}ByIds";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{_tableName}Ids", entityIds);
                var entities = dbConnection.Query<MISAEntity>(procName, param: parameters, commandType: CommandType.StoredProcedure);
                return (List<MISAEntity>)entities;
            }
        }

        /// <summary>
        /// Lấy ra bản ghi theo một thuộc tính nào đó
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        /// Author HieuNv
        public MISAEntity GetByProp(string propName, object propValue)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{propName}", propValue.ToString());
            var sql = $"select * from {_tableName} where {propName}= @{propName} ";
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var entity = dbConnection.QueryFirstOrDefault<MISAEntity>(sql, param: parameters);
                return entity;
            }
        }

        /// <summary>
        /// cập nhật bản ghi có id
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        public int Update(MISAEntity entity, Guid entityId)
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var props = entity.GetType().GetProperties();
                DynamicParameters parameters = new DynamicParameters();
                foreach (var prop in props)
                {
                    if (!prop.IsDefined(typeof(NotMap), false))
                    {
                        var propName = prop.Name;
                        var propValue = prop.GetValue(entity);
                        if (propName == $"{_tableName}Id")
                        {
                            propValue = entityId;
                        }
                        parameters.Add($"@{propName}", propValue);
                    }
                }
                var procName = $"Proc_Update{_tableName}";
                var rowAffect = dbConnection.Execute(procName, param: parameters, commandType: CommandType.StoredProcedure);
                return rowAffect;
            }
        }
    }
}
