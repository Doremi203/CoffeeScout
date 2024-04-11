import {makeAutoObservable} from "mobx";
import OrderService from "../services/OrderService";
import {Alert} from "react-native";

export default class OrderStore {

    constructor() {
        makeAutoObservable(this);
    }

    async makeNewOrder(menuItems) {
        try {
            const response = await OrderService.makeNewOrder(menuItems);
            console.log('new')
            console.log(response.status)
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async getOrdersPending() {
        try {
            const response = await OrderService.getOrdersPending();
            return response.data
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async getOrdersInProgress() {
        try {
            const response = await OrderService.getOrdersInProgress();
            return response.data
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async getOrdersCancelled() {
        try {
            const response = await OrderService.getOrdersCancelled();
            return response.data
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }


    async getOrdersCompleted() {
        try {
            const response = await OrderService.getOrdersCompleted();
            return response.data
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }


}