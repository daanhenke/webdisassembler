export interface MenuItem
{
    title: string;
    route: string;
    icon?: string;
}

export interface MenuSection
{
    title: string;
    items: MenuItem[]
}
