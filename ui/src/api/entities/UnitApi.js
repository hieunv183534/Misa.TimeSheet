import BaseApi from "../base/BaseApi.js"

class UnitApi extends BaseApi {
    constructor() {
        super();
        this.apiController = "api/v1/Units";
    }
}

export default new UnitApi();