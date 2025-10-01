<template>
  <div class="min-h-screen flex justify-center items-center">
    <AppForm class="w-1/3">
      <AppText class="text-2xl font-medium text-center">Личный кабинет</AppText>
      <AppText :class="['text-xl font-semibold text-center min-h-[1.5em]', resultClass]">{{resultText}}</AppText>
      <AppInput placeholder-value="Фамилия" v-model="surname" class="w-full"></AppInput>
      <AppInput placeholder-value="Имя" v-model="name" class="w-full"></AppInput>
      <AppInput placeholder-value="Отчество" v-model="patronymic" class="w-full"></AppInput>
      <AppInput placeholder-value="Роль" :is-disabled="true" v-model="role" class="w-full"></AppInput>
      <AppInput placeholder-value="Логин" :is-disabled="true" v-model="login" class="w-full"></AppInput>
      <AppInput placeholder-value="Почта" :is-disabled="true" v-model="email" class="w-full"></AppInput>
      <AppButton @click="handleUpdateData" class="w-full">Сохранить</AppButton>
    </AppForm>
  </div>
</template>

<script setup lang="ts">
import AppForm from '@/components/layout/AppForm.vue';
import AppButton from '@/components/ui/AppButton.vue';
import AppInput from '@/components/ui/AppInput.vue';
import AppText from '@/components/ui/AppText.vue';
import { userApi } from '@/lib/userApi';
import type { CreateUserDataDTO } from '@/types/user/createUserDataDTO';
import { ref } from 'vue';
import { computed, onMounted } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter()

const surname = defineModel<string>('surname');
const name = defineModel<string>('name');
const patronymic = defineModel<string>('patronymic');
const email = defineModel<string>('email');
const login = defineModel<string>('login');
const role = defineModel<string>('role');

onMounted(() => {
  fetchUserData();
})

const fetchUserData = async () => {
  try {
    const getUserExtendedDTO = await userApi.profile();
    surname.value = getUserExtendedDTO.surname;
    name.value = getUserExtendedDTO.name;
    patronymic.value = getUserExtendedDTO.patronymic;
    email.value = getUserExtendedDTO.email;
    login.value = getUserExtendedDTO.login;
    role.value = getUserExtendedDTO.role;
  } catch (error: any) {
    console.error('Ошибка загрузки профиля:', error)
  }
}

const dataUpdateSuccess = ref<boolean | null>(null);
const resultText = ref('');

const resultClass = computed(() => {
  if (dataUpdateSuccess.value === null) return 'text-text';
  return dataUpdateSuccess.value ? 'text-success' : 'text-fail';
});

async function handleUpdateData() {
  if (surname.value == null || name.value == null)
  {
    dataUpdateSuccess.value = false;
    resultText.value = 'Фамилия и имя обязательны';
    return;
  }

  const payload: CreateUserDataDTO = {
    surname: surname.value!,
    name: name.value!,
    patronymic: patronymic.value,
  }

  try {
    const updateDataAnswerDTO = await userApi.updateData(payload);
    dataUpdateSuccess.value = updateDataAnswerDTO.successful;
    resultText.value = updateDataAnswerDTO.message;

    setTimeout(() => {
        router.push({ name: 'objects' })
    }, 1500)
  } catch (e: any) {
    dataUpdateSuccess.value = false;
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