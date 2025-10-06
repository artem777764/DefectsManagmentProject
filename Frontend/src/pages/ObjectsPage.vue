<template>
    <div class="flex flex-col gap-7 p-7">
        <AppInput v-model="searchQuery" :is-search="true" placeholder-value="Поиск..."/>
        <div>
            <AppText class="text-4xl">Проекты</AppText>
            <div class="grid grid-cols-4 gap-x-5 mt-5">
                <AppCard v-for="object in objects" :key="object.id" @click="goToProject(object.id)" class="mb-5 block cursor-pointer">
                    <AppCardContent>
                        <AppText class="font-medium text-2xl">{{ object.name }}</AppText>
                    </AppCardContent>
                </AppCard>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import AppCard from '@/components/layout/AppCard.vue';
import AppCardContent from '@/components/layout/AppCardContent.vue';
import AppInput from '@/components/ui/AppInput.vue';
import AppText from '@/components/ui/AppText.vue';
import { objectApi } from '@/lib/api/objectApi';
import type { GetObjectDTO } from '@/types/objects/getObjectDTO';
import { debounce } from "lodash";
import { onMounted, ref, watch } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();

const searchQuery = ref<string>('')
const objects = ref<GetObjectDTO[]>();

onMounted(() => {
  fetchObjects();
})

const fetchObjects = async () => {
  try {
    objects.value = await objectApi.getAll(searchQuery.value);
  } catch (error: any) {

  }
}

const debouncedFetch = debounce(fetchObjects, 400);

watch(searchQuery, () => {
  debouncedFetch();
});

function goToProject(id: number) {
  router.push({ name: 'defects', params: { objectId: String(id) } });
}

</script>

<style>
    
</style>