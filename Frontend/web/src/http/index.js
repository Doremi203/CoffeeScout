import axios from 'axios';
import * as SecureStorage from "expo-secure-store";
import * as SecureStore from "expo-secure-store";


export const API_URL = `http://backend:8080/api`

const $api = axios.create({
    withCredentials: true,
    baseURL: API_URL
})

/*$api.interceptors.request.use((config) => {
    config.headers.Authorization = `Bearer ${SecureStore.getItem('accessToken')}`
    return config;
})

$api.interceptors.response.use((config) => {
    return config;
}, async (error) => {
    const originalRequest = error.config;
    if (error.response.status === 401 && error.config && !error.config._isRetry) {
        console.log('WTF')
        originalRequest._isRetry = true;
        try {
            const response = await axios.get(`${API_URL}/v1/account/refresh`, {withCredentials: true})
            let accessTokenName = "accessToken";
            let accessToken = response.data[accessTokenName];
            SecureStorage.setItem('accessToken', accessToken);
            return $api.request(originalRequest);
        } catch (e) {
            console.log('НЕ АВТОРИЗОВАН')
        }
    }
    throw error;
})*/


export default $api;