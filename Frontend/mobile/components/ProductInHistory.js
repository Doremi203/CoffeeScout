import React from 'react';
import {Image, StyleSheet, Text, View} from 'react-native';
import {RFValue} from "react-native-responsive-fontsize";
import LeaveReview from "./LeaveReview";


const ProductInHistory = ({name, price, count, menuItemId}) => {

    return (
        <View style={styles.container}>
            <View>
                <Image source={require('../assets/icons/coffee.png')} style={styles.image}/>
                <Text style={styles.label}>{name}</Text>
                <Text style={styles.price}> {price * count}₽</Text>
                <Text style={styles.count}> Кол-во: {count}</Text>
                <LeaveReview menuItemId={menuItemId}/>
            </View>
            <View style={[styles.line]}/>
        </View>
    );
}


const styles = StyleSheet.create({
    container: {
        width: '100%',
        marginTop: '3%',
        borderRadius: 15,
    },

    image: {
        width: RFValue(100),
        height: RFValue(100),
        marginLeft: RFValue(20)
    },
    label: {
        fontFamily: 'MontserratAlternatesMedium',
        fontSize: RFValue(20),
        top: RFValue(-90),
        marginLeft: RFValue(140)
    },
    price: {
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(23),
        top: RFValue(-60),
        marginLeft: RFValue(260)
    },
    line: {
        left: 0,
        right: 0,
        borderBottomWidth: 1,
        borderBottomColor: 'black',
        opacity: 0.1,
        marginTop: RFValue(10)
    },
    count: {
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(16),
        top: RFValue(-20),
        marginLeft: RFValue(20)
    }
});

export default ProductInHistory;

