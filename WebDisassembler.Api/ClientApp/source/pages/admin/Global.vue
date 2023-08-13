<script setup lang="ts">
import Button from "@/ui/misc/Button.vue";
import {useNotificationStore} from "@/stores/notification.ts";
import {useAdminClient} from "@/api/http-clients.ts";
import AdminPageLayout from "@/layouts/AdminPageLayout.vue";

const notificationStore = useNotificationStore();
const adminClient = useAdminClient();

const reindexElastic = async () =>
{
  const notification = notificationStore.createNotification('Reindexing', 'Working...');
  
  const response = await adminClient.reindexAll();
  if (response.ok)
  {
    notificationStore.updateNotification(notification, 'Done!');
    notificationStore.finishNotification(notification);
  }
}
</script>

<template>
  <AdminPageLayout>
    <Button @click="reindexElastic">Reindex Elastic</Button>
  </AdminPageLayout>
</template>
