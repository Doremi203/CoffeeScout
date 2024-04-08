import {makeAutoObservable} from "mobx";
import OrderService from "../services/OrderService";

export default class OrderStore {

    constructor() {
        makeAutoObservable(this);
    }

    async makeNewOrder(menuItems) {
        try {
            await OrderService.makeNewOrder(menuItems);
        } catch (error) {
            console.error('Что-то пошло не так', error);
        }
    }

}