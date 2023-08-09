﻿import {ColorTheme, IconTheme, ThemeManifest} from "@/lib/theming/cfg-models.ts";

interface Store
{
    colorThemes: ColorTheme[],
    iconThemes: IconTheme[]
}

const store: Store = {
    colorThemes: [],
    iconThemes: []
};

export async function loadInitialTheme()
{
    await loadTheme("/themes/default");
    await applyColorTheme("Catppuccin Mocha")
}

export function applyColorTheme(name: string)
{
    const theme = store.colorThemes.find(t => t.name === name);
    if (theme === undefined)
    {
        throw 'ohno';
    }
    
    const root = <HTMLElement> document.querySelector(':root')!;
    applyColor(root, 'bg-base', theme.colors.background.base)
    applyColor(root, 'bg-mantle', theme.colors.background.mantle)
    applyColor(root, 'bg-crust', theme.colors.background.crust)

    applyColor(root, 'text-normal', theme.colors.text.normal)
    applyColor(root, 'text-subtext-primary', theme.colors.text.subtextPrimary)
    applyColor(root, 'text-subtext-secondary', theme.colors.text.subtextSecondary)

    applyColor(root, 'error', theme.colors.error)
    applyColor(root, 'warning', theme.colors.warning)
    applyColor(root, 'success', theme.colors.success)
}

function applyColor(root: HTMLElement, cssName: string, value: string)
{
    root.style.setProperty(`--${cssName}`, value);
}

export async function loadTheme(path: string)
{
    const manifest = await fetchAsJson<ThemeManifest>(`${path}/manifest.json`);
    const promises: Promise<void>[] = [];

    if (manifest.colors !== undefined)
    {
        promises.push(...manifest.colors.map(file => loadColorTheme(`${path}/${file}`)));
    }
    if (manifest.icons !== undefined)
    {
        promises.push(...manifest.icons.map(file => loadIconTheme(`${path}/${file}`)));
    }
    
    await Promise.all(promises);
}

async function loadColorTheme(url: string)
{
    store.colorThemes.push(await fetchAsJson<ColorTheme>(url));
}

async function loadIconTheme(url: string)
{
    store.iconThemes.push(await fetchAsJson<IconTheme>(url));
}

async function fetchAsJson<T>(url: string): Promise<T>
{
    const response = await fetch(url);
    return <T> await response.json();
}