<template>
    <div v-if="isShow" :class="['combobox-list-item', {'filter-combobox-list' : id > 20}]" :style="dataListPositionString">
        <div v-for="(item,index) in listUser" :key="index" :id="`item-user-${index}`"
             :class="['combobox-item-user',{'item-user-active':  JSON.stringify(listValue).includes(JSON.stringify(item))&&!isBatchSelect, 
             'item-batch-active': JSON.stringify(listValue).includes(JSON.stringify(item))&&isBatchSelect,
             'item-batch-on-index' : index == myBatchIndex && isBatchSelect}]" 
             @click="comboboxItemOnClick(item)">
            <CompactAvatar :userName="item.FullName" size="40" />
            <div class="user-info">
                <div class="user-name"><b>{{item.FullName}}</b> ({{item.EmployeeCode}})</div>
                <div class="user-position">{{item.PositionName}}</div>
            </div>
            <div v-if="JSON.stringify(listValue).includes(JSON.stringify(item))&&isBatchSelect" class="is-check">
                <div class="background-icon position-icon-tick"></div>
            </div>
        </div>
        <div v-for="(item,index) in listItem" :key="index" :id="`item-${index}`"
             :class="['combobox-item',{'item-active':JSON.stringify(listValue).includes(JSON.stringify(item))}]" 
             @click="comboboxItemOnClick(item)">
            {{item[`${type}Name`]}}
        </div>
        <div v-if="listItem.length==0&&listUser.length==0" class="no-data-item">Không có dữ liệu</div>
    </div>
</template>


<script>
    import CompactAvatar from "./CompactAvatar.vue"
    import { EventBus } from "../../main.js"
    import EmployeeApi from "../../api/entities/EmployeeApi.js"

    export default {
        name: 'DataList',
        components: {
            CompactAvatar
        },
        data() {
            return {
                id: '',
                listValue: [],
                listItem: [],
                listUser: [],
                isShow: false,
                dataListPositionString: '',
                type: '',
                isBatchSelect: false,
                myBatchIndex: -1
            }
        },
        methods: {
            /**
             * Xử lí sự kiện khi click vào 1 option trên list
             * @param data
             * Author HieuNV
             */
            comboboxItemOnClick(data) {           
                if (this.isBatchSelect == true) {
                    if (!JSON.stringify(this.listValue).includes(JSON.stringify(data))) {
                        this.listValue.push(data);
                        EventBus.$emit('itemOnSelect', this.listValue, this.id);
                    } else {
                        this.listValue = this.listValue.filter((e) => e.EmployeeId !== data.EmployeeId);
                        EventBus.$emit('itemOnSelect', this.listValue, this.id);
                    }
                } else {
                    this.listValue = [];
                    this.listValue.push(data);
                    EventBus.$emit('itemOnSelect', this.listValue, this.id);
                }
            }
        },
        watch: {
            id() {
                //this.listValue = [];
                console.log(this.id);
            }
        },
        created() {
            // xử lí sự kiện load và hiển thị list data 
            // Author HieuNV
            EventBus.$on('showDataList', (positionStr, listData, type, isBatchSelect, value, id) => {
                this.myBatchIndex = -1;
                this.id = id;
                this.listValue = value;
                this.isBatchSelect = isBatchSelect;
                this.isShow = true;
                this.dataListPositionString = positionStr;
                this.type = type;
                if (type == 'user') {
                    this.listItem = [];
                    EmployeeApi.getAll().then(res => {
                        this.listUser = res.data;
                    })
                } else {
                    this.listItem = listData;
                    this.listUser = [];
                }
            });
            // xử lí sự kiện load và hiển thị list data tìm kiếm
            // Author HieuNV
            EventBus.$on('searchDataList', (positionStr, listData, type, isBatchSelect, id, searchTerms) => {
                this.myBatchIndex = -1;
                this.id = id;
                this.isBatchSelect = isBatchSelect;
                this.isShow = true;
                this.dataListPositionString = positionStr;
                this.type = type;
                if (type == 'user') {
                    this.listItem = [];
                    console.log(`searchTerms=${searchTerms}`);
                    if (searchTerms == '' || searchTerms == undefined) {
                        EmployeeApi.getAll().then(res => {
                            this.listUser = res.data;
                        })
                    } else {
                        EmployeeApi.getFilterPaging(`searchTerms=${searchTerms}`).then(res => {
                            this.listUser = res.data;
                        })
                    }
                } else {
                    this.listItem = listData;
                    this.listUser = [];
                }
            });

            // nhận sự kiện ẩn dataList và ẩn
            EventBus.$on('hideDataList', () => {
                this.isShow = false;
            })

            //  Xử lí khi nhận sự kiện bấm nút lên, xuống từ combobox cha
            // Author HieuNV
            EventBus.$on('comboboxOnUpDown', (a, value) => {
                this.listValue = value;
                var myIndex;

                if (this.type !== 'user') {
                    if (this.listValue[0] == '') {
                        const el = this.$el.querySelector(`#item-${0}`);
                        if (el) {
                            el.scrollIntoView();
                        }
                        this.listValue = [];
                        this.listValue.push(this.listItem[0]);
                        EventBus.$emit('itemOnSelect', this.listValue, this.id);
                    } else {
                        myIndex = -1;
                        this.listItem.forEach((item, index) => {
                            if (item[`${this.type}Id`] == this.listValue[0][`${this.type}Id`]) {
                                myIndex = index;
                            }
                        });
                        if (a == 1) {
                            const el = this.$el.querySelector(`#item-${myIndex}`);
                            if (el) {
                                el.scrollIntoView();
                            }
                        } else {
                            const el = this.$el.querySelector(`#item-${myIndex - 2}`);
                            if (el) {
                                el.scrollIntoView();
                            }
                        }

                        if (((myIndex < this.listItem.length - 1) && a == 1) || ((myIndex > 0) && a == -1)) {
                            this.listValue = [];
                            this.listValue.push(this.listItem[myIndex + a]);
                            console.log(this.listValue[0])
                            EventBus.$emit('itemOnSelect', this.listValue, this.id);
                        }
                    }
                } else {

                    if (this.listValue[0] == '') {
                        console.log(2222);
                        const el1 = this.$el.querySelector(`#item-user-${0}`);
                        if (el1) {
                            el1.scrollIntoView();
                        }

                        this.listValue = [];
                        this.listValue.push(this.listUser[0]);
                        EventBus.$emit('itemOnSelect', this.listValue, this.id);
                    } else {
                        console.log(3333);
                        myIndex = -1;
                        this.listUser.forEach((item, index) => {
                            if (item.EmployeeId == this.listValue[0].EmployeeId) {

                                myIndex = index;

                            }
                        });
                        if (a == 1) {
                            const el1 = this.$el.querySelector(`#item-user-${myIndex}`);
                            if (el1) {
                                el1.scrollIntoView();
                            }
                        } else {
                            const el1 = this.$el.querySelector(`#item-user-${myIndex - 2}`);
                            if (el1) {
                                el1.scrollIntoView();
                            }
                        }

                        if (((myIndex < this.listUser.length - 1) && a == 1) || ((myIndex > 0) && a == -1)) {
                            this.listValue = [];
                            this.listValue.push(this.listUser[myIndex + a]);
                            console.log(this.listValue[0])
                            EventBus.$emit('itemOnSelect', this.listValue, this.id);
                        }
                    }
                }
                this.isShow = true;
            });

            //  Xử lí khi nhận sự kiện bấm nút lên, xuống từ combobox chọn nhiều
            // Author HieuNV
            EventBus.$on('comboboxBatchOnUpDown', (a) => {
                if (this.myBatchIndex == -1) {
                    this.myBatchIndex = 0;
                } else {
                    if (((this.myBatchIndex < this.listUser.length - 1) && a == 1) || ((this.myBatchIndex > 0) && a == -1)) {
                        this.myBatchIndex += a;
                    }
                }

                const el = this.$el.querySelector(`#item-user-${this.myBatchIndex}`)
                if (el) {
                    el.scrollIntoView();
                }
            });

            // Xử lí sự kiện chọn hoặc bỏ chọn 1 option từ comboboxBatch
            // Author HieuNV
            EventBus.$on('comboboxBatchOnChoose', () => {
                var data = this.listUser[this.myBatchIndex];
                if (!JSON.stringify(this.listValue).includes(JSON.stringify(data))) {
                    this.listValue.push(data);
                    EventBus.$emit('itemOnSelect', this.listValue, this.id);
                } else {
                    this.listValue = this.listValue.filter((e) => e.EmployeeId !== data.EmployeeId);
                    EventBus.$emit('itemOnSelect', this.listValue, this.id);
                }
            })
        }
    }
