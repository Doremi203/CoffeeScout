import React from 'react';
import {StyleSheet, Text, View} from 'react-native';
import Footer from "./Footer";
import {RFPercentage, RFValue} from 'react-native-responsive-fontsize';
import Icon from "react-native-vector-icons/FontAwesome";


export default function Payment({navigation}) {
    return (
        <View style={styles.container}>
            <View style={styles.main}>
                <View style={styles.box}>
                    <Text style={styles.text}> Ваш заказ успешно оплачен!</Text>
                    <Icon name={'check-circle'} size={RFValue(100)} color="white" style={styles.icon}/>
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
        alignItems: 'center'
    },

    box: {
        width: '95%',
        height: RFValue(130),
        backgroundColor: '#169366',
        top: RFValue(280),
        alignItems: 'center',
        borderRadius: 20
    },
    text: {
        fontSize: RFPercentage(2.8),
        fontFamily: 'MontserratAlternatesMedium',
        color: 'white',
    },

});

