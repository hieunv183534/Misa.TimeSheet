<template>
    <button :class="buttonType" @click="$emit('btnClick')">
        <p class="button-text">{{buttonText}}</p>
    </button>
</template>


<script>
export default {
    name:'Button',
    props:{
        buttonText: {
            type: String,
            default: ''
        },
        buttonType: {
            type: String,
            default: ''
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
        min-width: 80px;
        padding: 0px 12px;
        display: flex;
        align-items: center;
        justify-content: center;
        border-radius: 4px;
    }

    .button-text{
        font-size: 14px;
        font-weight: 500;
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
        background-color: #EF292F;
        color: #fff;
    }

        .danger:hover {
            background-color: #ED1219;
        }
        
        .danger:focus {
            background-color: #ED1219;
        }
</style>