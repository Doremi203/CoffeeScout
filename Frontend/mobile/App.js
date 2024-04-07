import 'react-native-gesture-handler';
import {StyleSheet} from 'react-native';
import React, {useCallback, useContext, useEffect, useState} from 'react';
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
import Order from "./screens/Order";
import OrderFin from "./screens/OrderFin";
import Settings from "./screens/Settings";
import OrderScreen from "./screens/OrderScreen";
import SearchScreen from "./screens/SearchScreen";
import {Context} from "./index";
import ShopScreen from "./screens/ShopScreen";
import * as SplashScreen from 'expo-splash-screen';
import Entypo from "react-native-vector-icons/Entypo";
import {Asset as Font} from "expo-asset";


const Stack = createStackNavigator();



export default function App() {

    const [fontsLoaded] = useFonts({
        MontserratAlternates: require('./assets/fonts/MontserratAlternates-Regular.ttf'),
        MontserratAlternatesSemiBold: require('./assets/fonts/MontserratAlternates-SemiBold.ttf'),
        MontserratAlternatesMedium: require('./assets/fonts/MontserratAlternates-Medium.ttf'),
    });


    const {user} = useContext(Context);





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
                    name="order"
                    component={Order}
                    options={{headerShown: false}}
                />
                <Stack.Screen
                    name="orderFin"
                    component={OrderFin}
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
                    name="shopScreen"
                    component={ShopScreen}
                    options={{headerShown: false}}
                />
            </Stack.Navigator>
        </NavigationContainer>

    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#fff',
        alignItems: 'center',
        justifyContent: 'center',
    },
});


//export default App;
