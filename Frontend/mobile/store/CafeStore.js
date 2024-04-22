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
            console.log(response.data)
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

    async getTime(workingHours) {
        const date = new Date();
        const dayOfWeek = date.getDay();

        function addLeadingZero(value) {
            return value < 10 ? "0" + value : value;
        }

        return addLeadingZero(workingHours[dayOfWeek].closingTime.hour) + ":" +
            addLeadingZero(workingHours[dayOfWeek].closingTime.minute)
    }
}