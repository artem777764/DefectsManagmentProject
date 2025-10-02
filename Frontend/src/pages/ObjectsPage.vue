<template>
    <div class="flex flex-col gap-7 p-7">
        <AppInput :is-search="true" placeholder-value="Поиск..."/>
        <div>
            <AppText class="text-4xl">Задачи</AppText>
            <AppCard>
                <AppCardContent>
                    <p>Член</p>
                </AppCardContent>
                <AppCardContent>
                    <p>Член</p>
                </AppCardContent>
                <AppCardContent>
                    <AppLine></AppLine>
                    <p>Член</p>
                </AppCardContent>
            </AppCard>
        </div>
    </div>
</template>

<script setup lang="ts">
import AppCard from '@/components/layout/AppCard.vue';
import AppCardContent from '@/components/layout/AppCardContent.vue';
import AppInput from '@/components/ui/AppInput.vue';
import AppLine from '@/components/ui/AppLine.vue';
import AppText from '@/components/ui/AppText.vue';

</script>

<style>
    
</style>