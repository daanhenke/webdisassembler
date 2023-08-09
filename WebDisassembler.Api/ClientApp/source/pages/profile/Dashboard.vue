<script setup lang="ts">
  import {useProfileStore} from "@/stores/profile.ts";
  import Icon from "@/ui/misc/Icon.vue";
  import SimplePageLayout from "@/layouts/SimplePageLayout.vue";
  import ProjectsView from "@/pages/profile/dashboard/ProjectsView.vue";

  const profileStore = useProfileStore();

</script>

<template>
  <SimplePageLayout>
      dashboard
    <div style="color: var(--success)">
      hello {{ profileStore.userId }} <Icon name="folder-default-closed"></Icon>
    </div>
    <ProjectsView />
  </SimplePageLayout>
</template>

<style scoped>
</style>