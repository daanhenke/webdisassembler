<script setup lang="ts">
import Icon from "@/ui/misc/Icon.vue";

interface TableHeader
{
  key: string;
  name: string;
}

const props = defineProps<{
  headers: TableHeader[],
  items: object[]
}>();

</script>

<template>
<table class="table">
  <tr class="table-headers">
    <th class="table-header" v-for="header in headers" :key="header.key">{{ header.name }}</th>
    <th />
  </tr>
  <tr v-for="item in items" class="table-row">
    <td class="table-item" v-for="header in headers">{{ item[header.key] }}</td>
    <td class="context-menu">
      <div>
        <Icon name="folder-default-open" />
      </div>
    </td>
  </tr>
</table>
</template>

<style scoped>
.table {
  background: var(--bg-mantle);

  .table-header, .table-item {
    @apply px-4;
  }
  
  .context-menu {
    table-layout: fixed;
    * {
      @apply flex justify-center;
    }
  }
}

table, td, th {
  @apply border-2 border-collapse;
}
</style>