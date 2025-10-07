<template>
  <div class="flex flex-col gap-7 p-7">
    <div :v-if="roleId == 2" class="flex flex-row gap-7">
      <AppButton @click="goToCreateDefect()">Зарегистрировать дефект</AppButton>
      <AppButton>Создать новый отчёт</AppButton>
      <AppTextBlock>
        <AppText>Задач находится в работе:</AppText>
        <AppText>4221</AppText>
      </AppTextBlock>
    </div>
    <AppInput v-model="searchQuery" :is-search="true" placeholder-value="Поиск..."/>
    <div>
        <AppText class="text-4xl">Проекты</AppText>
        <div class="grid grid-cols-4 gap-x-5 mt-5">
            <AppCard v-for="defect in defects" :key="defect.id" @click="goToUpdateDefect(defect.id)" class="mb-5 block cursor-pointer">
                <AppCardContent>
                    <AppText class="font-medium text-2xl">{{ defect.title }}</AppText>
                    <AppLine/>
                    <AppText class="text-2xl text-justify line-clamp-3">{{ defect.description }}</AppText>
                </AppCardContent>

                <AppCardContent>
                    <AppLine/>
                    <AppText class="text-2xl">Статус: {{ defect.statusName }}</AppText>
                    <AppLine/>
                    <AppText class="font-medium text-2xl">Срок: {{ formatDeadline(defect.deadline) }}</AppText>
                </AppCardContent>
            </AppCard>
        </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import AppCard from '@/components/layout/AppCard.vue';
import AppCardContent from '@/components/layout/AppCardContent.vue';
import AppButton from '@/components/ui/AppButton.vue';
import AppInput from '@/components/ui/AppInput.vue';
import AppLine from '@/components/ui/AppLine.vue';
import AppText from '@/components/ui/AppText.vue';
import AppTextBlock from '@/components/ui/AppTextBlock.vue';
import { defectApi } from '@/lib/api/defectApi';
import { useUserStore } from '@/stores/userStore';
import type { GetDefectDTO } from '@/types/defects/getDefectDTO';
import { debounce } from 'lodash';
import { onMounted, ref, watch } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const roleId = useUserStore().roleId;

const props = defineProps<{
  objectId: string,
}>();

const objectId = Number(props.objectId);

const searchQuery = ref<string>('')
const defects = ref<GetDefectDTO[]>();

function formatDeadline(deadline: string | null | undefined)
{
    if (!deadline) return 'Не указан';
    const date = new Date(deadline);
    return date.toLocaleDateString();
}

onMounted(() => {
  fetchDefects();
})

const fetchDefects = async () => {
  try {
    defects.value = await defectApi.getByProject(objectId, searchQuery.value);
  } catch (error: any) {

  }
}

const debouncedFetch = debounce(fetchDefects, 400);

watch(searchQuery, () => {
  debouncedFetch();
});

function goToCreateDefect() {
  router.push({ name: 'defect-edit', params: { objectId: String(objectId), defectId: '0'} })
}

function goToUpdateDefect(id: number) {
  router.push({ name: 'defect-edit', params: { objectId: String(objectId), defectId: String(id) } })
}
</script>

<style>

</style>