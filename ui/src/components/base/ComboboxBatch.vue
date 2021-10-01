<template>
    <div :class="['combobox-batch',{'focus-border' : onFocus}]" ref="myCombobox" @click="showDataList">
        <div class="combobox-main">
            <div v-for="(item,index) in comboboxValue" :key="index" class="result-item">
                <p>{{item.FullName}}</p>
                <div class="btn-x background-icon position-icon-x" @click.prevent="removeItem(item)"></div>
            </div>
            <input type="text" class="combobox-search" ref="inputSearch" @focus="focusOn" 
                   @blur="hideDataList" @keyup.enter="search" v-model="valueInput"
                   @keyup.down="onDown" @keyup.up="onUp" @keyup.right="onChoose"/>
        </div>
        <div class="combobox-option background-icon position-icon-chevron-down"></div>
    </div>
</template>


<script>
    import EmployeeApi from "../../api/entities/EmployeeApi.js";
import { EventBus } from "../../main.js"

    export default {
        name: 'ComboboxBatch',
        components: {
        },
        props: {
            // ds các item để lựa chọn
            listItem: {
                type: Array,
                default: () => []
            },
            comboboxId: {
                type: String,
                default: ''
            },
            theValue: {
                type: String,
                value: ''
            }
        },
        data() {
            return {
                listItemShow: [],
                onFocus: false,
                comboboxValue: [],
                valueInput: ''
            }
        },
        watch: {
            /**
             * xử lí sự kiện khi giá trị truyền vào cho cbb thay đổi
             * Author HieuNV
             * */
            theValue() {
                if (this.theValue !== '' && this.theValue !== ' ' && this.theValue!== null) {
                    EmployeeApi.getByIds(this.theValue).then(res => {
                        this.comboboxValue = res.data;
                    })
                }
            }
        },
        created() {

            if (this.theValue !== '' && this.theValue !== ' ' && this.theValue !== null) {
                EmployeeApi.getByIds(this.value).then(res => {
                    this.comboboxValue = res.data;
                })
            }

            this.listItemShow = this.listItem;

            // Xử lí sự kiện khi chọn hoặc bỏ chọn 1 option bên datalist
            EventBus.$on('itemOnSelect', (data, id) => {
                if (this.comboboxId == id) {
                    this.comboboxValue = data;
                    var ids = '';
                    this.comboboxValue.forEach(item => {
                        ids += item.EmployeeId + ' ';
                    })
                    this.$emit('input', ids.trim());
                }
            })
        },
        methods: {
            /**
             * Xóa một item khỏi list được chọn
             * Author HieuNV
             * @param item
             */
            removeItem(item) {
                this.comboboxValue = this.comboboxValue.filter((e) => e.EmployeeId !== item.EmployeeId);
                var ids = '';
                this.comboboxValue.forEach(item => {
                    ids += item.EmployeeId + ' ';
                })
                this.$emit('input', ids.trim());
            },
            /**
             * Xử lí sự kiện gọi đến dataList để showList option
             * Author HieuNV
             * */
            showDataList() {
                var left = this.$refs.myCombobox.getBoundingClientRect().left;
                var top = this.$refs.myCombobox.getBoundingClientRect().top + 89;
                var positionStr = "top: " + top + "px; left: " + left + "px;";
                EventBus.$emit('hideDataList');
                setTimeout(() => {
                    EventBus.$emit('showDataList', positionStr, this.listItemShow,
                        'user', true, this.comboboxValue, this.comboboxId);
                }, 200)
                this.$refs.inputSearch.focus();
            },
            /**
             *Xử lí sự kiện gọi đến dataList để showList option hợp lệ với từ khóa được search
             * Author HieuNV
             * */
            searchDataList() {
                var left = this.$refs.myCombobox.getBoundingClientRect().left;
                var top = this.$refs.myCombobox.getBoundingClientRect().top + 89;
                var positionStr = "top: " + top + "px; left: " + left + "px;";
                EventBus.$emit('hideDataList');
                setTimeout(() => {
                    EventBus.$emit('searchDataList', positionStr, this.listItemShow,
                        'user', true, this.comboboxId, this.valueInput);
                }, 200)
                this.$refs.inputSearch.focus();
            },
            /**
             * Xử lí sự kiện ẩn datalist
             * Author HieuNV
             * */
            hideDataList() {
                this.onFocus = false;
                setTimeout(() => {
                    EventBus.$emit('hideDataList');
                }, 200)
            },
            /**
             * sự kiện khi focus vào component
             * */
            focusOn() {
                this.onFocus = true;
            },
            /**
             * sự kiện search
             * */
            search() {
                this.searchDataList();
            },
            /**
             * Sự kiện bấm nút up để di chuyển xuống dưới trong dataList
             * Author HieuNV
             * */
            onDown() {
                EventBus.$emit('comboboxBatchOnUpDown', 1);
            },
            /**
             * Sự kiện bấm nút up để di chuyển lên trên trong dataList
             * Author HieuNV
             * */
            onUp() {
                EventBus.$emit('comboboxBatchOnUpDown', -1);
            },
            /**
             * Sự kiện bám nút right để chọn hoặc bỏ chọn 1 item
             * Author HieuNV
             * */
            onChoose() {
                EventBus.$emit('comboboxBatchOnChoose');
            }
        }
    }
</script>


<style scoped>

    .combobox-batch {
        width: 240px;
        height: 87px;
        position: relative;
        border-radius: 4px;
        background-color: #fff;
        border: 1px solid #e0e0e0;
        border-radius: 4px;
        box-sizing: border-box;
    }

        .combobox-batch:hover {
            border-color: #EC5504;
        }

    .focus-border {
        border-color: #ec4405;
    }

    .combobox-main {
        width: calc(100% - 29px);
        height: 100%;
        background-color: aquamarine;
        overflow-y: auto;
        padding: 3px 5px;
        box-sizing: border-box;
        background-color: #fff;
        border-top-left-radius: 3px;
        border-bottom-left-radius: 3px;
    }

    .combobox-search {
        width: auto;
        height: 29px;
        border: none;
        outline: none;
        float: left;
    }

    .result-item {
        width: auto;
        height: 29px;
        position: relative;
        float: left;
        background-color: #E6E8ED;
        border-radius: 4px;
        display: flex;
        align-items: center;
        padding-left: 3px;
        box-sizing: border-box;
        margin-bottom: 2px;
    }

        .result-item p {
            padding: 0;
            display: block;
            line-height: 29px;
            width: auto;
            max-width: 163px;
            overflow-x: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
        }

    .btn-x {
        cursor: pointer;
        margin-left: 12px;
        margin-right: 12px;
    }


    .combobox-option {
        position: absolute;
        top: 39px;
        right: 10px;
    }
</style> 