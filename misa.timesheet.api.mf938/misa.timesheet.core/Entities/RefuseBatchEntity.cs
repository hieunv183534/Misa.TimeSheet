using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.core.Entities
{
    public class RefuseBatchEntity
    {
        #region Field

        private List<Guid> approvedIds;
        private List<Guid> waitIds;

        #endregion

        #region Property

        // list id các bảng ghi đã duyệt
        public List<Guid> ApprovedIds
        {
            get { return this.approvedIds; }
            set { this.approvedIds = value; }
        }

        // list id các bản ghi chờ duyệt
        public List<Guid> WaitIds
        {
            get { return this.waitIds; }
            set { this.waitIds = value; }
        }

        #endregion
    }
}
