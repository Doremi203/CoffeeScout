import React, {createContext} from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import UserStore from "./store/UserStore";
import CafeStore from "./store/CafeStore";


export const user = new UserStore();

export const cafe = new CafeStore();

export const Context = createContext({
    user, cafe
})

ReactDOM.render(
    <Context.Provider value={{
        user, cafe
    }}>
        <App />
    </Context.Provider>,
    document.getElementById('root')
);