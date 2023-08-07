export interface TreeItem
{
    title?: string,
    items?: Array<TreeItem>,
    isOpenByDefault?: boolean
}