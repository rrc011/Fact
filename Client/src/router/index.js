import Vue from 'vue'
import Router from 'vue-router'

import Default from '@/components/Default'
import WarehouseIndex from '@/components/Warehouse/Index'
import WarehouseCreateOrUpdate from '@/components/Warehouse/CreateOrUpdate'
import ProductIndex from '@/components/Products/Index'
import ProductCreateOrUpdate from '@/components/Products/CreateOrUpdate'
import ClientIndex from '@/components/Clients/Index'
import ClientCreateOrUpdate from '@/components/Clients/CreateOrUpdate'
import InvoicingIndex from '@/components/Invoicing/Index'
import InvoicingCreateOrUpdate from '@/components/Invoicing/CreateOrUpdate'
import InvoicingDetail from '@/components/Invoicing/Detail'


Vue.use(Router)

const routes = [
  { path: '/', name: 'Default', component: Default },
  { path: '/warehouse/page/:page', name: 'Warehouse', component: WarehouseIndex},
  { path: '/warehouse/add/', name: 'WarehouseCreate', component: WarehouseCreateOrUpdate },
  { path: '/warehouse/:id/edit', name: 'WarehouseEdit', component: WarehouseCreateOrUpdate },
  { path: '/product/page/:page', name: 'Product', component: ProductIndex},
  { path: '/product/add/', name: 'ProductCreate', component: ProductCreateOrUpdate },
  { path: '/product/:id/edit', name: 'ProductEdit', component: ProductCreateOrUpdate },
  { path: '/client/page/:page', name: 'Client', component: ClientIndex},
  { path: '/client/add/', name: 'ClientCreate', component: ClientCreateOrUpdate },
  { path: '/client/:id/edit', name: 'ClientEdit', component: ClientCreateOrUpdate },
  { path: '/invoicing/page/:page', name: 'Invoicing', component: InvoicingIndex},
  { path: '/invoicing/add/', name: 'InvoicingCreate', component: InvoicingCreateOrUpdate },
  { path: '/invoicing/:id/edit', name: 'InvoicingEdit', component: InvoicingCreateOrUpdate },
  { path: '/invoicing/:id/detail', name: 'InvoicingDetail', component: InvoicingDetail },
]

export default new Router({
  routes
})
