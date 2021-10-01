<template>
    <button :class="[buttonType, {'p-lr-8': buttonText == ''}]" @click="$emit('btnClick')">
        <div class="icon-button background-icon" :class="iconBtnClass"></div> 
        <p v-if="buttonText !== ''" class="button-text">{{buttonText}}</p>
    </button>
</template>


<script>
export default {
    name:'ButtonIcon',
    props:{
        buttonText: {
            type: String,
            default: ''
        },
        buttonType: {
            type: String,
            default: ''
        },
        iconBtnClass: {
            type: String,
            default: '',
            requied: true
        }
    }
}
</script>


<style scoped>
    button{
        border: 0;
        outline: 0;
        background-color: #fff;
        margin:0;
        padding: 0;
        height: 36px;
        width: auto;
        min-width: 36px;
        padding: 0px 12px;
        display: flex;
        align-items: center;
        justify-content: center;
        border-radius: 4px;
    }

    .button-text{
        font-size: 14px;
        font-weight: 500;
        margin-left: 8px;
        white-space: nowrap;
    }

    .primary {
        background-color: #EC5504;
        color: #fff;
    }

        .primary:hover {
            background-color: #FA6000;
        }

        .primary:focus {
            background-color: #FA6000;
        }

    .secondary {
        background-color: #fff;
        border: 1px solid #DADCE3;
        color: #212121;
    }

        .secondary:hover {
            background-color: #EFF1F6;
        }

        .secondary:focus {
            background-color: #EFF1F6;
        }

    .danger {
        background-color: #fff;
        border: 1px solid #D81010;
        color: #F44936;
    }

        .danger:hover {
            background-color: #FEE8E7;
        }

        .danger:focus {
            background-color: #FEE8E7;
        }

    .success {
        background-color: #fff;
        border: 1px solid #00C853;
        color: #00C853;
    }

        .success:hover {
            background-color: #B9F8E4;
            border-color: #0F996D;
        }

        .success:focus {
            background-color: #B9F8E4;
            border-color: #0F996D;
        }

        .p-lr-8{
            padding-left: 8px !important;
            padding-right: 8px !important;
        }
</style>