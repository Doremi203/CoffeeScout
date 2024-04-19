import React, {useContext, useState} from 'react';
import {StyleSheet, Text, TextInput, TouchableWithoutFeedback, View} from 'react-native';
import {RFValue} from "react-native-responsive-fontsize";
import {Context} from "../index";


export default function Auth({navigation}) {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const {user} = useContext(Context);

    const click = async () => {
        await user.login(email, password, navigation);
    }

    const forgot = async () => {
        navigation.navigate('forgot')
    }

    return (
        <View>
            <Text style={styles.header}>войти</Text>

            <View style={{height: '35%', marginTop: '30%'}}>
                <View style={[styles.box, {top: '13%'}]}>
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

            <TouchableWithoutFeedback onPress={forgot}>
                <Text style={styles.forgot}> забыли пароль? </Text>
            </TouchableWithoutFeedback>
        </View>
    );
}


const styles = StyleSheet.create({
    header: {
        fontSize: 36,
        top: '23%',
        left: 25,
        fontFamily: 'MontserratAlternates',
    },
    button: {
        backgroundColor: '#05704A',
        width: '80%',
        height: RFValue(43),
        left: '10%',
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
    },
    forgot: {
        textAlign: 'center',
        top: RFValue(10),
        fontFamily: 'MontserratAlternates',
        opacity: 0.5
    }
});
