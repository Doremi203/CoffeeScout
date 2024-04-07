import React from 'react';
import {StyleSheet, Text, TouchableOpacity, View} from 'react-native';
import {RFValue} from "react-native-responsive-fontsize";


const OrderComponent = ({navigation}) => {
    return (
        <TouchableOpacity onPress={() => navigation.navigate('orderScreen')}>
            <View style={styles.container}>
                <Text style={styles.price}> 100₽ </Text>
                <Text style={styles.data}> 28.03 </Text>
                <Text style={styles.place}> One Price Coffee </Text>
            </View>
        </TouchableOpacity>

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
    place: {
        fontFamily: 'MontserratAlternatesMedium',
        fontSize: RFValue(18),
        marginTop: RFValue(18),
        right: RFValue(10)
    }
});

export default OrderComponent;

