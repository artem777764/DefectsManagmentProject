<template>
  <textarea
    v-model="model"
    :placeholder="props.placeholderValue"
    :rows="props.rows"
    @input="autoResize"
    ref="textareaRef"
    class="bg-background text-justify placeholder-text w-full font-roboto rounded-lg shadow-md text-2xl p-3 hover:brightness-105 focus:brightness-105 focus:outline-none resize-none"
  ></textarea>
</template>

<script setup lang="ts">
import { nextTick, ref, watch } from 'vue';

interface Props {
  placeholderValue?: string,
  rows?: number,
}

const props = withDefaults(defineProps<Props>(), {
  placeholderValue: '',
  rows: 3,
})

const model = defineModel<string>();
const textareaRef = ref<HTMLTextAreaElement | null>(null);

const autoResize = () => {
  nextTick(() => {
    if (textareaRef.value) {
      textareaRef.value.style.height = 'auto';
      textareaRef.value.style.height = textareaRef.value.scrollHeight + 'px';
    }
  });
};

nextTick(() => {
  if (model.value && textareaRef.value) {
    textareaRef.value.style.height = 'auto';
    textareaRef.value.style.height = textareaRef.value.scrollHeight + 'px';
  }
});

watch(model, autoResize);
</script>