<script setup lang="ts">

import {useThemeStore} from "@/stores/theme.ts";
import {onMounted, ref} from "vue";

const themeStore = useThemeStore();
const props = defineProps<{
  name: string
}>();
let iconRaw = ref('');

const fetchIcon = async (name: string) =>
{
  if (themeStore.iconTheme === undefined) return;
  console.log(themeStore.iconTheme);

  const theme = themeStore.iconTheme;
  const icon = theme.icons[name];
  
  const iconPath = `${theme.base}/${theme.commonPrefix}${icon}${theme.commonSuffix}`;
  const response = await fetch(iconPath);
  
  iconRaw.value = await response.text();
  console.log(iconRaw, response)
}

onMounted(async () =>
{
  await fetchIcon(props.name);
})

themeStore.$subscribe(async () =>
{
  await fetchIcon(props.name);
});

</script>

<template>
<span class="icon" v-html="iconRaw"></span>
</template>

<style>
.icon {  
  vertical-align: center;
}
.icon > svg { @apply w-6 h-6 align-middle; }
.icon.icon-action { @apply cursor-pointer; }
</style>