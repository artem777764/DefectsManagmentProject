<template>
  <input :disabled="isDisabled" v-model="model" :type="type" :placeholder="props.placeholderValue" :class="['bg-background placeholder-text w-full font-roboto rounded-lg shadow-md text-2xl p-3 hover:brightness-105 focus:brightness-105 focus:outline-none', colorClass]">
      <slot></slot>
  </input>
</template>

<script setup lang="ts">
import { computed } from 'vue';

interface Props {
  placeholderValue?: string,
  isHidden?: boolean,
  isDisabled?: boolean,
  isSearch?: boolean,
  isDate?: boolean,
}

const props = withDefaults(defineProps<Props>(), {
  placeholderValue: '',
  isHidden: false,
  isDisabled: false,
  isSearch: false,
  isDate: false,
})

const colorClass = computed(() => {
  if (props.isSearch) return 'bg-secondary';
  else return 'bg-background';
});

const type = computed(() => {
  if (props.isHidden) return 'password';
  else if (props.isDate) return 'date'
  else return 'text';
})

const model = defineModel();
</script>