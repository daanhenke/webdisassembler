<script setup lang="ts">
  import TopBar from "@/ui/TopBar.vue";
  import {useProfileStore} from "@/stores/profile.ts";

  const sidebarItems = [
    {
      title: "Binaries",
      isOpenByDefault: true,
      items: [
        {
          title: "test.exe",
          items: [
            {
              title: "Symbols",
              items: [
                { title: "F_4240240" },
                { title: "F_4240240" },
                { title: "entry" }
              ]
            }
          ]
        },
        {
          title: "Upload new binary"
        }
      ]
    }
  ]
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