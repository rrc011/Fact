import Axios from 'axios'
import exampleService from '../services/ExampleService'
import PersonaService from '../services/PersonaService'
import WarehouseService from '../services/WarehouseService'
import ProductService from '../services/ProductService'
import ClientService from '../services/ClientService'

let apiUrl = 'http://localhost:58416/api/'

// Axios Configuration
Axios.defaults.headers.common.Accept = 'application/json'

export default {
    exampleService: new exampleService(Axios),
    personaService: new PersonaService(Axios, apiUrl),
    WarehouseService: new WarehouseService(Axios, apiUrl),
    ProductService: new ProductService(Axios, apiUrl),
    ClientService: new ClientService(Axios, apiUrl)
}