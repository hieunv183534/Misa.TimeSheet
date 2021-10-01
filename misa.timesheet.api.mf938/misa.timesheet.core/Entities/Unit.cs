using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.core.Entities
{
    public class Unit
    {

        #region Field

        private Guid unitId;
        private string unitName;
        private Guid? parentId;

        #endregion

        #region Property

        // id đơn vị
        public Guid UnitId
        {
            get { return this.unitId; }
            set { this.unitId = value; }
        }
        
        // tên đơn vị
        public string UnitName
        {
            get { return this.unitName; }
            set { this.unitName = value; }
        }

        // id của đơn vị cha
        public Guid? ParentId
        {
            get { return this.parentId; }
            set { this.parentId = value; }
        }

        #endregion

    }
}
