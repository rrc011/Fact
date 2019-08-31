class InvoicingService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}`
    }

    getAll(page) {
        let self = this;
        return self.axios.get(`${self.baseUrl}GetallOrder/${page}`);
    }

    get(id) {
        let self = this;
        return self.axios.get(`${self.baseUrl}GetOrder/${id}`);
    }

    add(model) {
        return this.axios.post(`${this.baseUrl}Order/`, model);
    }

    update(model) {
        return this.axios.put(`${this.baseUrl}Order/${model.OrderId}/`, model);
    }

    delete(id) {
        let self = this;
        return self.axios.delete(`${self.baseUrl}Order/${id}`);
    }

    exportPDF() {
        return `${this.baseUrl}GetPdfOrder`;
    }

    exportExcel(){
        return `${this.baseUrl}GetExcelOrder`;
    }
}

export default InvoicingService