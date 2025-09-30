<template>
  <div class="min-h-screen flex justify-center items-center">
    <AppForm class="w-1/3">
      <AppText class="text-4xl font-bold text-center drop-shadow-lg">Perfect Defect</AppText>
      <AppText class="text-xl font-semibold text-center min-h-[1.5em]">{{resultText}}</AppText>
      <AppInput v-model="email" placeholder-value="Почта" class="w-full"></AppInput>
      <AppInput v-model="login" placeholder-value="Логин" class="w-full"></AppInput>
      <AppInput v-model="password" :is-hidden="true" placeholder-value="Пароль" class="w-full"></AppInput>
      <AppInput v-model="passwordConfirm" :is-hidden="true" placeholder-value="Подтвердить пароль" class="w-full"></AppInput>
      <div class="flex flex-row items-center">
        <AppCheckbox/>
        <AppText class="inline-block leading-none align-middle ml-2 text-xl pt-1">Принимаю условию компании</AppText>
      </div>
      <div>
        <AppButton class="w-full">Зарегистрироваться</AppButton>
        <a href="">
          <AppText class="text-xl text-center underline mt-2">Уже есть аккаунт?</AppText>
        </a>
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
import { ref } from 'vue';

const email = defineModel<string>('email');
const login = defineModel<string>('login');
const password = defineModel<string>('password');
const passwordConfirm = defineModel<string>('confirmPassword');
const resultText = ref('');
</script>

<style>
    
</style>