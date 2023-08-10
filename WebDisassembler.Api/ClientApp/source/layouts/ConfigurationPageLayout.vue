<template>
<div class="layout">
  <ConfigurationSidemenu />
  <div class="content">
    <slot />
  </div>
</div>
</template>

<style scoped>
.layout {
    @apply px-12 flex justify-between;
  .content {
    @apply flex flex-col;
  }
}
</style>
<script setup lang="ts">
import ConfigurationSidemenu from "@/ui/navigation/ConfigurationSidemenu.vue";
</script>