class PersonaService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}`
    }

    getAll(page, tipo, search) {
        let self = this;
        return self.axios.get(`${self.baseUrl}getall/${tipo}/${page}/${search}`);
    }

    get(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}persona/${id}`);
    }

    add(model) {
        return this.axios.post(`${this.baseUrl}persona/`, model);
    }

    update(model) {
        return this.axios.put(`${this.baseUrl}persona/${model.PersonaId}/`, model);
    }

    delete(id) {
        let self = this;
        return self.axios.delete(`${self.baseUrl}persona/${id}`);
    }

    validateDNI(cedula){
        return this.axios.get(`${this.baseUrl}persona?cedula=${cedula}`);
    }
}

export default PersonaService