import RegisterPage from '@/pages/RegisterPage.vue'
import { createMemoryHistory, createRouter } from 'vue-router'

const routes = [
  { path: '/', redirect: '/register' },
  {
    path: '/register',
    name: 'register',
    component: RegisterPage
  },
]

export const router = createRouter({
  history: createMemoryHistory(),
  routes,
})