import React, {useRef, useEffect} from 'react';
import {StyleSheet, Text, TextInput, TouchableWithoutFeedback, View} from "react-native";
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import Footer from "./Footer";
import Icon from "react-native-vector-icons/FontAwesome";
import ProductInMenu from "../components/ProductInMenu";

export default function ShopScreen({navigation}) {


    return (
        <View style={styles.container}>
            <View style={styles.main}>

                <Text style={styles.header}> One Price Coffee </Text>
                <Text style={styles.info}> Адрес: ул.Виницкая 8к4</Text>
                <Text style={styles.info}> Часы работы: ПН-ВС 8:00 - 22:00</Text>

                <Text style={styles.menu}> меню </Text>

                <View style={styles.products}>
                    <ProductInMenu name={'Капучино'} menuItemId={1}/>
                    <ProductInMenu name={'Латте'} menuItemId={1}/>
                </View>


                <View style={styles.routeButton}>
                    <TouchableWithoutFeedback >
                        <Text style={styles.routeText}> МАРШРУТ </Text>
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
        flex: 1,
        //alignItems: 'center'
    },

    header: {
        fontSize: RFPercentage(5),
        marginTop: '15%',
        fontFamily: 'MontserratAlternatesSemiBold',
    },
    menu : {
        fontSize: RFPercentage(8),
        fontFamily: 'MontserratAlternates',
    },

    products: {
        alignItems: 'center'
    },

    routeButton: {
        borderColor: 'black',
        borderWidth: 1,
        width: RFValue(200),
        height: RFValue(40),
        borderRadius: 20,
        //top: '50%',
        // right: RFValue(350),
        //top : RFValue(-110),
        //  marginLeft: '2%'
        marginTop: '20%',
        marginLeft: '20%'

    },
    routeText: {
        fontSize: RFValue(13),
        // marginTop: RFValue(5),
        //marginLeft: RFValue(28),
        fontFamily: 'MontserratAlternates',
        textAlign: 'center',
        marginTop: RFValue(8),
    },

    info : {
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(15),
        marginLeft: RFValue(15)
    }



});
