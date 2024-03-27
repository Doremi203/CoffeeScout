import React from 'react';
import {Dimensions, StyleSheet, Text, TouchableWithoutFeedback, View} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";

const windowWidth = Dimensions.get('window').width;
const windowHeight = Dimensions.get('window').height;

//<Image source={require('../assets/image.svg')} />
export default function CoffeeShop({navigation}) {
    return (
        <View>
            <TouchableWithoutFeedback onPress={() => navigation.navigate('orderFin')}>
                <View style={styles.square}>

                    <Text style={styles.label}>One Price Coffee</Text>
                </View>
            </TouchableWithoutFeedback>
        </View>

    );
};

const styles = StyleSheet.create({
    container: {
    },
    square: {
        marginVertical: 10,
        flexDirection: 'row',
        width: RFValue(300),
        height: RFValue(40),
        //backgroundColor: 'white',
        borderRadius: 6,
        shadowColor: 'grey',
        shadowOffset: {
            width: 2,
            height: 1,
        },
        shadowOpacity: 0.6,
        shadowRadius: 3.84,
        elevation: 5,
        backgroundColor: '#05704A',
    },


    label: {
        fontSize: RFPercentage(2.3),
        color: 'white',
        fontFamily: 'MontserratAlternatesSemiBold',
        marginTop: RFValue(11),
        marginLeft: RFValue(20),
       // color: '#05704A',
    },

});


