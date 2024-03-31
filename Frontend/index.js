import {createContext} from 'react';
import UserStore from "./store/UserStore";

export const user = new UserStore();

export const Context = createContext({
    user,
});
/*const Main = () => (
    <Context.Provider value={{ user }}>
        <App />
    </Context.Provider>
);*/

//AppRegistry.registerComponent('MyApp', () => Main);
