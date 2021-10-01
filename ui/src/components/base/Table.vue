<template>
    <div class="box-grid">
        <div class="table-tool">
            <div class="btn-col-option" @click="btnGridSettingOnClick" ref="btnGridSetting">
                <div :class="['background-icon',{'position-icon-grid-setting': !isShowGridSetting,'position-icon-grid-setting-active' : isShowGridSetting}]"></div>
            </div>
        </div>
        <DxDataGrid ref="dxDataGrid"
                    :data-source="dataSource"
                    :remote-operations="false"
                    :allow-column-reordering="true"
                    :show-borders="true"
                    height="100%"
                    :title="titleTd"
                    :column-width="250"
                    :allow-column-resizing="true"
                    :column-resizing-mode="currentMode"
                    :column-min-width="100"
                    :column-auto-width="true"
                    :hover-state-enabled="true"
                    paging.enabled="false"
                    rowType="data"
                    noDataText=""
                    @content-ready="onContentReady" 
                    @selection-changed="onSelectionChanged"
                    @row-dbl-click="onRowDblClick">
            <DxSelection :select-all-mode="allMode"
                         :show-check-boxes-mode="checkBoxesMode"
                         mode="multiple"
                         :fixed="true" />
            <DxColumn v-for="(item, index) in headers"
                      :key="index"
                      :data-field="item.DataField"
                      :caption="item.Caption"
                      :data-type="item.DataType"
                      :alignment="item.Alignment"
                      :fixed="item.Fixed"
                      :width="item.Width"
                      cell-template="cellTemplate" />

            <DxColumn alignment="center"
                      :width="100"
                      :fixed="true"
                      fixed-position="right"
                      cell-template="option"
                      cssClass="custom-column custom-btn-command" />




            <template #cellTemplate="{ data }">
                <slot :name="data.column.dataField" :data="data.data">
                    <span v-if="data.column.dataType=='datetime'">{{ formatDateTime(data.data[data.column.dataField]) }}</span>
                    <span v-else-if="data.column.dataType=='number'">
                        <span v-if="data.column.dataField=='NumDayOfRest'">{{getNumberDayOfNP(data.data.StartTime, data.data.EndTime)}}</span>
                        <span v-if="data.column.dataField=='NumHoursOfRest'">{{getNumberHoursOfNP(data.data.StartTime, data.data.EndTime)}}</span>
                    </span>
                    <span v-else>{{ data.data[data.column.dataField] }}</span>
                </slot>
            </template>

            <template #option="{data}">
                <div class="box-option">
                    <div class="box-btn flex a-l-c">
                        <div class="btn-edit" @click="edit(data.data.RestPermissionId)">
                            <div class="background-icon position-icon-edit"></div>
                        </div>
                        <div class="btn-delete" @click="delete1(data.data.RestPermissionId)">
                            <div class="background-icon position-icon-delete"></div>
                        </div>
                    </div>
                </div>
            </template>
            <DxPaging :page-size="1000"
                      :page-index="1" />
        </DxDataGrid>
        <div v-if="dataSource.length==0" class="nodata-display"></div>
    </div>
</template>

