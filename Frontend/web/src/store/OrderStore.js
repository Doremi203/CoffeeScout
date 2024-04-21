import {makeAutoObservable} from "mobx";
import 'react-toastify/dist/ReactToastify.css';
import OrderService from "../services/OrderService";
import {toast} from "react-toastify";


export default class OrderStore {

    constructor() {
        makeAutoObservable(this);
    }

    async getOrdersPending() {
        try {
            const response = await OrderService.getOrdersPending();
            return response.data
        } catch (error) {
            toast.error("Что-то пошло не так")
        }
    }

    async getOrdersInProgress() {
        try {
            const response = await OrderService.getOrdersInProgress();
            return response.data
        } catch (error) {
            toast.error("Что-то пошло не так")
        }
    }


    async getOrdersCancelled() {
        try {
            const response = await OrderService.getOrdersCancelled();
            return response.data
        } catch (error) {
            toast.error("Что-то пошло не так")
        }
    }


    async getOrdersCompleted() {
        try {
            const response = await OrderService.getOrdersCompleted();
            return response.data
        } catch (error) {
            toast.error("Что-то пошло не так")
        }
    }


    async completeOrder(id) {
        try {
            await OrderService.completeOrder(id);
        } catch (error) {
            toast.error("Что-то пошло не так")
        }
    }

    async cancelOrder(id) {
        try {
            await OrderService.cancelOrder(id);
        } catch (error) {
            toast.error("Что-то пошло не так")
        }
    }

    totalPrice(items) {
        let totalPrice = 0
        items.map((item) => totalPrice += item.pricePerItem * item.quantity)
        return totalPrice;
    }

}