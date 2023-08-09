import {Authentication} from "@/api/http/Authentication.ts";
import {ApiConfig} from "@/api/http/http-client.ts";
import { Projects } from "./http/Projects";

const apiConfig: ApiConfig = {
    baseUrl: 'http://localhost:3000',
    baseApiParams: {
        credentials: 'include',
    }
};

export const useAuthenticationClient = () => new Authentication(apiConfig);
export const useProjectsClient = () => new Projects(apiConfig);