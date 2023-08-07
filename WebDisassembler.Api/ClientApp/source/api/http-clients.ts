import {Authentication} from "@/api/http/Authentication.ts";
import {ApiConfig} from "@/api/http/http-client.ts";

const apiConfig: ApiConfig = {
    baseUrl: 'http://localhost:3000',
    baseApiParams: {
        credentials: 'include',
    }
};

export const useAuthenticationClient = () =>
{
    return new Authentication(apiConfig);
};
