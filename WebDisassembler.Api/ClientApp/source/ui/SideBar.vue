<script lang="ts" setup>
import TabHeaderGroup from "@/ui/sidebar/TabHeaderGroup.vue";
import {TreeItem} from "@/ui/sidebar/types.ts";

defineProps<{
  items: TreeItem[]
}>()

</script>

<template>
  <div class="sidebar">
    <TabHeaderGroup v-for="item in items" :key="item.title" :title="item.title" :items="item.items" :isOpenByDefault="item.isOpenByDefault" />
  </div>
</template>

<style scoped>
  .sidebar {
    @apply flex flex-col w-1/4;
    background: var(--bg-darker);
  }
</style>