<script>
    import FormatData from "../../util/Format.js"
    import Common from "../../util/Common.js"
    import ResourceVn from "../../resources/ResourceVN.js"
    //import CheckBox from "./CheckBox.vue"
    import { EventBus } from "../../main.js"
    import {
        DxDataGrid,
        DxColumn,
        DxPaging,
        DxSelection
    } from "devextreme-vue/data-grid";
    import "devextreme/data/odata/store";
    let collapsed = false;
    export default {
        components: {
            DxDataGrid,
            DxColumn,
            DxPaging,
            DxSelection
        },
        props: {
            dataSource: {
                type: Array,
                default: () => []
            },
            headers: {
                type: Array,
                default: () => []
            },
            deSelectAll: {
                type: Boolean,
                default: false
            }
        },
        data() {
            return {
                isShowGridSetting: false,
                titleTd: "Hello",
                // data cho checkbox
                allMode: "page",
                checkBoxesMode: "always",
                // data cho resize column
                resizingModes: ["nextColumn", "widget"],
                currentMode: "widget",
                onContentReady: function (e) {
                    if (!collapsed) {
                        e.component.expandRow(["EnviroCare"]);
                        collapsed = true;
                    }
                },
            };
        },
        methods: {
            /**
             * sự kiện khi chọn hoặc bỏ chọn 1 hàng trên table
             * @param param0
             * Author HieuNV
             */
            onSelectionChanged({ selectedRowsData }) {
                this.$emit('onRowSelected', selectedRowsData);
            },
            /**
             * Sự kiện khi người cùng db click 1 hàng
             * @param e
             * Author HieuNV
             */
            onRowDblClick(e) {
                this.$emit('editRecord', e.data.RestPermissionId);
            },
            /**
             * Sự kiẹn khi người dùng click btn sửa trên 1 hàng
             * @param code
             * Author HieuNV
             */
            edit(code) {
                this.$emit('editRecord', code);
            },
            /**
             * Sự kiện khi người dùng click btn xóa trên 1 hàng
             * @param code
             * Author HieuNV
             */
            delete1(code) {
                EventBus.$emit('showPopup', ResourceVn.PopupMessenger.confirmDelete() , 'Cảnh báo', '14', 'delete' , code );
            },
            /**
             * gọi đến hà format datetime
             * @param d
             * Author HieuNV
             */
            formatDateTime(d) {
                return FormatData.formatDateTime(d);
            },
            /**
             * Gọi đến hàm tính số ngày nghỉ từ startTime và endTime
             * @param _start
             * @param _end
             * Author HieuNV
             */
            getNumberDayOfNP(_start, _end) {
                return Common.getNumberDayOfNP(_start, _end);
            },
            /**
             * Gọi đến hàm tính số giờ nghỉ từ startTime và endTime
             * @param _start
             * @param _end
             * Author HieuNV
             */
            getNumberHoursOfNP(_start, _end) {
                return Common.getNumberHoursOfNP(_start, _end);
            },
            /**
             * Sự kiện khi người dùng click vào btn setting grid (custom columns)
             * Author HieuNV
             * */
            btnGridSettingOnClick() {
                if (this.isShowGridSetting) {
                    EventBus.$emit('hideGridSetting');
                } else {
                    var left = this.$refs.btnGridSetting.getBoundingClientRect().left - 27;
                    var top = this.$refs.btnGridSetting.getBoundingClientRect().top + 38;
                    var positionStr = "top: " + top + "px; left: " + left + "px;";
                    EventBus.$emit('showGridSetting', positionStr);
                }
                this.isShowGridSetting = !this.isShowGridSetting;
            }
        },
        created() {
            // nhận sự kiện ẩn gridSetting
            EventBus.$on('hideThisGridSetting', () => {
                this.isShowGridSetting = false;
                EventBus.$emit('hideGridSetting');
            })
        },
        watch: {
            //bỏ chọn toàn bộ các row đã chọn trên table
            deSelectAll() {
                this.$refs.dxDataGrid.instance.deselectAll();
            }
        }
    };
</script>

