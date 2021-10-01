<template>
    <div class="tool-filter">
        <div class="tool-filter-header">
            <div class="tool-filter-title">Bộ lọc</div>
            <div class="btn-x background-icon position-icon-x-big" @click="hideToolFilter"></div>
        </div>
        <div class="input-search-wrapper">
            <Input inputPlacehoder="Tìm kiếm"
                   inputClass="input-search"
                   v-model="searchTerms"/>
            <div class="icon-search background-icon position-icon-search"></div>
        </div>
        <div class="tool-filter-body">
            <div v-for="(item,index) in myListFilter" :class="['filter-item',{'filter-item-disable': !item.Enable}]" 
                 :key="index" v-show="inShowList(item.Caption)">  
                <div class="filter-item-header"  @click="toggleEnableItem(item)">
                    <div class="checkbox-wrapper">
                        <CheckBox :checkOrNo="item.Enable" />
                    </div>                    
                    <div class="filter-item-caption">{{item.Caption}}</div>
                </div>
                <Combobox v-if="item.Enable" 
                          :comboboxId="`${index + 21}`" 
                          :listItem="item.FilterList" 
                          :comboboxType="item.FilterType"
                          :theValue="item.FilterOption"
                          v-model="item.FilterOption" />
                <Input v-if="item.Enable && (item.FilterType == 'FilterText' && item.FilterOption < 6)" 
                       v-model="item.FilterValue" 
                       :inputValue="item.FilterValue"/>
            </div>
        </div>
        <div class="tool-filter-footer">
            <Button buttonText="Bỏ lọc" buttonType="secondary" @btnClick="btnDisFilterOnClick"/>
            <Button buttonText="Áp dụng" buttonType="primary" @btnClick="btnApplyOnClick"/>
        </div>
    </div>
</template>


<script>
    import Input from "./Input.vue"
    import Button from "./Button.vue"
    import Combobox from "./Combobox"
    import CheckBox from "./CheckBox"
    import { filterList } from "../../model/listDatas.js"

    export default {
        name: 'ToolFilter',
        components: {
            Input, Button, Combobox, CheckBox
        },
        methods: {
            /**
             * Sự kiện ẩn đi bộ lọc
             * Author HieuNV
             * */
            hideToolFilter() {
                this.$emit('hideToolFilter');
            },
            /**
             * Sự kiện thay dổi trang thái có lọc hay không 1 trường nào đó đc click
             * @param item
             * Author HieuNV
             */
            toggleEnableItem(item) {
                item.Enable = !item.Enable;
            },
            /**
             * Sự kiện bỏ lọc
             * Author HieuNV
             * */
            btnDisFilterOnClick() {
                this.myListFilter = JSON.parse(JSON.stringify(filterList));
                this.$emit('disFilter');
            },
            /**
             * Kiểm tra từ khòa người dùng tìm kiếm để ẩn hiện 1 item
             * @param filterField
             * Author HieuNV
             */
            inShowList(filterField) {
                return filterField.toLowerCase().includes(this.searchTerms.toLowerCase());
            },
            /**
             * Sự kiện áp dụng bộ lọc
             * Author HieuNV
             * */
            btnApplyOnClick() {
                this.$emit('applyFiter', JSON.parse(JSON.stringify(this.myListFilter)));
            }
        },
        data() {
            return {
                myListFilter: [],
                searchTerms: ''
            }
        },
        created() {
            this.myListFilter = JSON.parse(JSON.stringify(filterList));
        }
    }
</script>


<style scoped>
    .tool-filter {
        width: 240px;
        height: calc(100% - 69px);
        margin-left: 16px;
        background-color: #fff;
        border: 1px solid #e0e0e0;
        border-radius: 3px;
        float: left;
        box-sizing: border-box;
    }

    .tool-filter-header {
        width: 100%;
        height: 56px;
        float: left;
        padding: 0px 16px 14px 12px;
        box-sizing: border-box;
        display: flex;
        align-items: flex-end;
        justify-content: space-between;
    }

    .tool-filter-title {
        font-size: 20px;
        font-weight: 600;
        margin-left: 4px;
    }

    .btn-x {
        margin-bottom: 3px;
        cursor: pointer;
    }

    .input-search-wrapper {
        width: 206px;
        height: 36px;
        position: relative;
        margin-left: 16px;
        margin-bottom: 8px;
        overflow: hidden;
        margin-right: 8px;
    }

        .input-search-wrapper .icon-search {
            position: absolute;
            top: 11px;
            left: 11px;
        }

    .tool-filter-body {
        width: 100%;
        height: calc(100% - 168px);
        float: left;
        padding: 0px 8px;
        box-sizing: border-box;
        overflow-y: auto;
    }



    .filter-item {
        width: 100%;
        height: auto;
        border-radius: 5px;
        padding: 0px 8px 0px 8px;
        box-sizing: border-box;
        background-color: #FFEDE2;
        float: left;
        margin-bottom: 8px;
        cursor: pointer;
    }
    .filter-item-disable{
        background-color:#fff !important;
    }

    .filter-item-header {
        height: 38px;
        width: 100%;
        float: left;
        display: flex;
        align-items: center;
        justify-content: flex-start;
    }

        .filter-item-header:hover .checkbox-wrapper {
            background-color: #EBEBEB;
        }

        .filter-item-header:hover .checkbox{
            background-color: #EBEBEB !important;
        }

        .checkbox-wrapper {
            width: 30px;
            height: 30px;
            border-radius: 50%;
            background-color: transparent;
        }

    .filter-item-caption{
        margin-left: 3px;
    }

    .filter-item input{
        float: left !important;
        width: 100% !important;
        margin-bottom: 8px !important;
    }

    .filter-item .combobox-user {
        float: left !important;
        width: 100% !important;
        margin-bottom: 8px !important;
    }


    .tool-filter-footer {
        width: 100%;
        height: 53px;
        border-top: 1px solid #fff;
        display: flex;
        align-items: flex-end;
        justify-content: center;
        float: left;
        margin-top: 4px;
    }

        .tool-filter-footer button:last-child {
            margin-left: 16px;
        }

        .tool-filter-footer button {
            width: 90px !important;
        }
</style>