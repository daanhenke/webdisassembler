import { createApp } from 'vue';
import {createPinia} from "pinia";
import App from '@/App.vue';

import createRouter from '@/router.ts';

import 'virtual:windi.css';
import '@/style/vars.css';
import {loadInitialTheme, loadTheme} from "@/lib/theming";

window.addEventListener('load', async () =>
{
    const themePromise = loadInitialTheme();
    const app = createApp(App);
    
    app.use(createRouter());
    app.use(createPinia());
    
    await themePromise;
    app.mount('body');
});
