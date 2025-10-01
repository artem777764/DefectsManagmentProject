<template>
  <div class="min-h-screen flex justify-center items-center">
    <AppForm class="w-1/3">
      <AppText class="text-4xl font-bold text-center drop-shadow-lg">Perfect Defect</AppText>
      <AppText :class="['text-xl font-semibold text-center min-h-[1.5em]', resultClass]">{{resultText}}</AppText>
      <AppInput v-model="userName" placeholder-value="Почта/Логин" class="w-full"></AppInput>
      <AppInput v-model="password" :is-hidden="true" placeholder-value="Пароль" class="w-full"></AppInput>
      <div>
        <AppButton @click="handleLogin" class="w-full">Войти</AppButton>
        <router-link :to="{ name: 'register' }">
          <AppText class="text-xl text-center underline mt-2">Ещё нет аккаунта?</AppText>
        </router-link>
      </div>
    </AppForm>
  </div>
</template>

<script setup lang="ts">
import AppForm from '@/components/layout/AppForm.vue';
import AppButton from '@/components/ui/AppButton.vue'
import AppInput from '@/components/ui/AppInput.vue';
import AppText from '@/components/ui/AppText.vue';
import { userApi } from '@/lib/userApi';
import type { AuthorizeDTO } from '@/types/user/authorizeDTO';
import { computed, ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter()

const userName = defineModel<string>('userName');
const password = defineModel<string>('password');

const loginSuccess = ref<boolean | null>(null);
const resultText = ref('');

const resultClass = computed(() => {
  if (loginSuccess.value === null) return 'text-text';
  return loginSuccess.value ? 'text-success' : 'text-fail';
});

async function handleLogin() {
  if (userName.value == null || password.value == null)
  {
    loginSuccess.value = false;
    resultText.value = 'Почта/Логин/Пароль не введены';
    return;
  }

  const payload: AuthorizeDTO = {
    userName: userName.value ?? '',
    password: password.value ?? '',
  }

  try {
    const authorizeAnswerDTO = await userApi.login(payload);
    loginSuccess.value = authorizeAnswerDTO.successful;
    resultText.value = authorizeAnswerDTO.message;

    setTimeout(() => {
      if (authorizeAnswerDTO.hasData) router.push({ name: 'projects' })
      else router.push({ name: 'update-data' })
    }, 1500)
  } catch (e: any) {
    loginSuccess.value = false;
    if (e.response?.data) {
      const data = e.response.data as { message?: string };
      resultText.value = data.message ?? 'Возникла непредвиденная ошибка';
    } else {
      resultText.value = 'Возникла непредвиденная ошибка';
    }
  }
}
</script>

<style>
    
</style>