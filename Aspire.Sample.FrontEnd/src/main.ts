import "./scss/styles.scss"

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import PrimeVue from "primevue/config";
import Aura from "@primevue/themes/aura";
import PrimeButton from "primevue/button";
import PrimeToast from "primevue/toast";
import ToastService from "primevue/toastservice";

import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(PrimeVue, {
    theme: {
        preset: Aura,
    },
});
app.use(ToastService);

app.component("PrimeButton", PrimeButton);
app.component("PrimeToast", PrimeToast);

app.use(createPinia())
app.use(router)

app.mount('#app')
