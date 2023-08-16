<script lang="ts" setup>
import Icon from '@/ui/misc/Icon.vue';
import OverlayPane from "@/ui/misc/OverlayPane.vue";
const props = defineProps<{
    title: string
}>();
const emit = defineEmits(['close']);
</script>

<template>
    <Teleport to="#overlay-container">
        <OverlayPane>
          <div class="modal">
            <div class="modal-header">
              <span class="modal-title">{{ props.title }}</span>
              <div>
                <Icon class="icon-action" @click="emit('close');" name="close" />
              </div>
            </div>
            <div class="modal-content">
              <slot />
            </div>
            <div class="modal-actions">
              <slot name="actions" />
            </div>
          </div>
        </OverlayPane>
    </Teleport>
</template>

<style scoped>
.modal {
    @apply flex flex-col min-w-1/3 min-h-1/3 rounded-lg border-2 pointer-events-auto;
    background: var(--bg-base);

    .modal-header {
        @apply flex justify-between items-center border-b-2 px-4 py-2;

        .modal-title {
            @apply text-lg font-semibold pointer-events-none;
        }
    }

    .modal-content {
        @apply flex flex-col px-6 py-4;
    }

    .modal-actions {
        @apply mt-auto px-3 py-1 flex justify-end;
    }
}
</style>