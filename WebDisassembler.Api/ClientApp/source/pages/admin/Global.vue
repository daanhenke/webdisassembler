<script setup lang="ts">
import Button from "@/ui/misc/Button.vue";
import {useNotificationStore} from "@/stores/notification.ts";
import {useAdminClient} from "@/api/http-clients.ts";

const notificationStore = useNotificationStore();
const adminClient = useAdminClient();

const reindexElastic = async () =>
{
  notificationStore.addNotification({
    title: 'Reindexing',
    body: ''
  });
  
  const response = await adminClient.reindexAll();
  if (response.ok)
  {
    notificationStore.addNotification({ title: 'Success', body: 'Reindexed elastic' })
  }
}
</script>

<template>
  
  <Button @click="reindexElastic">Reindex Elastic</Button>
</template>
