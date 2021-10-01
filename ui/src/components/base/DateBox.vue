<template>
    <div :class="['date-box',{'border-red': isBorderRed}]">
        <DxDateBox type="datetime" displayFormat="dd/MM/yyyy HH:mm"
                   applyButtonText="Lưu" cancelButtonText="Hủy"
                   locale="vi" v-model="value" 
                   ref="dateBox"
                    />
    </div>
</template>


<script>
    import 'devextreme/dist/css/dx.light.css';
    import { DxDateBox } from 'devextreme-vue/date-box';
    import { locale } from "devextreme/localization";


    export default {
        name: 'DateBox',
        components: {
            DxDateBox
        },
        created() {
            locale("vi-VN");
        },
        props: {
            value: {
                type: Date,
                default: new Date()
            },
            isBorderRed: {
                type: Boolean,
                default: false 
            },
            autoFocus: {
                type: Boolean,
                default: false
            }
        },
        watch: {
            // nhận sự thay đổi giá trị từ component thư viện và truyền lên cho v-model
            value() {
                this.$emit('input', this.value);
            }
        },
        mounted() {
            // autofocus
            if (this.autoFocus) {
                this.$nextTick(() => {
                    this.$refs.dateBox.instance && this.$refs.dateBox.instance.focus();
                })
            }
        }
    }
</script>


<style>



    .date-box .dx-texteditor.dx-editor-outlined {
        border: 1px solid #e0e0e0 !important;
        width: 240px !important;
    }

        .date-box .dx-texteditor.dx-editor-outlined:hover {
            border-color: #ec5504 !important;
        }

        .date-box .dx-texteditor.dx-editor-outlined.dx-state-focused {
            border-color: #ec5504 !important;
        }

    .date-box.border-red .dx-texteditor.dx-editor-outlined {
        border-color: #FF6161 !important;
    }

    .dx-datebox-wrapper-calendar .dx-popup-content {
        margin: 0 !important;
        padding: 16px !important;
    }

    .dx-datebox-wrapper-calendar .dx-calendar {
        border: none;
        margin: 0 !important;
        margin-right: 8px !important;
    }

    .dx-datebox-wrapper-calendar.dx-datebox-wrapper-datetime .dx-timeview {
        margin: 0 !important;
        margin-left: 8px;
    }

    .dx-button-today {
        display: none !important;
    }

    .dx-toolbar .dx-toolbar-after {
        display: flex;
        flex-direction: row-reverse;
    }

    .dx-popup-bottom.dx-toolbar .dx-toolbar-item:last-child {
        margin-right: 8px;
    }

    .dx-toolbar-after .dx-toolbar-item:first-child {
        padding: 0 !important;
    }

    .dx-popup-bottom.dx-toolbar {
        padding: 0 16px 16px 16px !important;
        overflow: visible;
    }

    .dx-calendar-navigator-previous-month.dx-button .dx-icon, .dx-calendar-navigator-previous-view.dx-button .dx-icon {
        color: #ec5504 !important;
    }

    .dx-calendar-navigator-previous-month.dx-button .dx-icon, .dx-calendar-navigator-next-view.dx-button .dx-icon {
        color: #ec5504 !important;
    }

    .dx-calendar-navigator .dx-calendar-caption-button.dx-button .dx-button-content {
        color: #ec5504 !important;
        text-transform: capitalize;
    }

    .dx-calendar-cell.dx-calendar-today {
        color: #ec5504 !important;
        box-shadow: none !important;
    }

    .dx-state-hover .dx-calendar-cell:hover {
        opacity: 1;
        background-color: #FFEDE2 !important;
        box-shadow: none !important;
    }

    .dx-calendar-cell.dx-calendar-selected-date.dx-calendar-contoured-date, .dx-calendar-cell.dx-calendar-selected-date.dx-calendar-today.dx-calendar-contoured-date {
        box-shadow: none !important;
        background-color: #ec5504 !important;
    }

        .dx-calendar-cell.dx-calendar-selected-date.dx-calendar-contoured-date.dx-calendar-today, .dx-calendar-cell.dx-calendar-selected-date.dx-calendar-today.dx-calendar-contoured-date.dx-calendar-today {
            color: #fff !important;
        }

        .dx-calendar-cell.dx-calendar-selected-date.dx-calendar-contoured-date, .dx-calendar-cell.dx-calendar-selected-date.dx-calendar-today.dx-calendar-contoured-date:hover {
            box-shadow: none !important;
            background-color: #ec5504 !important;
        }

    .dx-timeview-field .dx-numberbox {
        width: 70px !important;
    }

    .dx-calendar-body thead tr th {
        text-transform: capitalize;
    }

    .date-box .dx-dropdowneditor-button .dx-button-content {
        background-image: url('../../assets/image/all-icon.svg');
        background-position: -49px -24px;
        width: 24px;
        height: 24px;
        transform: translate(4px, 4px) !important;
    }

    .date-box .dx-datebox-calendar .dx-dropdowneditor-icon {
        display: none !important;
    }
</style>