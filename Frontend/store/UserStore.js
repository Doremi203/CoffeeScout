import {makeAutoObservable} from "mobx";
import AuthService from "../services/AuthService";
import * as SecureStorage from "expo-secure-store";
import {Alert} from "react-native";

export default class UserStore {

    constructor() {
        makeAutoObservable(this);
    }

    async registration(name, email, password, navigation) {
        try {
            await AuthService.registration(name, email, password).then(async response => {
                await AuthService.login(email, password).then(response2 => {
                    let accessTokenName = "accessToken";
                    let accessToken = response2.data[accessTokenName];

                    SecureStorage.setItem('accessToken', accessToken);

                    console.log(SecureStorage.getItem('accessToken'));

                    navigation.navigate('main');
                })
            })


        } catch (error) {
            let errors = "";

            Object.keys(error.response.data.errors).forEach(key => {
                error.response.data.errors[key].forEach(err => {
                    errors += err + "\n";
                });
            });

            Alert.alert('Ошибка регистрации', errors);
        }
    }


    async login(email, password, navigation) {
        try {

            const response = await AuthService.login(email, password);

            console.log(response.status)

            let accessTokenName = "accessToken";
            let accessToken = response.data[accessTokenName];

            console.log(accessToken)

            SecureStorage.setItem('accessToken', accessToken);
            navigation.navigate('main')

        } catch (error) {
            switch (error.response.status) {
                case 401:
                    Alert.alert('Ошибка', 'Неверный логин или пароль');
                    break;
                default:
                    Alert.alert('Ошибка', 'Что-то пошло не так');
            }
        }
    }


}