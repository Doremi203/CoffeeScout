import React from 'react';
import {StyleSheet, Text, View, Image, ScrollView, TouchableOpacity} from 'react-native';
import Footer from "./Footer";
import {RFValue} from "react-native-responsive-fontsize";
import ProductInHistory from "../components/ProductInHistory";


export default function OrderScreen({navigation}) {
    return (
        <View style={styles.container}>
            <View style={styles.main}>

                <TouchableOpacity onPress={() => navigation.navigate('profile')}>
                    <Image source={require('../assets/icons/left-arrow.png')} style={styles.arrow}/>
                </TouchableOpacity>

                <Text style={styles.number}> №101 </Text>

                <View>
                    <ScrollView style={styles.scroll}>
                        <ProductInHistory/>
                        <ProductInHistory/>

                        <View>
                            <Text style={[styles.text, {left: RFValue(15), top: RFValue(10)}]}> Оплачено </Text>
                            <Text style={[styles.text, {left: RFValue(290), top: RFValue(-15)}]}> 100₽ </Text>
                            <View style={[styles.line]}/>
                        </View>


                    </ScrollView>
                </View>

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

    },

    number: {
        fontFamily: 'MontserratAlternatesMedium',
        fontSize: RFValue(20),
        textAlign: 'center',
    },


    arrow: {
        width: RFValue(15),
        height: RFValue(15),
        left: '5%',
        marginTop: '15%'
    },
    scroll: {
        height: '88%',
    },
    text: {
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(17)
    },
    line: {
        left: 0,
        right: 0,
        borderBottomWidth: 1,
        borderBottomColor: 'black',
        opacity: 0.1
    },

});