<script lang="ts" setup>
const props = withDefaults(defineProps<{
        type?: 'text' | 'password' | 'email',
        name: string
    }>(),
    {
        type: 'text'
    });
</script>

<template>
    <input class="textbox" :type="props.type" :name="props.name"  />
</template>

<style>
.textbox {
  @apply border-b-2 text-base px-2 h-10;
  background: var(--bg-mantle);
  color: var(--text-subtext-primary);
}
</style>