import React, {useContext, useEffect, useState} from 'react';
import {
    StyleSheet,
    Text,
    TouchableWithoutFeedback,
    View,
    Image,
    TouchableOpacity,
    TextInput,
    Button
} from 'react-native';
import Footer from "./Footer";
import {RFValue} from "react-native-responsive-fontsize";
import {Context} from "../index";


export default function Settings({navigation}) {

    const {user} = useContext(Context);

    const [email, setEmail] = useState("")

    useEffect(() => {
        const fetchEmail = async () => {
            const email = await user.getEmail();
            setEmail(email);
        };

        fetchEmail();
    }, []);


    const [editableEmail, setEditableEmail] = useState(false);


    const handleSaveEmail = async () => {
        // добавить логику для сохранения нового email
        setEditableEmail(false);
        await user.changeEmail(email);
    };


    const [name, setName] = useState("")
    const [editableName, setEditableName] = useState(false);

    useEffect(() => {
        const fetchName = async () => {
            const name = await user.getName();
            setName(name);
        };

        fetchName();
    }, []);

    const handleSaveName = async () => {
        setEditableName(false);
        await user.changeName(name);
    };

    return (
        <View style={styles.container}>
            <View style={styles.main}>

                <TouchableWithoutFeedback onPress={() => navigation.navigate('profile')}>
                    <Image source={require('../assets/icons/left-arrow.png')} style={styles.arrow}/>
                </TouchableWithoutFeedback>


                <Text style={styles.email}> name </Text>
                <View style={styles.emailBox}>
                    <TextInput
                        style={[styles.input, !editableName && styles.disabled]}
                        value={name}
                        onChangeText={setName}
                        editable={editableName}
                    />
                    {!editableName && (
                        <TouchableOpacity onPress={() => setEditableName(true)}>
                            <Image source={require('../assets/icons/edit.png')} style={styles.edit}/>
                        </TouchableOpacity>
                    )}
                    {editableName && (
                        <Button title="Save" onPress={handleSaveName} color='#05704A'/>
                    )}
                </View>


                <View style={styles.line}/>


                <Text style={styles.email}> e-mail </Text>
                <View style={styles.emailBox}>
                    <TextInput
                        style={[styles.input, !editableEmail && styles.disabled]}
                        value={email}
                        onChangeText={setEmail}
                        editable={editableEmail}
                    />
                    {!editableEmail && (
                        <TouchableOpacity onPress={() => setEditableEmail(true)}>
                            <Image source={require('../assets/icons/edit.png')} style={styles.edit}/>
                        </TouchableOpacity>
                    )}
                    {editableEmail && (
                        <Button title="Save" onPress={handleSaveEmail} color='#05704A'/>
                    )}
                </View>


                <View style={styles.line}/>


                <TouchableOpacity style={styles.button}>
                    <Text style={styles.out}> Выйти </Text>
                </TouchableOpacity>


                <TouchableOpacity>
                    <Text style={styles.deleteAcc}> Удалить аккаунт </Text>
                </TouchableOpacity>

            </View>

            <Footer navigation={navigation}/>
        </View>
    );
}


const styles = StyleSheet.create({
    container: {
        flexDirection: 'column',
        minHeight: '100%',

    },
    main: {
        flex: 1,
        alignItems: 'center',
    },
    out: {
        fontSize: RFValue(20),
        color: '#05704A',
        marginTop: RFValue(18),
        fontFamily: 'MontserratAlternatesMedium',
        textAlign: 'center'
    },
    button: {
        backgroundColor: '#E2E9E6',
        width: '90%',
        height: RFValue(60),
        borderRadius: 15,
        marginTop: '5%'
    },
    deleteAcc: {
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(20),
        opacity: 0.5,
        marginTop: '3%'
        //top: 150,
    },
    arrow: {
        width: RFValue(15),
        height: RFValue(15),
        marginTop: RFValue(50),
        right: '45%'
    },


    boxEmail: {
        fontFamily: 'MontserratAlternates',
        width: '90%',
        height: '6%',
        paddingLeft: 10,
        top: '3%'
    },

    line: {
        borderBottomWidth: 1,
        borderBottomColor: 'black',
        opacity: 0.1,
        marginTop: '4%',
        width: '100%'

    },

    inputContainer: {
        marginBottom: 20,
    },
    input: {
        height: RFValue(25),
        borderColor: 'gray',
        marginTop: RFValue(4),
        width: '80%',
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(15)
    },

    email: {
        marginTop: '6%',
        fontFamily: 'MontserratAlternatesMedium',
        fontSize: RFValue(12),
        right: '40.9%'

    },
    edit: {
        width: RFValue(20),
        height: RFValue(20),
        marginLeft: RFValue(27),
        marginTop: '6%'
    },
    emailBox: {
        flexDirection: 'row'
    },


});