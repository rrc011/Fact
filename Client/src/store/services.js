import Axios from 'axios'
import WarehouseService from '../services/WarehouseService'
import ProductService from '../services/ProductService'
import ClientService from '../services/ClientService'
import InvoicingService from '../services/Invoicing'
import InvoicingDetailService from '../services/InvoicingDetailService'

let apiUrl = 'http://localhost:58416/api/'

// Axios Configuration
Axios.defaults.headers.common.Accept = 'application/json'

export default {
    WarehouseService: new WarehouseService(Axios, apiUrl),
    ProductService: new ProductService(Axios, apiUrl),
    ClientService: new ClientService(Axios, apiUrl),
    InvoicingService: new InvoicingService(Axios, apiUrl),
    InvoicingDetailService: new InvoicingDetailService(Axios, apiUrl)
}