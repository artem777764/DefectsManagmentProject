import type { UserState } from '@/types/user/userState';
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useUserStore = defineStore('userStore', () => {
  const userId = ref<number | null>(null);
  const roleId = ref<number | null>(null);

  function setUser(userState: UserState)
  {
    userId.value = userState.userId;
    roleId.value = userState.roleId;
  }

  function logout()
  {
    userId.value = null;
    roleId.value = null;
  }
  
  return {
    userId,
    roleId,
    setUser,
    logout,
  }
}, {
    persist: true
});