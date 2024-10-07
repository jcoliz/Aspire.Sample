// https://nuxt.com/docs/api/configuration/nuxt-config

import Aura from '@primevue/themes/aura';
import { definePreset } from '@primevue/themes';

// https://gist.github.com/lucaspmarra/8f076ceff408bc76e6775b8b752ac6a8
// You can change the name 'purple' to any name you like, in my case it was just to keep the standard
const purple = definePreset(Aura, {
  semantic: {
    primary: {
      50: '{purple.50}',
      100: '{purple.100}',
      200: '{purple.200}',
      300: '{purple.300}',
      400: '{purple.400}',
      500: '{purple.500}',
      600: '{purple.600}',
      700: '{purple.700}',
      800: '{purple.800}',
      900: '{purple.900}',
      950: '{purple.950}'
    },
    /*
    * This code snippet comes from the AppConfigurator.vue file, 
    * from the getPresetExt function that returns this configuration, 
    I thought it would be prudent to bring it too
    */
    colorScheme: {
      light: {
        primary: {
          color: "{primary.500}",
          contrastColor: "#ffffff",
          hoverColor: "{primary.600}",
          activeColor: "{primary.700}",
        },
        highlight: {
          background: "{primary.50}",
          focusBackground: "{primary.100}",
          color: "{primary.700}",
          focusColor: "{primary.800}",
        },
        surface: {
          0: '#ffffff',
          50: '{slate.50}',
          100: '{slate.100}',
          200: '{slate.200}',
          300: '{slate.300}',
          400: '{slate.400}',
          500: '{slate.500}',
          600: '{slate.600}',
          700: '{slate.700}',
          800: '{slate.800}',
          900: '{slate.900}',
          950: '{slate.950}'
        }
      },
    /*
    * This code snippet comes from the AppConfigurator.vue file, 
    * from the getPresetExt function that returns this configuration, 
    I thought it would be prudent to bring it too
    */
      dark: {
        primary: {
          color: "{primary.400}",
          contrastColor: "{surface.900}",
          hoverColor: "{primary.300}",
          activeColor: "{primary.200}",
        },
        highlight: {
          background: "color-mix(in srgb, {primary.400}, transparent 84%)",
          focusBackground:
            "color-mix(in srgb, {primary.400}, transparent 76%)",
          color: "rgba(255,255,255,.87)",
          focusColor: "rgba(255,255,255,.87)",
        },
        surface: {
          0: '#ffffff',
          50: '{slate.50}',
          100: '{slate.100}',
          200: '{slate.200}',
          300: '{slate.300}',
          400: '{slate.400}',
          500: '{slate.500}',
          600: '{slate.600}',
          700: '{slate.700}',
          800: '{slate.800}',
          900: '{slate.900}',
          950: '{slate.950}'
        }
      }
    }
  }
});


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
    components: {
      prefix: 'Prime'
    },
    options: {
      theme: {
        preset: purple
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
