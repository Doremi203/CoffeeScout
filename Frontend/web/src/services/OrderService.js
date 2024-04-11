import $api from "../http";


export default class OrderService {

    static async getOrdersPending() {
        return $api.get('v1/cafes/orders?Status=0&From=2024-04-08T15%3A54%3A15%2B0000')
    }

    static async getOrdersInProgress() {
        return $api.get('v1/cafes/orders?Status=1&From=2024-04-08T15%3A54%3A15%2B0000')
    }

    static async getOrdersCompleted() {
        return $api.get('v1/cafes/orders?Status=2&From=2024-04-08T15%3A54%3A15%2B0000')
    }

    static async getOrdersCancelled() {
        return $api.get('v1/cafes/orders?Status=3&From=2024-04-08T15%3A54%3A15%2B0000')
    }

    static async completeOrder(id) {
        console.log('service')
        console.log(id)
        return $api.patch(`v1/orders/${id}/complete`)
    }

    static async cancelOrder(id) {
        console.log('servicecancel')
        console.log(id)
        return $api.patch(`v1/orders/${id}/cancel`)
    }

}