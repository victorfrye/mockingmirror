import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import griffel from '@griffel/vite-plugin';
import { resolve } from 'path';

const config = defineConfig(({ command }) => ({
  plugins: [plugin(), command === 'build' && griffel()],
  server: {
    port: 5173,
  },
  resolve: {
    alias: {
      '@mockingmirror': resolve(__dirname, 'app'),
    },
  },
}));

export default config;
