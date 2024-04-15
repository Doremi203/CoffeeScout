import $api from "../http";


export default class CafeService {

    static async getMenu() {
        return $api.get('v1/cafes/menuItems')
    }

    static async addProduct(name, price, size, typeId) {
        return $api.post('v1/menu-items', {
            name: name,
            price: price,
            sizeInMl: size,
            beverageTypeId: typeId
        })
    }

    static async deleteProduct(menuItemId) {
        return $api.delete(`v1/menu-items/${menuItemId}`)
    }

    static async getTypes() {
        return $api.get(`v1/beverage-types?PageSize=100&PageNumber=1`)
    }

    static async getInfo() {
        return $api.get(`v1/cafes/info`)
    }

    static async changeInfo(name, address, latitude, longitude, workingHours) {
        return $api.patch(`v1/cafes`, {
            name: name,
            address: address,
            latitude : latitude,
            longitude : longitude,
            workingHours : workingHours
        })
    }

}