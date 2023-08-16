<script setup lang="ts">
  import TopBar from "@/ui/TopBar.vue";
  import NotificationManager from "@/ui/notification/NotificationManager.vue";
  import CommandPalette from "@/ui/palette/CommandPalette.vue";
</script>

<template>
<div class="application">
  <TopBar />
  <main>
    <RouterView />
  </main>
  <div>
    <div id="overlay-container" />
    <NotificationManager />
    <CommandPalette />
  </div>
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