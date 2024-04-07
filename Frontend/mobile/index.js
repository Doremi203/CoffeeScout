import {createContext} from 'react';
import UserStore from "./store/UserStore";
import ProductStore from "./store/ProductStore";
import CartStore from "./store/CartStore";
import CafeStore from "./store/CafeStore";

export const user = new UserStore();
export const product = new ProductStore();

export const cart = new CartStore();

export const cafe = new CafeStore();

export const Context = createContext({
    user, product, cart, cafe
});
/*const Main = () => (
    <Context.Provider value={{ user }}>
        <App />
    </Context.Provider>
);*/

//AppRegistry.registerComponent('MyApp', () => Main);