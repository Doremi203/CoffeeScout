import React, {useState} from 'react';
import {Alert, SafeAreaView, StyleSheet, Text, TextInput, TouchableWithoutFeedback, View} from 'react-native';
import {RFValue} from "react-native-responsive-fontsize";
import axios from "axios";

/*

const customFonts = {
    fontFamily: require('../assets/fonts/MontserratAlternates-Black.ttf'),
};
addCustomFonts(customFonts);*/


export default function Auth({navigation}) {

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    console.log(`Email: ${email}, Password: ${password}`);
    const auth = () => {
        axios.post("http://192.168.1.124:8000/api/v1/account/login", {
            //name: name,
            email: email,
            password: password
        })
            .then(response => {
                console.log(`RESPONSE ${response.status}`);
                navigation.navigate('main');
            })
            .catch(error => {
                switch(error.response.status) {
                    case 401:
                        Alert.alert('Ошибка', 'Неверный логин или пароль');
                        break;
                    default:
                        Alert.alert('Ошибка', 'Что-то пошло не так');
                }
            });
    };


    return (
        <SafeAreaView>
            <Text style={styles.header}>войти</Text>

            <View style={{height: '37%', marginTop:'23%'}}>
                <View style={[styles.box, {top: '10%'}]}>
                    <TextInput style={styles.input}
                               placeholder="e-mail"
                               placeholderTextColor="gray"
                               onChangeText={text => setEmail(text)}/>
                </View>

                <View style={[styles.box, {top: '25%'}]}>
                    <TextInput style={styles.input}
                               placeholder="пароль"
                               placeholderTextColor="gray"
                               onChangeText={text => setPassword(text)}/>
                </View>
            </View>


            <View style={styles.button}>
                <TouchableWithoutFeedback onPress={auth} >
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
