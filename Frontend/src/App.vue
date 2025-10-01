<script setup lang="ts">
import { RouterView } from 'vue-router';
import AppNavBar from '@/components/layout/AppNavBar.vue';

</script>

<template>
  <div class="min-h-screen bg-background">
    <AppNavBar class="sticky top-0" v-if="$route.meta.showNavbar"/>
    <RouterView/>
  </div>
</template>

<style scoped>

</style>