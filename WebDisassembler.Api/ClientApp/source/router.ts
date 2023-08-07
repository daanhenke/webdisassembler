import Dashboard from "@/pages/profile/Dashboard.vue";
import Login from "@/pages/Login.vue";
import {createRouter, createWebHistory, Router, RouteRecordRaw} from "vue-router";
import {useProfileStore} from "@/stores/profile.ts";

const routes: RouteRecordRaw[] = [
    { name: 'Profile', path: '/profile', component: Dashboard },
    { name: 'Login', path: '/login', component: Login },
    { name: 'Admin', path: '/admin', component: Login },
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
    
    router.beforeEach(async (to, from) =>
    {
        const profileStore = useProfileStore();
        if (! profileStore.hasIndexed)
        {
            await profileStore.update();
        }
        
        if (! anonymousRouteList.includes(to.name) && !profileStore.isLoggedIn)
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