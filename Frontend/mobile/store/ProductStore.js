import {makeAutoObservable} from "mobx";
import AuthService from "../services/AuthService";
import * as SecureStorage from "expo-secure-store";
import {Alert} from "react-native";
import ProductService from "../services/ProductService";

export default class ProductStore {

    constructor() {
        makeAutoObservable(this);
    }

    async getBeverageTypes() {

        try {
            const response = await ProductService.getBeverageTypes();
            return response.data;

        } catch (error) {
            console.log('NETAHJBHJBDL')
            console.log('Error fetching data: TYPES', error);
            console.log(error.message)
            console.log(error.response.status)
        }
    }

    async likeProduct(menuItemId) {
        try {
            await ProductService.likeProduct(menuItemId).then(response => {
                console.log('sDDDDDDDDDDDDDDDD')
                this.getBeverageTypes();
            })
        } catch (error) {
            console.log(menuItemId)
            console.log(error.message)
            //console.log(error.response)
        }
    }


    async getNearbyProducts(longitude, latitude, radiusInMeters, beverageTypeId) {
        try {
            const response = await ProductService.getNearbyProducts(longitude, latitude, radiusInMeters, beverageTypeId);
            return response.data;
        } catch (error) {
            console.error('Error fetching data: NEAR', error);
        }
    }


}