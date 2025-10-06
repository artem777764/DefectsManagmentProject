<template>
    <div class="min-h-screen flex justify-center items-center">
      <AppForm class="w-1/3">
        <AppInput v-model="title" placeholder-value="Название" class="w-full"/>
        <AppInput v-model="description" placeholder-value="Описание" class="w-full"></AppInput>
        <AppSelector v-model="priorityId" :options="priorities" placeholder="Приоритет"/>
        <AppSelector v-model="engineerId" :options="engineers" placeholder="Назначаемый инженер"/>
        <AppInput v-model="status" placeholder-value="Статус" class="w-full"></AppInput>
        <AppInput v-model="deadline" placeholder-value="Срок" :is-date="true" class="w-full"></AppInput>
        <div
          v-if="showNewStatusButtons"
          class="flex flex-row justify-between gap-5"
        >
          <AppButton>Сохранить</AppButton>
          <AppButton>Назначить</AppButton>
        </div>
      </AppForm>
    </div>
</template>

<script setup lang="ts">
import AppForm from '@/components/layout/AppForm.vue';
import AppButton from '@/components/ui/AppButton.vue';
import AppInput from '@/components/ui/AppInput.vue';
import AppSelector from '@/components/ui/AppSelector.vue';
import { defectApi } from '@/lib/api/defectApi';
import { priorityApi } from '@/lib/api/priorityApi';
import { userApi } from '@/lib/api/userApi';
import type { GetDefectDTO } from '@/types/defects/getDefectDTO';
import type { Option } from '@/types/option';
import { computed, onMounted, ref } from 'vue';

const isLoading = ref(true);

const props = defineProps<{
  objectId: string,
  defectId: string,
}>();

onMounted(async () => {
  await Promise.all([
    fetchPriorities(),
    fetchEngineers(),
    fetchDefect()
  ]);
  isLoading.value = false;
})

const objectId = Number(props.objectId);
const defectId = Number(props.defectId);
const title = defineModel('title');
const description = defineModel('description');
const priorityId = defineModel<number>('priorityId');
const engineerId = defineModel<number>('engineerId')
const status = defineModel('status');
const deadline = defineModel('deadline');

const defect = ref<GetDefectDTO | null>();
const priorities = ref<Option[]>();
const engineers = ref<Option[]>();

const fetchPriorities = async () => {
  try {
    priorities.value = await priorityApi.getPriorities();
  } catch (error: any) {

  }
}

const fetchEngineers = async () => {
  try {
    engineers.value = await userApi.getEngineers();
  } catch (error: any) {

  }
}

const fetchDefect = async () => {
  try {
    defect.value = await defectApi.getById(defectId);
  } catch (error: any) {
    defect.value = null
  }
}

const showNewStatusButtons = computed(() => {
  if (isLoading.value) return false;
  return defect.value?.statusId == null || defect.value?.statusId == 1;
});
</script>

<style>

</style>