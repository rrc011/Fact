class WarehouseService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}`
    }

    getAll(page, search) {
        let self = this;
        return self.axios.get(`${self.baseUrl}GetallWarehouse/${page}?search=${search}`);
    }

    loadSelectWarehouse() {
        let self = this;
        return self.axios.get(`${self.baseUrl}GetallSelectWarehouseProduct/`);
    }

    get(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}GetWarehouse/${id}`);
    }

    add(model) {
        return this.axios.post(`${this.baseUrl}warehouse/`, model);
    }

    update(model) {
        return this.axios.put(`${this.baseUrl}warehouse/${model.warehouseId}/`, model);
    }

    delete(id) {
        let self = this;
        return self.axios.delete(`${self.baseUrl}warehouse/${id}`);
    }

    validateName(id, nombre){
        return this.axios.get(`${this.baseUrl}ValidateNameWarehouse/${id}/${nombre}`);
    }
}

export default WarehouseService