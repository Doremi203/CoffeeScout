import $api from "../http";


export default class CafeService {

    static async getNearbyCafes(longitude, latitude, radius) {
        return $api.get(`/v1/cafes?Location.Latitude=${latitude}&Location.Longitude=${longitude}&RadiusInMeters=${radius}`)
    }

    static async getMenu(cafeId) {
        return $api.get(`/v1/cafes/${cafeId}/menuItems`)
    }

    static async getInfo(cafeId) {
        return $api.get(`/v1/cafes/${cafeId}`)
    }

}