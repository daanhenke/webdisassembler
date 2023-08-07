<script setup lang="ts">
  import TopBar from "@/ui/TopBar.vue";
</script>

<template>
<div class="application">
  <TopBar />
  <main>
    <RouterView />
  </main>
</div>
</template>

<style>
.application { 
  @apply flex flex-col h-full;

  main {
    @apply flex flex-col flex-grow;
  }
}
</style>