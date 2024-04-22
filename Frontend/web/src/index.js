import React, {createContext} from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import UserStore from "./store/UserStore";
import CafeStore from "./store/CafeStore";
import OrderStore from "./store/OrderStore";


export const user = new UserStore();

export const cafe = new CafeStore();

export const order = new OrderStore();

export const Context = createContext({
    user, cafe, order
})

ReactDOM.render(
    <Context.Provider value={{
        user, cafe, order
    }}>
        <App />
    </Context.Provider>,
    document.getElementById('root')
);