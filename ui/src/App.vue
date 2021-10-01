<template>
    <div id="app">
        <div class="menu-bar">
            <div class="menu-bar-left">
                <div class="toggle-btn">
                    <div class="background-icon position-icon-toggle-menu"></div>
                </div>
                <div class="logo position-icon-logo-time-keeping"></div>
                <p class="title-for-logo">Chấm công</p>
                <div class="menu-list">
                    <MenuItem v-for="(item,index) in myMenuList" :key="index"
                              :menuItemText="item.menuItemText"
                              :menuItemType="item.menuItemType"
                              :moreListItem="item.moreListItem"
                              />
                </div>
            </div>
            <div class="menu-bar-right">
                <div class="user-option">
                    <div class="background-icon position-icon-notification"></div>
                </div>
                <div class="user-option">
                    <div class="background-icon position-icon-info"></div>
                </div>
                <div class="user-option">
                    <div class="background-icon position-icon-more-option"></div>
                </div>
                <CompactAvatar userName="Nguyễn Văn Hiếu" size="32"/>
            </div>
        </div>
        <RestPermission />
    </div>
</template>

<script>
    import { menuList } from "./model/menuList.js"
    import MenuItem from "./components/base/MenuItem.vue"
    import CompactAvatar from "./components/base/CompactAvatar.vue"
    //import TextFile from "./components/base/TextFile.vue"
    import RestPermission from "./view/RestPermission.vue"

    export default {
        name: 'App',
        components: {
            MenuItem, RestPermission, CompactAvatar, /*TextFile*/
        },
        data() {
            return {
                myMenuList: menuList,
                //mesType: "danger",
                //mesText: "Bạn không có quyền thực hiện chức năng này",
                //isShowMes: true
            }
        },
        methods: {
            changePagingSize(newPagingSize) {
                this.pagingSize = newPagingSize;
            },
            changeCurrentPage(newCurrentPage) {
                this.currentPage = newCurrentPage;
            }
        }
    }
</script>

<style>
    @import url('css/icon-position.css');

    @font-face {
        font-family: 'Roboto-Regular';
        src: url('assets/font/Roboto-Regular.ttf') format('truetype');
    }

    @font-face {
        font-family: 'Roboto-Italic';
        src: url('assets/font/Roboto-Italic.ttf') format('truetype');
    }

    @font-face {
        font-family: 'Roboto-Bold';
        src: url('assets/font/Roboto-Bold.ttf') format('truetype');
    }

    @font-face {
        font-family: 'Roboto-BoldItalic';
        src: url('assets/font/Roboto-BoldItalic.ttf') format('truetype');
    }

    body * {
        font-family: Roboto,Helvetica,Arial,sans-serif;
    }

    ::-webkit-scrollbar {
        width: 8px;
        border-radius: 4px;
        background-color: #F1F1F1;
    }

    ::-webkit-scrollbar-thumb {
        border-radius: 4px;
        background-color: #9E9E9E;
    }

        ::-webkit-scrollbar-thumb:hover {
            background-color: #757575;
        }

    ::-webkit-scrollbar:horizontal {
        height: 8px;
        border-radius: 4px;
        background-color: #F1F1F1;
    }

    ::-webkit-scrollbar-thumb:horizontal {
        border-radius: 4px;
        background: #9E9E9E;
    }

        ::-webkit-scrollbar-thumb:horizontal:hover {
            background: #757575;
        }

    body {
        padding: 0;
        margin: 0;
        font-family: Roboto-Regular;
        color: #212121;
        font-size: 14px;
    }

    button{
        cursor: pointer;
        padding: 0; 
        margin: 0;
        outline: 0;
        border: 0;
        background-color: #fff;
    }

    .menu-bar {
        width: 100%;
        height: 56px;
        border-bottom: 1px solid #DBDCDF;
        box-shadow: 0px 1px 3px 0px rgba(0, 0, 0, 0.5);
        display: flex;
        align-items: center;
        justify-content: space-between;
    }

    .menu-bar-left {
        height: 100%;
        width: auto;
        display: flex;
        align-items: center;
        justify-content: flex-start;
    }

    .toggle-btn {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 37px;
        height: 37px;
        border-radius: 50%;
        margin-left: 10px;
        cursor: pointer;
    }

        .toggle-btn:hover {
            background-color: #F2F2F2;
        }

        .logo{
            margin-left: 10px;
            cursor: pointer;
            transform: scale(0.9);
        }

    .title-for-logo {
        line-height: 100%;
        margin-left: 14px;
        font-size: 18px;
        font-weight: 700;
        letter-spacing: 0.14px;
        cursor: pointer;
    }
        .menu-list{
            width: auto;
            height: 100%;
            margin-left: 50px;
            display: flex;
            align-items: center;
            justify-content: flex-start;
        }


    .menu-bar-right {
        width: auto;
        height: 100%;
        display: flex;
        justify-content: flex-end;
        align-items: center;
        padding-right: 24px;
    }

        .menu-bar-right .compact-avatar{
            margin-left: 24px;
        }


        .user-option {
            width: 36px;
            height: 36px;
            border-radius: 50%;
            display: flex;
            justify-content: center;
            align-items: center;
            cursor: pointer;
            margin-left: 10px;
        }

        .user-option:hover {
            background-color: #F4F4F4;
        }

</style>
