import React from 'react';
import {ScrollView, StyleSheet, Text, TouchableWithoutFeedback, View} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import Footer from "./Footer";
import CoffeeShopCard from "../components/CoffeeShopCard";

export default function ProductScreen({navigation}) {
    return (
        <View style={styles.container}>
            <View style={styles.main}>
                <Text style={styles.header}> капучино </Text>
                <Text style={styles.description}>
                    Капучино - это идеальное сочетание эспрессо и молока, создающее насыщенный вкус и неповторимую
                    текстуру. Его аромат и бархатистая молочная пена станут настоящим утешением в любой момент
                    дня.</Text>
                <Text style={styles.toOrder}>где заказать:</Text>
                <View style={styles.shops}>
                    <ScrollView style={styles.scroll}>
                        <CoffeeShopCard />
                        <CoffeeShopCard />
                        <CoffeeShopCard />
                        <CoffeeShopCard />
                    </ScrollView>
                </View>

                <View style={styles.button}>
                    <TouchableWithoutFeedback onPress={() => navigation.navigate('order')} >
                        <Text style={styles.buttonText}> добавить в корзину </Text>
                    </TouchableWithoutFeedback>
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
        marginTop:'4%',
        fontSize: RFPercentage(1.7),
    },
    toOrder: {
        fontFamily:'MontserratAlternatesSemiBold',
        fontSize: RFPercentage(4),
        marginLeft: '5%',
        marginTop: '5%',
    },
    shops: {
        alignItems: 'center',
    },
    scroll : {
        height: '55%',
    },
    button : {
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

