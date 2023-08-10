import {defineStore} from "pinia";
import {useAuthenticationClient} from "@/api/http-clients.ts";

interface ProfileStoreState
{
    userId?: string;
    isAdministrator: boolean;
    username: string;
    
    hasIndexed: boolean;
}

export const useProfileStore = defineStore('profile', {
    state: () => {
        return <ProfileStoreState> {
            userId: undefined,
            hasIndexed: false,
            isAdministrator: false,
            username: ''
        }
    },
    getters: {
        isLoggedIn: state => state.userId != null
    },
    actions: {
        async update() {
            const authClient = useAuthenticationClient();
            try
            {
                const response = await authClient.me()

                if (response.ok && response.data != null)
                {
                    const user = response.data;
                    this.userId = user.userId;
                    this.username = user.username;
                    this.isAdministrator = user.isAdministrator;
                }

                this.hasIndexed = true;
            }
            catch(e) {}
        }
    }
})