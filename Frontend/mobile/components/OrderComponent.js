import React, {useContext} from 'react';
import {StyleSheet, Text, TouchableWithoutFeedback, View} from 'react-native';
import {RFValue} from "react-native-responsive-fontsize";
import {Context} from "../index";

const OrderComponent = ({navigation, status, number, items, date, cafe}) => {

    const parts = date.split("T")[0].split("-");
    const day = parts[2];
    const month = parts[1];
    const year = parts[0];

    const newDate = `${day}.${month}.${year}`;

    const {order} = useContext(Context)

    const totalPrice = order.totalPrice(items)

    return (
        <TouchableWithoutFeedback
            onPress={() => navigation.navigate('orderScreen', {number: number, items: items, totalPrice: totalPrice})}>
            <View style={styles.container}>
                <Text style={styles.price}> {totalPrice}₽ </Text>
                <Text style={styles.data}> {newDate} </Text>
                <Text style={styles.status}> {status} </Text>
                <Text style={styles.cafe}> {cafe.name} </Text>
            </View>
        </TouchableWithoutFeedback>

    );
}


const styles = StyleSheet.create({
    container: {
        backgroundColor: '#E2E9E6',
        width: RFValue(330),
        height: RFValue(70),
        marginTop: '3%',
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
        marginTop: RFValue(6),
        marginLeft: RFValue(10)
    },
    status: {
        fontFamily: 'MontserratAlternatesMedium',
        fontSize: RFValue(18),
        marginTop: RFValue(-55),
        marginLeft: RFValue(180)
    },
    cafe: {
        fontFamily: 'MontserratAlternatesMedium',
        fontSize: RFValue(15),
        marginTop: RFValue(10),
        marginLeft: RFValue(180),
        color: 'gray'
    }
});

export default OrderComponent;

