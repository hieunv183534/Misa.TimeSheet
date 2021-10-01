<template>
    <input type="text"
           :class="[inputClass,{'border-red': isBorderRed}]"
           :placeholder="inputPlacehoder"
           :value="inputValue"
           @input="[$emit('inputOnInput'), $emit('input', $event.target.value)]"
           @keyup.enter="$emit('inputOnEnter')"/>
</template>


<script>
    export default {
        name: 'Input',
        props: {
            inputClass: String,
            inputPlacehoder: String,
            inputValue: String,
            isBorderRed: {
                type: Boolean,
                default: false
            }
        }
    }
</script>


<style scoped>
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

    .border-red {
        border-color: #FF6161 !important;
    }

    input:focus {
        border-color: #EC5504;
    }

    input:hover {
        border-color: #EC5504;
    }

    input.input-search {
        padding: 6px 8px 6px 36px;
        width: 100% !important;
    }

        input.input-search::placeholder {
            font-family: Roboto-Regular;
            font-size: 14px;
            color: #999;
        }

    .w-100 {
        width: 100% !important;
    }
</style>