<template>
  <div class="flex flex-row justify-between bg-secondary shadow-md px-10 py-2 ">
    <AppText class="text-xl font-bold drop-shadow-lg">Perfect Defect</AppText>
    <router-link :to="{ name: 'objects' }">
      <AppText class="text-xl font-medium">Объекты</AppText>
    </router-link>
    <router-link :to="{ name: 'profile' }">
      <AppText class="text-xl font-medium">Профиль</AppText>
    </router-link>
    <AppText @click="handleExit" class="text-xl font-medium cursor-pointer">Выход</AppText>
  </div>
</template>

<script setup lang="ts">
import { logout } from '@/utils/auth';
import { useRouter } from 'vue-router';

const router = useRouter();

async function handleExit() {
  logout();
  router.push({name: 'login'})
}
</script>