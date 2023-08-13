<script setup lang="ts">
import AdminPageLayout from "@/layouts/AdminPageLayout.vue";
import ObjectTable from "@/ui/table/ObjectTable.vue";
import {nextTick, onMounted, ref} from "vue";
import {useAdminClient} from "@/api/http-clients.ts";
import {CreateAdminUser, QueryRequest} from "@/api/http/data-contracts.ts";
import Button from "@/ui/misc/Button.vue";
import Modal from "@/ui/modal/Modal.vue";
import Form from "@/ui/form/Form.vue";
import TextField from "@/ui/form/TextField.vue";
import SubmitButton from "@/ui/form/SubmitButton.vue";

const adminClient = useAdminClient();

const openCreateUserModal = ref(false);
const createAdminUserForm = ref<Form>();

const request: QueryRequest = { index: 0, size: 30, query: '' } 
const tableItems = ref([]);
const tableHeaders = [
  { key: "id", name: 'ID' },
  { key: "username", name: 'Username' },
  { key: "email", name: "Email" }
];

const updateUsers = async () =>
{
  const response = await adminClient.listUsers(request);
  tableItems.value = response.data.items;
};

const onCreateAdminUserSubmit = async (createAdminUser: CreateAdminUser) =>
{
  console.log('Create admin user', createAdminUser);
  openCreateUserModal.value = false;

  const response = await adminClient.createAdminUser(createAdminUser);
  if (response.ok) {
    await nextTick(updateUsers);
  }
}

onMounted(updateUsers);
</script>

<template>
  <AdminPageLayout>
    <h2>Users</h2>
    <div>
      <Button @click="openCreateUserModal = true;">Create admin</Button>
    </div>
    <ObjectTable :headers="tableHeaders" :items="tableItems" />
  </AdminPageLayout>
  <Modal v-if="openCreateUserModal" @close="openCreateUserModal = false;" title="Create admin">
    <Form ref="createAdminUserForm" @submit="onCreateAdminUserSubmit">
      <div>
        <label for="username">Username:</label>
        <TextField name="username" />
      </div>
      <div>
        <label for="email">E-mail:</label>
        <TextField name="email" />
      </div>
    </Form>
    <template v-slot:actions>
      <Button @click="createAdminUserForm.submit();">Create</Button>
    </template>
  </Modal>
</template>

<style scoped>

</style>