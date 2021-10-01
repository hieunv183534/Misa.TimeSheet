<template>
    <div class="paging-bar">
        <div class="paging-left">Tổng số bản ghi: <span>{{totalRecord}}</span></div>
        <div class="paging-right">
            <div class="dropdown-paging-size">
                <button class="dropdown-main" @click="showDropdownList" @blur="hideDropdownList">
                    <p>{{pagingSize}}</p>
                    <div class="dropdown-icon background-icon position-icon-chevron-down"></div>
                </button>
                <div v-if="isShowDropdownList" class="dropdown-list">
                    <div v-for="item in pagingSizeList"
                         :key="item"
                         :class="['dropdown-item',{'dropdown-item-active': pagingSize == item }]"
                         @click="changePagingSize(item)">
                        {{item}}
                        <div v-if="pagingSize == item" class="tick-icon background-icon position-icon-tick"></div>
                    </div>
                </div>
            </div>
            <div class="page-info">Từ <span>{{startRecord}}</span> đến <span>{{endRecord}}</span> bản ghi</div>
            <button class="pre-page" @click="preCurrentPage" :disabled="currentPage==1">
                <div :class="['background-icon','position-icon-chevron-left', {'disabled-btn':currentPage==1 }] "></div>
            </button>
            <button class="next-page" @click="nextCurrentPage" :disabled="currentPage==totalPage">
                <div :class="['background-icon','position-icon-chevron-right', {'disabled-btn':currentPage==totalPage } ]"></div>
            </button>
        </div>
    </div>
</template>


<script>
    export default {
        name: 'PagingBar',
        props: {
            totalRecord: {
                default: 0,
                type: Number
            },
            pagingSize: {
                default: 10,
                type: Number
            },
            currentPage: {
                default: 1,
                type: Number
            }
        },
        data() {
            return {
                pagingSizeList: [10, 20, 30, 50, 100],
                startRecord: 0,
                endRecord: 0,
                totalPage: 0,
                isShowDropdownList: false
            }
        },
        methods: {
            /**
             * load lại các data hiển thị đi kèm của paging khi được gọi đến
             * Author HieuNV
             * */
            reloadPagingBar() {
                // load giá trị start và end r
                this.startRecord = this.pagingSize * (this.currentPage - 1) + 1;
                this.endRecord = this.startRecord + this.pagingSize - 1;
                if (this.endRecord > this.totalRecord) {
                    this.endRecord = this.totalRecord;
                }
                // tính toán mảng các choose paging từ các dữ liệu
                this.totalPage = Math.ceil(this.totalRecord / this.pagingSize);
            },
            /**
             * Sự kiện hiển thị list các pagingSize khi click vào btn option
             * Author HieuNV
             * */
            showDropdownList() {
                this.isShowDropdownList = true;
            },
            /**
             * Sự kiện ẩn list pagingSize
             * Author HieuNV
             * */
            hideDropdownList() {
                setTimeout(() => {
                    this.isShowDropdownList = false;
                }, 200)
            },
            /**
             * Thay dổi pagingSize
             * @param newPagingSize
             * Author HieuNV
             */
            changePagingSize(newPagingSize) {
                this.$emit('changePagingSize', newPagingSize);
                this.$emit('changeCurrentPage', 1);
            },
            /**
             * trở về trang trước đó
             * Author HieuNV
             * */
            preCurrentPage() {
                this.$emit('changeCurrentPage', this.currentPage - 1);
            },
            /**
             * đến trang tiếp theo
             * Author HieuNV
             * */
            nextCurrentPage() {
                this.$emit('changeCurrentPage', this.currentPage + 1);
            }
        },
        watch: {
            totalRecord() {
                this.reloadPagingBar();
            },
            pagingSize() {
                this.reloadPagingBar();
            },
            currentPage() {
                this.reloadPagingBar();
            },
        },
        created() {
            this.reloadPagingBar();
        }
    }
</script>


<style scoped>
    .paging-bar {
        float: left;
        width: 100%;
        height: 61px;
        padding: 0px 16px;
        border: 1px solid #E0E0E0;
        border-bottom-left-radius: 3px;
        border-bottom-right-radius: 3px;
        background-color: #fff;
        box-sizing: border-box;
        display: flex;
        justify-content: space-between;
    }

    .paging-left {
        font-size: 14px;
        letter-spacing: 0.14px;
        line-height: 61px;
    }

        .paging-left span {
            font-weight: 700;
            margin-left: 3px;
        }

    .paging-right {
        width: auto;
        height: 100%;
        display: flex;
        justify-content: flex-end;
        align-items: center;
    }


    .dropdown-paging-size {
        width: 75px;
        height: 36px;
        position: relative;
        box-sizing: border-box;
    }

    .dropdown-main {
        width: 100%;
        height: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
        border: 1px solid #DADCE3;
        box-sizing: border-box;
        border-radius: 4px;
        cursor: pointer;
        font-weight: 500;
    }


    .dropdown-icon {
        margin-left: 10px;
    }

    .dropdown-list {
        position: absolute;
        bottom: 38px;
        right: 0px;
        width: 88px;
        height: auto;
        padding: 8px;
        box-sizing: border-box;
        box-shadow: 0px 0px 4px 0px rgba(0,0,0,0.3);
        border-radius: 4px;
        background-color: #fff;
        z-index: 21;
    }

    .dropdown-item {
        width: 100%;
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

        .dropdown-item:hover {
            background-color: #FFEDE2;
        }

    .dropdown-item-active {
        background-color: #FFEDE2;
        color: #EF5504;
    }

    .pre-page, .next-page {
        width: 24px;
        height: 24px;
        cursor: pointer;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .next-page {
        margin-left: 20px;
    }

    .page-info {
        line-height: 61px;
        width: auto;
        line-height: 61px;
        font-size: 14px;
        letter-spacing: 0.14px;
        padding: 0px 16px;
    }

        .page-info span {
            font-weight: 700;
        }

    .disabled-btn {
        opacity: 0.4 !important;
    }
</style>