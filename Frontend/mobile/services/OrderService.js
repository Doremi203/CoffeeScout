import $api from "../http";


export default class OrderService {

    static async makeNewOrder(menuItems) {
        return $api.post('v1/orders', {
            menuItems: menuItems
        })
    }
}