<script setup lang="ts">
import {ref} from "vue";
import Icon from "@/ui/misc/Icon.vue";

const isOpen = ref(false);

const props = defineProps<{
  label?: string,
  name?: string,
  value?: string
}>()

const autoCloseListener = e =>
{
  let elem = <HTMLElement> e.target;
  
  do {
    if (elem && elem.classList)
    {
      if (elem.classList.contains('select-box')) return;
      if (elem.classList.contains('select-box-item'))
      {
        toggleOverlay();
      }
    }

    elem = <HTMLElement> elem.parentNode;
  }
  while (elem);

  toggleOverlay();
  isOpen.value = false;
};

const toggleOverlay = () =>
{
  isOpen.value = !isOpen.value;
  
  if (isOpen.value)
  {
    window.addEventListener('click', autoCloseListener);
  }
  else
  {
    window.removeEventListener('click', autoCloseListener);
  }
}
</script>

<template>
<div class="select-box">
  <div class="select-box-inner" @click="toggleOverlay">
    <Icon name="expand"></Icon>
    <span>{{ label }}</span>
  </div>
  <div v-if="isOpen" class="select-box-popup">
    <div class="select-box-items">
      <slot />
    </div>
  </div>
  <input type="hidden" :name="name" :value="value" />
</div>
</template>

<style scoped>
.select-box {
  @apply border-b-2 text-base relative;
  background: var(--bg-mantle);
  color: var(--text-subtext-primary);
  
  .select-box-inner {
    @apply flex justify-between cursor-pointer px-2;
  }
  
  .select-box-popup {
    @apply absolute top-full border-2 flex flex-col px-2;
    background: var(--bg-mantle);
    left: calc(var(--select-box-width) / 2 * -1 + 50%);
    width: var(--select-box-width);
    --select-box-width: 150%;
    
    .select-box-items {
      @apply flex flex-col cursor-pointer;
    }
  }
}
</style>