import $api from "../http";


export default class CafeService {


    static async getNearbyCafes(longitude, latitude, radius) {
        return $api.get(`/v1/cafes?latitude=${latitude}&longitude=${longitude}&radiusInMeters=${radius}`)
    }


}