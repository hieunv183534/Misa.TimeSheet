<template>
    <div class="compact-avatar" :style="iStyle">
        <p :style="fontSize">{{iText}}</p>
    </div>
</template>


<script>
    import FormatData from "../../util/Format.js"

    export default {
        name: 'CompactAvatar',
        props: {
            userName: {
                type: String,
                default: ''
            },
            size: {
                type: String,
                default: ''
            }
        },
        data() {
            return {
                iText: '',
                colors: ['FD90AF', '86AE94', '63D170', '4186E1', 'CB8488', 'FD612E', '7B6EF1'],
                iStyle: '',
                fontSize:''
            }
        },
        methods: {
            /**
             * gen hai kí tự từ tên
             * Author HieuNV
             * */
            genIText() {
                var arr = this.userName.split(' ');
                var char1 = arr[0][0].toUpperCase();
                var char2 = arr[arr.length - 1][0].toUpperCase();
                this.iText = this.removeAccents(char1 + char2);
            },
            /**
             * Gọi đến hàm bỏ dấu tiếng việt
             * Author HieuNV
             * @param str
             */
            removeAccents(str) {
                return FormatData.removeAccents(str);
            }
        },
        watch: {
            // thực hiện gen lại 2 kí tự khi tên truyền vào thay đổi
            userName() {
                this.genIText();
            }
        },
        created() {
            this.iStyle =
                `background-color: #${this.colors[Math.floor(Math.random() * 7)]}; width: ${this.size}px; height: ${this.size}px; `;
            this.fontSize = `font-size: ${((this.size)/2)}px;`
            this.genIText();
        }
    }
</script>


<style scoped>
    .compact-avatar{
        border-radius: 50%;
        display: flex;
        justify-content: center;
        align-items: center;
        float: left;
    }

    .compact-avatar p{
        font-weight: 500;
        color: #fff;
        letter-spacing: 0.123px;
    }
</style>