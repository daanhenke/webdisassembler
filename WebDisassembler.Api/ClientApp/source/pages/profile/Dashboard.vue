<script setup lang="ts">
  import {useProfileStore} from "@/stores/profile.ts";
  import Icon from "@/ui/misc/Icon.vue";
  import SimplePageLayout from "@/layouts/SimplePageLayout.vue";
  import Button from "@/ui/misc/Button.vue";
  import Modal from "@/ui/modal/Modal.vue";
  import TextField from "@/ui/form/TextField.vue";
  import { useProjectsClient } from "@/api/http-clients";
  import { onMounted, ref } from "vue";

  const profileStore = useProfileStore();
  const projectsClient = useProjectsClient();

  let pagedRequest =
  {
    index: 0,
    size: 12
  };
  let projects = ref<ProjectSummary[]>([]);
  let doCreateProject = ref(false);

  onMounted(async () =>
  {
    const result = await projectsClient.projectsListCreate(pagedRequest);
    projects.value = result.data.items;
  });
</script>

<template>
  <SimplePageLayout>
      dashboard
    <div style="color: var(--success)">
      hello {{ profileStore.userId }} <Icon name="folder-default-closed"></Icon>
    </div>
    <div class="button-group">
        <Button @click="doCreateProject = true;" class="button-success">
          Create Project
        </Button>
    </div>
    <div class="projects-list">
      <div class="project-snippet-container" v-for="project in projects" :key="project.id">
        <RouterLink :to="`/project/${project.id}/editor`">
          <div class="project-snippet">
            <span class="project-name">{{ project.name }}</span>
            <span>{{ project.shortDescription }}</span>
          </div>
        </RouterLink>
      </div>
    </div>
  </SimplePageLayout>
  <Modal @close="doCreateProject = false;" title="Create project" v-if="doCreateProject">
    <div class="form-item">
      <label for="name">Name:</label>
      <TextField name="name" />
    </div>
    <template v-slot:actions>
      <Button>Create</Button>
    </template>
  </Modal>
</template>

<style scoped>
.projects-list {
  @apply flex flex-wrap;

  .project-snippet-container {
    @apply w-1/3;

    .project-snippet {
      @apply border-2 rounded-xl p-4 flex flex-col flex-grow m-4;

      .project-name {
        @apply text-lg font-semibold;
      }
    }
  }
}

.form-item { @apply flex justify-between items-center; }
</style>