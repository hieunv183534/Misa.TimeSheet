<template>
    <span>
        <div v-if="isShow" class="pointer" :style="gridSettingPositionString"></div>
        <div class="grid-setting" v-if="isShow" :style="gridSettingPositionString">
            <div class="grid-setting-header">
                <div class="title-grid-setting">Tùy chỉnh cột</div>
                <div class="btn-x background-icon position-icon-x-big" @click="hideGridSetting"></div>
            </div>
            <input type="text" placeholder="Tìm kiếm" class="search-column" v-model="searchTerms" ref="mySearch"/>
            <div class="grid-setting-body">
                <draggable :list="myColumns" ghost-class="ghost" @start="drag=true" @end="drag=false">
                    <div class="column-item" v-for="column in myColumns" :key="column.DataField" v-show="inShowList(column.Caption)">
                        <CheckBox :checkOrNo="column.Enable" @check="check(column)" @uncheck="uncheck(column)"/>
                        <div class="display-column">{{column.Caption}}</div>
                        <div v-if="column.Enable" 
                             :class="['pin-col background-icon',{'position-icon-pin':column.Fixed, 'position-icon-not-pin' : !column.Fixed }]"
                             @click="btnPinOnClick(column)"></div>
                        <div class="icon-draggable background-icon position-icon-draggable"></div>
                    </div>
                </draggable>

            </div>
            <div class="grid-setting-footer">
                <Button buttonText="Mặc định" buttonType="secondary" @btnClick="refresh"/>
                <Button buttonText="Lưu" buttonType="primary" @btnClick="btnSaveOnClick"/>
            </div>
        </div>
    </span>
</template>


<script>
    import draggable from "vuedraggable";
    import { EventBus } from "../../main.js"
    import Button from "./Button.vue"
    import CheckBox from "./CheckBox.vue"
    import { headers } from "../../model/listDatas.js"

    export default {
        name: 'GridSetting',
        components: {
            Button, CheckBox, draggable
        },
        data() {
            return {
                searchTerms: '',
                isShow: false,
                gridSettingPositionString: '',
                myColumns: [],
            }
        },
        created() {
            // khởi tạo list
            this.myColumns = JSON.parse(JSON.stringify(headers));

            // xử lí sự kiện hiển thị gridSetting
            EventBus.$on('showGridSetting', (positionStr) => {
                this.gridSettingPositionString = positionStr;
                this.isShow = true;
                setTimeout(() => {
                    this.$refs.mySearch.focus();
                }, 100);
            })

            // Xử lí sự kiện ẩn gridSetting
            EventBus.$on('hideGridSetting', () => {
                this.isShow = false;
            })
        },
        watch: {
            myColumns() {
                /*this.change();*/
                console.log(1);
            }
        },
        methods: {
            /**
             * xác định columns này ẩn hay hiện theo từ khóa người dùng search
             * @param colDisplay
             * Author HieuNV
             */
            inShowList(colDisplay) {
                return colDisplay.toLowerCase().includes(this.searchTerms.toLowerCase());
            },
            /**
             * Sự kiện khi người dùng chọn hiển thị cột này
             * @param col
             * Author HieuNV
             */
            check(col) {
                col.Enable = true;
                this.change();
            },
            /**
             * Sự kiện khi người dùng chọn không hiển thị cột này
             * @param col
             * Author HieuNV
             */
            uncheck(col) {
                col.Enable = false;
                this.change();
            },
            /**
             * sắp xếp lại các cột ghim lên trước và các cột không ghim về sau
             * Author HieuNV
             * */
            change() {
                console.log(JSON.stringify(this.myColumns));
                var pinList = [];
                var notPinList = [];
                this.myColumns.forEach(col => {
                    if (col.Fixed) {
                        pinList.push(col);
                    } else {
                        notPinList.push(col);
                    }
                });
                this.myColumns = JSON.parse(JSON.stringify(pinList.concat(notPinList)));
            },
            /**
             * Sự kiện reset lại các cột như mặc định
             * Author HieuNV
             * */
            refresh() {
                this.myColumns = JSON.parse(JSON.stringify(headers));
            },
            /**
             * ẩn đi gridSetting
             * */
            hideGridSetting() {
                EventBus.$emit('hideThisGridSetting');
            },
            /**
             * Sự kiển chọn ghim hoặc bỏ ghim 1 cột
             * Author HieuNV
             * @param col
             */
            btnPinOnClick(col) {
                col.Fixed = !col.Fixed;
                this.change();
            },
            /**
             * Sự kiện lưu và áp dụng setting các cột hiện tại
             * Author HieuNV
             * */
            btnSaveOnClick() {
                var enableList = [];
                this.myColumns.forEach(col => {
                    if (col.Enable) {
                        enableList.push(col);
                    }
                });
                this.$emit('changeGridHeaders', JSON.parse(JSON.stringify(enableList)));
                console.log(JSON.parse(JSON.stringify(enableList)));
                EventBus.$emit('hideThisGridSetting');
            }
        }
    }
