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

    static async getTypes() {
        return $api.get(`/v1/beverage-types?PageSize=100&PageNumber=1`)
    }


    static async leaveReview(menuItemId, content, rating) {
        return $api.post(`/v1/menu-items/${menuItemId}/reviews`, {
            content : content,
            rating : rating
        })
    }

}