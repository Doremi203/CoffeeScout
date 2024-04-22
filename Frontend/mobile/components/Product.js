import React from 'react';
import {Image, StyleSheet, Text, TouchableWithoutFeedback, View} from 'react-native';
import {RFPercentage, RFValue} from 'react-native-responsive-fontsize';

export default function Product({navigation, name, type}) {
    return (
        <View style={styles.square}>
            <TouchableWithoutFeedback onPress={() => navigation.navigate('product', {type: type})}>
                <View>
                    <Image source={require('../assets/icons/coffee.png')} style={styles.image}/>
                    <Text style={styles.label}> {name} </Text>
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
        marginLeft: RFPercentage(3),
        marginTop: RFPercentage(2),
        width: RFValue(70),
        height: RFValue(70),
    },
    label: {
        top: '7%',
        fontSize: RFPercentage(2),
        color: 'white',
        marginLeft: 10,
        fontFamily: 'MontserratAlternates',
    },
});