<style>
    td .checkbox {
        margin-top: 16px;
    }

    td .compact-avatar {
        margin-top: 11px;
        margin-right: 8px;
    }

    .nodata-display {
        background-image: url('../../assets/image/nodata.jpg');
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        z-index: 178;
        width: 118px;
        height: 108px;
    }

    /* margin cho table */
    .box-grid {
        width: 100%;
        height: 100%;
        position: relative;
    }

    .table-tool {
        width: auto;
        height: auto;
        position: absolute;
        top: 8px;
        left: 0px;
        z-index: 200;
        display: flex;
        align-items: center;
        justify-content: flex-start;
    }

    .btn-col-option {
        width: 30px;
        height: 30px;
        border-radius: 50%;
        display: flex;
        align-items: center;
        justify-content: center;
        margin-left: 12px;
        margin-right: 24px;
        cursor: pointer;
    }

        .btn-col-option:hover {
            background-color: #DADCE3;
        }

    /* fix height cho tr */
    .dx-datagrid-content .dx-datagrid-table .dx-row {
        line-height: 33px !important;
        height: 33px !important;
    }

    /* padding text thead */
    .dx-datagrid-text-content {
        color: #212121 !important;
        font-weight: 700 !important;
        font-size: 14px !important;
    }


    .dx-datagrid-headers .dx-datagrid-table .dx-row > td {
        cursor: pointer;
    }

    /* padding */
    .dx-datagrid .dx-row > td {
        padding: 0 16px !important;
        line-height: 48px;
        cursor: pointer;
    }

    /* padding text td */
    .dx-datagrid-content .dx-datagrid-table .dx-row td {
        margin-left: 8px !important;
    }

        .dx-datagrid-content .dx-datagrid-table .dx-row td:last-child {
            margin-left: 0px !important;
        }

    /* set color cho td */
    .dx-datagrid .dx-row-alt > td {
        background-color: #fff !important;
    }

    .dx-datagrid-content .dx-datagrid-table .dx-row td {
        border-bottom: 1px solid #e0e0e0 !important;
    }


    /* bỏ border table */
    .dx-datagrid .dx-row > td {
        border-left: none !important;
        border-right: none !important;
    }

    /* fix width checkbox */
    .dx-datagrid-content
    .dx-datagrid-table.dx-datagrid-table-fixed
    colgroup
    col:first-child {
        width: 100px !important;
        text-align: right !important;
    }

        .dx-datagrid-content
        .dx-datagrid-table.dx-datagrid-table-fixed
        colgroup
        col:first-child .dx-select-checkbox{
            margin-left: 20px !important;
        }
        /*   .dx-datagrid-content
    .dx-datagrid-table.dx-datagrid-table-fixed
    colgroup
    col:nth-child(2) {
        width: 40px !important;
    }*/
        /*.dx-datagrid-headers td:last-child {
        display: none;
    }*/
        /*.dx-datagrid-rowsview tr td:last-child {
        display: none;
    }

    .dx-datagrid-rowsview .dx-state-hover td:last-child {
        display: block;
    }*/
        .dx-widget .dx-visibility-change-handler {
            border-left: none !important;
            border-right: none !important;
        }


    .dx-datagrid-borders > .dx-datagrid-headers {
        border-left: none !important;
        border-right: none !important;
        border-bottom: none !important;
    }

    .dx-data-row.dx-state-hover:not(.dx-selection):not(.dx-row-inserted):not(.dx-row-removed):not(.dx-edit-row) > td:not(.dx-focused) {
        background-color: #FFEDE2 !important;
    }


    .dx-datagrid-rowsview .dx-selection.dx-row > td,
    .dx-datagrid-rowsview .dx-selection.dx-row:hover > td {
        background-color:  #FFF4E5 !important;
    }

    .icon-status {
        width: 8px;
        height: 8px;
        border-radius: 8px;
        background: rgb(52, 176, 87);
        color: rgb(0, 0, 0);
    }


    .text-status {
        margin-left: 8px;
        color: rgb(52 172 84);
    }


    .box-option {
        display: none;
    }


    .dx-state-hover .box-option {
        display: flex !important;
        justify-content: space-between !important;
        align-items: center;
    }


    /* style btn sửa */
    .btn-edit {
        display: flex;
        justify-content: center;
        align-items: center;
        width: 30px;
        height: 30px;
        border-radius: 15px;
        background-color: #fff;
        margin-right: 20px;
    }

    /* style btn delete */
    .btn-delete {
        display: flex;
        justify-content: center;
        align-items: center;
        width: 30px;
        height: 30px;
        border-radius: 15px;
        background-color: #fff;
    }

    .a-l-c {
        align-items: center;
    }

    .flex {
        display: flex;
        justify-content: space-between;
        height: 47px;
    }

    .dx-scrollable-scrollbar.dx-widget.dx-scrollbar-horizontal.dx-scrollbar-hoverable {
        background-color: rgba(0,0,0,0.0) !important;
        border-radius: 10px;
    }

        .dx-scrollable-scrollbar.dx-widget.dx-scrollbar-horizontal.dx-scrollbar-hoverable.dx-state-hover {
            background-color: rgba(0,0,0,0.03) !important;
        }

    .dx-scrollable-scroll-content {
        background-color: #9E9E9E !important;
        border-radius: 10px;
    }

        .dx-scrollable-scroll-content.dx-state-hover {
            background-color: #757575 !important;
            border-radius: 10px;
        }


    .dx-row.dx-freespace-row.dx-column-lines {
        display: none;
    }

    /*  .dx-datagrid-content .dx-datagrid-table .dx-row td:last-child {
        box-sizing: content-box !important;
    }*/

    .custom-column {
        border: unset !important;
        border-bottom: 2px solid #e0e0e0 !important;

    }

    .dx-row.dx-data-row.dx-state-hover .custom-btn-command {
        visibility: visible !important;
        right: 0.1px !important;
    }

    .visibility-hidden{
        visibility: hidden !important;
    }

    .custom-column.custom-btn-command{
        visibility: hidden !important;
    }

    td.custom-column.custom-btn-command.dx-hidden-cell{
        visibility: visible !important;
    }

    .dx-sort{
        visibility: hidden !important;
    }

    .cls{
        background-color: red !important;
    }

    .dx-datagrid-content .dx-datagrid-table .dx-row > td:first-child, .dx-datagrid-content .dx-datagrid-table .dx-row > tr > td:first-child{
        text-align: right !important;
    }


    /* checkbox*/

    .dx-checkbox .dx-checkbox-icon {
        border: 2px solid #737888 !important;
        border-radius: 4px !important;
    }

    .dx-checkbox-checked .dx-checkbox-icon {
        font: 16px/16px DXIcons;
        color: #fff !important;
        background-color: #ec5504 !important;
        border: 1px solid #ec5504 !important;
        text-align: center !important;
    }

    .dx-checkbox.dx-state-hover .dx-checkbox-icon {
        border: 2px solid #737888 !important;
    }

    .dx-checkbox.dx-state-focused .dx-checkbox-icon {
        border: 2px solid #737888 !important;
    }

    .dx-checkbox-checked.dx-checkbox.dx-state-hover .dx-checkbox-icon {
        border: 2px solid #ec5504 !important;
    }

    .dx-checkbox-checked.dx-checkbox.dx-state-focused .dx-checkbox-icon {
        border: 2px solid #ec5504 !important;
    }

    .dx-checkbox-indeterminate .dx-checkbox-icon::before{
        background-color: #fff;
    }
</style>