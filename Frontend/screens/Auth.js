import React from 'react';
import {SafeAreaView, StyleSheet, Text, TextInput, TouchableWithoutFeedback, View} from 'react-native';
import {RFValue} from "react-native-responsive-fontsize";

/*

const customFonts = {
    fontFamily: require('../assets/fonts/MontserratAlternates-Black.ttf'),
};
addCustomFonts(customFonts);*/


export default function Auth({navigation}) {

    return (
        <SafeAreaView>
            <Text style={styles.header}>войти</Text>

            <View style={{height: '37%', marginTop:'23%'}}>
                <View style={[styles.box, {top: '10%'}]}>
                    <TextInput style={styles.input}
                               placeholder="e-mail"
                               placeholderTextColor="gray"/>
                </View>

                <View style={[styles.box, {top: '25%'}]}>
                    <TextInput style={styles.input}
                               placeholder="пароль"
                               placeholderTextColor="gray"/>
                </View>
            </View>


            <View style={styles.button}>
                <TouchableWithoutFeedback onPress={() => navigation.navigate('main')} >
                    <Text style={styles.buttonText}> войти </Text>
                </TouchableWithoutFeedback>
            </View>


            <TouchableWithoutFeedback onPress={() => navigation.navigate('registration')}>
                <Text style={styles.text}> нет аккаунта?<Text style={{fontFamily: 'MontserratAlternatesSemiBold'}}> зарегистрироваться</Text> </Text>
            </TouchableWithoutFeedback>
        </SafeAreaView>
    );
}

//<Text style={styles.text}> нет аккаунта?  <TouchableWithoutFeedback onPress={() => navigation.navigate('registration')}><Text style={{fontWeight:'bold'}}> зарегистрироваться</Text> </TouchableWithoutFeedback></Text>

const styles = StyleSheet.create({
    header: {
        fontSize: 36,
        //position: 'absolute',
        //top: 149,
        //left: 40,
        top: '17%',
        left: 25,
        fontFamily: 'MontserratAlternates',
    },
    button: {
        backgroundColor: '#05704A',
        width: '80%',
        height: 49,
        //position: 'absolute',
        //top: 373,
        //top:'0%',
        left: '10%',
        right: '0%',
        alignItems: 'center',
        justifyContent: 'center',
        borderRadius: 10,
        elevation: 3,
    },
    text: {
       // position: 'absolute',
        top: '1%',
        //top: 441,
        //left: '0%',
        textAlign: 'center',
        fontFamily: 'MontserratAlternates',
    },
    input: {
        height: RFValue(55),
        //width: 350,
        width: '85%',
        borderWidth: 1,
        paddingLeft: 10,
        fontFamily: 'MontserratAlternates',
    },

    box : {
        //position: 'absolute',
        left: 0,
        right: 0,
        alignItems: 'center',
    },

    buttonText: {
        fontSize: RFValue(15),
        //fontWeight: 'bold',
        color: 'white',
        fontFamily: 'MontserratAlternates',
    }
});
