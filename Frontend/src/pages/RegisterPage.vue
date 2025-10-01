<template>
  <div class="min-h-screen flex justify-center items-center">
    <AppForm class="w-1/3">
      <AppText class="text-4xl font-bold text-center drop-shadow-lg">Perfect Defect</AppText>
      <AppText :class="['text-xl font-semibold text-center min-h-[1.5em]', resultClass]">{{resultText}}</AppText>
      <AppInput v-model="email" placeholder-value="Почта" class="w-full"></AppInput>
      <AppInput v-model="login" placeholder-value="Логин" class="w-full"></AppInput>
      <AppInput v-model="password" :is-hidden="true" placeholder-value="Пароль" class="w-full"></AppInput>
      <AppInput v-model="passwordConfirm" :is-hidden="true" placeholder-value="Подтвердить пароль" class="w-full"></AppInput>
      <div class="flex flex-row items-center">
        <AppCheckbox v-model="policeAccept"/>
        <AppText class="inline-block leading-none align-middle ml-2 text-xl pt-1">Принимаю условию компании</AppText>
      </div>
      <div>
        <AppButton @click="handleRegister" class="w-full">Зарегистрироваться</AppButton>
        <router-link :to="{ name: 'login' }">
          <AppText class="text-xl text-center underline mt-2">Уже есть аккаунт?</AppText>
        </router-link>
      </div>
    </AppForm>
  </div>
</template>

<script setup lang="ts">
import AppForm from '@/components/layout/AppForm.vue';
import AppButton from '@/components/ui/AppButton.vue'
import AppCheckbox from '@/components/ui/AppCheckbox.vue';
import AppInput from '@/components/ui/AppInput.vue';
import AppText from '@/components/ui/AppText.vue';
import { userApi } from '@/lib/userApi';
import type { CreateUserDTO } from '@/types/user/createUserDTO';
import { computed, ref } from 'vue';

const email = defineModel<string>('email');
const login = defineModel<string>('login');
const password = defineModel<string>('password');
const passwordConfirm = defineModel<string>('confirmPassword');
const policeAccept = defineModel<boolean>('policeAccept');

const registerSuccess = ref<boolean | null>(null);
const resultText = ref('');

const resultClass = computed(() => {
  if (registerSuccess.value === null) return 'text-text';
  return registerSuccess.value ? 'text-[var(--color-success)]' : 'text-[var(--color-fail)]';
});

async function handleRegister() {
  if (password.value != passwordConfirm.value)
  {
    registerSuccess.value = false;
    resultText.value = 'Пароли не совпадают';
    return;
  }

  const payload: CreateUserDTO = {
    email: email.value ?? '',
    login: login.value ?? '',
    password: password.value ?? '',
    policeAccept: policeAccept.value ?? false,
  }

  try {
    const registerAnswerDTO = await userApi.create(payload);
    registerSuccess.value = registerAnswerDTO.successful;
    resultText.value = registerAnswerDTO.message;
  } catch (e: any) {
    registerSuccess.value = false;
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