export default class FormatData {
    /**
     * trả về text trang thái từ giá trị int
     * @param {any} _status
     * Author HieuNV
     */
    static formatStatus(_status) {
        switch (_status) {
            case -1:
                return "Tất cả";
            case 0:
                return "Chờ duyệt";
            case 1:
                return "Đã duyệt";
            case 2:
                return "Từ chối";
            default:
                return null;
        }
    }

    /**
     * Xóa dấu tiếng việt
     * @param {any} str
     * Author HieuNV
     */
    static removeAccents(str) {
        return str.normalize('NFD')
            .replace(/[\u0300-\u036f]/g, '')
            .replace(/đ/g, 'd').replace(/Đ/g, 'D');
    }

    /**
     * thêm 0 trước nếu có 1 chữa số
     * @param {any} n
     * Author HieuNV
     */
    static formatNumber(n) {
        if (n >= 10)
            return n;
        else
            return '0' + n;
    }

    /**
     * Format DateTime về dang dd/MM/yyyy HH:mm
     * @param {any} d
     * Author HieuNV
     */
    static formatDateTime(d) {
        d = new Date(d);
        return `${this.formatNumber(d.getDate())}/${this.formatNumber(d.getMonth() + 1)}/${this.formatNumber(d.getFullYear())} ${this.formatNumber(d.getHours())}:${this.formatNumber(d.getMinutes())}`;
    }

}