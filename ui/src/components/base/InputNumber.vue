<template>
    <div class="input-number-wrapper">
        <input type="text"
               v-model="iValue" 
               @input="[$emit('inputOnInput'), $emit('input', $event.target.value)]"/>
        <div class="up-number background-icon position-icon-angle-down" @click="upNumber"></div>
        <div class="down-number background-icon position-icon-angle-down" @click="downNumber"></div>
    </div>
</template>


<script>
    export default {
        name: 'InputNumber',
        props: {
            value: {
                type: String,
                default: '0'
            },
        },
        data() {
            return {
                iValue: '0'
            }
        },
        created() {
            this.iValue = this.value;
        },
        watch: {
            value() {
                this.iValue = this.value;
            },
        },
        methods: {
            /**
             * tăng 1 đơn vị khi click up
             * Author HieuNV
             * */
            upNumber() {
                this.iValue = parseInt(this.iValue)+1;
                console.log(this.iValue);
            },
            /**
             * giảm một đơn vị khi click down
             * Author HieuNV
             * */
            downNumber() {
                if (this.iValue > 0) {
                    this.iValue = parseInt(this.iValue) - 1;
                    console.log(this.iValue);
                }
            }
        }
    }
</script>


<style scoped>

    .input-number-wrapper{
        position: relative;
        width: auto;
        height:auto;
    }

        .input-number-wrapper .background-icon{
            cursor: pointer;
        }

        input {
            width: 240px;
            height: 36px;
            padding: 6px 8px;
            border-radius: 4px;
            outline: none;
            color: #111111;
            border: solid 1px #e0e0e0;
            box-sizing: border-box;
            font-family: Roboto-Regular;
            font-size: 13px;
        }

        input:focus {
            border-color: #EC5504;
        }

        input:hover {
            border-color: #EC5504;
        }

    .up-number {
        position: absolute;
        top: 5px;
        right: 2px;
        transform: rotate(180deg);
    }

    .down-number {
        position: absolute;
        top: 18px;
        right: 2px;
    }
</style>