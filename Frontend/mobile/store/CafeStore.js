import {makeAutoObservable} from "mobx";
import CafeService from "../services/CafeService";
import {Alert} from "react-native";

export default class CafeStore {

    constructor() {
        makeAutoObservable(this);
    }

    async getNearbyCafes(longitude, latitude, radius) {
        try {
            const response = await CafeService.getNearbyCafes(longitude, latitude, radius);
            return response.data;
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async getMenu(cafeId) {
        try {
            const response = await CafeService.getMenu(cafeId);
            return response.data;
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async getInfo(cafeId) {
        try {
            const response = await CafeService.getInfo(cafeId);
            return response.data;
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }
}