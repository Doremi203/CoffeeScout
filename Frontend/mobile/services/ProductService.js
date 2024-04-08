import $api from "../http";


export default class ProductService {

    static async getFavoredBeverageTypes() {
        return $api.get('/v1/customers/favored-beverage-types')
    }

    static async likeProduct(menuItemId) {
        return $api.post(`/v1/customers/favored-menu-items?menuItemId=${menuItemId}`)
    }


    static async getNearbyProducts(longitude, latitude, radiusInMeters, beverageTypeId) {
        return $api.get(`/v1/menu-items?latitude=${latitude}&longitude=${longitude}&radiusInMeters=${radiusInMeters}&beverageTypeId=${beverageTypeId}`)
    }

    static async getFavMenuItems() {
        return $api.get(`/v1/customers/favored-menu-items`)
    }

    static async dislikeMenuItem(menuItemId) {
        return $api.delete(`/v1/customers/favored-menu-items/${menuItemId}`)
    }

}