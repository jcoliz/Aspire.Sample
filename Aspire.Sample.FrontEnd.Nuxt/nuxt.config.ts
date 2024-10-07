// https://nuxt.com/docs/api/configuration/nuxt-config

import Aura from '@primevue/themes/aura';

export default defineNuxtConfig({
  compatibilityDate: '2024-04-03',
  devtools: { enabled: true },
  devServer: {
    port: parseInt(process.env.PORT ?? "5173"),
  },
  modules: [
    '@primevue/nuxt-module'
  ],
  primevue: {
    options: {
      theme: {
        preset: Aura
      }
    }
      /* Options */
  },
  css: [
    '~/assets/scss/styles.scss'
  ],
  routeRules: {
    '/api/**': { cors: true, proxy: `${process.env.services__backend__http__0}/**` }
  },
})
