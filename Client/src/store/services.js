import Axios from 'axios'
import exampleService from '../services/ExampleService'
import PersonaService from '../services/PersonaService'
import CategoriaService from '../services/CategoriaService'
import ProductServiceService from '../services/ProductServiceService'

let apiUrl = 'http://localhost:58416/api/'

// Axios Configuration
Axios.defaults.headers.common.Accept = 'application/json'

export default {
    exampleService: new exampleService(Axios),
    personaService: new PersonaService(Axios, apiUrl),
    categoriaService: new CategoriaService(Axios, apiUrl),
    psService: new ProductServiceService(Axios, apiUrl)
}