import 'react-native-gesture-handler';
import React, {useContext} from 'react';
import {NavigationContainer} from '@react-navigation/native';
import {createStackNavigator} from '@react-navigation/stack';
import Registration from "./screens/Registration";
import Login from "./screens/Login";
import Profile from "./screens/Profile";
import Main from "./screens/Main";
import {useFonts} from "expo-font";
import Cart from "./screens/Cart";
import MapScreen from "./screens/MapScreen";
import ProductScreen from "./screens/ProductScreen";
import Settings from "./screens/Settings";
import OrderScreen from "./screens/OrderScreen";
import SearchScreen from "./screens/SearchScreen";
import {Context} from "./index";
import CafeScreen from "./screens/CafeScreen";
import Payment from "./screens/Payment";

const Stack = createStackNavigator();


export default function App() {

    const [fontsLoaded] = useFonts({
        MontserratAlternates: require('./assets/fonts/MontserratAlternates-Regular.ttf'),
        MontserratAlternatesSemiBold: require('./assets/fonts/MontserratAlternates-SemiBold.ttf'),
        MontserratAlternatesMedium: require('./assets/fonts/MontserratAlternates-Medium.ttf'),
    });


    const {loc} = useContext(Context);
    loc.setLocation().then();
    console.log(loc.location.coords.latitude)


    return (
        <NavigationContainer>
            <Stack.Navigator>
                <Stack.Screen
                    name="registration"
                    component={Registration}
                    options={{headerShown: false}}
                />
                <Stack.Screen
                    name="auth"
                    component={Login}
                    options={{headerShown: false}}
                />
                <Stack.Screen
                    name="profile"
                    component={Profile}
                    options={{headerShown: false}}
                />
                <Stack.Screen
                    name="main"
                    component={Main}
                    options={{headerShown: false}}
                />
                <Stack.Screen
                    name="cart"
                    component={Cart}
                    options={{headerShown: false}}
                />
                <Stack.Screen
                    name="map"
                    component={MapScreen}
                    options={{headerShown: false}}
                />
                <Stack.Screen
                    name="product"
                    component={ProductScreen}
                    options={{headerShown: false}}
                />
                <Stack.Screen
                    name="settings"
                    component={Settings}
                    options={{headerShown: false}}
                />
                <Stack.Screen
                    name="orderScreen"
                    component={OrderScreen}
                    options={{headerShown: false}}
                />
                <Stack.Screen
                    name="searchScreen"
                    component={SearchScreen}
                    options={{headerShown: false}}
                />
                <Stack.Screen
                    name="cafeScreen"
                    component={CafeScreen}
                    options={{headerShown: false}}
                />
                <Stack.Screen
                    name="payment"
                    component={Payment}
                    options={{headerShown: false}}
                />
            </Stack.Navigator>
        </NavigationContainer>

    );
}




