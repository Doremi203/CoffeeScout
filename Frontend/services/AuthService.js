import $api from "../http";


export default class AuthService {
    static async login(email, password) {
        return $api.post('/v1/account/login', {
            email: email,
            password: password
        })
    }

    static async registration(name, email, password) {
        return $api.post('/v1/account/customRegister',
            {
                firstName: name,
                email: email,
                password: password
            })
    }
}