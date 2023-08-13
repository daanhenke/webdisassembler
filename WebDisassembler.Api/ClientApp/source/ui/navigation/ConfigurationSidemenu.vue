<script setup lang="ts">
import Icon from "@/ui/misc/Icon.vue";
import {MenuSection} from "@/ui/navigation/sidemenu-types.ts";

const props = defineProps<{
  sections: MenuSection[]
}>();
</script>

<template>
  <div class="sidemenu">
    <span class="menu-title">Admin</span>
    <div v-for="section in sections" :key="section.title" class="section">
      <span class="section-title">{{ section.title }}</span>
      <div v-for="item in section.items" :key="item.title">
        <RouterLink :to="item.route">
          <Icon :name="item.icon ?? 'folder-default-closed'" />
          <span class="item-title">{{ item.title }}</span>
        </RouterLink>
      </div>
    </div>
  </div>
</template>

<style scoped>
.sidemenu {
  @apply flex flex-col min-w-1/3 px-4;
  background: var(--bg-mantle);
  
  .menu-title {
    @apply pb-3 font-semibold;
  }
  
  .section {
    @apply pb-6;
    .section-title {
      @apply font-semibold;
    }
    
    .item-title {
      @apply pl-2;
    }
  }
}
</style>