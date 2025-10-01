import { userApi } from '@/lib/userApi';
import { useUserStore } from '@/stores/userStore'

export const logout = () => {
  userApi.logout();
  useUserStore().logout();
}