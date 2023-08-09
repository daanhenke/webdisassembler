<script setup lang="ts">
  import {useProfileStore} from "@/stores/profile.ts";
  import Icon from "@/ui/misc/Icon.vue";

  const profileStore = useProfileStore();
</script>

<template>
  dashboard
  <div style="color: var(--success)">
    hello {{ profileStore.userId }} <Icon name="folder-default-closed"></Icon>
  </div>
</template>

<style scoped>

</style>