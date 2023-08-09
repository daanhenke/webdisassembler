import { resolve } from 'path';
import { defineConfig } from 'vite';
import { createHtmlPlugin } from 'vite-plugin-html';
import vue from '@vitejs/plugin-vue';
import WindiCSS from 'vite-plugin-windicss';

export default defineConfig({
    server: {
        port: 3001
    },
    resolve: {
        alias: {
            '@': resolve(__dirname, 'source')
        }
    },
    build: {
        rollupOptions: {
            input: {
                app: resolve(__dirname, 'source/main.ts')
            }
        }
    },
    plugins: [
        createHtmlPlugin({
            entry: '/source/main.ts'
        }),
        vue(),
        WindiCSS()
    ]
});