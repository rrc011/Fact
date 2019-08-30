class InvoicingDetailService {
    axios
    baseUrl

    constructor(axios, apiUrl) {
        this.axios = axios
        this.baseUrl = `${apiUrl}`
    }

    getAll(orderId) {
        let self = this;
        return self.axios.get(`${self.baseUrl}GetallOrderDetail/${orderId}`);
    }

    add(model) {
        return this.axios.post(`${this.baseUrl}OrderDetail/`, model);
    }
}

export default InvoicingDetailService