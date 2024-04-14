import $api from "../http";


export default class OrderService {

    static async getOrdersPending() {
        return $api.get('v1/cafes/orders?Status=0&PageSize=100&PageNumber=1')
    }

    static async getOrdersInProgress() {
        return $api.get('v1/cafes/orders?Status=1&PageSize=100&PageNumber=1')
    }

    static async getOrdersCompleted() {
        return $api.get('v1/cafes/orders?Status=2&PageSize=100&PageNumber=1')
    }

    static async getOrdersCancelled() {
        return $api.get('v1/cafes/orders?Status=3&PageSize=100&PageNumber=1')
    }

    static async completeOrder(id) {
        console.log('service')
        console.log(id)
        window.location.reload();
        return $api.patch(`/v1/cafes/orders/${id}/complete`)
    }

    static async cancelOrder(id) {
        console.log('servicecancel')
        console.log(id)
        window.location.reload();
        return $api.patch(`/v1/cafes/orders/${id}/cancel`)
    }

}