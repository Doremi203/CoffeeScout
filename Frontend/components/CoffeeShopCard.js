import React from 'react';
import {Image, StyleSheet, Text, View} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";


export default function CoffeeShopCard({navigation}) {
    return (
        <View style={styles.square}>
            <Image source={require('../assets/icons/coffee.png')} style={styles.image} />
            <Text style={styles.label}>One Price Coffee</Text>


            <View style={styles.routeButton}>
                <Text style={styles.routeText}> МАРШРУТ </Text>
            </View>



        </View>
    );
};

const styles = StyleSheet.create({
    container: {
    },
    square: {
        marginVertical: 10,
        flexDirection: 'row',
        // width: windowWidth * 0.85,
        // height: windowHeight* 0.15,
        width: RFValue(300),
        height: RFValue(115),
        backgroundColor: 'white',
        borderRadius: 6,
        shadowColor: 'grey',
        shadowOffset: {
            width: 2,
            height: 1,
        },
        shadowOpacity: 0.6,
        shadowRadius: 3.84,
        elevation: 5,
    },

    routeButton : {
        borderColor:'black',
        borderWidth:1,
        //backgroundColor:'red',
        width: RFValue(150),
        height: RFValue(40),
        borderRadius:20,
        top:'20%',
        right: RFValue(130),

    },
    routeText: {
        fontSize: RFValue(13),
        marginTop: RFValue(11),
        marginLeft: RFValue(32),
        fontFamily: 'MontserratAlternates',
        //top:'60%'
    },
    image: {
        marginLeft: 20,
        marginTop: 10,
        width: RFValue(90),
        height: RFValue(90)
    },
    label: {
        fontSize: RFPercentage(2.3),
        color: 'black',
        fontFamily: 'MontserratAlternates',
        marginTop: 20,
        marginLeft: 20
    },

});


