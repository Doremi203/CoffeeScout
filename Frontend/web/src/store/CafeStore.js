import {makeAutoObservable} from "mobx";
import 'react-toastify/dist/ReactToastify.css';
import CafeService from "../services/CafeService";
import {toast} from "react-toastify";


export default class CafeStore {

    constructor() {
        makeAutoObservable(this);
    }

    async getMenu() {
        try {
            const response = await CafeService.getMenu();
            return response.data;
        } catch (error) {
            toast.error('Что-то пошло не так')
        }
    }

    async addProduct(name, price, size, typeId) {
        try {
            await CafeService.addProduct(name, price, size, typeId);
            window.location.reload();
        } catch (error) {
            switch (error.response.status) {
                case 400:
                    toast.error('Некорректно введенные данные');
                    break;
                default:
                    toast.error('Что-то пошло не так');
            }
        }
    }

    async deleteProduct(menuItemId) {
        try {
            await CafeService.deleteProduct(menuItemId);
            window.location.reload();
        } catch (error) {
            toast.error("Что-то пошло не так")
        }
    }

    async getTypes() {
        try {
            const response = await CafeService.getTypes();
            return response.data;
        } catch (error) {
            toast.error("Что-то пошло не так")
        }
    }

    async getInfo() {
        try {
            const response = await CafeService.getInfo();
            return response.data;
        } catch (error) {
            toast.error("Что-то пошло не так")
        }
    }

    async changeInfo() {
        try {

        } catch (error) {

        }

    }


}