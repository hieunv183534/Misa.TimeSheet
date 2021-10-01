<template>
    <div class="dialog">
        <div class="dialog-modal"></div>
        <div class="dialog-content">
            <div class="dialog-header">
                <div class="dialog-title">
                    <div v-if="mode=='add'" class="title-mode">Thêm đơn</div>
                    <div v-if="mode=='edit'" class="title-mode">Sửa đơn</div>
                    <div class="title-model">- Nghỉ</div>
                </div>
                <div class="btn-x" @click="btnXOnClick">
                    <div class="background-icon position-icon-x-big"></div>
                </div>
            </div>
            <div class="dialog-body">
                <div class="dialog-col">
                    <div class="dialog-cell">
                        <div class="title-info">Người nộp đơn <font color="red">*</font></div>
                        <Combobox v-if="mode=='add'" comboboxType="user"
                                  :isFocus="mode=='add'" comboboxId="1"
                                  v-model="restFormModel.ApplicantId"
                                  :theValue="restFormModel.ApplicantId"
                                  :isBorderRed="validateStrApplicantId!==''"
                                  @cbbOnSelect="loadEmployeeModel" />
                        <div v-if="mode=='edit'" class="item-read-only">
                            {{restFormModel.ApplicantName}}
                            <div class="combobox-option background-icon position-icon-chevron-down"></div>
                        </div>
                        <NoteTitle :titleValue="validateStrApplicantId" right="29" />
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Đơn vị công tác</div>
                        <div class="item-read-only">
                            {{employeeModel.UnitName}}
                        </div>
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Ngày nộp đơn <font color="red">*</font></div>
                        <DateBox :isBorderRed="validateStrDateOfApplication!==''" v-model="restFormModel.DateOfApplication"
                                 :dateValue="restFormModel.DateOfApplication" :autoFocus="mode=='edit'" />
                        <NoteTitle :titleValue="validateStrDateOfApplication" right="34" />
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Từ ngày <font color="red">*</font></div>
                        <DateBox :isBorderRed="validateStrStartTime!==''" v-model="restFormModel.StartTime" :dateValue="restFormModel.StartTime" />
                        <NoteTitle :titleValue="validateStrStartTime" right="34" />
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Đến ngày <font color="red">*</font></div>
                        <DateBox :isBorderRed="validateStrEndTime!==''" v-model="restFormModel.EndTime" :dateValue="restFormModel.EndTime" />
                        <NoteTitle :titleValue="validateStrEndTime" right="34" />
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Số ngày nghỉ</div>
                        <InputNumber :value="diffTime" />
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Loại nghỉ <font color="red">*</font></div>
                        <Combobox :listItem="listRestType"
                                  comboboxType="RestType"
                                  comboboxId="2"
                                  :isBorderRed="validateStrRestTypeId!==''"
                                  v-model="restFormModel.RestTypeId"
                                  :theValue="restFormModel.RestTypeId" />
                        <NoteTitle :titleValue="validateStrRestTypeId" right="29" />
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Tỷ lệ hưởng lương (%)</div>
                        <div class="item-read-only">{{employeeModel.SalaryRate}}</div>
                    </div>
                </div>
                <div class="dialog-col">
                    <div class="dialog-cell">
                        <div class="title-info">Số NP được sử dụng</div>
                        <div class="item-read-only">{{employeeModel.TotalNP}}</div>
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Số NP đã nghỉ</div>
                        <div class="item-read-only">
                            <span v-if="!employeeModel.UsedNP">{{employeeModel.UsedNP}}</span>
                            <span v-else>{{Math.round(employeeModel.UsedNP * 100) / 100}}</span>
                        </div>
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Số NP còn lại</div>
                        <div class="item-read-only">
                            <span v-if="!employeeModel.RemainingNP">{{employeeModel.RemainingNP}}</span>
                            <span v-else>{{Math.round(employeeModel.RemainingNP * 100) / 100}}</span>
                        </div>
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Lý do nghỉ <font color="red">*</font></div>
                        <TextArea :isBorderRed="validateStrReason!==''" v-model="restFormModel.Reason" />
                        <NoteTitle :titleValue="validateStrReason" right="10" />
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Người duyệt <font color="red">*</font></div>
                        <Combobox comboboxType="user" comboboxId="6" :isBorderRed="validateStrApprovedById!==''" v-model="restFormModel.ApprovedById"
                                  :theValue="restFormModel.ApprovedById" />
                        <NoteTitle :titleValue="validateStrApprovedById" right="29" />
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Người thay thế</div>
                        <Combobox comboboxType="user" comboboxId="5" v-model="restFormModel.ReplaceById" :theValue="restFormModel.ReplaceById" />
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Người liên quan</div>
                        <ComboboxBatch comboboxType="user" comboboxId="3" v-model="restFormModel.InvolvedIds" :theValue="restFormModel.InvolvedIds" />
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Ghi chú</div>
                        <TextArea v-model="restFormModel.Note" />
                    </div>
                    <div class="dialog-cell">
                        <div class="title-info">Trạng thái <font color="red">*</font></div>
                        <Combobox :listItem="listStatus" comboboxType="Status" comboboxId="4" v-model="restFormModel.Status" :theValue="statusValueStr" />
                    </div>
                </div>
            </div>
            <div class="dialog-footer">
                <Button buttonText="Hủy" buttonType="secondary" @btnClick="btnCancelOnClick" />
                <Button buttonText="Lưu" buttonType="primary" @btnClick="btnSaveOnClick" />
            </div>
        </div>
    </div>
