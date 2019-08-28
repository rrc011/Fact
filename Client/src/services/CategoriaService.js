class CategoriaService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}`
    }

    getAll(page, search) {
        let self = this;
        return self.axios.get(`${self.baseUrl}GetAll/${page}/${search}`);
    }

    get(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}getcategoria/${id}`);
    }

    add(model) {
        return this.axios.post(`${this.baseUrl}categoria/`, model);
    }

    update(model) {
        return this.axios.put(`${this.baseUrl}categoria/${model.CategoriaId}/`, model);
    }

    delete(id) {
        let self = this;
        return self.axios.delete(`${self.baseUrl}categoria/${id}`);
    }

    validateName(id, nombre){
        return this.axios.get(`${this.baseUrl}categoria?Id=${id}&name=${nombre}`);
    }
}

export default CategoriaService