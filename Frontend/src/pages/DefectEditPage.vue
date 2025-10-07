<template>
    <div class="min-h-screen flex justify-center items-center">
      <AppForm class="w-1/3">
        <AppInput v-model="title" placeholder-value="Название" class="w-full"/>
        <AppTextarea v-model="description" placeholder-value="Описание" class="w-full"/>
        <AppSelector v-model="priorityId" :options="priorities" placeholder="Приоритет"/>
        <AppSelector
          v-model="engineerId" :options="engineers" placeholder="Назначаемый инженер"
          v-if="showNewStatusElements"
        />
        <AppInput v-if="showStatus" :is-disabled="true" v-model="status" placeholder-value="Статус" class="w-full"></AppInput>
        <AppInput :is-disabled="inputsEnabled" v-model="deadline" placeholder-value="Срок" :is-date="true" class="w-full"></AppInput>
        <div
          v-if="showNewStatusElements"
          class="flex flex-row justify-between gap-5"
        >
          <AppButton @click="goToProject(objectId)">Назад</AppButton>
          <AppButton @click="handleCreateOrUpdateDefect()">Сохранить</AppButton>
          <AppButton @click="handleAppointmentDefect()">Назначить</AppButton>
        </div>
        <div
          v-if="showWorkStatusElements"
          class="flex flex-row justify-between gap-5"
        >
          <AppButton @click="goToProject(objectId)">Назад</AppButton>
          <AppButton @click="handleCreateOrUpdateDefect()">Сохранить</AppButton>
          <AppButton @click="handleSendOnVerifyDefect()">Отправить</AppButton>
        </div>
        <div
          v-if="showVerifyStatusElements"
          class="flex flex-row justify-between gap-5"
        >
          <AppButton @click="goToProject(objectId)">Назад</AppButton>
          <AppButton @click="handleDenyDefect()">Отклонить</AppButton>
          <AppButton @click="handleAcceptDefect()">Подтвердить</AppButton>
        </div>
      </AppForm>
    </div>
</template>

<script setup lang="ts">
import AppForm from '@/components/layout/AppForm.vue';
import AppButton from '@/components/ui/AppButton.vue';
import AppInput from '@/components/ui/AppInput.vue';
import AppSelector from '@/components/ui/AppSelector.vue';
import AppTextarea from '@/components/ui/AppTextarea.vue';
import { defectApi } from '@/lib/api/defectApi';
import { priorityApi } from '@/lib/api/priorityApi';
import { userApi } from '@/lib/api/userApi';
import { useUserStore } from '@/stores/userStore';
import type { AppointmentDTO } from '@/types/defects/appointmentDTO';
import type { CreateDefectDTO } from '@/types/defects/createDefectDTO';
import type { GetDefectDTO } from '@/types/defects/getDefectDTO';
import type { UpdateDefectDTO } from '@/types/defects/updateDefectDTO';
import type { Option } from '@/types/option';
import { computed, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';

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
  setModels(defect.value);
  isLoading.value = false;
})

const router = useRouter();

const objectId = Number(props.objectId);
const defectId = Number(props.defectId);
const roleId = useUserStore().roleId;
const title = defineModel<string>('title');
const description = defineModel<string>('description');
const priorityId = defineModel<number>('priorityId');
const engineerId = defineModel<number>('engineerId')
const status = defineModel('status');
const deadline = defineModel<string>('deadline');

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

const setModels = (defect?: GetDefectDTO | null) => {
  title.value = defect?.title ?? 'БЕЗ НАЗВАНИЯ';
  description.value = defect?.description ?? 'БЕЗ ОПИСАНИЯ';
  priorityId.value = defect?.priorityId ?? 0;
  engineerId.value = defect?.executorId ?? 0;
  deadline.value = defect?.deadline ? toDateTimeLocal(defect.deadline) : '';
  status.value = defect?.statusName ?? 'БЕЗ СТАТУСА'
}

const showNewStatusElements = computed(() => {
  if (isLoading.value) return false;
  return defect.value?.statusId == null || defect.value?.statusId == 1;
});

const showWorkStatusElements = computed(() => {
  if (isLoading.value) return false;
  return defect.value?.statusId == 2;
});

const showVerifyStatusElements = computed(() => {
  if (isLoading.value) return false;
  return defect.value?.statusId == 3;
});

const inputsEnabled = computed(() => {
  return roleId != 2;
});

const showStatus = computed(() => {
  if (defect == null) return true;
  if (defect.value?.statusId != 1) return true;
  return false;
})

async function createDefect() {
  const payload: CreateDefectDTO = {
    title: title.value!,
    description: description.value || undefined,
    projectId: objectId,
    priorityId: priorityId.value!,
    deadline: deadline.value ? new Date(deadline.value).toISOString() : undefined,
  }
  try {
    await defectApi.create(payload);
    setTimeout(() => {
        goToProject(objectId);
    }, 1500)
  } catch (e: any) {

  }
}

async function updateDefect() {
  const payload: UpdateDefectDTO = {
    id: defectId,
    title: title.value!,
    description: description.value || undefined,
    priorityId: priorityId.value!,
    deadline: deadline.value ? new Date(deadline.value).toISOString() : undefined,
  }
  try {
    await defectApi.update(payload);
    setTimeout(() => {
        goToProject(objectId);
    }, 1500)
  } catch (e: any) {

  }
}

async function appointDefect() {
  const payload: AppointmentDTO = {
    DefectId: defectId,
    ExecutorId: engineerId.value!,
  }
  try {
    await defectApi.appointment(payload);
    setTimeout(() => {
      goToProject(objectId);
    }, 1500)
  } catch (e: any) {

  }
}

async function sendOnVerifyDefect() {
  try {
    await defectApi.verify(defectId);
    setTimeout(() => {
      goToProject(objectId);
    }, 1500)
  } catch (e: any) {

  }
}

async function denyDefect() {
  try {
    await defectApi.deny(defectId);
    setTimeout(() => {
      goToProject(objectId);
    }, 1500)
  } catch (e: any) {

  }
}

async function acceptDefect() {
  try {
    await defectApi.accept(defectId);
    setTimeout(() => {
      goToProject(objectId);
    }, 1500)
  } catch (e: any) {

  }
}

async function handleCreateOrUpdateDefect() {
  if (defectId == 0) {
    createDefect();
  }
  else {
    updateDefect();
  }
}

async function handleAppointmentDefect() {
  handleCreateOrUpdateDefect();
  appointDefect();
}

async function handleSendOnVerifyDefect() {
  sendOnVerifyDefect();
}

async function handleDenyDefect() {
  denyDefect();
}

async function handleAcceptDefect() {
  acceptDefect();
}

function toDateTimeLocal(isoString: string) {
  const date = new Date(isoString);
  if (isNaN(date.getTime())) return '';
  
  const year = date.getFullYear();
  const month = (date.getMonth() + 1).toString().padStart(2, '0');
  const day = date.getDate().toString().padStart(2, '0');
  const hours = date.getHours().toString().padStart(2, '0');
  const minutes = date.getMinutes().toString().padStart(2, '0');

  return `${year}-${month}-${day}T${hours}:${minutes}`;
}

function goToProject(id: number) {
  router.push({ name: 'defects', params: { objectId: String(id) } });
}

</script>

<style>

</style>