<script setup lang="ts">

</script>

<template>
<div class="overlay-pane">
  <slot />
</div>
</template>

<style>
.overlay-pane {
  @apply pointer-events-none left-0 right-0 top-0 bottom-0 absolute flex justify-center items-center;
  background-color: color-mix(in srgb, var(--bg-crust), transparent 10%);
  animation: fade-in .15s ease-in;

  * {
    @apply pointer-events-auto;
  }
}

@keyframes fade-in {
  from {
    opacity: 0;
  }
  
  to {
    opacity: 1;
  }
}
</style>