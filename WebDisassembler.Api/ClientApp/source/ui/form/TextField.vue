<script lang="ts" setup>
const props = withDefaults(defineProps<{
        type: 'text' | 'password' | 'email',
        name: string
    }>(),
    {
        type: 'text'
    });
</script>

<template>
    <input :type="props.type" :name="props.name"  />
</template>

<style scoped>
input {
  @apply border-b-2 text-base;
  background: var(--bg-mantle);
  color: var(--text-subtext-primary);
}
</style>