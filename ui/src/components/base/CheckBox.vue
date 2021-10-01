<template>
    <div class="checkbox" :class="{'checkbox-check': isCheck }" @click="clickCheckBox">
        <div v-if="isCheck" class="check-icon background-icon position-icon-checkbox">
        </div>
    </div>
</template>


<script>

    export default {
        name: 'Checkbox',
        props: {
            // truyền giá trị true/false
            checkOrNo: {
                type: Boolean,
                default: false
            }
        },
        data() {
            return {
                isCheck: false
            }
        },
        methods: {
            /**
             * Sự kiện khi click vào checkbox
             * Author HieuNV
             * */
            clickCheckBox() {
                // thay đổi trang thái
                this.isCheck = !this.isCheck;
                if (this.isCheck) {
                    this.$emit('check')
                } else {
                    this.$emit('uncheck')
                }
            }
        },
        created() {
            this.isCheck = this.checkOrNo;
        },
        watch: {
            checkOrNo() {
                this.isCheck = this.checkOrNo;
            }
        }
    }
</script>


<style scoped>
    .checkbox {
        width: 16px;
        height: 16px;
        border: 2px solid #737888;
        border-radius: 4px;
        background-color: #fff;
        box-sizing: border-box;
        overflow:hidden;
        cursor: pointer;
    }

    .checkbox-check {
        background-color: #fff;
        border-color: #EC5504;
        border: 0;
    }
</style>