using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.core.Entities
{
    public class RestPermission
    {

        #region Field

        private Guid restPermissionId;
        private DateTime dateOfApplication;
        private DateTime startTime;
        private DateTime endTime;
        private string involvedIds;
        private string note;
        private int status;
        private string reason;
        private string employeeCode;
        private Guid applicantId;
        private string applicantName;
        private string positionName;
        private Guid? unitId;
        private string unitName;
        private Guid approvedById;
        private string approvedByName;
        private Guid? replaceById;
        private string replaceByName;
        private Guid restTypeId;
        private string restTypeName;

        #endregion

        #region Property

        [Requied]
        // id đơn xin nghỉ
        public Guid RestPermissionId
        {
            get { return this.restPermissionId; }
            set { this.restPermissionId = value; }
        }

        [Requied]
        // ngày nộp đơn
        public DateTime DateOfApplication
        {
            get { return this.dateOfApplication; }
            set { this.dateOfApplication = value; }
        }

        [Requied]
        // từ ngày
        public DateTime StartTime
        {
            get { return this.startTime; }
            set { this.startTime = value; }
        }

        [Requied]
        // đến ngày
        public DateTime EndTime
        {
            get { return this.endTime; }
            set { this.endTime = value; }
        }

        // danh sách id những người liên quan
        public string InvolvedIds
        {
            get { return this.involvedIds; }
            set { this.involvedIds = value; }
        }

        // chú ý
        public string Note
        {
            get { return this.note; }
            set { this.note = value; }
        }

        [Requied]
        // trạng thái
        public int Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        [Requied]
        // lí do nghỉ
        public string Reason
        {
            get { return this.reason; }
            set { this.reason = value; }
        }

        [NotMap]
        // mã nhân viên củ người nộp đơn
        public string EmployeeCode
        {
            get { return this.employeeCode; }
            set { this.employeeCode = value; }
        }

        [Requied]
        // id người nộp đơn
        public Guid ApplicantId
        {
            get { return this.applicantId; }
            set { this.applicantId = value; }
        }

        [NotMap]
        // tên người nộp đơn
        public string ApplicantName
        {
            get { return this.applicantName; }
            set { this.applicantName = value; }
        }

        [NotMap]
        // tên vị trí công việc
        public string PositionName
        {
            get { return this.positionName; }
            set { this.positionName = value; }
        }

        [NotMap]
        // id đơn vị công tác
        public Guid? UnitId
        {
            get { return this.unitId; }
            set { this.unitId = value; }
        }

        [NotMap]
        // tên đơn vị công tác
        public string UnitName
        {
            get { return this.unitName; }
            set { this.unitName = value; }
        }

        [Requied] 
        // id người duyệt
        public Guid ApprovedById
        {
            get { return this.approvedById; }
            set { this.approvedById = value; }
        }

        [NotMap]
        // tên người duyệt
        public string ApprovedByName
        {
            get { return this.approvedByName; }
            set { this.approvedByName = value; }
        }

        // id người thay thế
        public Guid? ReplaceById
        {
            get { return this.replaceById; }
            set { this.replaceById = value; }
        }

        [NotMap]
        // tên người thay thế
        public string ReplaceByName
        {
            get { return this.replaceByName; }
            set { this.replaceByName = value; }
        }

        [Requied]
        // id loại nghỉ
        public Guid RestTypeId
        {
            get { return this.restTypeId; }
            set { this.restTypeId = value; }
        }

        [NotMap]
        // tên loại nghỉ
        public string RestTypeName
        {
            get { return this.restTypeName; }
            set { this.restTypeName = value; }
        }

        #endregion

    }
}
