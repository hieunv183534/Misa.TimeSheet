<template>
    <div :class="['toast-messenger', `${mesType}` ,{'mes-show': isShowMes,'mes-hide': !isShowMes}]">
        <div v-if="mesType=='danger'" class="mes-icon mes-icon-danger">
            <div class="background-icon position-icon-x-small"></div>
        </div>
        <div v-if="mesType=='success'" class="mes-icon mes-icon-success"></div>
        <div class="mes-text">{{mesText}}</div>
    </div>
</template>


<script>
    export default {
        name: 'ToastMessenger',
        props: {
            mesText: String,
            mesType: String,
            isShowMes: {
                type: Boolean,
                default: false
            }
        },
        watch: {
            // mỗi khi toastMessenger hiện lên thì cho ẩn đi sau 4.2s
            isShowMes() {
                if (this.isShowMes == true) {
                    setTimeout(() => {
                        this.$emit('hideMes');
                    }, 4200);
                }
            }
        }
    }
</script>


<style scoped>

    .toast-messenger {
        width: auto;
        padding-right: 10px;
        height: 40px;
        border-radius: 3px;
        position: fixed;
        bottom: 35px;
        right: 35px;
        box-shadow: 0px 1px 2px 0px rgba(0, 0, 0, 0.3);
        display: flex;
        align-items: center;
        justify-content: flex-start;
        border-left: 4px solid;
        box-sizing: border-box;
        z-index: 380;
    }

    .mes-show {
        transform: translate(0, 0);
        opacity: 1;
        transition: 0.7s;
    }

    .mes-hide {
        transform: translate(100%, 0);
        opacity: 0;
        transition: 0.7s;
    }

    .mes-icon {
        width: 16px;
        height: 16px;
        margin-left: 8px;
        margin-right: 10px;
    }

    .mes-icon-danger {     
        border-radius: 50%;
        display: flex;
        align-items: center;
        justify-content: center;
        background-color: #EF292F;
    }

    .mes-icon-success {
        -webkit-mask-image: url('../../assets/image/Sprites.64af8f61.svg');
        background-repeat: no-repeat;
        background-color: #11AA7A;
        -webkit-mask-position: -512px -408px;
    }

    .mes-text {
        line-height: 40px;
        color: #363A36;
    }


    .danger { 
        border-left-color: #EF292F !important;
        background-color: #FFE6EA;
    }


    .success {
        border-left-color: #11AA7A !important;
        background-color: #E6FFE1;
    }

</style>