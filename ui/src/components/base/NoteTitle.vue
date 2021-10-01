<template>
    <div v-if="titleValue!==''" class="note-title" :style="`right: ${right}px;`">
        <div ref="thisEle" class="note-icon background-icon position-icon-note" @mouseover="showTitle" @mouseleave="hideTitle"></div>
    </div>
</template>


<script>
    import { EventBus } from "../../main.js"

    export default {
        name: 'NoteTitle',
        props: {
            titleValue: {
                type: String,
                default: ''
            },
            right: {
                type: String,
                default: ''
            }
        },
        methods: {
            /**
             * Sự kiện gọi đến title để hiển thị ghi chú
             * Author HieuNV
             * */
            showTitle() {
                var left = this.$refs.thisEle.getBoundingClientRect().left + 10;
                var top = this.$refs.thisEle.getBoundingClientRect().top - 32;
                var positionStr = "top: " + top + "px; left: " + left + "px;";
                EventBus.$emit('showTitle', this.titleValue, positionStr)
            },
            /**
             * ẩn đi title
             * */
            hideTitle() {
                EventBus.$emit('hideTitle')
            }
        }
    }
</script>


<style scoped>

    .note-title {
        width: 20px;
        height: 20px;
        position: absolute;
        top: 50%;
        transform: translate(0, -50%);
        display: flex;
        justify-content: center;
        align-items: center;
        z-index: 400;
    }

    .note-icon {
        position: relative;
        overflow: visible;
    }
</style>