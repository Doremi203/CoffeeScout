import $api from "../http";


export default class ProductService {

    static async getFavoredBeverageTypes() {
        return $api.get('/v1/customers/favored-beverage-types')
    }

    static async likeProduct(menuItemId) {
        return $api.post(`/v1/customers/favored-menu-items?menuItemId=${menuItemId}`)
    }


    static async getNearbyProducts(longitude, latitude, radiusInMeters, beverageTypeId) {
        return $api.get(`/v1/menu-items?Location.Latitude=${latitude}&Location.Longitude=${longitude}&RadiusInMeters=${radiusInMeters}&BeverageTypeId=${beverageTypeId}`)
    }

    static async getFavMenuItems() {
        return $api.get(`/v1/customers/favored-menu-items`)
    }

    static async dislikeMenuItem(menuItemId) {
        return $api.delete(`/v1/customers/favored-menu-items/${menuItemId}`)
    }

    static async getTypes() {
        return $api.get(`/v1/beverage-types?Pagination.PageSize=100&Pagination.PageNumber=1`)
    }


    static async leaveReview(menuItemId, content, rating) {
        return $api.post(`/v1/menu-items/${menuItemId}/reviews`, {
            content: content,
            rating: rating
        })
    }

    static async search(name, limit) {
        return $api.get(`/v1/menu-items/search?name=${name}&limit=${limit}`)
    }

    static async getReviews(menuItemId) {
        return $api.get(`/v1/menu-items/${menuItemId}/reviews`)
    }

}