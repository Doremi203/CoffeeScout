import {makeAutoObservable} from "mobx";
import ProductService from "../services/ProductService";
import {Alert} from "react-native";

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
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async likeProduct(menuItemId) {
        try {
            await ProductService.likeProduct(menuItemId)
        } catch (error) {
            console.error('Error fetching data: LIKE', error);
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }


    async getNearbyProducts(longitude, latitude, radiusInMeters, beverageTypeId) {
        try {
            const response = await ProductService.getNearbyProducts(longitude, latitude, radiusInMeters, beverageTypeId);
            return response.data;
        } catch (error) {
            console.error('Error fetching data: NEAR', error);
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async getFavMenuItems() {
        try {
            const response = await ProductService.getFavMenuItems();
            return response.data;
        } catch (error) {
            console.error('Error fetching data: NEAR', error);
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async dislikeMenuItem(menuItemId) {
        try {
            await ProductService.dislikeMenuItem(menuItemId)
        } catch (error) {
            console.error('Error fetching data: disLIKE', error);
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async isProductLiked(menuItemId) {
        const favItems = await this.getFavMenuItems();
        return !!favItems.find(item => item.id === menuItemId);
    }

    async getTypes() {
        try {
            const response = await ProductService.getTypes();
            return response.data;
        } catch (error) {
            console.log('Error fetching data: TYPES', error);
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }


    async leaveReview(menuItemId, content, rating) {
        try {
            /*if (rating < 0 || rating > 5 || rating === undefined) {
                throw new Error();
            }*/
            await ProductService.leaveReview(menuItemId, content, rating);
        } catch (error) {
            Alert.alert('Оценка должна быть от 1 до 5')
        }
    }

}