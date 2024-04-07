import React, {useContext, useEffect, useState} from 'react';
import {Alert, ScrollView, StyleSheet, Text, TouchableWithoutFeedback, View} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import Footer from "./Footer";
import CoffeeShopCard from "../components/CoffeeShopCard";
import {Context} from "../index";
import Product from "../components/Product";
import * as Location from "expo-location";

export default function ProductScreen({navigation}) {


    const {product} = useContext(Context);
    const {user} = useContext(Context);

    const [location, setLocation] = useState(null);
    useEffect(() => {
        const getLocation = async () => {
            const location = await user.getLocation()
            setLocation(location);
        };

        getLocation().then();
    }, [])


    //console.log('jjjj')
    //console.log(location.coords.latitude)
    //console.log(location.coords.longitude)

    const [nearProducts, setNearProducts] = useState([]);

    useEffect(() => {
        const fetchProducts = async () => {
            if (location) {
                console.log('ssFFFFFFFFFFFFFs')
                console.log(location.coords.latitude)
                console.log(location.coords.longitude)
                const products = await product.getNearbyProducts(location.coords.longitude, location.coords.latitude, 1000, 1);
                setNearProducts(products);
            }

        };

        fetchProducts().then();
    }, []);

    console.log(nearProducts)



    ///console.log(location.longitude)
    //console.log(location.latitude)



    return (
        <View style={styles.container}>
            <View style={styles.main}>
                <Text style={styles.header}> капучино </Text>
                <Text style={styles.description}>
                    Капучино - это идеальное сочетание эспрессо и молока, создающее насыщенный вкус и неповторимую
                    текстуру. Его аромат и бархатистая молочная пена станут настоящим утешением в любой момент
                    дня.</Text>
                <Text style={styles.toOrder}>закажите:</Text>
                <View style={styles.shops}>
                    <ScrollView style={styles.scroll}>


                        <CoffeeShopCard navigation={navigation} menuItemId={1} name={'Капучино'} price={100}/>
                        <CoffeeShopCard navigation={navigation} menuItemId={2} name={'Латте'} price={100}/>

                    </ScrollView>
                </View>

            </View>
            <Footer navigation={navigation}/>
        </View>
    );
}


const styles = StyleSheet.create({
    container: {
        flexDirection: 'column',
        minHeight: '100%',
    },
    main: {
        flex: 1
    },
    header: {
        fontSize: RFPercentage(8),
        marginTop: '15%',
        fontFamily: 'MontserratAlternates',
    },
    description: {
        fontFamily: 'MontserratAlternates',
        marginLeft: '5%',
        width: '90%',
        marginTop: '4%',
        fontSize: RFPercentage(1.7),
    },
    toOrder: {
        fontFamily: 'MontserratAlternatesSemiBold',
        fontSize: RFPercentage(4),
        marginLeft: '5%',
        marginTop: '5%',
    },
    shops: {
        alignItems: 'center',
    },
    scroll: {
        height: '62%',
    },
    button: {
        backgroundColor: '#169366',
        height: RFValue(45),
        alignItems: 'center',
        justifyContent: 'center',
        borderRadius: 10,
        elevation: 3,
        marginRight: 10,
        marginLeft: 10
    },
    buttonText: {
        fontSize: RFValue(15),
        color: 'white',
        fontFamily: 'MontserratAlternates',
    }

});

