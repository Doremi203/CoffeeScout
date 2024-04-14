import {makeAutoObservable} from "mobx";
import CafeService from "../services/CafeService";

export default class CafeStore {

    constructor() {
        makeAutoObservable(this);
    }

    async getNearbyCafes(longitude, latitude, radius) {
        try {
            const response = await CafeService.getNearbyCafes(longitude, latitude, radius);
            return response.data;
        } catch (error) {
            console.error('Error fetching data: NEAR cafe', error);
        }
    }

    async getMenu(cafeId) {
        try {
            const response = await CafeService.getMenu(cafeId);

            console.log(response.status)
            return response.data;
        } catch (error) {
            console.error('Error fetching data', error);
        }
    }

    async getInfo(cafeId) {
        try {
            const response = await CafeService.getInfo(cafeId);
            console.log(response.status)
            return response.data;
        } catch (error) {
            console.error('Error fetching data', error);
        }
    }

}