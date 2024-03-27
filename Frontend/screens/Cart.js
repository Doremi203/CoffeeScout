import React from 'react';
import {ScrollView, StyleSheet, Text, TouchableWithoutFeedback, View} from 'react-native';
import Footer from "./Footer";
import ProductInCart from "../components/ProductInCart";
import {RFPercentage, RFValue} from 'react-native-responsive-fontsize';


export default function Cart({navigation}) {
    return (
        <View style={styles.container}>

            <View style={styles.main}>
                <Text style={styles.header}>корзина</Text>

                <View style={styles.products}>
                    <ScrollView style={styles.scroll}>
                        <ProductInCart />
                        <ProductInCart />
                        <ProductInCart />
                        <ProductInCart />
                        <ProductInCart />





                    </ScrollView>
                </View>

                <View style={styles.button}>
                    <TouchableWithoutFeedback onPress={() => navigation.navigate('main')} >
                        <Text style={styles.buttonText}> к оплате </Text>
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
        alignItems: 'center',
    },
    main : {
        flex: 1,
    },
    /*container : {
        flex: 1,
        flexDirection: 'column',
        justifyContent: 'space-between', // Это свойство добавляет расстояние между элементами
        alignItems: 'center',
        top: 100
    },
    main : {

    },*/
    header: {
        fontSize: RFPercentage(4),
        marginTop:'16%',
       // position: 'absolute',
       // top: 70,
        //left: '32%',
        fontFamily: 'MontserratAlternates',
        textAlign: 'center'
    },

    button : {
        backgroundColor: '#169366',
       // position: 'relative',
        //top: 700,
        //width: 350,
       // width: RFValue(300),
        height: RFValue(45),
        alignItems: 'center',
        justifyContent: 'center',
        borderRadius: 10,
        elevation: 3,
        //left: 33,
        //right: 0,
        marginRight: 10,
        marginLeft: 10
    },

    products : {
        //flex : 1,
        //padding : 60
       // backgroundColor:'blue',
        //marginTop: '0%'
        height: '80%'
    },
    scroll : {
        //height: 600
        //backgroundColor: 'red',
        //height: 630,
        //marginTop: 70,
        height: '80%',

    },
    buttonText: {
        fontSize: RFValue(15),
        //fontWeight: 'bold',
        color: 'white',
        fontFamily: 'MontserratAlternates',
    }



});

