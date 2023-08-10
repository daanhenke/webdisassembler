import Dashboard from "@/pages/profile/Dashboard.vue";
import AdminGlobal from "@/pages/admin/Global.vue";
import Login from "@/pages/Login.vue";
import {createRouter, createWebHistory, Router, RouteRecordRaw} from "vue-router";
import {useProfileStore} from "@/stores/profile.ts";

const routes: RouteRecordRaw[] = [
    { name: 'Profile', path: '/profile', component: Dashboard },
    { name: 'Login', path: '/login', component: Login },
    { name: 'Admin', path: '/admin/global', component: AdminGlobal },
]

const anonymousRouteList = [
    'Login'
]

export default (): Router =>
{
    const router = createRouter({
        history: createWebHistory(),
        routes: routes
    });
    
    router.beforeEach(async (to, _) =>
    {
        const profileStore = useProfileStore();
        if (! profileStore.hasIndexed)
        {
            await profileStore.update();
        }

        const destination = to.name?.toString();
        if (destination == undefined)
        {
            console.error(`Unnamed route: ${to}`);
            throw new Error();
        }
        
        if (! anonymousRouteList.includes(destination) && !profileStore.isLoggedIn)
        {
            console.warn('Redirecting to login page');
            return '/login';
        }
        
        if (to.name == 'Login' && profileStore.isLoggedIn)
        {
            return '/profile';
        }
    });
    
    return router;
};