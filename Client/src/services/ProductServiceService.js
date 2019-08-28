class ProductServiceService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}`
    }

    getAll(page, tipo, search) {
        let self = this;
        return self.axios.get(`${self.baseUrl}getallpd/${tipo}/${page}/${search}`);
    }

    get(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}getpd/${id}`);
    }

    add(model) {
        return this.axios.post(`${this.baseUrl}ProductoServicio/`, model);
    }

    update(model) {
        return this.axios.put(`${this.baseUrl}ProductoServicio/${model.PersonaId}/`, model);
    }

    delete(id) {
        let self = this;
        return self.axios.delete(`${self.baseUrl}ProductoServicio/${id}`);
    }

    validateName(id, nombre){
        return this.axios.get(`${this.baseUrl}ProductoServicio?id=${cedula}&nombre=${nombre}`);
    }
}

export default ProductServiceService