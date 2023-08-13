<script setup lang="ts">
import ConfigurationSidemenu from "@/ui/navigation/ConfigurationSidemenu.vue";
import AdminSidemenu from "@/cfg/admin-sidemenu.ts";
</script>

<template>
<div class="layout">
  <ConfigurationSidemenu :sections="AdminSidemenu" />
  <div class="content">
    <slot />
  </div>
</div>
</template>

<style scoped>
.layout {
    @apply flex justify-between h-full;
  .content {
    @apply p-4 flex flex-col flex-grow;
  }
}
</style>