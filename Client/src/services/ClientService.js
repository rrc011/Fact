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
        return this.axios.post(`${this.baseUrl}Person/`, model);
    }

    update(model) {
        return this.axios.put(`${this.baseUrl}Person/${model.PersonId}/`, model);
    }

    delete(id) {
        let self = this;
        return self.axios.delete(`${self.baseUrl}Person/${id}`);
    }

    ValidateDNI(personId, dni){
        return this.axios.get(`${this.baseUrl}ValidateDNI/${personId}/${dni}`)
    }

    loadSelectPerson(){
        let self = this;
        return self.axios.get(`${self.baseUrl}LoadSelectPerson/`);
    }
}

export default ClientService