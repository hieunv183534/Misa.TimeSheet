<template>
    <div :class="['loader', {'show-loader' :isShow}]">
        <div class="loader-modal"></div>
        <div class="loader-icon"></div>
    </div>
</template>


<script>
export default {
    name:'Loader',
    props:{
        isShow: {
            type: Boolean,
            default: false
        }
    },
}
</script>


<style scoped>
    .loader{
        z-index: 450;
        visibility: hidden;
    }


    .loader-modal {
        position: fixed;
        top: 0;
        bottom: 0;
        left: 0;
        right: 0;
        background-color: #fff;
        opacity: 0.3;
        z-index: 444;
    }

    .show-loader{
        visibility: visible !important;
    }

    @keyframes rotate {
        0% {
            transform: rotate(0);
        }

        100% {
            transform: rotate(360deg);
        }
    }

    .loader-icon {
        width: 118px;
        height: 118px;
        position: fixed;
        margin-left: -59px;
        margin-top: -59px;
        top: 50%;
        left: 50%;
        background-image: url('../../assets/image/loader.svg');
        background-repeat: no-repeat;
        background-size: contain;
        z-index: 445;
        animation: rotate 0.9s linear infinite;
    }
</style>