<script setup lang="ts">
import {useNotificationStore, Notification} from "@/stores/notification.ts";
import {onMounted, ref} from "vue";

const notificationStore = useNotificationStore();
const notification = ref<Notification | undefined>();
let isShowing = false;

const tryShowNotification = () =>
{
  if (notificationStore.pendingNotifications.length === 0 || isShowing)
  {
    return;
  }
  
  notification.value = notificationStore.popNext();
  isShowing = true;
  setTimeout(() =>
  {
    notification.value = undefined;
    isShowing = false;
    setTimeout(tryShowNotification, 1000);
  }, 5000);
}

notificationStore.$subscribe(() =>
{
  tryShowNotification();  
});

onMounted(() =>
{
  tryShowNotification();
});
</script>

<template>
  <div v-if="notification" class="notification-container">
    <span class="notification-title">{{ notification.title }}</span>
    <p class="notification-body">{{ notification.body }}</p>
  </div>
</template>

<style scoped>
.notification-container {
  @apply fixed right-0 bottom-0 m-4 p-2;
  background: var(--bg-crust);
  
  .notification-title {
    @apply text-lg font-semibold;
  }
}
</style>