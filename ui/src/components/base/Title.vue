<template>
    <span>
        <div v-if="isShow" class="title" :style="positionString">
            <div class="title-text">{{text}}</div>
        </div>
        <div v-if="isShow" class="nobody" :style="positionString"></div>
    </span>

</template>


<script>
    import { EventBus } from "../../main.js"

    export default {
        name: 'Title',
        data() {
            return {
                isShow: false,
                positionString: '',
                text: ''
            }
        },
        created() {
            // nhận và xử lí sự kiện hiẻn thị title
            EventBus.$on('showTitle', (text, positionStr) => {
                this.isShow = true;
                this.text = text;
                this.positionString = positionStr;
            })

            // nhận và xử lí sự kiện ẩn title
            EventBus.$on('hideTitle', () => {
                this.isShow = false;
            })
        }
    }
</script>


<style scoped>

    .title {
        z-index: 450;
        height: 32px;
        padding: 0px 12px;
        position: fixed;
        background-color: #000;
        color: #fff;
        transform: translate(-50%, -12px);
        border-radius: 4px;
    }

    .title-text {
        white-space: nowrap;
        line-height: 32px;
        font-size: 12px;
    }

    .nobody {
        position: fixed;
        width: 12px;
        height: 12px;
        background-color: #000;
        transform: translate(-6px, 14px) rotate(45deg);
        z-index: 451;
    }
</style>