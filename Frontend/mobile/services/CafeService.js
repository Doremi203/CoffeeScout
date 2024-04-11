import $api from "../http";


export default class CafeService {

    static async getNearbyCafes(longitude, latitude, radius) {
        return $api.get(`/v1/cafes?Latitude=${latitude}&Longitude=${longitude}&RadiusInMeters=${radius}`)
    }

}