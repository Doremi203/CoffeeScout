import $api from "../http";


export default class UserService {
    static async login(email, password) {
        return $api.post('/v1/accounts/login', {
            email: email,
            password: password
        })
    }
}