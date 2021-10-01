<template>
    <div class="content">
        <div class="content-header">
            <div v-if="checkedIds.length > 0" class="tool-batch">
                <div class="choosed">Đã chọn {{checkedIds.length}}</div>
                <button class="unchoose" @click="unSelectAll">Bỏ chọn</button>
                <ButtonIcon v-if="(checkedApprovedIds.length+checkedWaitIds.length)>0" buttonText="Từ chối"
                            buttonType="danger"
                            iconBtnClass="position-icon-danger" 
                            @btnClick="refuseBatch"/>
                <ButtonIcon v-if="(checkedRefuseIds.length+checkedWaitIds.length)>0" buttonText="Duyệt"
                            buttonType="success"
                            iconBtnClass="position-icon-success" 
                            @btnClick="approveBatch"/>
                <ButtonIcon buttonText="Xóa"
                            buttonType="danger"
                            iconBtnClass="position-icon-delete"
                            @btnClick="deleteBatch" />
            </div>
            <div v-if="checkedIds.length == 0" class="content-header-left">
                <div class="page-title">Đơn xin nghỉ</div>
                <p>Trạng thái:</p>
                <div class="dropdown-paging-size">
                    <button class="dropdown-main"
                            @click="showDropdownList"
                            @blur="hideDropdownList">
                        <p>{{ formatStatus(status) }}</p>
                        <div class="dropdown-icon background-icon position-icon-chevron-down"></div>
                    </button>
                    <div v-if="isShowDropdownList" class="dropdown-list">
                        <div v-for="item in statusList"
                             :key="item"
                             :class="[
                'dropdown-item',
                { 'dropdown-item-active': status == item },
              ]"
                             @click="changeStatus(item)">
                            <p>{{ formatStatus(item) }}</p>
                            <div v-if="status == item"
                                 class="tick-icon background-icon position-icon-tick"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div v-if="checkedIds.length == 0" class="content-header-right">
                <div class="input-search-wrapper">
                    <Input inputPlacehoder="Tìm kiếm"
                           inputClass="input-search"
                           v-model="searchTerms"
                           @inputOnEnter="loadDataTablePaging" />
                    <div class="icon-search background-icon position-icon-search"></div>
                </div>
                <ComboboxTreeView style="margin-right: 10px; width: 316px"
                                  :treeDataSource="treeDataSource"
                                  :placeholderProp="'Tất cả đơn vị'"
                                  :valueExprProp="'UnitId'"
                                  :displayExprProp="'UnitName'"
                                  :parentIdExprProp="'ParentId'"
                                  :valueDefault="null"
                                  v-model="unitId"
                                  @comboboxtOnSelect="loadDataTablePaging" />
                <ButtonIcon buttonText="Thêm"
                            buttonType="primary"
                            iconBtnClass="position-icon-add"
                            @btnClick="showDialogAdd" />
                <div :class="['btn-filter',{'btn-filter-active' : isShowToolFilter}]" @click="toggleToolFilter">
                    <div class="icon-filter background-icon position-icon-filter"></div>
                </div>
            </div>
        </div>
        <div :class="['table-paging-wrapper', {'table-paging-wrapper-on-collapse' : isShowToolFilter}]">
            <div class="grid">
                <Table :dataSource="dataSource"
                       :headers="headers"
                       @editRecord="editRecord"
                       @onRowSelected="onRowSelected"
                       :deSelectAll="deSelectAll">
                    <template v-slot:ApplicantName="{ data }">
                        <div class="text-ok">
                            <CompactAvatar :userName="data.ApplicantName" size="24" />
                            {{ data.ApplicantName }}
                        </div>
                    </template>
                    <template v-slot:Status="{ data }">
                        <StatusBlock :statusValue="formatStatus(data.Status)" />
                    </template>
                </Table>
            </div>
            <PagingBar :totalRecord="totalRecord"
                       :pagingSize="pagingSize"
                       :currentPage="currentPage"
                       @changePagingSize="changePagingSize"
                       @changeCurrentPage="changeCurrentPage" />
        </div>
        <ToolFilter v-show="isShowToolFilter"  
                    @hideToolFilter="hideToolFilter" 
                    @disFilter="disFilter"
                    @applyFiter="applyFiter"/>
        <RestDialog v-if="isShowDialog"
                    @hideDialog="hideDialog"
                    :isReOpen="isReOpenDialog"
                    :mode="dialogMode"
                    :myId="restPermissionId" />
        <DataList />
        <GridSetting @changeGridHeaders="changeGridHeaders"/>
        <Title />
        <Popup v-if="isShowPopup"
               :popupText="popupText"
               :popupType="popupType"
               :popupBtns="popupBtns"
               @hidePopup="hidePopup"
               @hideDialog="hideDialog"
               @popupOnConfirm="popupOnConfirm" />
        <ToastMessenger :isShowMes="isShowMes"
                        :mesText="mesText"
                        :mesType="mesType"
                        @hideMes="hideMes" />
        <Loader :isShow="isShowLoader" />
    </div>
