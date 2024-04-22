import axios from 'axios';

export const API_URL = `http://localhost/api`

const $api = axios.create({
    withCredentials: true,
    baseURL: API_URL
})

$api.interceptors.request.use((config) => {
    config.headers.Authorization = `Bearer ${localStorage.getItem('accessToken')}`
    return config;
})

$api.interceptors.response.use((config) => {
    return config;
}, async (error) => {
    const originalRequest = error.config;
    if (error.response.status === 401 && error.config && !error.config._isRetry) {
        originalRequest._isRetry = true;
        try {
            const response = await axios.post(`${API_URL}/v1/accounts/refresh`, {refreshToken: localStorage.getItem('refreshToken')}, {withCredentials: true})
            let accessTokenName = "accessToken";
            let accessToken = response.data[accessTokenName];
            localStorage.setItem('accessToken', accessToken);

            let refreshTokenName = "refreshToken";
            let refreshToken = response.data[refreshTokenName];
            localStorage.setItem('refreshToken', refreshToken)

            return $api.request(originalRequest);
        } catch (e) {
            console.log('НЕ АВТОРИЗОВАН')
        }
    }
    throw error;
})


export default $api;