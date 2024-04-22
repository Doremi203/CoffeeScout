import axios from 'axios';
import * as SecureStore from "expo-secure-store";
import * as SecureStorage from "expo-secure-store";


export const API_URL = `http://192.168.1.124:8000/api`

const $api = axios.create({
    withCredentials: true,
    baseURL: API_URL
})

$api.interceptors.request.use((config) => {
    config.headers.Authorization = `Bearer ${SecureStore.getItem('accessToken')}`
    return config;
})

$api.interceptors.response.use((config) => {
    return config;
}, async (error) => {
    const originalRequest = error.config;
    if (error.response.status === 401 && error.config && !error.config._isRetry) {
        originalRequest._isRetry = true;
        try {
            const response = await axios.post(`${API_URL}/v1/account/refresh`, {refreshToken: SecureStorage.getItem('refreshToken')}, {withCredentials: true})
            let accessTokenName = "accessToken";
            let accessToken = response.data[accessTokenName];
            SecureStorage.setItem('accessToken', accessToken);

            let refreshTokenName = "refreshToken";
            let refreshToken = response.data[refreshTokenName];
            SecureStorage.setItem('refreshToken', refreshToken)

            return $api.request(originalRequest);
        } catch (e) {
            console.log('НЕ АВТОРИЗОВАН')
        }
    }
    throw error;
})


export default $api;