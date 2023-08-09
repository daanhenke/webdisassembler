<script setup lang="ts">

import {useThemeStore} from "@/stores/theme.ts";
import {onMounted, ref} from "vue";

const themeStore = useThemeStore();
const props = defineProps<{
  name: string
}>();
const iconRaw = ref('');
const fillColor = ref('');

const fetchIcon = async (name: string) =>
{
  if (themeStore.iconTheme === undefined) return;

  const theme = themeStore.iconTheme;
  const iconCfg = theme.icons[name];
  const isSimple = typeof iconCfg === 'string';

  const icon = isSimple ? iconCfg : iconCfg.file;
  fillColor.value = isSimple ? '' : iconCfg.fillColor;
  
  const iconPath = `${theme.base}/${theme.commonPrefix}${icon}${theme.commonSuffix}`;
  const response = await fetch(iconPath);
  
  iconRaw.value = await response.text();
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
<span class="icon" :style="{fill: fillColor}" v-html="iconRaw"></span>
</template>

<style>
.icon {  
  vertical-align: center;
}
.icon > svg { @apply w-6 h-6 align-middle; }
.icon.icon-action { @apply cursor-pointer; }
</style>