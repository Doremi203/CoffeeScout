import $api from "../http";


export default class CafeService {

    static async getMenu() {
        return $api.get('v1/cafes/menuItems')
    }

    static async addProduct(name, price, size, typeName) {
        return $api.post('v1/menu-items', {
            name: name,
            price: price,
            sizeInMl: size,
            beverageTypeName: typeName
        })
    }

    static async deleteProduct(menuItemId) {
        return $api.delete(`v1/menu-items/${menuItemId}`)
    }

    static async getTypes() {
        return $api.get(`v1/beverage-types?PageSize=100&PageNumber=1`)
    }

}