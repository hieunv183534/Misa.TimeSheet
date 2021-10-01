export default class ResourceVn {
    // fieldName tiếng việt
    static DisplayName = {
        EmployeeCode: 'Mã nhân viên',
        FullName: 'Tên',
        UnitId: 'Đơn vị công tác',
        ApplicantId: 'Người nộp đơn',
        DateOfApplication: 'Ngày nộp đơn',
        StartTime: 'Từ ngày',
        EndTime: 'Đến ngày',
        RestTypeId: 'Loại nghỉ',
        Reason: 'Lí do nghỉ',
        ApprovedById: 'Người duyệt',
        Status: 'Trang thái'
    }

    // các messenger của popup
    static PopupMessenger = {
        updateSuccess: () => {
            return 'Sửa đơn thành công';
        },
        addSuccess: () => {
            return 'Thêm đơn thành công';
        },
        dataChanged: () => {
            return 'Dữ liệu đã bị thay đổi. Bạn có muốn lưu không?';
        },
        deleteSuccess: () => {
            return "Xóa thành công";
        },
        refuseSuccess: () => {
            return "Từ chối thành công";
        },
        refuseFailed: () => {
            return "Từ chối không thành công";
        },
        approveSuccess: () => {
            return "Duyệt thành công";
        },
        approveFailed: () => {
            return "Duyệt không thành công";
        },
        confirmDeleteBatch: () => {
            return 'Bạn có chắc chắn muốn xóa những đơn này không?';
        },
        confirmRefuseBatch: () => {
            return 'Bạn có chắc chắn muốn từ chối các đơn đã chọn không?';
        },
        confirmApproveBatch: () => {
            return 'Bạn có chắc chắn muốn duyệt các đơn đã chọn không?';
        },
        confirmDelete: () => {
            return "Bạn có chắc chắn muốn xóa đơn này không?";
        }
    }


    // các messenger validate
    static ValidateMessenger = {
        requiedField: (fieldName) => {
            return `${this.DisplayName[`${fieldName}`]} không được để trống`;
        },
        invalidField: (fieldName) => {
            return `${this.DisplayName[`${fieldName}`]} không đúng định dạng`;
        },
        startTimeOutRangeEndTime: () => {
            return 'Từ ngày phải nhỏ hơn hoặc bằng đến ngày';
        },
        endTimeOutRangeStartTime: () => {
            return 'Đến ngày phải lớn hơn hoặc bằng từ ngày';
        },
        numberOfNPOutRange: () => {
            return 'Số ngày nghỉ trên đơn phải nhỏ hơn hoặc bằng số ngày phép còn lại. Vui lòng kiểm tra lại';
        }
    }
}