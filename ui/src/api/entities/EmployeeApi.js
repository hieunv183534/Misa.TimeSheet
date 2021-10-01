import BaseApi from "../base/BaseApi.js"
import BaseApiConfig from "../base/BaseApiConfig.js"

class RestPermissionApi extends BaseApi {
    constructor() {
        super();
        this.apiController = "api/v1/Employees";
    }

    /**
     * Cập nhật số nghỉ phép của một nhân viên khi được duyệt nghỉ
     * @param {any} id
     * @param {any} numberOfNP
     */
    update(id, numberOfNP) {
        return BaseApiConfig.put(`${this.apiController}/updateNP?employeeId=${id}&numberOfNP=${numberOfNP}`);
    }
}

export default new RestPermissionApi();