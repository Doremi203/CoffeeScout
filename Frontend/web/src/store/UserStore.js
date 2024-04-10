import {makeAutoObservable} from "mobx";
import UserService from "../services/UserService";
import {toast} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export default class UserStore {

    constructor() {
        makeAutoObservable(this);
    }

    async login(email, password, navigate) {
        try {
            const response = await UserService.login(email, password);

            let accessTokenName = "accessToken";
            let accessToken = response.data[accessTokenName];

            localStorage.setItem('accessToken', accessToken)

            navigate('/main')

        } catch (error) {
            switch (error.response.status) {
                case 401:
                    toast.error('Неверный логин или пароль');
                    break;
                default:
                    toast.error('Что-то пошло не так');
            }
        }
    }

}