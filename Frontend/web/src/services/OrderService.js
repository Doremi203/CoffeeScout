import $api from "../http";


export default class OrderService {

    static async getOrdersPending() {
        return $api.get('v1/cafes/orders?Status=0&Pagination.PageSize=100&Pagination.PageNumber=1')
    }

    static async getOrdersInProgress() {
        return $api.get('v1/cafes/orders?Status=1&Pagination.PageSize=100&Pagination.PageNumber=1')
    }

    static async getOrdersCompleted() {
        return $api.get('v1/cafes/orders?Status=2&Pagination.PageSize=100&Pagination.PageNumber=1')
    }

    static async getOrdersCancelled() {
        return $api.get('v1/cafes/orders?Status=3&Pagination.PageSize=100&Pagination.PageNumber=1')
    }

    static async completeOrder(id) {
        window.location.reload();
        return $api.patch(`/v1/cafes/orders/${id}/complete`)
    }

    static async cancelOrder(id) {
        window.location.reload();
        return $api.patch(`/v1/cafes/orders/${id}/cancel`)
    }

}