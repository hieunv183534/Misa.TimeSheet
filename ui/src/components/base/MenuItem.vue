<template>
    <button @blur="hideListItem" @click="showListItem" class="menu-item">
        <div class="menu-item-display">{{menuItemText}}</div>
        <div v-if="menuItemType=='more'" class="menu-item-more-option background-icon position-icon-chevron-down"></div>
        <div v-if="menuItemType=='more' && isShowListItem" class="menu-item-list-option">
            <div v-for="(item,index) in moreListItem" :key="index" class="item-menu">{{item.itemText}}</div>
        </div>
    </button>
</template>


<script>
    export default {
        name: 'MenuItem',
        props: {
            menuItemText: String,
            menuItemType: String,
            moreListItem: Array
        },
        data() {
            return {
                isShowListItem : false
            }
        },
        methods: {
            /**
             * Hiển thị list menu con
             * */
            showListItem() {
                this.isShowListItem = true;
            },
            /**
             * Ẩn list menu con
             * */
            hideListItem() {
                setTimeout(() => {
                    this.isShowListItem = false;
                },200)
            }
        }
    }
</script>


<style scoped>
    .menu-item {
        margin:0;
        padding: 0;
        border:0;
        outline: 0;
        background-color: transparent;
        width: auto;
        height: 100%;
        display: flex;
        align-items:center;
        margin-right: 30px;
        cursor:pointer;
        position: relative;
    }

    .menu-item-display{
        width: auto;
        height: 100%;
        line-height: 56px;
        border-bottom: 4px solid transparent;
        font-size: 14px;
        box-sizing:border-box;
        white-space: nowrap;
        text-overflow: ellipsis;
    }

    .menu-item:focus .menu-item-display {
        font-weight: 600;
        border-bottom-color: #EC5504;
    }

    .menu-item:hover .menu-item-display {
        border-bottom-color: #EC5504;
    }

    .menu-item-more-option{
        margin-left: 10px;
        margin-right: 10px;
    }

    .menu-item-list-option {
        width: 200px;
        height: auto;
        padding: 12px 8px;
        box-sizing: border-box;
        border-radius: 4px;
        position: absolute;
        top: 52px;
        left: 0px;
        background-color: #fff;
        z-index: 10;
        box-shadow: 0px 0px 4px 0px rgba(0,0,0,0.1);
    }

    .item-menu{
        text-align: left;
        width: 100%;
        height:37px;
        line-height: 37px;
        padding: 0 8px;
        box-sizing: border-box;
        border-radius: 4px;
    }

        .item-menu:hover {
            background-color: #FFEDE2;
        }
</style>