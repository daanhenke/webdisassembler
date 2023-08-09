import {defineStore} from "pinia";
import {ColorTheme, IconTheme} from "@/lib/theming/cfg-models.ts";

interface ThemeStoreState
{
    iconTheme?: IconTheme
    colorTheme?: ColorTheme
}

export const useThemeStore = defineStore('theme', {
    state: () => {
        return <ThemeStoreState> {
            iconTheme: undefined,
            colorTheme: undefined
        }
    },
    actions: {
        refreshIcons(theme: IconTheme) {
            this.iconTheme = theme;
        },
        refreshColors(theme: ColorTheme) {
            this.colorTheme = theme;
        }
    }
})