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
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async likeProduct(menuItemId) {
        try {
            await ProductService.likeProduct(menuItemId)
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }


    async getNearbyProducts(longitude, latitude, radiusInMeters, beverageTypeId) {
        try {
            const response = await ProductService.getNearbyProducts(longitude, latitude, radiusInMeters, beverageTypeId);
            return response.data;
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async getFavMenuItems() {
        try {
            const response = await ProductService.getFavMenuItems();
            return response.data;
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async dislikeMenuItem(menuItemId) {
        try {
            await ProductService.dislikeMenuItem(menuItemId)
        } catch (error) {
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
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }


    async leaveReview(menuItemId, content, rating) {
        try {
            await ProductService.leaveReview(menuItemId, content, rating);
        } catch (error) {
            Alert.alert('Оценка должна быть от 1 до 5')
        }
    }

    async search(name, limit) {
        try {
            const response = await ProductService.search(name, limit)
            return response.data
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async getReviews(menuItemId) {
        try {
            const response = await ProductService.getReviews(menuItemId)
            return response.data
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

}