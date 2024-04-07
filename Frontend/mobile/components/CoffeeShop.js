import React from 'react';
import {Dimensions, Image, StyleSheet, Text, TouchableWithoutFeedback, View} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";

const windowWidth = Dimensions.get('window').width;
const windowHeight = Dimensions.get('window').height;

//<Image source={require('../assets/image.svg')} />
export default function CoffeeShop({navigation, name}) {
    return (
        <View style={styles.square}>
            <TouchableWithoutFeedback onPress={() => navigation.navigate('shopScreen')}>
                <View>
                    <Image source={require('../assets/icons/shop2.png')} style={styles.image} />
                    <Text style={styles.label}> {name} </Text>
                </View>
            </TouchableWithoutFeedback>

        </View>

    );
};

const styles = StyleSheet.create({
    container: {
    },
    square: {
        marginHorizontal: 10,
        //flex: 1,
        // alignItems : 'center',
        // justifyContent : 'space-between',
        // width: '32%',
        // height: '40%',
        width: windowWidth * 0.32, // 32% of screen width
        // height: windowHeight * 0.15,
        height: '90%',
        backgroundColor: '#169366',
        borderRadius: 6,
        // position: 'absolute',
        shadowColor: '#000',
        shadowOffset: {
            width: 5,
            height: 2,
            //width: '5%',
            //height: '2%',
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
        // backgroundColor: 'black'
    },
    label: {
        //marginTop: '80%',
        //top: '7%',
        //marginTop: 90,
        fontSize: RFPercentage(1.8),
        //fontSize: normalizeFont(14),
        color: 'white',
        //marginLeft: 10,
        fontFamily: 'MontserratAlternates',
        //backgroundColor:'red'
    },
});


