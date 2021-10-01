<template>
    <div class="combobox-user" @click="showDataList" ref="myCombobox">
        <input :class="['combobox-search',{'border-red': isBorderRed}]" ref="inputSearch" 
               @blur="hideDataList"
               v-model="valueInput"
               @keyup.enter="search"
               @keyup.down="downKeyup"
               @keyup.up="upKeyup" />
        <div class="combobox-option background-icon position-icon-chevron-down"></div>
    </div>
</template>


<script>
import EmployeeApi from "../../api/entities/EmployeeApi.js"
import { EventBus } from "../../main.js"

    export default {
        name: 'Combobox',
        components: {
        },
        props: {
            // ds các item để lựa chọn
            listItem: {
                type: Array,
                default: () => []
            },
            // kiểu của combobox
            comboboxType: {
                type: String,
                default: ''
            },
            // autofocus nếu true
            isFocus: {
                type: Boolean,
                default: false
            },
            // id để phân biệt các cbb khi dùng chung 1 datalist
            comboboxId: {
                type: String,
                default: ''
            },
            isBorderRed: {
                type: Boolean,
                default: false
            },
            // truyền vào giá trị cho cbb
            theValue: {
                type: String,
                default: ''
            }
        },
        data() {
            return {
                isShowData: false,
                listItemShow: [],
                comboboxValue: [''],
                valueInput: ''
            }
        },
        watch: {
            // khởi tạo mà match cbb khi truyền vào 1 giá trị
            theValue() {
                if (this.theValue !== '' && this.theValue!==null && this.comboboxType == 'user') {
                    EmployeeApi.getById(this.theValue).then(res => {
                        this.comboboxValue[0] = res.data;
                        this.valueInput = res.data.FullName;
                    })
                } else if (this.theValue !== '' && this.comboboxType !== 'user') {
                    this.listItem.forEach(item => {
                        if (item[`${this.comboboxType}Id`] == this.theValue) {
                            this.comboboxValue[0] = item;
                            this.valueInput = item[`${this.comboboxType}Name`];
                        }
                    })
                }
            }
        },
        created() {

            if (this.theValue !== '' && this.theValue !== null && this.comboboxType == 'user') {
                EmployeeApi.getById(this.value).then(res => {
                    this.comboboxValue[0] = res.data;
                    this.valueInput = res.data.FullName;
                })
            } else if (this.theValue !== '' && this.comboboxType !== 'user') {
                this.listItem.forEach(item => {
                    if (item[`${this.comboboxType}Id`] == this.theValue) {
                        this.comboboxValue[0] = item;
                        this.valueInput = item[`${this.comboboxType}Name`];
                    }
                })
            }

            this.listItemShow = this.listItem;

            // autofocus
            if (this.isFocus) {
                setTimeout(() => {
                    this.$refs.inputSearch.focus();
                },100)            
            }

            // xử lí sự kiện khi chọn 1 option
            EventBus.$on('itemOnSelect', (data, id) => {
                if (this.comboboxId == id) {
                    this.comboboxValue = data;
                    this.valueInput = data[0].FullName;
                    if (this.comboboxType == 'user') {
                        this.valueInput = data[0].FullName;
                        this.$emit('input', this.comboboxValue[0].EmployeeId);
                        if (this.comboboxId == 1) {
                            this.$emit('cbbOnSelect');
                        }
                    } else {
                        this.valueInput = data[0][`${this.comboboxType}Name`];
                        this.$emit('input', this.comboboxValue[0][`${this.comboboxType}Id`]);
                    }
                }
            })
        },
        methods: {
            /**
             * gọi đến component dataList để hiện thị list option
             * Author HieuNV
             * */
            showDataList() {
                this.listItemShow = this.listItem;
                var left = this.$refs.myCombobox.getBoundingClientRect().left;
                var top = this.$refs.myCombobox.getBoundingClientRect().top + 38;
                var positionStr = "top: " + top + "px; left: " + left + "px;";
                EventBus.$emit('hideDataList');
                setTimeout(() => {
                    EventBus.$emit('showDataList', positionStr, this.listItemShow,
                        this.comboboxType, false, this.comboboxValue , this.comboboxId);
                }, 200)
                this.$refs.inputSearch.focus();
            },
            /**
             * gọi đến component dataList để hiện thị list option hợp lệ theo từ khóa tìm kiếm
             * Author HieuNV
             * */
            searchDataList() {
                var left = this.$refs.myCombobox.getBoundingClientRect().left;
                var top = this.$refs.myCombobox.getBoundingClientRect().top + 38;
                var positionStr = "top: " + top + "px; left: " + left + "px;";
                EventBus.$emit('hideDataList');
                setTimeout(() => {
                    EventBus.$emit('searchDataList', positionStr, this.listItemShow,
                        this.comboboxType, false, this.comboboxId, this.valueInput);
                }, 200)
                this.$refs.inputSearch.focus();
            },
            /**
             * Ẩn dataList
             * Author HieuNV
             * */
            hideDataList() {
                setTimeout(() => {
                    EventBus.$emit('hideDataList');
                },200)
            },
            /**
             * xử lí sự kiện khi người sùng search trên combobox
             * Author HieuNV
             * */
            search() {
                if (this.comboboxType == 'user') {
                    this.searchDataList();
                } else {
                    this.listItemShow = [];
                    this.listItem.forEach(item => {
                        if (item[`${this.comboboxType}Name`].toLowerCase().includes(this.valueInput.toLowerCase())) {
                            this.listItemShow.push(item);
                        }
                    });
                    this.searchDataList();
                }
            },
            /**
             * Xử lí sự kiện người dùng di chuyển và chọn option dưới
             * Author HieuNV
             * */
            downKeyup() {
                EventBus.$emit('comboboxOnUpDown',1, this.comboboxValue);
            },
            /**
             * Xử lí sự kiện người dùng di chuyển và chọn option trên
             * Author HieuNV
             * */
            upKeyup() {
                EventBus.$emit('comboboxOnUpDown',-1,this.comboboxValue);
            }
        }
    }
</script>


<style scoped>

    .combobox-user {
        width: 240px;
        height: 36px;
        position: relative;
        border-radius: 4px;
        background-color: #fff;
        z-index: 60;
    }

    .combobox-search {
        outline: 0;
        border: 0;
        width: 100%;
        height: 100%;
        border: 1px solid #e0e0e0;
        border-radius: 4px;
        padding: 6px 40px 6px 8px;
        box-sizing: border-box;
        z-index: 43;
        font-size: 13px;
        font-family: Roboto;
    }

    .border-red {
        border-color: #FF6161 !important;
    }

        .combobox-search:focus {
            border-color: #EC5504;
        }

        .combobox-search:hover {
            border-color: #EC5504;
        }

    .combobox-option {
        position: absolute;
        top: 15px;
        right: 10px;
        z-index: 43;
    }
</style>