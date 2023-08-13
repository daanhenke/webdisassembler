import {MenuSection} from "@/ui/navigation/sidemenu-types.ts";

const config: MenuSection[] = [
  {
    title: 'Entities',
    items: [
      { title: 'Tenants', route: '/admin/tenants', icon: 'folder-default-closed' },
      { title: 'Users', route: '/admin/users' },
    ]
  },
  {
    title: 'Misc',
    items: [
      { title: 'Global', route: '/admin/global' }
    ]
  }
]

export default config;