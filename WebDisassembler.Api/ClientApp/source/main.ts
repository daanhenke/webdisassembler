import { createApp } from 'vue';
import {createPinia} from "pinia";
import App from '@/App.vue';

import createRouter from '@/router.ts';

import 'virtual:windi.css';
import '@/style/vars.css';

window.addEventListener('load', () =>
{
    const app = createApp(App);
    
    app.use(createRouter());
    app.use(createPinia());
    
    app.mount('body');
});