</template>


<script>
    import Button from "../components/base/Button.vue"
    import Combobox from "../components/base/Combobox.vue"
    import ComboboxBatch from "../components/base/ComboboxBatch.vue"
    import DateBox from "../components/base/DateBox.vue"
    import InputNumber from "../components/base/InputNumber.vue"
    import TextArea from "../components/base/TextArea.vue"
    import NoteTitle from "../components/base/NoteTitle.vue"
    import { restFormModel } from "../model/restFormModel"
    import { EventBus } from "../main.js"
    import { listStatus, dataListRestType } from "../model/listDatas"
    import ResourceVn from "../resources/ResourceVN.js"
    import EmployeeApi from "../api/entities/EmployeeApi"
    import RestPermissionApi from "../api/entities/RestPermissionApi"
    import Common from "../util/Common.js"

    export default {
        name: 'RestDialog',
        components: {
            Button, Combobox, ComboboxBatch, DateBox, InputNumber, TextArea, NoteTitle
        },
        props: {
            isReOpen: {
                type: Boolean,
                default: false
            },
            mode: {
                type: String,
                default: ''
            },
            myId: {
                type: String,
                default: ''
            }
        },
        data() {
            return {
                requiedList: ['ApplicantId', 'DateOfApplication', 'StartTime', 'EndTime', 'RestTypeId', 'Reason', 'ApprovedById', 'Status'],
                employeeModel: {},
                restFormModel: {},
                originalModel: {},
                listStatus: [],
                listRestType: [],
                valueDate: '',
                // các biến validate--------------------------------------------
                validateStrApplicantId: '',
                validateStrReason: '',
                validateStrDateOfApplication: '',
                validateStrStartTime: '',
                validateStrEndTime: '',
                validateStrRestTypeId: '',
                validateStrApprovedById: ''
            }
        },
        computed: {
            statusValueStr() {
                return '' + this.restFormModel.Status;
            },
            // tính số ngày nghỉ từ : từ ngày và đến ngày
            diffTime() {
                if (this.restFormModel.StartTime !== null && this.restFormModel.EndTime !== null) {
                    this.validateTime();
                    return Common.getNumberDayOfNP(this.restFormModel.StartTime, this.restFormModel.EndTime);
                } else {
                    return '0';
                }
            }
        },
        methods: {
            /**
             * Sự kiện khi click btn x trên dialog
             * Author HieuNV
             * */
            btnXOnClick() {
                if (JSON.stringify(this.originalModel) === JSON.stringify(this.restFormModel)) {
                    this.$emit('hideDialog');
                } else {
                    EventBus.$emit('showPopup', ResourceVn.PopupMessenger.dataChanged() , 'Thông báo', '123', 'save', '');
                }
            },
            /**
            * Sự kiện khi click btn hủy trên dialog
            * Author HieuNV
            * */
            btnCancelOnClick() {
                this.$emit('hideDialog');
            },
            /**
            * Sự kiện khi click btn lưu trên dialog
            * validate
            * Author HieuNV
            * */
            btnSaveOnClick() {
                this.validateStrApplicantId = '';
                this.validateStrReason = '';
                this.validateStrDateOfApplication = '';
                this.validateStrStartTime = '';
                this.validateStrEndTime = '';
                this.validateStrRestTypeId = '';
                this.validateStrApprovedById = '';
                var listNull = [];
                for (let i = 0; i < this.requiedList.length; i++) {
                    var fieldName = this.requiedList[i];
                    if (this.restFormModel[fieldName] === "" || this.restFormModel[fieldName] == undefined) {
                        listNull.push(fieldName);
                        this[`validateStr${fieldName}`] = ResourceVn.ValidateMessenger.requiedField(fieldName);
                    }
                }
                if (listNull.length == 0) {
                    if ((this.diffTime > this.employeeModel.RemainingNP) && !(this.restFormModel.Status !== 1 && this.originalModel.Status == 1)) {
                        EventBus.$emit('showPopup', ResourceVn.ValidateMessenger.numberOfNPOutRange() , 'Cảnh báo', '5', 'messenger', '');
                    } else {
                        if (this.mode == 'add') {
                            this.add();
                        } else if (this.mode == 'edit') {
                            this.update();
                        }
                    }
                }
            },
            /**
             * xử lí khi confirm thêm mới một bản ghi
             * Author HieuNV
             * */
            add() {
                RestPermissionApi.add(this.restFormModel).then(res => {
                    console.log(res);
                    this.$emit('hideDialog');
                    EventBus.$emit('showMes', ResourceVn.PopupMessenger.addSuccess() , 'success');
                    EventBus.$emit('loadTableData');
                }).catch(error => {
                    alert(error.response);
                });
                if (this.restFormModel.Status == 1) {
                    EmployeeApi.update(this.restFormModel.ApplicantId, this.diffTime).then(res => {
                        console.log(res);
                    }).catch(error => {
                        EventBus.$emit('showPopup', error.response.data, 'Cảnh báo', '5', 'messenger', '');
                    })
                }
            },
            /**
             * xử lí khi confirm chỉnh sửa một bản ghi
             * Author HieuNV
             * */
            update() {
                RestPermissionApi.update(this.myId, this.restFormModel).then(res => {
                    console.log(res);
                    this.$emit('hideDialog');
                    EventBus.$emit('showMes', ResourceVn.PopupMessenger.updateSuccess() , 'success');
                    EventBus.$emit('loadTableData');
                }).catch(error => {
                    alert(error.response);
                });
                if (this.restFormModel.Status == 1 && (this.originalModel.Status !== 1)) {
                    EmployeeApi.update(this.restFormModel.ApplicantId, this.diffTime).then(res => {
                        console.log(res);
                    }).catch(error => {
                        EventBus.$emit('showPopup', error.response.data, 'Cảnh báo', '5', 'messenger', '');
                    })
                }
                if (this.restFormModel.Status !== 1 && this.originalModel.Status == 1) {
                    EmployeeApi.update(this.restFormModel.ApplicantId, -this.diffTime).then(res => {
                        console.log(res);
                    }).catch(error => {
                        EventBus.$emit('showPopup', error.response.data, 'Cảnh báo', '5', 'messenger', '');
                    })
                }
            },
            /**
             * lấy các thông tinn thuộc về người nộp đơn
             * Author HieuNV
             * */
            loadEmployeeModel() {
                console.log(32428478294324);
                if (this.restFormModel.ApplicantId !== '' && this.employeeModel.ApplicantId !== null) {
                    EmployeeApi.getById(this.restFormModel.ApplicantId).then(res => {
                        this.employeeModel = res.data;
                    })
                }
            },
            /**
             * kiểm tra điều kiện hộ lệ của từ ngày và đến ngày
             * Author HieuNV
             * */
            validateTime() {
                var numday = Common.getNumberDayOfNP(this.restFormModel.StartTime, this.restFormModel.EndTime);
                if (numday <= 0) {
                    this.validateStrStartTime = ResourceVn.ValidateMessenger.startTimeOutRangeEndTime();
                    this.validateStrEndTime = ResourceVn.ValidateMessenger.endTimeOutRangeStartTime();
                } else {
                    this.validateStrStartTime = '';
                    this.validateStrEndTime = '';
                }
            }
        },
        created() {
            this.listStatus = listStatus;
            this.listRestType = dataListRestType.listRestType;

            this.restFormModel = Object.assign({}, restFormModel);
            this.restFormModel.DateOfApplication = new Date();
            this.restFormModel.StartTime = new Date();
            this.restFormModel.EndTime = new Date();

            EventBus.$on('confirmToSave', () => {
                this.btnSaveOnClick();
            })
        },
        watch: {
            // khi mở form thì gọi đến nhờ sự thay đổi của biên isReOpen để khởi tạo theo mode đi kèm
            isReOpen() {
                if (this.mode == 'add') {
                    this.originalModel = Object.assign({}, this.restFormModel);
                    console.log(this.originalModel);
                } else if (this.mode == 'edit') {
                    RestPermissionApi.getById(this.myId).then(res => {
                        this.restFormModel = res.data;
                        this.originalModel = Object.assign({}, this.restFormModel);
                        EmployeeApi.getById(this.restFormModel.ApplicantId).then(res => {
                            this.employeeModel = res.data;
                        })
                    })
                }
            },


            valueDate() {
                console.log(this.valueDate);
            },
            'restFormModel.ApplicantId': function (newData) {
                if (newData !== '') {
                    this.validateStrApplicantId = '';
                }
            },
            'restFormModel.DateOfApplication': function (newData) {
                if (newData !== '') {
                    this.validateStrDateOfApplication = '';
                }
            },
            'restFormModel.StartTime': function (newData) {
                if (newData !== '') {
                    this.validateStrStartTime = '';
                }
            },
            'restFormModel.EndTime': function (newData) {
                if (newData !== '') {
                    this.validateStrEndTime = '';
                }
            },
            'restFormModel.RestTypeId': function (newData) {
                if (newData !== '') {
                    this.validateStrRestTypeId = '';
                }
            },
            'restFormModel.Reason': function (newData) {
                if (newData !== '') {
                    this.validateStrReason = '';
                }
            },
            'restFormModel.ApprovedById': function (newData) {
                if (newData !== '') {
                    this.validateStrApprovedById = '';
                }
            },
        }
    }
