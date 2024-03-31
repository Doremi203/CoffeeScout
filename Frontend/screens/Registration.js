import React, {useContext, useState} from 'react';
import {
    SafeAreaView,
    StyleSheet,
    Text,
    TextInput,
    TouchableOpacity,
    View
} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";


import {Context} from "../index";


export default function Registration({navigation}) {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const {user} = useContext(Context);

    const click = () => {
        console.log(name, email, password)
        user.registration(name, email, password, navigation).then();
    }

    return (
        <SafeAreaView style={styles.container}>
            <Text style={styles.header}>регистрация</Text>

            <View style={{height: '45%', marginTop: '23%'}}>
                <View style={[styles.box, {top: '10%'}]}>
                    <TextInput style={styles.input}
                               placeholder="имя"
                               placeholderTextColor="gray"
                               onChangeText={text => setName(text)}
                               value={name}
                    />
                </View>
                <View style={[styles.box, {top: '40%'}]}>
                    <TextInput style={styles.input}
                               placeholder="e-mail"
                               placeholderTextColor="gray"
                               onChangeText={text => setEmail(text)}
                               value={email}/>

                </View>
                <View style={[styles.box, {top: '70%'}]}>
                    <TextInput style={styles.input}
                               placeholder="пароль"
                               placeholderTextColor="gray"
                               onChangeText={text => setPassword(text)}
                               value={password}/>
                </View>
            </View>


            <View style={styles.button}>
                <TouchableOpacity onPress={click}>
                    <Text style={styles.buttonText}>зарегистрироваться</Text>
                </TouchableOpacity>
            </View>
            <TouchableOpacity onPress={() => navigation.navigate('auth')} style={{marginTop: '1%'}}>
                <Text style={styles.text}> уже есть аккаунт? <Text
                    style={{fontFamily: 'MontserratAlternatesSemiBold'}}> войти </Text> </Text>
            </TouchableOpacity>
        </SafeAreaView>
    );
}


const styles = StyleSheet.create({
    header: {
        fontSize: RFValue(34),
        top: '17%',
        left: 25,
        fontFamily: 'MontserratAlternates',
    },
    button: {
        backgroundColor: '#05704A',
        width: '80%',
        height: RFValue(43),
        top: '0%',
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
        height: RFValue(52),
        width: '85%',
        borderWidth: 1,
        paddingLeft: 10,
        fontFamily: 'MontserratAlternates',
    },

    box: {
        position: 'absolute',
        left: 0,
        right: 0,
        alignItems: 'center',
    },
    buttonText: {
        fontSize: RFPercentage(2.3),
        color: 'white',
        fontFamily: 'MontserratAlternates',
    }

});
