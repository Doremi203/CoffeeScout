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

    async getFavMenuItems() {
        try {
            const response = await ProductService.getFavMenuItems();
            console.log(response.status)
            console.log(response.data)
            return response.data;
        } catch (error) {
            console.error('Error fetching data: NEAR', error);
        }
    }

    async dislikeMenuItem(menuItemId) {
        try {
            await ProductService.dislikeMenuItem(menuItemId)
        } catch (error) {
            console.error('Error fetching data: disLIKE', error);
        }
    }

    async isProductLiked(menuItemId) {
        const favItems = await this.getFavMenuItems();
        return !!favItems.find(item => item.id === menuItemId);
    }
}