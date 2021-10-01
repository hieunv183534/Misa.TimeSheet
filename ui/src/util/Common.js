export default class Common {

    /**
     * trả về số ngày nghỉ phép từ startTime và endTime
     * @param {any} _startTime
     * @param {any} _endTime
     * Author HieuNV
     */
    static getNumberDayOfNP(_startTime, _endTime) {
        var startTime = new Date(_startTime);
        var endTime = new Date(_endTime);
        var numDay = ((endTime.getTime() - startTime.getTime()) / (24 * 60 * 60 * 1000));
        numDay = Math.round(numDay * 100) / 100
        return numDay;
    }

    /**
     * trả về số ngày nghỉ phép từ startTime và endTime
     * @param {any} _startTime
     * @param {any} _endTime
     * Author HieuNV
     */
    static getNumberHoursOfNP(_startTime, _endTime) {
        return 8 * this.getNumberDayOfNP(_startTime, _endTime);
    }
}