<template>
<div class="layout">
  <div class="content">
    <slot />
  </div>
</div>
</template>

<style scoped>
.layout {
    @apply px-12;
  .content {
    @apply flex flex-col;
  }
}
</style>