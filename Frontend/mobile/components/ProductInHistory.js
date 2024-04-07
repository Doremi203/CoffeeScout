import React from 'react';
import {Image, StyleSheet, Text, View} from 'react-native';
import {RFValue} from "react-native-responsive-fontsize";


const ProductInHistory = ({name}) => {
    return (

        <View>
            <View style={styles.container}>
                <Image source={require('../assets/icons/coffee.png')} style={styles.image}/>
                <Text style={styles.label}>{name}</Text>
                <Text style={styles.price}> 100₽</Text>


            </View>

            <View style={[styles.line]}/>
        </View>


    );
}


const styles = StyleSheet.create({
    container: {

        //  backgroundColor: '#E2E9E6',
        width: '100%',
        height: RFValue(100),
        marginTop: '3%',
        //top: '25%',
        //flexDirection: 'row',
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
        //marginTop: RFValue(20)
        top: RFValue(-90),
        marginLeft: RFValue(140)
    },
    price: {
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(23),
        // marginTop: RFValue(5),
        //marginTop: '16%',
        //marginLeft: RFValue(50)
        top: RFValue(-60),
        marginLeft: RFValue(260)
    },
    line: {
        //position: 'absolute',
        left: 0,
        right: 0,
        borderBottomWidth: 1,
        borderBottomColor: 'black',
        opacity: 0.1,
        marginTop: RFValue(10)
    },
});

export default ProductInHistory;

