import React, {useContext, useState} from 'react';
import {StyleSheet, Text, TextInput, TouchableWithoutFeedback, View} from 'react-native';
import {RFValue} from "react-native-responsive-fontsize";
import {Context} from "../index";


export default function ResetPassword({navigation, route}) {

    const [code, setCode] = useState('');
    const [newPassword, setNewPassword] = useState('');

    const {user} = useContext(Context);

    const changePassword = async () => {
        await user.resetPassword(route.params.email, code, newPassword)
        navigation.navigate('auth')
    }

    return (
        <View>
            <Text style={styles.header}>введите код, который вам пришел на почту</Text>

            <View style={{height: '35%', top: RFValue(210)}}>
                <View style={[styles.box]}>
                    <TextInput style={styles.input}
                               placeholder="код"
                               placeholderTextColor="gray"
                               onChangeText={text => setCode(text)}
                    />
                </View>

                <Text style={styles.new}>введите новый пароль </Text>
                <View style={[styles.box, {top: RFValue(30)}]}>
                    <TextInput style={styles.input}
                               placeholder="новый пароль"
                               placeholderTextColor="gray"
                               onChangeText={text => setNewPassword(text)}
                               secureTextEntry={true}/>
                </View>
            </View>

            <View style={styles.newPass}>
                <TouchableWithoutFeedback onPress={changePassword}>
                    <Text style={styles.buttonText}> сменить пароль </Text>
                </TouchableWithoutFeedback>
            </View>
        </View>
    );
}


const styles = StyleSheet.create({
    header: {
        fontSize: RFValue(17),
        top: RFValue(200),
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
    },
    new: {
        fontSize: RFValue(17),
        top: RFValue(20),
        left: 25,
        fontFamily: 'MontserratAlternates',
    },

    newPass: {
        backgroundColor: '#05704A',
        width: '80%',
        height: RFValue(43),
        left: '10%',
        alignItems: 'center',
        justifyContent: 'center',
        borderRadius: 10,
        elevation: 3,
        top: RFValue(260)
    }
});
