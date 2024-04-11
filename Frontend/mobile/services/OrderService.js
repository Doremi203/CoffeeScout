import $api from "../http";


export default class OrderService {

    static async makeNewOrder(menuItems) {
        return $api.post('/v1/orders', {
            menuItems: menuItems
        })
    }

    static async getOrdersPending() {
        return $api.get('/v1/customers/orders?Status=0&From=2024-04-08T15%3A20%3A15%2B0000')
    }

    static async getOrdersInProgress() {
        return $api.get('/v1/customers/orders?Status=1&From=2024-04-08T15%3A20%3A15%2B0000')
    }

    static async getOrdersCompleted() {
        return $api.get('/v1/customers/orders?Status=2&From=2024-04-08T15%3A20%3A15%2B0000')
    }

    static async getOrdersCancelled() {
        return $api.get('/v1/customers/orders?Status=3&From=2024-04-08T15%3A20%3A15%2B0000')
    }


}