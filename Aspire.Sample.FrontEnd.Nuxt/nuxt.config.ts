// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2024-04-03',
  devtools: { enabled: true },
  devServer: {
    port: parseInt(process.env.PORT ?? "5173"),
  },
  routeRules: {
    '/api/weatherforecast': { cors: true, proxy: `${process.env.services__backend__https__0}/weatherforecast` }
  },
  nitro: {
    routeRules: {
      '/api/weatherforecast': { cors: true, proxy: `https://www.coliz.com/weatherforecast` }
    }
  },
  modules: [
    'nuxt-primevue'
  ],
  primevue: {
      /* Options */
  },
  css: ['primevue/resources/themes/aura-light-green/theme.css']
})
