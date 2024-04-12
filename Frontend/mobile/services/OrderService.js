import $api from "../http";


export default class OrderService {

    static async makeNewOrder(menuItems, cafeId) {
        return $api.post(`/v1/cafes/${cafeId}/orders`, {
            menuItems: menuItems
        })
    }

    static async payForOrder(orderId) {
        return $api.patch(`/v1/customers/orders/${orderId}/pay`)
    }

    static async getOrdersPending() {
        return $api.get('/v1/customers/orders?Status=0&PageSize=100&PageNumber=1')
    }

    static async getOrdersInProgress() {
        return $api.get('/v1/customers/orders?Status=1&PageSize=100&PageNumber=1')
    }

    static async getOrdersCompleted() {
        return $api.get('/v1/customers/orders?Status=2&PageSize=100&PageNumber=1')
    }

    static async getOrdersCancelled() {
        return $api.get('/v1/customers/orders?Status=3&PageSize=100&PageNumber=1')
    }

}