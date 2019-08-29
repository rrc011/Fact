class ProductService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}`
    }

    getAll(page, search) {
        let self = this;
        return self.axios.get(`${self.baseUrl}GetallProduct/${page}?search=${search}`);
    }

    get(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}GetProduct/${id}`);
    }

    add(model) {
        return this.axios.post(`${this.baseUrl}product/`, model);
    }

    update(model) {
        return this.axios.put(`${this.baseUrl}product/${model.productId}/`, model);
    }

    delete(id) {
        let self = this;
        return self.axios.delete(`${self.baseUrl}product/${id}`);
    }

    validateName(id, nombre){
        return this.axios.get(`${this.baseUrl}ValidateNameProduct/${id}/${nombre}`);
    }
}

export default ProductService