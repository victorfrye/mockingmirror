import { defineConfig } from 'vite'
import plugin from '@vitejs/plugin-react'
import griffel from '@griffel/vite-plugin'

// https://vitejs.dev/config/
export default defineConfig(({ command }) => ({
  plugins: [plugin(), command === 'build' && griffel()],
  server: {
    port: 4173,
  },
}))
