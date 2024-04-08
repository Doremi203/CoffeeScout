import $api from "../http";


export default class UserService {
    static async login(email, password) {
        return $api.post('/v1/accounts/login', {
            email: email,
            password: password
        })
    }

    static async registration(name, email, password) {
        return $api.post('/v1/accounts/customer/register',
            {
                firstName: name,
                email: email,
                password: password
            })
    }

    static async getName() {
        return $api.get('v1/customers/info')
    }

    static async getEmail() {
        return $api.get('v1/accounts/manage/info')
    }

    static async changeName(name) {
        return $api.patch('v1/customers/info', {
            firstName: name
        })
    }

    static async changeEmail(email) {
        return $api.post('v1/accounts/manage/info', {
            newEmail: email
        })
    }

}