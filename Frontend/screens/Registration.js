import React from 'react';
import {SafeAreaView, StyleSheet, Text, TextInput, TouchableWithoutFeedback, View} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";

export default function Registration({navigation}) {
  return (
    <SafeAreaView style={styles.container}>
        <Text style={styles.header}>регистрация</Text>

        <View style={{height: '45%', marginTop:'23%'}}>
            <View style={[styles.box, {top: '10%'}]}>
                <TextInput style={styles.input}
                           placeholder="имя"
                           placeholderTextColor="gray"/>
            </View>
            <View style={[styles.box, {top: '40%'}]}>
                <TextInput style={styles.input}
                           placeholder="e-mail"
                           placeholderTextColor="gray"/>
            </View>
            <View style={[styles.box, {top: '70%'}]}>
                <TextInput style={styles.input}
                           placeholder="пароль"
                           placeholderTextColor="gray"/>
            </View>
        </View>



        <View style={styles.button}>
            <TouchableWithoutFeedback onPress={() => navigation.navigate('main')} >
                <Text style={styles.buttonText}>зарегистрироваться</Text>
            </TouchableWithoutFeedback>
        </View>
         <TouchableWithoutFeedback onPress={() => navigation.navigate('auth')}>
                <Text style={styles.text}> уже есть аккаунт? <Text style={{fontFamily: 'MontserratAlternatesSemiBold'}}> войти </Text> </Text>
        </TouchableWithoutFeedback>
    </SafeAreaView>
  );
}


const styles = StyleSheet.create({
    header: {
        fontSize: RFValue(34),
        //position: 'absolute',
        //top: 149,
        top: '17%',
        left: 25,
        fontFamily: 'MontserratAlternates',
       // backgroundColor: 'blue'
    },
    button: {
        backgroundColor: '#05704A',
        //width: 329,
        width: '80%',
        height: RFValue(43),
        //position: 'absolute',
        //top: 457,
        top: '0%',
        //left: 43,
        left: '10%',
        //right: '0%',
        alignItems: 'center',
        justifyContent: 'center',
        borderRadius: 10,
        elevation: 3,

    },
    text: {
        //position: 'absolute',
        //top: 513,
        top: '1%',
        textAlign: 'center',
        //left: '22.5%',
        //left: 100,
        fontFamily: 'MontserratAlternates',
    },
    input: {
        height: RFValue(52),
        //width: 350,
        width: '85%',
        borderWidth: 1,
        paddingLeft: 10,
        fontFamily: 'MontserratAlternates',
    },

    box : {
        position: 'absolute',
        left: 0,
        right: 0,
        alignItems: 'center',
    },
    buttonText: {
        fontSize: RFPercentage(2.3),
        //fontWeight: 'bold',
        color: 'white',
        fontFamily: 'MontserratAlternates',
    }

});

//export default Registration;