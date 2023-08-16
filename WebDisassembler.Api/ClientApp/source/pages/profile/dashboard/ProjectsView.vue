<script setup lang="ts">
import {useProjectsClient,useTenantsClient} from "@/api/http-clients.ts";
import {nextTick, onMounted, ref} from "vue";
import Modal from "@/ui/modal/Modal.vue";
import TextField from "@/ui/form/TextField.vue";
import Button from "@/ui/misc/Button.vue";
import Form from "@/ui/form/Form.vue";
import {CreateProject, ProjectSummary} from "@/api/http/data-contracts.ts";
import TenantSelectBox from "@/ui/form/select/TenantSelectBox.vue";

const projectsClient = useProjectsClient();

let pagedRequest =
    {
      index: 0,
      size: 12
    };
let projects = ref<ProjectSummary[]>([]);
const doCreateProject = ref(false);
const createProjectForm = ref<Form>();

const refreshProjects = async () =>
{
  const result = await projectsClient.projectsListCreate(pagedRequest);
  projects.value = result.data.items;
}

onMounted(async () =>
{
  await refreshProjects();
});

const onCreateTenantSubmit = async (createProject: CreateProject) =>
{
  console.log('Create project', createProject);
  doCreateProject.value = false;
  
  const response = await projectsClient.projectsCreateCreate(createProject);
  if (response.ok) {
    //alert('cool!');
    await nextTick(refreshProjects);
  }
}
</script>

<template>
  <div class="button-group">
    <Button @click="doCreateProject = true;" class="button-success">
      Create Project
    </Button>
  </div>
  <div class="projects-list">
    <RouterLink class="project-snippet" v-for="project in projects" :key="project.id" :to="`/project/${project.id}`">
      <span class="project-name">{{ project.name }}</span>
      <span>{{ project.shortDescription }}</span>
    </RouterLink>
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
      <div>
        <label for="shortDescription">Short Description:</label>
        <TextField name="shortDescription" />
      </div>
    </Form>
    <template v-slot:actions>
      <Button @click="createProjectForm.submit();">Create</Button>
    </template>
  </Modal>
</template>

<style scoped>
.projects-list {
  @apply flex flex-wrap items-stretch;

  .project-snippet {
    @apply border-2 rounded-xl p-4 flex flex-col;
    margin: .5rem;
    width: calc(33.3% - 1rem);

    .project-name {
      @apply text-lg font-semibold;
    }
  }
}
</style>