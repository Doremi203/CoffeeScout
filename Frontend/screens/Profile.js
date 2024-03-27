import React from 'react';
import {StyleSheet, Text, TouchableWithoutFeedback, View} from 'react-native';
import Footer from "./Footer";
import {RFValue} from "react-native-responsive-fontsize";


export default function Profile({navigation}) {
    return (
        <View style={styles.container}>
            <View style={styles.main}>
                <Text style={styles.name}>Майя</Text>
                <View style={[styles.line, {top: RFValue(100)}]} />
                <Text style={[styles.text, { top: RFValue(110)}]}> Личные данные </Text>
                <View style={[styles.line, {top: RFValue(120)}]} />
                <Text style={[styles.text, { top: RFValue(130)}]}> История заказов </Text>
                <View style={[styles.line, {top: RFValue(140)}]} />
                <Text style={[styles.text, { top: RFValue(150)}]}> Настройки </Text>
                <View style={[styles.line, {top: RFValue(160)}]} />
                <Text style={[styles.text, { top: RFValue(170)}]}> Управление аккаунтом </Text>
                <TouchableWithoutFeedback onPress={() => navigation.navigate('main')}>
                    <Text style={styles.out}> Выйти </Text>
                </TouchableWithoutFeedback>
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
    main : {
        flex: 1,
    },

    name: {
        fontSize: RFValue(36),
        //position: 'absolute',
        top: RFValue(84),
        left: 38,
        fontFamily: 'MontserratAlternatesMedium',
    },
    line: {
        //position: 'absolute',
        left: 0,
        right: 0,
        borderBottomWidth: 1,
        borderBottomColor: 'black'
    },
    text: {
        fontSize: RFValue(20),
       // position: 'absolute',
        left: 27,
        fontFamily: 'MontserratAlternates',
    },
    out: {
        fontSize: RFValue(20),
        color: '#05704A',
       // position:'absolute',
        top: RFValue(190),
        //fontWeight: 'bold',
        fontFamily: 'MontserratAlternates',
        textAlign: 'center'
    }

});

//export de