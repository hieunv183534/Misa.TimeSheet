<template>
    <div class="popup">
        <div class="popup-modal"></div>
        <div class="popup-content">
            <div class="popup-header">
                <div class="popup-title">{{popupType}}</div>
                <div class="btn-x" @click="hidePopup">
                    <div class="background-icon position-icon-x-big"></div>
                </div>
            </div>
            <div class="popup-body">
                <div class="popup-text">{{popupText}}</div>
            </div>
            <div class="popup-footer">
                <Button v-if="popupBtns.includes('1')" buttonText="Hủy" buttonType="secondary" @btnClick="$emit('popupOnConfirm','1')"/>
                <Button v-if="popupBtns.includes('2')" buttonText="Không lưu" buttonType="secondary" @btnClick="$emit('popupOnConfirm','2')" />
                <Button v-if="popupBtns.includes('3')" buttonText="Lưu" buttonType="primary"  @btnClick="$emit('popupOnConfirm','3')"/>
                <Button v-if="popupBtns.includes('4')" buttonText="Xóa" buttonType="danger"  @btnClick="$emit('popupOnConfirm','4')"/>
                <Button v-if="popupBtns.includes('5')" buttonText="Đóng" buttonType="primary" @btnClick="$emit('popupOnConfirm','5')"/>
                <Button v-if="popupBtns.includes('6')" buttonText="Từ chối" buttonType="primary" @btnClick="$emit('popupOnConfirm','6')"/>
                <Button v-if="popupBtns.includes('7')" buttonText="Duyệt" buttonType="primary" @btnClick="$emit('popupOnConfirm','7')"/>
            </div>
        </div>
    </div>
</template>


<script>
    import Button from "./Button.vue"
    export default {
        name: 'Popup',
        components: {
            Button
        },
        props: {
            popupText: {
                type: String,
                default: ''
            },
            popupType: {
                type: String,
                default: 'Cảnh báo'
            },
            popupBtns: {
                type: String,
                default: ''
            }
        },
        methods: {
            /**
             * Ẩn popup
             * */
            hidePopup() {
                this.$emit('hidePopup')
            },
        }
    }
</script>


<style scoped>
    .popup-modal {
        position: fixed;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background-color: #000;
        opacity: 0.4;
        z-index: 280;
    }

    .popup-content {
        z-index: 281;
        position: fixed;
        top: 182px;
        left: 50%;
        transform: translate(-50%, -50%);
        width: 415px;
        min-height: 190px;
        height: auto;
        background-color: #fff;
        padding: 32px;
        box-sizing: border-box;
        border-radius: 4px;
        padding: 0px 24px;
    }

    .popup-header {
        width: 100%;
        height: 53px;
        float: left;
        display: flex;
        align-items: flex-end;
        position: relative;
    }

    .popup-title {
        line-height: 29px;
        font-size: 24px;
        letter-spacing: 0.576px;
        font-weight: 800;
        color: rgb(33,33,33);
    }

    .btn-x {
        width: 36px;
        height: 36px;
        border-radius: 50%;
        position: absolute;
        top: 21px;
        right: 0px;
        display: flex;
        align-items: center;
        justify-content: center;
        cursor: pointer;
        background-color: #EFF1F6;
    }

        .btn-x:hover {
            background-color: #EFF1F6;
        }

    .popup-body {
        width: 100%;
        height: auto;
        float: left;
        padding: 24px 0px;
        overflow: auto;
        box-sizing: border-box;
    }

    .popup-text {
        line-height: 20px;
        width: 100%;
    }

    .popup-footer {
        height: 68px;
        float: left;
        width: 100%;
        display: flex;
        align-items: center;
        justify-content: flex-end;
    }

        .popup-footer button {
            margin-left: 10px;
        }
</style>