</template>


<script>
    import UnitApi from "../api/entities/UnitApi.js";
    import RestPermissionApi from "../api/entities/RestPermissionApi.js";
    import ButtonIcon from "../components/base/ButtonIcon.vue";
    import Input from "../components/base/Input.vue";
    import ComboboxTreeView from "../components/base/ComboboxTreeView.vue";
    import PagingBar from "../components/base/PagingBar.vue";
    import CompactAvatar from "../components/base/CompactAvatar.vue";
    import StatusBlock from "../components/base/StatusBlock.vue";
    import RestDialog from "./RestDialog.vue";
    import Popup from "../components/base/Popup.vue";
    import Loader from "../components/base/Loader.vue";
    import Table from "../components/base/Table.vue";
    import DataList from "../components/base/DataList.vue";
    import ToolFilter from "../components/base/ToolFilter.vue";
    import Title from "../components/base/Title.vue";
    import GridSetting from "../components/base/GridSetting.vue";
    import ToastMessenger from "../components/base/ToastMessenger.vue";
    import { EventBus } from "../main.js";
    import FormatData from "../util/Format.js";
    import { dataListRestType, headers } from "../model/listDatas.js";
    import ResourceVn from "../resources/ResourceVN.js"

    export default {
        name: "RestPermission",
        components: {
            ButtonIcon,
            Input,
            PagingBar,
            Table,
            CompactAvatar,
            ComboboxTreeView,
            RestDialog,
            DataList,
            Popup,
            StatusBlock,
            Title,
            ToastMessenger,
            Loader,
            GridSetting,
            ToolFilter
        },
        methods: {
            /**
             * Sự kiện thay đổi kích thước trang
             * @param newPagingSize
             * Author HieuNV
             */
            changePagingSize(newPagingSize) {
                this.pagingSize = newPagingSize;
                this.loadDataTablePaging();
            },
            /**
             * Sự kiện thay đổi chỉ số trang
             * @param newCurrentPage
             * Author HieuNV
             */
            changeCurrentPage(newCurrentPage) {
                this.currentPage = newCurrentPage;
                this.loadDataTablePaging();
            },
            /**
             * Ẩn popup
             * */
            hidePopup() {
                this.isShowPopup = false;
            },
            /**
             * Ẩn dialog
             * */
            hideDialog() {
                this.isShowDialog = false;
            },
            /**
             * Mở dialog thêm mới đơn nghỉ
             * Author HieuNV
             * */
            showDialogAdd() {
                setTimeout(() => {
                    this.isReOpenDialog = !this.isReOpenDialog;
                }, 100);
                this.isShowDialog = true;
                this.dialogMode = "add";
            },
            /**
             * Hiển thị dropdownList(Status)
             * */
            showDropdownList() {
                this.isShowDropdownList = true;
            },
            /**
             * Ẩn dropdownList
             * */
            hideDropdownList() {
                setTimeout(() => {
                    this.isShowDropdownList = false;
                }, 200);
            },
            /**
             * Ẩn toastMessenger
             * */
            hideMes() {
                this.isShowMes = false;
            },
            /**
             * Thay đổi trang thái được lọc
             * @param newStatus
             * Author HieuNV
             */
            changeStatus(newStatus) {
                this.status = newStatus;
                this.loadDataTablePaging();
            },
            /**
             * Bỏ chọn toàn bộ các hàng được chọn trên table
             * Author HieuNV
             * */
            unSelectAll() {
                this.deSelectAll = !this.deSelectAll;
                this.checkedIds = [];
                this.checkedApprovedIds = [];
                this.checkedRefuseIds = [];
                this.checkedWaitIds = [];
            },
            /**
             * Thay đổi các cột của table theo giá trị nhận từ gridSetting
             * @param newHeaders
             * Author HieuNV
             */
            changeGridHeaders(newHeaders) {
                this.headers = newHeaders;
            },
            /**
             * Ẩn hiện bộ lọc
             * Author HieuNV
             * */
            toggleToolFilter() {
                this.isShowToolFilter = !this.isShowToolFilter;
            },
            /**
             * 
             * Ẩn bộ lọc 
             * */
            hideToolFilter() {
                this.isShowToolFilter = false;
            },
            /**
             * bỏ lọc theo bộ lọc
             * Author HieuNV
             * */
            disFilter() {
                this.isShowToolFilter = false;
                this.loadDataTablePaging();
            },
            /**
             * Áp dụng thông só trong bộ lọc để lọc và hiển thị 
             * @param listFilter
             * Author HieuNV
             */
            applyFiter(listFilter) {
                EventBus.$emit('showLoader');
                var vm = this;
                console.log(listFilter);
                console.log(this.pagingSize)
                console.log(this.currentPage)
                RestPermissionApi.getFilter(listFilter, this.pagingSize, this.currentPage).then(res => {
                    vm.dataSource = res.data.Data;
                    vm.totalRecord = res.data.TotalRecord;
                    EventBus.$emit('hideLoader');
                });
            },
            /**
             * Từ searchTerms, pagingSize, currentPage => load ra query tương ứng để gọi api filter
             * Author HieuNv
             * */
            getQueryStringFilter() {
                var paramStrs = `pageSize=${this.pagingSize}&pageNumber=${this.currentPage}`;
                if (this.searchTerms !== undefined && this.searchTerms !== "") {
                    paramStrs += `&searchTerms=${this.searchTerms}`;
                }
                if (this.status !== -1) {
                    paramStrs += `&status=${this.status}`;
                }
                if (this.unitId !== undefined && this.unitId !== "") {
                    paramStrs += `&unitId=${this.unitId}`;
                }
                return paramStrs;
            },
            /**
             * Gọi api filter để thực hiện lấy dữ liệu đã được tìm kiếm và phân trang,
             * nhận res.data là list employee truyền cho Table và ToltalRecord để truyền cho pagingBar
             * Author HieuNV
             * */
            loadDataTablePaging() {
                EventBus.$emit('showLoader');
                var vm = this;
                RestPermissionApi.getFilterPaging(this.getQueryStringFilter()).then(
                    (res) => {
                        vm.dataSource = res.data.Data;
                        vm.totalRecord = res.data.TotalRecord;
                        EventBus.$emit('hideLoader');
                    }
                );
            },
            /**
             * chuyển trường trạng thái từ giá trị int thành chữ tường minh
             * @param _status
             * Author HieuNV
             */
            formatStatus(_status) {
                return FormatData.formatStatus(_status);
            },
            /**
             * sự kiện khi click vào 1 btn trên popup , dự vào mode và btn click để thực hiện công việc tương ứng
             * @param btnClicked
             * Author HieuNV
             */
            popupOnConfirm(btnClicked) {
                switch (btnClicked) {
                    case "1":
                        this.isShowPopup = false;
                        break;
                    case "2":
                        if (this.popupMode == "save") {
                            this.isShowPopup = false;
                            this.isShowDialog = false;
                        }
                        break;
                    case "3":
                        if (this.popupMode == 'save') {
                            EventBus.$emit('confirmToSave');
                            this.isShowPopup = false;
                        }
                        break;
                    case "4":
                        if (this.popupMode == "delete") {
                            RestPermissionApi.delete(this.popupData).then((res) => {
                                console.log(res);
                                this.loadDataTablePaging();
                                EventBus.$emit("showMes", ResourceVn.PopupMessenger.deleteSuccess() , "success");
                                this.isShowPopup = false;
                            });
                        } else if (this.popupMode == 'deleteBatch') {
                            RestPermissionApi.deleteBatch(this.popupData).then(res => {
                                console.log(res);
                                this.loadDataTablePaging();
                                EventBus.$emit("showMes", ResourceVn.PopupMessenger.deleteSuccess() , "success");
                                this.isShowPopup = false;
                            })
                        }
                        break;
                    case "5":
                        if (this.popupMode == "messenger") {
                            this.isShowPopup = false;
                        }
                        break;
                    case "6":
                        if (this.popupMode == "refuseBatch") {
                            RestPermissionApi.refuseBatch(this.popupData).then(res => {
                                console.log(res);
                                this.loadDataTablePaging();
                                EventBus.$emit("showMes", ResourceVn.PopupMessenger.refuseSuccess() , "success");
                                this.isShowPopup = false;
                            }).catch(error => {
                                console.log(error.response);
                                EventBus.$emit("showMes", ResourceVn.PopupMessenger.refuseFailed(), "danger");
                                this.isShowPopup = false;
                            })
                        }
                        break;
                    case "7":
                        if (this.popupMode == "approveBatch") {
                            RestPermissionApi.approveBatch(this.popupData).then(res => {
                                console.log(res);
                                this.loadDataTablePaging();
                                EventBus.$emit("showMes", ResourceVn.PopupMessenger.approveSuccess(), "success");
                                this.isShowPopup = false;
                            }).catch(error => {
                                console.log(error.response);
                                EventBus.$emit("showMes", ResourceVn.PopupMessenger.approveFailed() , "danger");
                                this.isShowPopup = false;
                            })
                        }
                        break;
                }
            },
            /**
             * Thực hiện mở form sửa đơn nghỉ
             * @param id
             * Author HieuNV
             */
            editRecord(id) {
                setTimeout(() => {
                    this.isReOpenDialog = !this.isReOpenDialog;
                }, 100);
                this.isShowDialog = true;
                this.dialogMode = "edit";
                this.restPermissionId = id;
            },
            /**
             * Sự kiện khi click vào 1 checkbox trên 1 hàng của table. lọc và lấy ra các list để xóa , duyệt, từ chối hàng loạt
             * @param selectedRowsData
             * Author HieuNV
             */
            onRowSelected(selectedRowsData) {
                this.checkedIds = [];
                this.checkedApprovedIds = [];
                this.checkedRefuseIds = [];
                this.checkedWaitIds = [];
                this.checkedToApproveIds = [];
                selectedRowsData.forEach(item => {
                    this.checkedIds.push(item.RestPermissionId);
                    if (item.Status == 0) {
                        this.checkedWaitIds.push(item.RestPermissionId);
                        this.checkedToApproveIds.push(item);
                    }
                    if (item.Status == 1) {
                        this.checkedApprovedIds.push(item.RestPermissionId);
                    }
                    if (item.Status == 2) {
                        this.checkedRefuseIds.push(item.RestPermissionId);
                        this.checkedToApproveIds.push(item);
                    }
                });
                console.log()
            },
            /**
             * Xử lí sự kiện xóa nhiều
             * Author HieuNV
             * */
            deleteBatch() {
                EventBus.$emit('showPopup', ResourceVn.PopupMessenger.confirmDeleteBatch() , 'Cảnh báo', '14', 'deleteBatch', this.checkedIds);
            },
            /**
             * Xử lí sự kiệm từ chối nhiều
             * Author HieuNv
             * */
            refuseBatch() {
                var data = {
                    ApprovedIds: this.checkedApprovedIds,
                    WaitIds: this.checkedWaitIds
                };
                console.log(data)
                EventBus.$emit('showPopup', ResourceVn.PopupMessenger.confirmRefuseBatch() , 'Từ chối', '16', 'refuseBatch', data);
            },
            /**
             * xử lí sự kiện duyệt nhiều
             * Author HieuNV
             * */
            approveBatch() {
                EventBus.$emit('showPopup', ResourceVn.PopupMessenger.confirmApproveBatch() , 'Duyệt', '17', 'approveBatch', this.checkedToApproveIds);
            }
        },
        data() {
            return {
                isShowToolFilter : false,
                isShowLoader: false,
                deSelectAll: false,
                //toast-messenger-------------------------------
                isShowMes: false,
                mesText: "",
                mesType: "",
                //----------------------------------------------
                unitId: "",
                searchTerms: "",
                //--------------------------------------------
                isShowDropdownList: false,
                //---------------------------------------------
                status: -1,
                statusList: [-1, 0, 1, 2],
                checkedIds: [],
                checkedApprovedIds: [],
                checkedRefuseIds: [],
                checkedToApproveIds:[],
                checkedWaitIds: [],
                //dialog---------------------------------------
                isShowDialog: false,
                dialogMode: "",
                isReOpenDialog: false,
                restPermissionId: "",
                //---------------------------------------------
                totalRecord: 0,
                pagingSize: 10,
                currentPage: 1,
                //---------------------------------------------
                popupType: "",
                popupText: "",
                popupBtns: "14",
                isShowPopup: false,
                popupMode: "",
                popupData: "",
                //---------------------------------------------
                dataSource: [],
                // header grid
                headers: [],
                treeDataSource: [],
            };
        },
        created() {
            dataListRestType.loadDataList();

            this.headers = headers;

            this.loadDataTablePaging();

            UnitApi.getAll().then((res) => {
                this.treeDataSource = res.data;
            });

            //EventBus.$on("pushToCheckedList", (id) => {
            //    this.chechkedRecord.push(id);
            //});
            //EventBus.$on("removeFromCheckedList", (id) => {
            //    this.chechkedRecord = this.chechkedRecord.filter((e) => e !== id);
            //});

            // xử lí khi nhận sự kiên hiển thị popup
            EventBus.$on("showPopup", (popupText, popupType, popupBtns, mode, data) => {
                this.popupText = popupText;
                this.popupType = popupType;
                this.popupBtns = popupBtns;
                this.isShowPopup = true;
                this.popupMode = mode;
                this.popupData = data;
            });

            //xử lí khi nhận sự kiện load lại trang
            EventBus.$on("loadTableData", () => {
                this.loadDataTablePaging();
            });

            // xử lí khi nhận sự kiện hiển thị toastMessenger
            EventBus.$on("showMes", (text, type) => {
                this.mesText = text;
                this.mesType = type;
                this.isShowMes = true;
            });

            // xử lí khi nhận sự kiện hiển thị loader
            EventBus.$on('showLoader', () => {
                this.isShowLoader = true;
            })

            // xử lí khi nhận sự kiện ẩn loader
            EventBus.$on('hideLoader', () => {
                setTimeout(() => {
                    this.isShowLoader = false;
                }, 1000)
            })
        },
    };
