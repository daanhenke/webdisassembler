<script lang="ts" setup>
  import TabHeader from "@/ui/sidebar/TabHeader.vue";
  import {TreeItem} from "@/ui/sidebar/types.ts";
  import {ref} from "vue";
  
  const props = defineProps<TreeItem>();
  let isOpen = ref(props.isOpenByDefault);
  
  console.log(props.title, isOpen.value);
</script>

<template>
  <div class="tab-header-group">
    <span @click="isOpen = !isOpen; console.log('hi', isOpen)" class="header">
      {{ title }}
    </span>
    <div v-if="isOpen" class="items">
      <TabHeader v-for="item in items" :key="item.title" :title="item.title" :items="item.items" />
    </div>
  </div>
</template>

<style scoped>
  .tab-header-group {
    @apply flex flex-col;
    
    .header {
      @apply pl-4;
      background: var(--bg-crust);
    }
    
    .items {
      @apply flex flex-col h-min;
    }
  }
</style>