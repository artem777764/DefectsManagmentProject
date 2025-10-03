import axios, { type AxiosInstance } from 'axios';
import type { Router } from 'vue-router';

export const api: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASEURL ?? 'http://localhost:5234',
  withCredentials: true,
});

export function setupApiInterceptor(router: Router) {
  api.interceptors.response.use(
    (response) => response,
    (error) => {
      const status = error?.response?.status;

      if (status === 401) {
        const current = router.currentRoute.value;
        if (current.name !== 'login') {
          router.replace({ name: 'login' }).catch(() => {});
        }
      }

      return Promise.reject(error);
    }
  );
}

export default api;