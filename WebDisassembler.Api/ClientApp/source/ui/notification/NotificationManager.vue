<script setup lang="ts">
import {useNotificationStore} from "@/stores/notification.ts";
const notificationStore = useNotificationStore();
</script>

<template>
  <div v-if="notificationStore.hasNotifications" class="notification-container">
    <div v-for="notification in notificationStore.orderedNotifications" class="notification">
      <span class="notification-title">{{ notification.title }}</span>
      <p class="notification-body">{{ notification.body }}</p>
    </div>
  </div>
</template>

<style scoped>
.notification-container {
  @apply fixed right-0 bottom-0 m-4 p-2;
  background: var(--bg-crust);
  
  .notification {
    .notification-title {
      @apply text-lg font-semibold;
    }
  }
}
</style>