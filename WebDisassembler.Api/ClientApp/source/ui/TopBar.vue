<script setup lang="ts">
  import { useProfileStore } from "@/stores/profile";
import { useRoute } from "vue-router";
  const route = useRoute();
  const profileStore = useProfileStore();


</script>

<template>
  <div class="topbar">
    <span>
      WebDisassembler
      <span v-if="route.name">
        - {{ route.name }}
      </span>
    </span>
    <ul>
    </ul>
    <div>
      <RouterLink v-if="! profileStore.isLoggedIn" to="/login">Login</RouterLink>
      <RouterLink v-if="profileStore.isLoggedIn && profileStore.isAdministrator" to="/admin/global">Admin</RouterLink>
      <RouterLink v-if="profileStore.isLoggedIn" to="/profile">Dashboard</RouterLink>
      <RouterLink v-if="profileStore.isLoggedIn" to="/logout">Logout</RouterLink>
    </div>
  </div>
</template>

<style scoped>
.topbar {
  @apply flex justify-between items-center px-8;
  background: var(--bg-crust);
}

a {
  @apply font-bold pl-2;
}
</style>