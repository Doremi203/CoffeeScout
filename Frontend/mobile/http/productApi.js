/*import axios from 'axios';
import {loadTokens} from "./userApi";



export const getBeverageTypes = async () => {

    let tokens = await loadTokens();
    let accessToken = tokens['accessToken'];
    let bearer = 'Bearer ' + accessToken;

    //console.log(tokens)
   // console.log(accessToken)
    //console.log(bearer)

    axios.get('http://192.168.1.124:8000/api/v1/Customer/favored-beverage-types', {
        headers: {
            Authorization: bearer
        }
    })
        .then(response => {
            console.log(response.data);
        })
        .catch(error => {
            console.error('Error fetching data: ', error);
        });
}


export const likeProduct = async (menuItemId) => {

    let tokens = await loadTokens();
    let accessToken = tokens['accessToken'];
    let bearer = 'Bearer ' + accessToken;

    console.log(bearer)
    console.log(menuItemId)

    axios.post(`http://192.168.1.124:8000/api/v1/Customer/favored-menu-items?menuItemId=${menuItemId}`, {
        headers: {
            Authorization: bearer
        }
    })
        .then(response => {
            console.log('sDDDDDDDDDDDDDDDD')
        })
        .catch(error => {
           console.log(error.message)
        });
}*/