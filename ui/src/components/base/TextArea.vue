<template>
    <div :class="['textarea-wrapper', {'focus-border' : onFocus , 'border-red': isBorderRed}]">
        <textarea cols="28" v-model="textValue" @focus="focusOn" @blur="focusOff"></textarea>
    </div>
</template>


<script>
    export default {
        name: 'TextArea',
        props: {
            value: {
                type: String,
                default: ''
            },
            isBorderRed: {
                type: Boolean,
                default: false
            }
        },
        data() {
            return {
                onFocus: false,
                textValue: ''
            }
        },
        methods: {
            focusOn() {
                this.onFocus = true;
            },
            focusOff() {
                this.onFocus = false;
            }
        },
        created() {
            this.textValue = this.value;
        },
        watch: {
            value() {
                this.textValue = this.value;
            },
            textValue() {
                this.$emit('input', this.textValue);
            }
        }
    }
</script>


<style scoped>
    .textarea-wrapper {
        width: 240px;
        height: 87px;
        padding: 6px 8px;
        border-radius: 4px;
        border: 1px solid #e0e0e0;
        box-sizing: border-box;
    }
    .border-red {
        border-color: #FF6161 !important;
    }

    .textarea-wrapper:hover{
        border-color: #ec4405;
    }

    .focus-border {
        border-color: #ec4405;
    }

    textarea {
        width: 100%;
        height: 100%;
        overflow-y: auto;
        background-color: #fff;
        border: none;
        resize: none;
        outline: none;
        font-family: Roboto-Regular;
        font-size: 14px;
        line-height: 18px;
    }
</style>