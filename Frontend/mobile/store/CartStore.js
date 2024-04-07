import React, { createContext, useState } from 'react';
import {makeAutoObservable} from "mobx";


export default class CartStore  {

    constructor() {
        makeAutoObservable(this);
    }

    cart = [];

    addProductToCart(menuItemId, name, price) {
        let product = {
            id: menuItemId,
            name: name,
            price: price,
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


};