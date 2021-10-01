<template>
    <div :class="['status-block',{'color1':statusValue=='Chờ duyệt','color2':statusValue=='Đã duyệt','color3':statusValue=='Từ chối'}]">{{statusValue}}</div>
</template>


<script>
export default {
    name:'Name',
    props:{
        statusValue: {
            type: String,
            default: ''
        }
    },
}
</script>


<style scoped>
    .status-block{
        height: 31px;
        line-height: 31px;
        display: flex;
        justify-content: center;
        padding: 0px 6px;
        border-radius: 4px;
        margin-top: 9px;
    }

    .color1 {
        background-color: #D0DEFB;
        color: #5259EA;
        width: 76px !important;
    }
    .color2 {
        background-color: #B9F8E4;
        color: #11AA7A;
        width: 68px !important;
    }
    .color3 {
        background-color: #FBD0D1;
        color: #F0295E;
        width: 60px !important;
    }
</style>