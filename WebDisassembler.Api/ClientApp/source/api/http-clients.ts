import {Authentication} from "@/api/http/Authentication.ts";
import {ApiConfig} from "@/api/http/http-client.ts";
import { Projects } from "./http/Projects";
import {Tenant} from "@/api/http/Tenant.ts";
import {Admin} from "@/api/http/Admin.ts";
import {CommandPalette} from "@/api/http/CommandPalette.ts";

const apiUrl = import.meta.env.VITE_API_URL

const apiConfig: ApiConfig = {
    baseUrl: apiUrl,
    baseApiParams: {
        credentials: 'include',
    }
};

export const useAuthenticationClient = () => new Authentication(apiConfig);
export const useProjectsClient = () => new Projects(apiConfig);
export const useTenantsClient = () => new Tenant(apiConfig);
export const useAdminClient = () => new Admin(apiConfig);
export const useCommandPaletteClient = () => new CommandPalette(apiConfig);