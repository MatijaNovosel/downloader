
const routes = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { name: 'home', path: '', component: () => import('pages/Index.vue') },
      { name: 'downloader', path: 'downloader', component: () => import('pages/Downloader.vue') },
      {
        name: 'settings',
        path: 'settings',
        component: () => import('pages/Settings.vue'),
        children: [
          { name: 'settings-account', path: '/account', component: () => import('../components/Settings/Account.vue') },
          { name: 'settings-appearance', path: '/appearance', component: () => import('../components/Settings/Appearance.vue') }
        ]
      }
    ]
  }
]

// Always leave this as last one
if (process.env.MODE !== 'ssr') {
  routes.push({
    path: '*',
    component: () => import('pages/Error404.vue')
  })
}

export default routes
