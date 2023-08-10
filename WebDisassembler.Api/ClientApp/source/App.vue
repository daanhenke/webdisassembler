<script setup lang="ts">
  import TopBar from "@/ui/TopBar.vue";
  import NotificationManager from "@/ui/notification/NotificationManager.vue";
</script>

<template>
<div class="application">
  <TopBar />
  <main>
    <RouterView />
  </main>
  <div>
    <div id="modal-container" />
    <NotificationManager />
  </div>
</div>
</template>

<style>
.application { 
  @apply flex flex-col h-full;

  main {
    @apply flex flex-col flex-grow;
  }

  #modal-container {
    @apply pointer-events-none left-0 right-0 top-0 bottom-0 absolute flex justify-center items-center;
  }

  #modal-container:not(:empty) {
    background-color: color-mix(in srgb, var(--bg-crust), transparent 10%);
  }
}
</style>