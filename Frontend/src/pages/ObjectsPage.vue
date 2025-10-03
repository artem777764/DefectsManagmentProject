<template>
    <div class="flex flex-col gap-7 p-7">
        <AppInput v-model="searchQuery" :is-search="true" placeholder-value="Поиск..."/>
        <div>
            <AppText class="text-4xl">Задачи</AppText>
            <div class="grid grid-cols-4 gap-x-5 mt-5">
                <AppCard v-for="object in objects" :id="object.id" class="mb-5 block">
                    <AppCardContent>
                        <AppText class="font-medium">{{ object.name }}</AppText>
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
import type { GetObjectDTO } from '@/types/user/getObjectDTO';
import { debounce } from "lodash";
import { onMounted, ref, watch } from 'vue';

const searchQuery = ref<string>('')
const objects = ref<GetObjectDTO[]>();

onMounted(() => {
  fetchObjects();
})

const fetchObjects = async () => {
  try {
    objects.value = await objectApi.getAll(searchQuery.value);
    console.log(objects.value);
  } catch (error: any) {

  }
}

const debouncedFetch = debounce(fetchObjects, 400);

watch(searchQuery, () => {
  debouncedFetch();
});

</script>

<style>
    
</style>