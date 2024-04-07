import $api from "../http";


export default class ProductService {


    static async getBeverageTypes() {
        return $api.get('/v1/customers/favored-beverage-types')
    }

    static async likeProduct(menuItemId) {
        return $api.post(`/v1/customers/favored-menu-items?menuItemId=${menuItemId}`)
    }


    static async getNearbyProducts(longitude, latitude, radiusInMeters, beverageTypeId) {
        return $api.get(`/v1/menu-items?latitude=${latitude}&longitude=${longitude}&radiusInMeters=${radiusInMeters}&beverageTypeId=${beverageTypeId}`)
    }


}