</script>


<style scoped>
    .content {
        float: left;
        width: 100%;
        height: calc(100vh - 57px);
        background-color: #eeeeee;
        padding: 0px 24px 24px 24px;
        box-sizing: border-box;
    }

    .content-header {
        width: 100%;
        height: 69px;
        float: left;
        display: flex;
        align-items: center;
        justify-content: space-between;
    }

    .tool-batch {
        height: 100%;
        width: auto;
        display: flex;
        justify-content: flex-start;
        align-items: center;
    }

        .tool-batch > * {
            margin-right: 8px;
        }

    button.unchoose {
        background-color: transparent;
        color: #ef292f;
        font-size: 14px;
        margin-left: 5px;
        margin-right: 13px;
    }

    .content-header-left {
        width: auto;
        height: 100%;
        display: flex;
        align-items: center;
        justify-content: flex-start;
    }

    .page-title {
        font-size: 20px;
        font-weight: 700;
        letter-spacing: 0.384px;
        line-height: 69px;
    }

    .content-header-left > p {
        line-height: 69px;
        color: #9e9e9e;
        margin-left: 50px;
        margin-right: 5px;
    }

    .content-header-right {
        width: auto;
        height: 100%;
        display: flex;
        justify-content: flex-end;
        align-items: center;
    }

        .content-header-right button {
            margin-left: 8px;
        }

    .input-search-wrapper {
        width: 240px;
        height: 36px;
        position: relative;
        margin-left: 8px;
        overflow: hidden;
        margin-right: 8px;
    }

        .input-search-wrapper .icon-search {
            position: absolute;
            top: 11px;
            left: 11px;
        }

        .btn-filter{
            background-color: #fff;
            width: 36px;
            height: 36px;
            border-radius: 4px;
            border: 1px solid #e0e0e0;
            display: flex;
            justify-content: center;
            align-items: center;
            margin-left: 8px;
            box-sizing: border-box;
            cursor: pointer;
        }

            .btn-filter:hover {
                background-color: #EFF1F6;
            }

    .btn-filter-active {
        background-color: #FFEDE2 !important;
        border-color: #ec5504 !important;
    }

        .btn-filter-active .icon-filter{
            background-color: #ec5504 !important;
        }


        .table-paging-wrapper {
            float: left;
            width: 100%;
            height: calc(100% - 69px);
        }

    .table-paging-wrapper-on-collapse {
        width: calc(100% - 260px) !important;
    }

    .grid {
        width: 100%;
        height: calc(100% - 61px);
        float: left;
        border: 1px solid #e0e0e0;
        border-top-width: 0px;
        border-bottom: 0;
        border-top-left-radius: 3px;
        border-top-right-radius: 3px;
        background-color: #fff;
        box-sizing: border-box;
        overflow: hidden;
    }



    .dropdown-paging-size {
        width: auto;
        height: 36px;
        position: relative;
        box-sizing: border-box;
    }

    .dropdown-main {
        background-color: transparent;
        width: auto;
        height: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
        cursor: pointer;
        font-weight: 600;
        font-size: 14px !important;
    }

    .dropdown-icon {
        margin-left: 10px;
    }

    .dropdown-list {
        position: absolute;
        top: 38px;
        right: 0px;
        width: auto;
        height: auto;
        padding: 8px;
        box-sizing: border-box;
        box-shadow: 0px 0px 4px 0px rgba(0, 0, 0, 0.3);
        border-radius: 4px;
        background-color: #fff;
        z-index: 21;
    }

    .dropdown-item {
        width: auto;
        height: 37px;
        border-radius: 4px;
        line-height: 37px;
        padding-left: 8px;
        padding-right: 14px;
        box-sizing: border-box;
        display: flex;
        align-items: center;
        justify-content: space-between;
        cursor: pointer;
    }

        .dropdown-item p {
            white-space: nowrap;
            margin-right: 16px;
        }

        .dropdown-item:hover {
            background-color: #ffede2;
        }

    .dropdown-item-active {
        background-color: #ffede2;
        color: #ef5504;
    }
</style>