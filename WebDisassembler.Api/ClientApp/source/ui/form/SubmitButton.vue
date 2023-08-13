<script lang="ts" setup>
const props = withDefaults(defineProps<{
        text: string
    }>(),
    {
        text: 'Submit'
    });
</script>

<template>
    <input class="button" type="submit" />
</template>

<style>
input {
  @apply border-b-2 text-base px-2;
  background: var(--bg-mantle);
  color: var(--text-subtext-primary);
}
</style>