import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import CounterView from '../views/CounterView.vue'
import WeatherView from '../views/WeatherView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: { title: 'Home' }
    },
    {
      path: '/counter',
      name: 'counter',
      component: CounterView,
      meta: { title: 'Counter' }
    },
    {
      path: '/weather',
      name: 'weather',
      component: WeatherView,
      meta: { title: 'Weather' }
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue'),
      meta: { title: 'About' }
    }
  ]
})

router.afterEach((to, from) => {
      // TODO: Use next tick to handle router history correctly
      // see: https://github.com/vuejs/vue-router/issues/914#issuecomment-384477609
      const title = to.meta.title as string
      document.title = `${title} | Aspire.Sample`
});

export default router
