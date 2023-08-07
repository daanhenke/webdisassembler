<script setup lang="ts">
  import {TreeItem} from "@/ui/sidebar/types.ts";
  import {ref} from "vue";

  const props = defineProps<TreeItem>();
  let isOpen = ref(props.isOpenByDefault);
</script>

<template>
  <div class="tab-header">
    <span @click="isOpen = !isOpen; console.log('hi', isOpen, items)" class="header">
      {{ title }}
    </span>
    <div v-if="isOpen" class="items">
      <TabHeader v-for="item in items" :key="item.title" :title="item.title" :items="item.items" />
    </div>
  </div>
</template>

<style scoped>
.tab-header {
  @apply flex flex-col;

  .header {
    @apply pl-4;
  }

  .items {
    @apply flex flex-col h-min pl-4;
  }
}
</style>