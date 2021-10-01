using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.core.Entities
{
    public class Employee
    {

        #region Field

        private Guid employeeId;
        private string employeeCode;
        private string fullName;
        private Guid? unitId;
        private string unitName;
        private Guid? positionId;
        private string positionName;
        private double? totalNP;
        private double? usedNP;
        private double? remainingNP;
        private int? salaryRate;

        #endregion

        #region Property
        // id nhân viên  
        public Guid EmployeeId
        {
            get { return this.employeeId; }
            set { this.employeeId = value; }
        }

        // mã nhân viên
        public string EmployeeCode
        {
            get { return this.employeeCode; }
            set { this.employeeCode = value; }
        }

        // họ và tên
        public string FullName
        {
            get { return this.fullName; }
            set { this.fullName = value; }
        }

        // id đơn vị công tác
        public Guid? UnitId
        {
            get { return this.unitId; }
            set { this.unitId = value; }
        }

        // tên đơn vị công tác
        public string UnitName
        {
            get { return this.unitName; }
            set { this.unitName = value; }
        }

        // id vị trí công việc
        public Guid? PositionId
        {
            get { return this.positionId; }
            set { this.positionId = value; }
        }

        // tên vị trí công việc
        public string PositionName
        {
            get { return this.positionName; }
            set { this.positionName = value; }
        }

        // tổng số nghỉ phép
        public double? TotalNP
        {
            get { return this.totalNP; }
            set { this.totalNP = value; }
        }

        // số nghỉ phép đã dùng
        public double? UsedNP
        {
            get { return this.usedNP; }
            set { this.usedNP = value; }
        }

        // số nghỉ phép còn lại
        public double? RemainingNP
        {
            get { return this.remainingNP; }
            set { this.remainingNP = value; }
        }

        // tỷ lệ hưởng lương
        public int? SalaryRate
        {
            get { return this.salaryRate; }
            set { this.salaryRate = value; }
        }

        #endregion

    }
}
