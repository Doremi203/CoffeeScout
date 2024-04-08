import React from 'react';
import {Image, StyleSheet, Text, TouchableWithoutFeedback, View} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";

export default function Cafe({navigation, cafe}) {
    return (
        <View style={styles.square}>
            <TouchableWithoutFeedback onPress={() => navigation.navigate('cafeScreen', {cafe: cafe})}>
                <View>
                    <Image source={require('../assets/icons/shop2.png')} style={styles.image}/>
                    <Text style={styles.label}> {cafe.name} </Text>
                </View>
            </TouchableWithoutFeedback>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {},
    square: {
        marginHorizontal: 10,
        width: RFValue(110),
        height: '90%',
        backgroundColor: '#169366',
        borderRadius: 6,
        shadowColor: '#000',
        shadowOffset: {
            width: 5,
            height: 2,
        },
        shadowOpacity: 0.6,
        shadowRadius: 3.84,
        elevation: 10

    },
    image: {
        marginLeft: RFPercentage(1),
        marginTop: RFPercentage(1),
        width: RFValue(80),
        height: RFValue(80),
    },
    label: {
        fontSize: RFPercentage(1.8),
        color: 'white',
        fontFamily: 'MontserratAlternates',
    },
});


