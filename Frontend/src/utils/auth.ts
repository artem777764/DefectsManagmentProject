import { userApi } from '@/lib/api/userApi';
import { useUserStore } from '@/stores/userStore'

export const logout = () => {
  userApi.logout();
  useUserStore().logout();
}