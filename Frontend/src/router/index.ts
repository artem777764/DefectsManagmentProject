import LoginPage from '@/pages/LoginPage.vue'
import ProjectsPage from '@/pages/ProjectsPage.vue'
import RegisterPage from '@/pages/RegisterPage.vue'
import UpdateDataPage from '@/pages/UpdateDataPage.vue'
import { createWebHistory, createRouter } from 'vue-router'

const routes = [
  {
    path: '/register',
    name: 'register',
    component: RegisterPage,
  },
  {
    path: '/update-data',
    name: 'update-data',
    component: UpdateDataPage,
  },
  {
    path: '/login',
    name: 'login',
    component: LoginPage,
  },
  {
    path: '/projects',
    name: 'projects',
    component: ProjectsPage,
  },
]

export const router = createRouter({
  history: createWebHistory(),
  routes,
})