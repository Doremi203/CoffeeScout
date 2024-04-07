import React, {useContext, useState} from 'react';
import {SafeAreaView, StyleSheet, Text, TextInput, TouchableWithoutFeedback, View} from 'react-native';
import {RFValue} from "react-native-responsive-fontsize";
import {Context} from "../index";


export default function Login({navigation}) {

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    console.log(`Email: ${email}, Password: ${password}`);


    const {user} = useContext(Context);

    const click = () => {
        user.login(email, password, navigation).then();
    }


    return (
        <SafeAreaView>
            <Text style={styles.header}>войти</Text>

            <View style={{height: '37%', marginTop: '23%'}}>
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
                               onChangeText={text => setPassword(text)}
                               secureTextEntry={true}/>
                </View>
            </View>


            <View style={styles.button}>
                <TouchableWithoutFeedback onPress={click}>
                    <Text style={styles.buttonText}> войти </Text>
                </TouchableWithoutFeedback>
            </View>


            <TouchableWithoutFeedback onPress={() => navigation.navigate('registration')}>
                <Text style={styles.text}> нет аккаунта?<Text
                    style={{fontFamily: 'MontserratAlternatesSemiBold'}}> зарегистрироваться</Text> </Text>
            </TouchableWithoutFeedback>
        </SafeAreaView>
    );
}


const styles = StyleSheet.create({
    header: {
        fontSize: 36,
        top: '17%',
        left: 25,
        fontFamily: 'MontserratAlternates',
    },
    button: {
        backgroundColor: '#05704A',
        width: '80%',
        height: RFValue(43),   ///////
        left: '10%',
        right: '0%',
        alignItems: 'center',
        justifyContent: 'center',
        borderRadius: 10,
        elevation: 3,
    },
    text: {
        top: '1%',
        textAlign: 'center',
        fontFamily: 'MontserratAlternates',
    },
    input: {
        height: RFValue(55),
        width: '85%',
        borderWidth: 1,
        paddingLeft: 10,
        fontFamily: 'MontserratAlternates',
    },

    box: {
        left: 0,
        right: 0,
        alignItems: 'center',
    },

    buttonText: {
        fontSize: RFValue(15),
        color: 'white',
        fontFamily: 'MontserratAlternates',
    }
});
