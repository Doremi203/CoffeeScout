import {makeAutoObservable} from "mobx";
import AuthService from "../services/AuthService";
import * as SecureStorage from "expo-secure-store";
import {Alert} from "react-native";
import ProductService from "../services/ProductService";
import CafeService from "../services/CafeService";

export default class CafeStore {

    constructor() {
        makeAutoObservable(this);
    }

    async getNearbyCafes(longitude, latitude, radius) {
        try {
            console.log('JJJJJJJJj')
            const response = await CafeService.getNearbyCafes(longitude, latitude, radius);
            console.log('JJJJJJJJj')
            console.log(response.status)
            console.log(response.data)
            return response.body;
        } catch (error) {
            console.error('Error fetching data: NEAR', error);
        }
    }


}