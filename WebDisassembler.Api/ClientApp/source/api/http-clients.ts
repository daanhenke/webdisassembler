import {Authentication} from "@/api/http/Authentication.ts";
import {ApiConfig} from "@/api/http/http-client.ts";
import { Projects } from "./http/Projects";
import {Tenant} from "@/api/http/Tenant.ts";
import {Admin} from "@/api/http/Admin.ts";

const apiConfig: ApiConfig = {
    baseUrl: `http://${window.location.hostname}:3000`,
    baseApiParams: {
        credentials: 'include',
    }
};

export const useAuthenticationClient = () => new Authentication(apiConfig);
export const useProjectsClient = () => new Projects(apiConfig);
export const useTenantsClient = () => new Tenant(apiConfig);
export const useAdminClient = () => new Admin(apiConfig);