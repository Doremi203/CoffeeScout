import React from 'react';
import {StyleSheet, Text, TouchableWithoutFeedback, View, Image, ScrollView} from 'react-native';
import Footer from "./Footer";
import {RFValue} from "react-native-responsive-fontsize";
import OrderComponent from "../components/OrderComponent";


export default function Profile({navigation}) {
    return (
        <View style={styles.container}>
            <View style={styles.main}>

                <TouchableWithoutFeedback onPress={() => navigation.navigate('settings')}>
                    <View style={styles.profile}>
                        <Image source={require('../assets/icons/user.png')} style={styles.image}/>
                        <Text style={styles.name}>Майя</Text>
                        <Text style={styles.email}>
                            maiya.ifraimova@yandex.ru
                        </Text>
                        <Image source={require('../assets/icons/right-arrow.png')} style={styles.arrow}/>
                    </View>
                </TouchableWithoutFeedback>


                <Text style={styles.historyHeader}> История заказов </Text>

                <ScrollView style={styles.scroll}>
                    <OrderComponent navigation={navigation}/>
                    <OrderComponent navigation={navigation}/>
                    <OrderComponent navigation={navigation}/>
                    <OrderComponent navigation={navigation}/>
                    <OrderComponent navigation={navigation}/>
                    <OrderComponent navigation={navigation}/>
                </ScrollView>

            </View>

            <Footer navigation={navigation}/>
        </View>
    );
}


const styles = StyleSheet.create({
    container: {
        flexDirection: 'column',
        minHeight: '100%',
        // alignItems: 'center',

    },
    main: {
        flex: 1,
        alignItems: 'center',
    },
    profile: {
        //backgroundColor: '#AEC09A',
        //backgroundColor: '#D5E2DD',
        backgroundColor: '#E2E9E6',
        width: '90%',
        height: RFValue(100),
        top: '25%',
        flexDirection: 'row',
        borderRadius: 15,

    },

    image: {
        width: RFValue(70),
        height: RFValue(70),
        marginTop: RFValue(17),
        marginLeft: RFValue(15),
    },

    name: {
        fontSize: RFValue(22),
        fontFamily: 'MontserratAlternatesMedium',
        marginTop: RFValue(26),
        marginLeft: RFValue(20),
    },
    email: {

        marginTop: RFValue(60),
        right: RFValue(62),
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(12),
    },
    arrow: {
        width: RFValue(15),
        height: RFValue(15),
        marginTop: RFValue(42),
        right: RFValue(45),
    },
    historyHeader: {
        fontSize: RFValue(20),
        fontFamily: 'MontserratAlternates',
        marginTop: '30%',
        right: '16%',
    },

    orders: {},
    scroll: {}

});