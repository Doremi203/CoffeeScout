import {makeAutoObservable} from "mobx";
import UserService from "../services/UserService";
import * as SecureStorage from "expo-secure-store";
import {Alert} from "react-native";

export default class UserStore {

    constructor() {
        makeAutoObservable(this);
    }


    async registration(name, email, password, navigation) {
        try {
            await UserService.registration(name, email, password).then(async () => {
                await UserService.login(email, password).then(response2 => {
                    let accessTokenName = "accessToken";
                    let accessToken = response2.data[accessTokenName];

                    SecureStorage.setItem('accessToken', accessToken);
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
            const response = await UserService.login(email, password);

            let accessTokenName = "accessToken";
            let accessToken = response.data[accessTokenName];
            SecureStorage.setItem('accessToken', accessToken);

            let refreshTokenName = "refreshToken";
            let refreshToken = response.data[refreshTokenName];
            SecureStorage.setItem('refreshToken', refreshToken)

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

    async logOut() {
        try {
            await SecureStorage.deleteItemAsync('accessToken');
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }


    async getName() {
        try {
            const response = await UserService.getName();
            return response.data.firstName;
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }


    async getEmail() {
        try {
            const response = await UserService.getEmail();
            return response.data.email;
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async changeName(name) {
        try {
            await UserService.changeName(name);
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }

    async changeEmail(email) {
        try {
            await UserService.changeEmail(email);
        } catch (error) {
            Alert.alert('Ошибка', 'Что-то пошло не так')
        }
    }
}