<template>
  <input v-model="model" :type="props.isHidden ? 'password' : 'text'" :placeholder="props.placeholderValue" class="bg-background placeholder-text w-full font-roboto rounded-lg shadow-md text-2xl p-3 hover:brightness-105 focus:brightness-105 focus:outline-none">
      <slot></slot>
  </input>
</template>

<script setup lang="ts">
interface Props {
  placeholderValue?: string,
  isHidden?: boolean,
}

const props = withDefaults(defineProps<Props>(), {
  placeholderValue: '',
  isHidden: false
})

const model = defineModel();
</script>