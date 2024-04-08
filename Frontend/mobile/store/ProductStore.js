import {makeAutoObservable} from "mobx";
import ProductService from "../services/ProductService";

export default class ProductStore {

    constructor() {
        makeAutoObservable(this);
    }

    async getFavoredBeverageTypes() {

        try {
            const response = await ProductService.getFavoredBeverageTypes();
            return response.data;
        } catch (error) {
            console.log('Error fetching data: TYPES', error);
        }
    }

    async likeProduct(menuItemId) {
        try {
            await ProductService.likeProduct(menuItemId)

        } catch (error) {
            console.error('Error fetching data: LIKE', error);
        }
    }


    async getNearbyProducts(longitude, latitude, radiusInMeters, beverageTypeId) {
        try {
            const response = await ProductService.getNearbyProducts(longitude, latitude, radiusInMeters, beverageTypeId);
            return response.data;
        } catch (error) {
            console.error('Error fetching data: NEAR', error);
        }
    }

}