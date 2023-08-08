<script lang="ts" setup>
  interface TreeItem
  {
    label: string,
    children?: TreeItem[]
  }
  
  defineProps<TreeItem>()
</script>

<template>
  <span>{{ label }}</span>
  <div v-if="children" v-for="item in children" :key="item.label">
    <TreeViewItem :label="item.label" :children="item.children" />
  </div>
</template>