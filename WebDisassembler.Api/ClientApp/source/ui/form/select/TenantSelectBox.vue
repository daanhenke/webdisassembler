<script setup lang="ts">

import SelectBox from "@/ui/form/SelectBox.vue";
import {onMounted, ref} from "vue";
import {useTenantsClient} from "@/api/http-clients.ts";
import {TenantSummary} from "@/api/http/data-contracts.ts";

const tenantsClient = useTenantsClient();

const tenants = ref<TenantSummary[]>([]);
const chosen = ref<{
  id?: string,
  name?: string
}>({});

const props = defineProps<{
  name: string;
}>();

onMounted(async () =>
{
  const response = await tenantsClient.getPublicTenants({
    index: 0,
    size: 12
  });
  
  tenants.value = response.data.items;
});

const setValue = (tenant: TenantSummary) =>
{
  chosen.value.id = tenant.id;
  chosen.value.name = tenant.name;
}
</script>

<template>
<SelectBox class="tenant-select-box" :label="chosen.name" :value="chosen.id" :name="name">
  <div class="select-box-item" v-for="tenant in tenants" :key="tenant.id" @click="setValue(tenant)">
    <span class="tenant-name">{{ tenant.name }}</span>
    <span>{{ tenant.id }}</span>
  </div>
</SelectBox>
</template>

<style>
.select-box-item { @apply flex flex-col; }
.tenant-name { @apply font-semibold text-lg; }

</style>