</script>


<style scoped>
    .combobox-list-item {
        height: auto;
        max-height: 326px;
        width: 240px;
        border-radius: 4px;
        position: absolute;
        top: 37px;
        left: 0px;
        border: 1px solid #E0E0E0;
        padding: 2px 0px;
        box-sizing: border-box;
        background-color: #fff;
        overflow-y: auto;
        z-index: 230;
        box-shadow: 0px 0px 3px 0px rgba(0,0,0,0.2);
    }

    .combobox-item-user {
        width: 100%;
        height: 66px;
        padding-left: 20px;
        display: flex;
        justify-content: flex-start;
        align-items: center;
        box-sizing: border-box;
        cursor: pointer;
        position: relative;
    }

        .combobox-item-user:hover {
            background-color: #FFEDE2;
        }

            .combobox-item-user:hover .is-check {
                background-color: #FFEDE2;
            }

            .combobox-item {
                width: 100%;
                height: 36px;
                padding-left: 20px;
                text-align: left;
                cursor: pointer;
                padding-left: 20px;
                box-sizing: border-box;
                line-height: 36px;
            }

        .combobox-item:hover {
            background-color: #FFEDE2;
        }

    .user-info {
        height: 100%;
        width: calc(100% - 48px);
        margin-left: 8px;
        display: flex;
        flex-direction: column;
        justify-content: center;
    }

    .user-name, .user-position {
        width: 100%;
        height: 22px;
        line-height: 22px;
        overflow: hidden;
        white-space: nowrap;
        text-overflow: ellipsis;
    }

    .no-data-item {
        height: 36px;
        width: 100%;
        padding-left: 12px;
        box-sizing: border-box;
        color: #ec4405;
        line-height: 36px;
    }

    .item-active {
        background-color: #FFF4EE;
        color: #ec5504;
    }

    .item-user-active {
        background-color: #FFF4EE;
        color: #ec5504;
    }

    .item-batch-active .user-info{
        width: calc(100% - 88px);
    }

    .item-batch-on-index {
        background-color: #FFF4EE;
        color: #ec5504;
    }

    .is-check {
        height: 100%;
        width: 40px;
        position: absolute;
        top: 0px;
        right: 0px;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .filter-combobox-list{
        width: 206px !important;
        max-height: 165px !important;
    }
</style>