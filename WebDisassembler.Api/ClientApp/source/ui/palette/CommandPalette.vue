<script setup lang="ts">
import OverlayPane from "@/ui/misc/OverlayPane.vue";
import {onMounted, onUnmounted, ref} from "vue";
import {useCommandPaletteClient} from "@/api/http-clients.ts";
import {CommandType, GotoProjectCommand, GotoTenantCommand} from "@/api/http/data-contracts.ts";

const commandPaletteClient = useCommandPaletteClient();

const inputElement = ref<HTMLInputElement>();
const results = ref<(GotoProjectCommand | GotoTenantCommand)[]>();
const query = ref("test");
const isOpen = ref(false);

const onGlobalKey = (e: KeyboardEvent) =>
{
  if (e.key === '/') {
    isOpen.value = !isOpen.value;
  }
}

onMounted(() =>
{
  document.addEventListener('keyup', onGlobalKey);
});

onUnmounted(() =>
{
  document.removeEventListener('keyup', onGlobalKey);
});

const onOpen = () =>
{
  inputElement.value.focus();
}

const onInput = () =>
{
  fetchResults();
}

const fetchResults = async () =>
{
  const response = await commandPaletteClient.queryCommands({
    query: query.value
  });

  if (! response.ok) {
    return;
  }
  
  results.value = response.data;
}
</script>

<template>
  <OverlayPane v-if="isOpen">
    <div class="command-palette">
      <div class="results">
        <div class="result" v-for="result in results" :key="result.title">
          <div v-if="result.type === CommandType.GotoProject">
            Goto project {{ result.projectId }}
          </div>
          <div v-if="result.type === CommandType.GotoTenant">
            Goto tenant {{ result.tenantId }}
          </div>
        </div>
      </div>
      <input v-model="query" ref="inputElement" @input="onInput" class="textbox" type="text">
    </div>
  </OverlayPane>
</template>

<style scoped>
.command-palette {
  @apply w-2/3 rounded-lg min-h-1/8 max-h-3/4 flex flex-col;
  @apply absolute bottom-1/8;
  background: var(--bg-crust);
  border: var(--text-normal) 3px solid;
  
  .results {
    @apply h-full flex-grow overflow-y-auto;
  }
}
</style>