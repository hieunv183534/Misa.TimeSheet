import BaseApi from "../base/BaseApi.js"

class RestTypeApi extends BaseApi {
    constructor() {
        super();
        this.apiController = "api/v1/RestTypes";
    }
}

export default new RestTypeApi();