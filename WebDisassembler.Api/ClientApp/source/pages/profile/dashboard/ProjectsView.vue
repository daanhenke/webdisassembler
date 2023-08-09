<script setup lang="ts">
import {useProjectsClient,useTenantsClient} from "@/api/http-clients.ts";
import {onMounted, ref} from "vue";
import Modal from "@/ui/modal/Modal.vue";
import TextField from "@/ui/form/TextField.vue";
import Button from "@/ui/misc/Button.vue";
import Form from "@/ui/form/Form.vue";
import {ProjectSummary} from "@/api/http/data-contracts.ts";
import SelectBox from "@/ui/form/SelectBox.vue";
import TenantSelectBox from "@/ui/form/select/TenantSelectBox.vue";

const projectsClient = useProjectsClient();
const tenantsClient = useTenantsClient();

let pagedRequest =
    {
      index: 0,
      size: 12
    };
let projects = ref<ProjectSummary[]>([]);
const doCreateProject = ref(false);
const createProjectForm = ref<Form>();

onMounted(async () =>
{
  const result = await projectsClient.projectsListCreate(pagedRequest);
  projects.value = result.data.items;
});

const onCreateTenantSubmit = createTenant =>
{
  console.log('Create tenant', createTenant);
  doCreateProject.value = false;
}
</script>

<template>
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
  <Modal @close="doCreateProject = false;" title="Create project" v-if="doCreateProject">
    <Form @submit="onCreateTenantSubmit" ref="createProjectForm">
      <div>
        <label for="name">Name:</label>
        <TextField name="name" />
      </div>
      <div>
        <label for="tenantId">Tenant:</label>
        <TenantSelectBox name="tenantId" />
      </div>
    </Form>
    <template v-slot:actions>
      <Button @click="createProjectForm.submit();">Create</Button>
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
</style>