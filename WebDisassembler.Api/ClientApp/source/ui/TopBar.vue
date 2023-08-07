<script setup lang="ts">
  import {ref} from "vue";

  let subtitle = ref<string>(null);
</script>

<template>
  <span>
    WebDisassembler
    <span v-if="subtitle != null">
      - {{ subtitle }}
    </span>
  </span>
</template>

<style scoped>

</style>