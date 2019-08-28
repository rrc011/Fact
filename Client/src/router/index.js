import Vue from 'vue'
import Router from 'vue-router'

import Default from '@/components/Default'
import ExampleIndex from '@/components/example/Index'
import ExampleView from '@/components/example/View'
import PersonaIndex from '@/components/Persona/Index'
import PersonaCreateOrUpdate from '@/components/Persona/CreateOrUpdate'
import CategoriaIndex from '@/components/Categoria/Index'
import CategoriaCreateOrUpdate from '@/components/Categoria/CreateOrUpdate'
import ProductServiceIndex from '@/components/ProductoServicio/Index'
import ProductServiceCreateOrUpdate from '@/components/ProductoServicio/CreateOrUpdate'

Vue.use(Router)

const routes = [
  { path: '/', name: 'Default', component: Default },
  { path: '/example', name: 'ExampleIndex', component: ExampleIndex },
  { path: '/example/:id', name: 'ExampleView', component: ExampleView },
  { path: '/persona/page/:page/:tipo', name: 'Persona', component: PersonaIndex },
  { path: '/persona/add/:tipo', name: 'PersonaCreate', component: PersonaCreateOrUpdate },
  { path: '/persona/:id/edit', name: 'PersonaEdit', component: PersonaCreateOrUpdate },
  { path: '/categoria/page/:page', name: 'Categoria', component: CategoriaIndex },
  { path: '/categoria/add/', name: 'CategoriaCreate', component: CategoriaCreateOrUpdate },
  { path: '/categoria/:id/edit', name: 'CategoriaEdit', component: CategoriaCreateOrUpdate },
  { path: '/productservice/page/:page/:tipo', name: 'ProductService', component: ProductServiceIndex },
  { path: '/productservice/add/:tipo', name: 'ProductServiceCreate', component: ProductServiceCreateOrUpdate },
  { path: '/productservice/:id/edit', name: 'ProductServiceEdit', component: ProductServiceCreateOrUpdate }
]

export default new Router({
  routes
})
