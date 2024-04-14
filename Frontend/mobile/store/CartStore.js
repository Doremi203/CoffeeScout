import {makeAutoObservable} from "mobx";
import {Alert} from "react-native";


export default class CartStore {

    constructor() {
        makeAutoObservable(this);
    }

    cart = [];

    cafeId

    addProductToCart(menuItemId, name, price, cafeId) {
        if (this.cart.length === 0) {
            this.cafeId = cafeId
        } else {
            if (this.cafeId !== cafeId) {
                Alert.alert('Нельзя добавлять в один заказ напитки из разных кофеен')
                return
            }
        }
        let product = {
            id: menuItemId,
            name: name,
            price: price,
            quantity: 1,
        };
        this.cart.push(product);
    }

    deleteProductFromCart(menuItemId) {
        this.cart = this.cart.filter(item => item.id !== menuItemId);
    }


    decreaseProductQuantity(menuItemId) {
        let product = this.cart.find(item => item.id === menuItemId);
        if (product) {
            const productIndex = this.cart.indexOf(product);
            if (product.quantity > 1) {
                this.cart[productIndex].quantity -= 1;
            } else {
                this.deleteProductFromCart(menuItemId);
            }
        }
    }

    increaseProductQuantity(menuItemId) {
        let product = this.cart.find(item => item.id === menuItemId);
        if (product) {
            const productIndex = this.cart.indexOf(product);
            this.cart[productIndex].quantity += 1;
        }
    }

    clearCart() {
        this.cart = [];
    }

    getMenuItems() {

        return this.cart.map(item => ({
            id: item.id,
            quantity: item.quantity
        }));
    }


};