import BaseApi from "../base/BaseApi.js"
import BaseApiConfig from "../base/BaseApiConfig.js"

class RestPermissionApi extends BaseApi {
    constructor() {
        super();
        this.apiController = "api/v1/RestPermissions";
    }

    refuseBatch(data) {
        return BaseApiConfig.put(`${this.apiController}/refuseBatch`, data);
    }

    approveBatch(data) {
        return BaseApiConfig.put(`${this.apiController}/approveBatch`, data);
    }

    getFilter(data, pageSize, pageNumber) {
        return BaseApiConfig.post(`${this.apiController}/getFilter?pageSize=${pageSize}&pageNumber=${pageNumber}`, data);
    }
}



export default new RestPermissionApi();