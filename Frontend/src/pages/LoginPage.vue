<template>
  <div class="min-h-screen flex justify-center items-center">
    <AppForm class="w-1/3">
      <AppText class="text-4xl font-bold text-center drop-shadow-lg">Perfect Defect</AppText>
      <AppText class="text-xl font-semibold text-center min-h-[1.5em]">{{resultText}}</AppText>
      <AppInput v-model="userName" placeholder-value="Почта/Логин" class="w-full"></AppInput>
      <AppInput v-model="password" :is-hidden="true" placeholder-value="Пароль" class="w-full"></AppInput>
      <div>
        <AppButton class="w-full">Войти</AppButton>
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
import { ref } from 'vue';

const userName = defineModel<string>('userName');
const password = defineModel<string>('password');
const resultText = ref('');
</script>

<style>
    
</style>