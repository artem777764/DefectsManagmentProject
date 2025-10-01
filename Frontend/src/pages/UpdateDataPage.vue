<template>
  <div class="min-h-screen flex justify-center items-center">
    <AppForm class="w-1/3">
      <AppText class="text-4xl font-bold text-center drop-shadow-lg">Perfect Defect</AppText>
      <AppText :class="['text-xl font-semibold text-center min-h-[1.5em]', resultClass]">{{resultText}}</AppText>
      <AppInput v-model="surname" placeholder-value="Фамилия" class="w-full"></AppInput>
      <AppInput v-model="name" placeholder-value="Имя" class="w-full"></AppInput>
      <AppInput v-model="patronymic" placeholder-value="Отчество" class="w-full"></AppInput>
      <AppButton @click="handleUpdateData" class="w-full">Войти</AppButton>
    </AppForm>
  </div>
</template>

<script setup lang="ts">
import AppForm from '@/components/layout/AppForm.vue';
import AppButton from '@/components/ui/AppButton.vue'
import AppInput from '@/components/ui/AppInput.vue';
import AppText from '@/components/ui/AppText.vue';
import { userApi } from '@/lib/userApi';
import type { CreateUserDataDTO } from '@/types/user/createUserDataDTO';
import { computed, ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter()

const surname = defineModel<string>('surname');
const name = defineModel<string>('name');
const patronymic = defineModel<string>('patronymic');

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