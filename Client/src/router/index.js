import Vue from 'vue'
import Router from 'vue-router'

import Default from '@/components/Default'
import ExampleIndex from '@/components/example/Index'
import ExampleView from '@/components/example/View'
import PersonaIndex from '@/components/Persona/Index'
import PersonaCreateOrUpdate from '@/components/Persona/CreateOrUpdate'
import WarehouseIndex from '@/components/Warehouse/Index'
import WarehouseCreateOrUpdate from '@/components/Warehouse/CreateOrUpdate'
import ProductIndex from '@/components/Products/Index'
import ProductCreateOrUpdate from '@/components/Products/CreateOrUpdate'
import ClientIndex from '@/components/Clients/Index'
import ClientCreateOrUpdate from '@/components/Clients/CreateOrUpdate'


Vue.use(Router)

const routes = [
  { path: '/', name: 'Default', component: Default },
  { path: '/example', name: 'ExampleIndex', component: ExampleIndex },
  { path: '/example/:id', name: 'ExampleView', component: ExampleView },
  { path: '/persona/page/:page/:tipo', name: 'Persona', component: PersonaIndex },
  { path: '/persona/add/:tipo', name: 'PersonaCreate', component: PersonaCreateOrUpdate },
  { path: '/persona/:id/edit', name: 'PersonaEdit', component: PersonaCreateOrUpdate },
  { path: '/warehouse/page/:page', name: 'Warehouse', component: WarehouseIndex},
  { path: '/warehouse/add/', name: 'WarehouseCreate', component: WarehouseCreateOrUpdate },
  { path: '/warehouse/:id/edit', name: 'WarehouseEdit', component: WarehouseCreateOrUpdate },
  { path: '/product/page/:page', name: 'Product', component: ProductIndex},
  { path: '/product/add/', name: 'ProductCreate', component: ProductCreateOrUpdate },
  { path: '/product/:id/edit', name: 'ProductEdit', component: ProductCreateOrUpdate },
  { path: '/client/page/:page', name: 'Client', component: ClientIndex},
  { path: '/client/add/', name: 'ClientCreate', component: ClientCreateOrUpdate },
  { path: '/client/:id/edit', name: 'ClientEdit', component: ClientCreateOrUpdate },
]

export default new Router({
  routes
})