</script>


<style scoped>
    .grid-setting {
        width: 260px;
        height: auto;
        border-radius: 5px;
        background-color: #fff;
        box-shadow: 0px 0px 4px 0px rgba(0,0,0,0.2);
        z-index: 350;
        position: fixed;
        padding: 0px 8px 10px 10px;
        box-sizing: border-box;
    }

    .pointer {
        width: 14px;
        height: 14px;
        background-color: #fff;
        z-index: 351;
        position: fixed;
        transform: rotate(45deg) translate(21px, -29px);
        border-left: 1px solid rgba(0,0,0,0.08);
        border-top: 1px solid rgba(0,0,0,0.08);
    }

    .grid-setting-header {
        width: 100%;
        height: 56px;
        float: left;
        padding: 0px 16px 14px 12px;
        box-sizing: border-box;
        display: flex;
        align-items: flex-end;
        justify-content: space-between;
    }

    .title-grid-setting {
        font-size: 18px;
        font-weight: 600;
    }

    .btn-x {
        margin-bottom: 3px;
        cursor: pointer;
    }

    .search-column {
        border: 0;
        outline: 0;
        width: 214px;
        height: 36px;
        border: 1px solid #e0e0e0;
        border-radius: 4px;
        margin-left: 12px;
        margin-bottom: 16px;
        padding: 0px 16px;
        box-sizing: border-box;
    }

        .search-column:hover {
            border-color: #ec5504;
        }

        .search-column:focus {
            border-color: #ec5504;
        }

        .search-column::placeholder {
            color: #e0e0e0;
            font-size: 15px;
        }

    .grid-setting-body {
        width: 100%;
        height: 300px;
        float: left;
        overflow: auto;
        height: auto;
        max-height: 300px;
    }

    .column-item {
        width: 100%;
        height: 40px;
        border-radius: 4px;
        display: flex;
        float: left;
        align-items: center;
        justify-content: flex-start;
        padding-left: 16px;
        box-sizing: border-box;
        position: relative;
    }

        .column-item:hover {
            background-color: #EBF0FF;
        }

    .display-column {
        margin-left: 10px;
    }

    .icon-draggable {
        position: absolute;
        right: 8px;
        cursor: move;
    }

    .pin-col {
        position: absolute;
        right: 40px !important;
        cursor: pointer;
    }

    .position-icon-not-pin{
        visibility: hidden;
    }

    .column-item:hover .position-icon-not-pin{
        visibility: visible;
    }

    .grid-setting-footer {
        width: 100%;
        height: 53px;
        border-top: 1px solid #e0e0e0;
        display: flex;
        align-items: flex-end;
        justify-content: center;
        float: left;
        margin-top: 4px;
    }

        .grid-setting-footer button:last-child {
            margin-left: 16px;
        }

        .grid-setting-footer button {
            width: 90px !important;
        }
</style>