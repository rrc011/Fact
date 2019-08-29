class ClientService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}`
    }

    getAll(page, search) {
        let self = this;
        return self.axios.get(`${self.baseUrl}GetallPerson/${page}?search=${search}`);
    }

    get(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}GetPerson/${id}`);
    }

    add(model) {
        return this.axios.post(`${this.baseUrl}client/`, model);
    }

    update(model) {
        return this.axios.put(`${this.baseUrl}client/${model.clientId}/`, model);
    }

    delete(id) {
        let self = this;
        return self.axios.delete(`${self.baseUrl}client/${id}`);
    }
}

export default ClientService