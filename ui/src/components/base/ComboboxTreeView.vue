<template>
    <div class="dx-field-value">
        <DxDropDownBox :value="treeBoxValue"
                       ref="dropdownBox"
                       :show-clear-button="true"
                       :data-source="treeDataSource"
                       :value-expr="valueExprProp"
                       :display-expr="displayExprProp"
                       :placeholder="placeholderProp"
                       @value-changed="syncTreeViewSelection($event)"
                       >
            <template #content="{}">
                <div class="tree-view">
                    <DxTreeView :searchEnabled="true"
                                :ref="treeViewRefName"
                                noDataText="Không có dữ liệu"
                                :data-source="treeDataSource"
                                :select-by-click="true"
                                data-structure="plain"
                                :key-expr="valueExprProp"
                                :parent-id-expr="parentIdExprProp"
                                selection-mode="single"
                                :display-expr="displayExprProp"
                                @content-ready="$event.component.selectItem(treeBoxValue)"
                                @item-selection-changed="treeView_itemSelectionChanged($event)"
                                @item-click="onTreeItemClick($event)" />
                    <div :class="['show-stop-follow',{'on-check':isStopFollow}]" @click="changeStatusFollow">
                        <div class="checkbox-wrapper">
                            <CheckBox :checkOrNo="isStopFollow"/>
                        </div>           
                        <span>Hiển thị đơn vị ngừng theo dõi</span>
                    </div>
                </div>          
            </template>
        </DxDropDownBox>
    </div>
</template>

<script>
    import DxDropDownBox from "devextreme-vue/drop-down-box";
    import DxTreeView from "devextreme-vue/tree-view";
    import CheckBox from "./CheckBox.vue"
    export default {
        name: "DropdownTreeView",
        components: {
            DxDropDownBox,
            DxTreeView,
            CheckBox
        },
        props: [
            "treeDataSource",
            "placeholderProp",
            "valueExprProp",
            "displayExprProp",
            "parentIdExprProp",
            "valueDefault",
        ],
        data() {
            return {
                isStopFollow: false,
                treeBoxValue: null,
                treeViewRefName: "tree-view",
            };
        },
        created() {
            // set value default cho dropdown single
            // this.treeBoxValue = this.valueDefault;
        },
        methods: {
            syncTreeViewSelection() {
                console.log(this.treeBoxValue);
                this.$emit('input', this.treeBoxValue);
                this.$emit('comboboxtOnSelect')
                if (!this.$refs[this.textBoxRefName]) return;
                if (!this.treeBoxValue) {
                    this.$refs[this.textBoxRefName].instance.unselectAll();
                } else {
                    this.$refs[this.textBoxRefName].instance.selectItem(this.treeBoxValue);
                }
            },
            treeView_itemSelectionChanged(e) {
                // console.log(this.treeBoxValue);
                this.treeBoxValue = e.component.getSelectedNodeKeys();
            },
            onTreeItemClick() {
                // console.log(this.treeBoxValue);
                this.$refs.dropdownBox.instance.close();
            },
            changeStatusFollow() {
                // console.log(this.treeBoxValue);
                this.isStopFollow = !this.isStopFollow;
            }
        },
    };
</script>

