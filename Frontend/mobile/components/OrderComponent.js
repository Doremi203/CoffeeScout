import React from 'react';
import {StyleSheet, Text, TouchableWithoutFeedback, View} from 'react-native';
import {RFValue} from "react-native-responsive-fontsize";


const OrderComponent = ({navigation, status, number, items}) => {
    return (
        <TouchableWithoutFeedback onPress={() => navigation.navigate('orderScreen', {number: number, items: items})}>
            <View style={styles.container}>
                <Text style={styles.price}> 100₽ </Text>
                <Text style={styles.data}> 28.03 </Text>
                <Text style={styles.status}> {status} </Text>
                <Text style={styles.cafe}> One Price Coffee </Text>
            </View>
        </TouchableWithoutFeedback>

    );
}


const styles = StyleSheet.create({
    container: {
        backgroundColor: '#E2E9E6',
        width: '100%',
        height: RFValue(70),
        marginTop: '3%',
        flexDirection: 'row',
        borderRadius: 15,
    },
    price: {
        fontFamily: 'MontserratAlternatesSemiBold',
        fontSize: RFValue(23),
        marginLeft: RFValue(20),
        marginTop: RFValue(5)
    },
    data: {
        fontFamily: 'MontserratAlternatesMedium',
        fontSize: RFValue(18),
        right: RFValue(65),
        marginTop: RFValue(34)
    },
    status: {
        fontFamily: 'MontserratAlternatesMedium',
        fontSize: RFValue(18),
        marginTop: RFValue(10),
        right: RFValue(10)
    },
    cafe: {
        fontFamily: 'MontserratAlternatesMedium',
        fontSize: RFValue(15),
        marginTop: RFValue(40),
        right: RFValue(100),
        color: 'gray'
    }
});

export default OrderComponent;

