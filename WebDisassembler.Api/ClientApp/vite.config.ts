import { resolve } from 'path';
import { defineConfig } from 'vite';
import { createHtmlPlugin } from 'vite-plugin-html';
import vue from '@vitejs/plugin-vue';
import WindiCSS from 'vite-plugin-windicss';

export default defineConfig({
    server: {
        port: 3001,
        host: '0.0.0.0'
    },
    resolve: {
        alias: {
            '@': resolve(__dirname, 'source')
        }
    },
    plugins: [
        vue(),
        WindiCSS()
    ]
});