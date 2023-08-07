<script setup lang="ts">
  import {useProfileStore} from "@/stores/profile.ts";

  const profileStore = useProfileStore();
</script>

<template>
  dashboard
  <div>
    hello {{ profileStore.userId }}
  </div>
</template>

<style scoped>

</style>