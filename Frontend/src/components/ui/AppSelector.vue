<template>
  <div>
    <select
      v-model="selectedValue"
      class="bg-background w-full font-roboto rounded-lg shadow-md text-2xl p-3 hover:brightness-105 focus:brightness-105 focus:outline-none "
    >
      <option :value="0">{{ props.placeholder }}</option>
      <option
        v-for="option in props.options"
        :key="option.value"
        :value="option.value"
      >
        {{ option.name }}
      </option>
    </select>
  </div>
</template>

<script setup lang="ts">
import type { Option } from '@/types/option';

interface Props {
  options?: Option[];
  placeholder?: string;
}

const props = defineProps<Props>();

const selectedValue = defineModel<number | undefined>({ 
  default: 0,
  required: true 
});
</script>