<style>
    .show-stop-follow {
        display: flex;
        align-items: center;
        height: 36px;
        width: 278px;
        border-radius: 4px;
        background-color: #F2F2F2;
        transform: translate(0px , -59px);
        cursor: pointer;
    }

    .on-check span {
        color: #ED5504;
    }

        .show-stop-follow:hover .checkbox-wrapper {
            background-color: #EBEBEB;
        }

        .checkbox-wrapper {
            width: 30px;
            height: 30px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            margin-left: 4px;
            margin-right: 2px;
        }
    
    /* set bg cho item khi selected */
    .dx-treeview-item-without-checkbox.dx-state-selected > .dx-treeview-item {
        background-color: #FFEDE2 !important;
        color: #ec5504 !important;
        border-radius: 4px;
    }

    .dx-treeview-item-without-checkbox.dx-state-selected.dx-state-focused > .dx-treeview-item {
        background-color: #FFEDE2 !important;
        color: #ec5504 !important;
        border-radius: 4px;
    }

    /* set bg cho item khi focus */
     .dx-treeview-item-without-checkbox.dx-state-focused > .dx-treeview-item {
        /*background-color: #eafbf2 !important;
        color: #34b057 !important;*/
        background-color: #fff !important;
        color: #333 !important;
    }

    /* ẩn cái icon mặc định của tree */
     .dx-treeview-toggle-item-visibility.dx-treeview-toggle-item-visibility-opened::before, .dx-treeview-toggle-item-visibility::before {
        content: none !important;
    }

    /* set icon + */
     .dx-treeview-toggle-item-visibility {
        top: 0px !important;
        left: 2px;
        margin-top: 5px;
        width: 20px !important;
        height: 20px !important;
        -webkit-mask-image: url(https://testcdnamisapp.misa.vn/apps/payroll/static/img/Icon.9254d79.svg) !important;
        -webkit-mask-repeat: no-repeat;
        -webkit-mask-position: -118px -38px;
        background-color: #666666;
        margin-right: 5px;
    }

        /* set icon - */
         .dx-treeview-toggle-item-visibility.dx-treeview-toggle-item-visibility-opened {
            -webkit-mask-position: -98px -38px;
            font: 22px/22px DXIcons;
            text-align: center;
            margin-right: 5px;
        }

    /* padding text dropdown single */
    .dx-treeview-item {
        box-sizing: border-box;
        margin: 2px 0px !important;
    }

     .dx-item.dx-treeview-item.dx-state-hover {
        border-radius: 4px;
        background-color: #FFEDE2;
    }

     .dx-treeview-node {
        padding-left: 21px !important;
    }

    /* ẩn icon delete default */
    .dx-field-value .dx-icon-clear::before {
        content: none !important;
    }

    /* style icon delete mới */
    .dx-field-value .dx-show-clear-button .dx-icon-clear {
        margin-left: 10px;
        margin-top: -11px !important;
        width: 20px !important;
        height: 20px !important;
        -webkit-mask-image: url(https://testcdnamisapp.misa.vn/apps/payroll/static/img/Icon.9254d79.svg) !important;
        -webkit-mask-repeat: no-repeat;
        -webkit-mask-position: -238px -38px;
        background-color: #707070 !important
    }

    /* ẩn icon dropdown default */
    .dx-field-value .dx-dropdowneditor-icon::before {
        content: none !important;
    }

    /* style icon dropdown mới */
    .dx-field-value .dx-dropdowneditor-icon {
        margin-left: 3px;
        width: 20px !important;
        height: 20px !important;
        -webkit-mask-image: url(https://testcdnamisapp.misa.vn/apps/payroll/static/img/Icon.9254d79.svg) !important;
        -webkit-mask-repeat: no-repeat;
        -webkit-mask-position: -98px 7px;
        background-color: #707070 !important
    }

    /* đè content lên placeholder của dropdown */
     .dx-searchbox .dx-placeholder::before {
        content: "Tìm kiếm";
    }

    /* chỉnh cỡ chữ và màu chữ cho placeholder */
    .dx-field-value .dx-placeholder::before {
        font-size: 14px;
        color: #999;
    }

    .dx-field-value .dx-texteditor.dx-editor-outlined {
        border: 1px solid #e0e0e0 !important;
    }

         .dx-texteditor.dx-editor-outlined:hover {
            border-color: #ec5504 !important;
        }

         .dx-texteditor.dx-editor-outlined.dx-state-focused {
            border-color: #ec5504 !important;
        }

     .dx-searchbox .dx-icon-search::before {
        color: #666666 !important;
    }

     .dx-treeview-search {
        margin-bottom: 14px !important;
    }

     .dx-item-content.dx-treeview-item-content {
        line-height: 26px !important;
    }

     .dx-field-value{
         margin-right: 0px !important;
     }


    .dx-treeview-with-search.dx-widget.dx-collection.dx-treeview{
        margin-bottom: 22px !important;
    }
</style>