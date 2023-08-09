export interface ThemeManifest
{
    name: string;
    colors?: string[];
    icons?: string[];
}

export interface IconThemeFolderIcon
{
    open: string;
    closed: string;
}

export interface ColorTheme
{
    name: string;
    colors: {
        background: {
            base: string;
            mantle: string;
            crust: string;
        },
        text: {
            normal: string;
            subtextPrimary: string;
            subtextSecondary: string;
        },
        error: string;
        warning: string;
        success: string;
    }
}

export interface IconTheme
{
    name: string;
    base: string;
    commonPrefix?: string;
    commonSuffix?: string;
    icons: { [id: string]: string }
}