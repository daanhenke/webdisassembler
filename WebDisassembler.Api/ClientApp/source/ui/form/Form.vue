<script setup lang="ts">
import {ref} from "vue";

const element = ref<HTMLFormElement>();

const emit = defineEmits(['submit'])

const submit = () =>
{
  const result = {};
  const inputElements = <HTMLInputElement[]> element.value.querySelectorAll('input');
  
  for (let inputElement of inputElements)
  {
    result[inputElement.name] = inputElement.value;
  }
  
  emit('submit', result);
}

const onElementSubmit = e =>
{
  e.preventDefault();
  submit();
}

defineExpose({
  submit
})
</script>

<template>
  <form ref="element" @submit="onElementSubmit">
    <slot />
  </form>
</template>

<style>
form > div { @apply flex justify-between items-baseline py-2; }
form > div > * { @apply min-w-1/2; }
</style>