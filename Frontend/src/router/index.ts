import DefectsPage from '@/pages/DefectsPage.vue'
import LoginPage from '@/pages/LoginPage.vue'
import ObjectsPage from '@/pages/ObjectsPage.vue'
import ProfilePage from '@/pages/ProfilePage.vue'
import RegisterPage from '@/pages/RegisterPage.vue'
import UpdateDataPage from '@/pages/UpdateDataPage.vue'
import { createWebHistory, createRouter } from 'vue-router'

const routes = [
  {
    path: '/register',
    name: 'register',
    component: RegisterPage,
    meta: { showNavbar: false },
  },
  {
    path: '/update-data',
    name: 'update-data',
    component: UpdateDataPage,
    meta: { showNavbar: false },
  },
  {
    path: '/login',
    name: 'login',
    component: LoginPage,
    meta: { showNavbar: false },
  },
  {
    path: '/profile',
    name: 'profile',
    component: ProfilePage,
    meta: { showNavbar: true },
  },
  {
    path: '/objects',
    name: 'objects',
    component: ObjectsPage,
    meta: { showNavbar: true },
  },
  {
    path: '/objects/:objectId/defects',
    name: 'defects',
    component: DefectsPage,
    meta: { showNavnar: true },
    props: true,
  }
]

export const router = createRouter({
  history: createWebHistory(),
  routes,
})