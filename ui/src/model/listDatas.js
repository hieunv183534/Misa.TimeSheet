import RestTypeApi from "../api/entities/RestTypeApi";

export const listStatus = [
    {
        StatusId: 0,
        StatusName: 'Chờ duyệt'
    },
    {
        StatusId: 1,
        StatusName: 'Đã duyệt'
    },
    {
        StatusId: 2,
        StatusName: 'Từ chối'
    }
];


export class dataListRestType {

    constructor() {
        this.listRestType = [];
    }

    static loadDataList() {
        RestTypeApi.getAll().then(res => {
            this.listRestType = res.data;
        })
    }
}

export const headers = [
    {
        DataField: "EmployeeCode",
        Caption: "Mã nhân viên",
        DataType: "text",
        Alignment: "left",
        Width: 220,
        Fixed: true,
        Enable: true
    },
    {
        DataField: "ApplicantName",
        Caption: "Người nộp đơn",
        DataType: "text",
        Alignment: "left",
        Width: 200,
        Fixed: true,
        Enable: true
    },
    {
        DataField: "PositionName",
        Caption: "Vị trí công việc",
        DataType: "text",
        Alignment: "left",
        Width: 280,
        Fixed: false,
        Enable: true
    },
    {
        DataField: "UnitName",
        Caption: "Đơn vị công tác",
        DataType: "text",
        Alignment: "left",
        Width: 250,
        Fixed: false,
        Enable: true
    },
    {
        DataField: "RestTypeName",
        Caption: "Loại nghỉ",
        DataType: "text",
        Alignment: "left",
        Width: 200,
        Fixed: false,
        Enable: true
    },
    {
        DataField: "NumDayOfRest",
        Caption: "Số ngày nghỉ",
        DataType: "number",
        Alignment: "right",
        Width: 140,
        Fixed: false,
        Enable: true
    },
    {
        DataField: "NumHoursOfRest",
        Caption: "Số giờ nghỉ",
        DataType: "number",
        Alignment: "right",
        Width: 140,
        Fixed: false,
        Enable: true
    },
    {
        DataField: "ApprovedByName",
        Caption: "Người duyệt",
        DataType: "text",
        Alignment: "left",
        Width: 200,
        Fixed: false,
        Enable: true
    },
    {
        DataField: "DateOfApplication",
        Caption: "Ngày nộp đơn",
        DataType: "datetime",
        Alignment: "left",
        Width: 220,
        Fixed: false,
        Enable: true
    },
    {
        DataField: "StartTime",
        Caption: "Từ ngày",
        DataType: "datetime",
        Alignment: "left",
        Width: 220,
        Fixed: false,
        Enable: true
    },
    {
        DataField: "EndTime",
        Caption: "Đến ngày",
        DataType: "datetime",
        Alignment: "left",
        Width: 220,
        Fixed: false,
        Enable: true
    },
    {
        DataField: "Reason",
        Caption: "Lí do",
        DataType: "text",
        Alignment: "left",
        Width: 320,
        Fixed: false,
        Enable: true
    },
    {
        DataField: "Status",
        Caption: "Trạng thái",
        DataType: "text",
        Alignment: "left",
        Width: 220,
        Fixed: false,
        Enable: true
    },
];

export const listFilterText = [
    {
        FilterTextId: '1',
        FilterTextName: 'Chứa'
    },
    {
        FilterTextId: '2',
        FilterTextName: 'Không chứa'
    },
    {
        FilterTextId: '3',
        FilterTextName: 'Bằng'
    },
    {
        FilterTextId: '4',
        FilterTextName: 'Bắt đầu bằng'
    },
    {
        FilterTextId: '5',
        FilterTextName: 'Kết thúc bằng'
    },
    {
        FilterTextId: '6',
        FilterTextName: 'Trống'
    },
    {
        FilterTextId: '7',
        FilterTextName: 'Không trống'
    }
];

