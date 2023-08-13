<script setup lang="ts">
import AdminPageLayout from "@/layouts/AdminPageLayout.vue";
import ObjectTable from "@/ui/table/ObjectTable.vue";
import {onMounted, ref} from "vue";
import {useAdminClient} from "@/api/http-clients.ts";
import {PagedQueryRequest} from "@/api/http/data-contracts.ts";
import Button from "@/ui/misc/Button.vue";
import Modal from "@/ui/modal/Modal.vue";
import Form from "@/ui/form/Form.vue";

const adminClient = useAdminClient();

const openCreateTenantModal = ref(false);

const request: PagedQueryRequest = { index: 0, size: 30, query: '' } 
const tableItems = ref([]);
const tableHeaders = [
  { key: "id", name: 'ID' },
  { key: "name", name: 'Name' },
  { key: "subdomain", name: "Subdomain" }
];

const searchBar = ref<HTMLInputElement>();

const updateTenants = async () =>
{
  request.query = searchBar.value.value;
  const response = await adminClient.listTenants(request);
  tableItems.value = response.data.items;
};

onMounted(updateTenants);
</script>

<template>
  <AdminPageLayout>
    <h2>Tenants</h2>
    <div class="action-group">
      <Button @click="openCreateTenantModal = true;">Create tenant</Button>
      <input class="textbox" ref="searchBar" @input="updateTenants()" type="text" />
    </div>
    <ObjectTable :headers="tableHeaders" :items="tableItems" />
  </AdminPageLayout>
  <Modal v-if="openCreateTenantModal" @close="openCreateTenantModal = false;" title="Create tenant">
    <Form>
      Test
    </Form>
    <template v-slot:actions>
      <Button>Create</Button>
    </template>
  </Modal>
</template>

<style scoped>
.action-group { 
  @apply flex justify-center w-full pb-4 items-center; 
  
  * { @apply mx-8; }
}
</style>