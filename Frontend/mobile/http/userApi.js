/*import axios from "axios";
import {Alert} from "react-native";

import * as SecureStore from 'expo-secure-store';

export const saveTokens = async (accessToken, refreshToken) => {
    try {
        await SecureStore.setItemAsync('accessToken', accessToken);
        await SecureStore.setItemAsync('refreshToken', refreshToken);
        console.log('Токены успешно сохранены.');
    } catch (error) {
        console.error('Ошибка при сохранении токенов:', error);
    }
};

export const loadTokens = async () => {
    try {
        const accessToken = await SecureStore.getItemAsync('accessToken');
        const refreshToken = await SecureStore.getItemAsync('refreshToken');
        if (accessToken && refreshToken) {
            // Токены успешно загружены
            return {accessToken, refreshToken};
        } else {
            // Токены не найдены
            return null;
        }
    } catch (error) {
        console.error('Ошибка при загрузке токенов:', error);
        return null;
    }
};


export const register = (name, email, password, navigation) => {
    axios.post("http://192.168.1.124:8000/api/v1/account/customRegister", {
        firstName: name,
        email: email,
        password: password
    })
        .then(response => {
            axios.post("http://192.168.1.124:8000/api/v1/account/login", {
                email: email,
                password: password
            }).then(response => {
                let accessTokenName = "accessToken";
                let refreshTokenName = "refreshToken";

                let accessToken = response.data[accessTokenName];
                let refreshToken = response.data[refreshTokenName];

                saveTokens(accessToken, refreshToken).then(() => navigation.navigate('main'));
            })
                .catch(error => {
                    Alert.alert('Ошибка второго запроса', error.message);
                });
        })
        .catch(error => {
            let errors = "";

            Object.keys(error.response.data.errors).forEach(key => {
                error.response.data.errors[key].forEach(err => {
                    errors += err + "\n";
                });
            });

            Alert.alert('Ошибка регистрации', errors);

        });
};

export const auth = (email, password, navigation) => {
    axios.post("http://192.168.1.124:8000/api/v1/account/login", {
        //name: name,
        email: email,
        password: password
    })
        .then(response => {
            console.log(`RESPONSE ${response.status}`);
            let accessTokenName = "accessToken";
            let refreshTokenName = "refreshToken";

            let accessToken = response.data[accessTokenName];
            let refreshToken = response.data[refreshTokenName];

            saveTokens(accessToken, refreshToken).then(() => navigation.navigate('main'));
        })
        .catch(error => {
            switch (error.response.status) {
                case 401:
                    Alert.alert('Ошибка', 'Неверный логин или пароль');
                    break;
                default:
                    Alert.alert('Ошибка', 'Что-то пошло не так');
            }
        });
};*/