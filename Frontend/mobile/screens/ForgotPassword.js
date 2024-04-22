import React, {useContext, useState} from 'react';
import {StyleSheet, Text, TextInput, TouchableWithoutFeedback, View} from 'react-native';
import {RFValue} from "react-native-responsive-fontsize";
import {Context} from "../index";


export default function ForgotPassword({navigation}) {
    const [email, setEmail] = useState('');

    const {user} = useContext(Context);

    const getCode = async () => {
        await user.forgotPassword(email)
        navigation.navigate('reset', {email: email})
    }


    return (
        <View>
            <Text style={styles.header}>укажите email, под которым вы регистрировались </Text>


            <View style={{height: '35%', top: RFValue(250)}}>
                <View style={[styles.box]}>
                    <TextInput style={styles.input}
                               placeholder="e-mail"
                               placeholderTextColor="gray"
                               onChangeText={text => setEmail(text)}/>
                </View>

            </View>

            <View style={styles.button}>
                <TouchableWithoutFeedback onPress={getCode}>
                    <Text style={styles.buttonText}> получить код </Text>
                </TouchableWithoutFeedback>
            </View>

        </View>
    );
}


const styles = StyleSheet.create({
    header: {
        fontSize: RFValue(17),
        top: RFValue(230),
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
        top: RFValue(200)
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
