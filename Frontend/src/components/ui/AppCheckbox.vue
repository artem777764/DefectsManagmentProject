<template>
  <input v-model="model" type="checkbox" class="appearance-none w-8 h-8 border-2 border-primary rounded-sm checked:bg-primary checked:border-primary bg-background shadow-md hover:brightness-105"/>
</template>

<script setup lang="ts">
const model = defineModel();
</script>