export const listFilterDate = [
    {
        FilterDateId: '1',
        FilterDateName: 'Bằng'
    },
    {
        FilterDateId: '2',
        FilterDateName: 'Khác'
    },
    {
        FilterDateId: '3',
        FilterDateName: 'Nhỏ hơn'
    },
    {
        FilterDateId: '4',
        FilterDateName: 'Nhỏ hơn hoặc bằng'
    },
    {
        FilterDateId: '5',
        FilterDateName: 'Lớn hơn'
    },
    {
        FilterDateId: '6',
        FilterDateName: 'Lớn hơn hoặc bằng'
    },
    {
        FilterDateId: '7',
        FilterDateName: 'Trống'
    },
    {
        FilterDateId: '8',
        FilterDateName: 'Không trống'
    },
    {
        FilterDateId: '9',
        FilterDateName: 'Hôm nay'
    },
    {
        FilterDateId: '10',
        FilterDateName: 'Tuần này'
    },
    {
        FilterDateId: '11',
        FilterDateName: 'Tháng này'
    },
    {
        FilterDateId: '12',
        FilterDateName: 'Năm nay'
    },
    {
        FilterDateId: '13',
        FilterDateName: 'Hôm qua'
    },
    {
        FilterDateId: '14',
        FilterDateName: 'Tuần trước'
    },
    {
        FilterDateId: '15',
        FilterDateName: 'Tháng trước'
    },
    {
        FilterDateId: '16',
        FilterDateName: 'Năm trước'
    },
    {
        FilterDateId: '17',
        FilterDateName: 'Ngày mai'
    },
    {
        FilterDateId: '18',
        FilterDateName: 'Tuần sau'
    },
    {
        FilterDateId: '19',
        FilterDateName: 'Tháng sau'
    },
    {
        FilterDateId: '20',
        FilterDateName: 'Năm sau'
    },
    {
        FilterDateId: '21',
        FilterDateName: 'Trong khoảng'
    }
];


export const filterList = [
    {
        DataField: 'EmployeeCode',
        Caption: 'Mã nhân viên',
        FilterOption: '1',
        FilterType: 'FilterText',
        FilterList: listFilterText,
        FilterValue: '',
        Enable: false
    },
    {
        DataField: 'ApplicantName',
        Caption: 'Người nộp đơn',
        FilterOption: '1',
        FilterType: 'FilterText',
        FilterList: listFilterText,
        FilterValue: '',
        Enable: false
    },
    {
        DataField: 'PositionName',
        Caption: 'Vị trí công việc',
        FilterOption: '1',
        FilterType: 'FilterText',
        FilterList: listFilterText,
        FilterValue: '',
        Enable: false
    },
    {
        DataField: 'RestTypeName',
        Caption: 'Loại nghỉ',
        FilterOption: '1',
        FilterType: 'FilterText',
        FilterList: listFilterText,
        FilterValue: '',
        Enable: false
    },
    {
        DataField: 'ApprovedByName',
        Caption: 'Người duyệt',
        FilterOption: '1',
        FilterType: 'FilterText',
        FilterList: listFilterText,
        FilterValue: '',
        Enable: false
    },
    {
        DataField: 'Reason',
        Caption: 'Lí do',
        FilterOption: '1',
        FilterType: 'FilterText',
        FilterList: listFilterText,
        FilterValue: '',
        Enable: false
    },
    {
        DataField: 'DateOfApplication',
        Caption: 'Ngày lập',
        FilterOption: '1',
        FilterType: 'FilterDate',
        FilterList: listFilterDate,
        FilterValue: '',
        Enable: false
    },
    {
        DataField: 'StartTime',
        Caption: 'Từ ngày',
        FilterOption: '1',
        FilterType: 'FilterDate',
        FilterList: listFilterDate,
        FilterValue: '',
        Enable: false
    },
    {
        DataField: 'EndTime',
        Caption: 'Đến ngày',
        FilterOption: '1',
        FilterType: 'FilterDate',
        FilterList: listFilterDate,
        FilterValue: '',
        Enable: false
    }
]