</script>


<style scoped>
    .dialog .dialog-modal {
        position: fixed;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background-color: #000;
        opacity: 0.4;
        z-index: 220;
    }

    .dialog .dialog-content {
        z-index: 221;
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        width: 930px;
        height: 806px;
        background-color: #fff;
        border-radius: 4px;
        overflow: hidden;
    }

    .dialog-header {
        width: 100%;
        height: 53px;
        float: left;
        padding: 24px 24px 0px 24px;
        box-sizing: border-box;
        position: relative;
    }

    .dialog-title {
        width: auto;
        height: 29px;
        display: flex;
        justify-content: flex-start;
    }

    .title-mode {
        line-height: 29px;
        font-weight: 700;
        font-size: 20px;
        letter-spacing: 0.576px;
    }

    .title-model {
        letter-spacing: 0.384px;
        margin-top: -4px;
        line-height: 39px;
        font-weight: 700;
        font-size: 16px;
        margin-left: 5px;
    }

    .btn-x {
        width: 36px;
        height: 36px;
        border-radius: 50%;
        position: absolute;
        top: 21px;
        right: 24px;
        display: flex;
        align-items: center;
        justify-content: center;
        cursor: pointer;
    }

        .btn-x:hover {
            background-color: #EFF1F6;
        }

    .dialog-body {
        width: 100%;
        height: calc(100% - 121px);
        float: left;
        display: flex;
        align-items: flex-start;
        justify-content: space-between;
        overflow-y: auto;
        padding: 24px;
        box-sizing: border-box;
    }

    .dialog-col {
        width: 415px;
        height: auto
    }

    .dialog-cell {
        width: 100%;
        height: auto;
        float: left;
        display: flex;
        align-items: flex-start;
        justify-content: space-between;
        margin-bottom: 16px;
        position: relative;
    }

    .title-info {
        letter-spacing: 0.14px;
        padding-right: 8px;
        line-height: 36px;
    }

    .item-read-only {
        width: 240px;
        height: 36px;
        border-radius: 4px;
        border: 1px solid #e0e0e0;
        background-color: #ececec;
        box-sizing: border-box;
        padding: 0px 8px;
        line-height: 36px;
        position: relative;
    }

    .combobox-option {
        position: absolute;
        top: 15px;
        right: 10px;
        z-index: 43;
    }

    .dialog-footer {
        width: 100%;
        height: 68px;
        float: left;
        border-top: 1px solid #DADCE3;
        box-sizing: border-box;
        padding: 0px 24px;
        display: flex;
        align-items: center;
        justify-content: flex-end;
    }

        .dialog-footer button:last-child {
            margin-left: 8px;
        }
</style>