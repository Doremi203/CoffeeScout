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

}