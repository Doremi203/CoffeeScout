import {makeAutoObservable} from "mobx";
import AuthService from "../services/AuthService";
import * as SecureStorage from "expo-secure-store";
import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import $api from "../http";


export default class UserStore {

    constructor() {
        makeAutoObservable(this);
    }






    async login(email, password) {
        console.log(email, password)
        try {

            console.log('TRY')
            await $api.post('/v1/accounts/login', {
                email: email,
                password: password
            }).then((response) => {
                console.log('RESPONSE')
                console.log(response)
            })
            //const response = await AuthService.login(email, password);
           // console.log(response)

            //console.log(response.status)

            /*let accessTokenName = "accessToken";
            let accessToken = response.data[accessTokenName];

            console.log(accessToken)

            SecureStorage.setItem('accessToken', accessToken);*/
            //navigation.navigate('main')

        } catch (error) {
            console.log("ERRRRROOOE")
            /*switch (error.response.status) {
                case 401:
                    //Alert.alert('Ошибка', 'Неверный логин или пароль');
                    break;
                default:
                    //Alert.alert('Ошибка', 'Что-то пошло не так');
            }*/
        }
    }



}