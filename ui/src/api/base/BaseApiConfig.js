import axios from "axios";

var BaseAPIConfig = axios.create({
	baseURL: "http://localhost:26775/",
});

export default BaseAPIConfig;