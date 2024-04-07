import React, {useState} from 'react';
import {StyleSheet, Text, TouchableOpacity, View} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import Footer from "./Footer";
import Checkbox from "../components/CheckBox";

export default function OrderFin({navigation}) {
    const [checked, setChecked] = useState('first');

    return (
        <View style={styles.container}>
            <View style={styles.main}>
                <Text style={[styles.header, {marginTop: '15%'}]}> Выберите объем </Text>

                <View style={styles.sizes}>
                    <TouchableOpacity style={styles.sizeButton}>
                        <Text style={styles.sizeText}> 200 мл </Text>
                    </TouchableOpacity>
                    <TouchableOpacity style={styles.sizeButton}>
                        <Text style={styles.sizeText}> 300 мл </Text>
                    </TouchableOpacity>
                    <TouchableOpacity style={styles.sizeButton}>
                        <Text style={styles.sizeText}> 400 мл </Text>
                    </TouchableOpacity>
                </View>

                <Text style={[styles.header, {marginTop: '5%'}]}> Добавки </Text>

                <View style={styles.additives}>
                    <Checkbox />
                    <Checkbox />
                    <Checkbox />
                    <Checkbox />
                </View>

                <Text style={[styles.header, {marginTop: '3%'}]}> сиропы </Text>

                <View style={styles.syrops}>
                    <Checkbox />
                    <Checkbox />
                    <Checkbox />
                    <Checkbox />
                </View>

                <View style={styles.addButtons}>
                    <TouchableOpacity style={styles.priceButton}>
                        <Text style={styles.sizeText}> 100 ₽ </Text>
                    </TouchableOpacity>

                    <TouchableOpacity style={styles.addButton}>
                        <Text style={styles.addText}> добавить </Text>
                    </TouchableOpacity>
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
        flex: 1
    },

    header: {
        fontSize: RFPercentage(4),
      //  marginTop: '15%',
        fontFamily: 'MontserratAlternates',
        marginLeft: '2%'
    },
    size: {
        fontSize: RFPercentage(4),
        fontFamily: 'MontserratAlternates',
    },
    sizeButton : {
        borderColor:'black',
        borderWidth:1,
        //backgroundColor:'red',
        width: RFValue(80),
        height: RFValue(40),
        borderRadius:20,


    },
    sizeText: {
        fontSize: RFValue(13),
        marginTop: RFValue(11),
        //marginLeft: RFValue(32),
        fontFamily: 'MontserratAlternates',
        textAlign:'center'
        //top:'60%'
    },
    sizes : {
        flexDirection:'row',
        marginTop: '4%',
        justifyContent: 'space-evenly'
    },
    additives: {
        marginLeft: '7%',
    },
    syrops : {
        marginLeft: '7%',
    },
    priceButton : {
        borderColor:'black',
        borderWidth:1,
        //backgroundColor:'red',
        width: RFValue(80),
        height: RFValue(40),
        borderRadius:20,
        marginLeft: '10%'
    },

    addButton : {
        borderColor:'black',
        borderWidth:1,
        //backgroundColor:'red',
        width: RFValue(180),
        height: RFValue(40),
        borderRadius:20,
        marginLeft: '10%'
    },
    addText: {
        fontFamily: 'MontserratAlternates',
        textAlign:'center',
        fontSize: RFPercentage(3),
        marginTop: '1%'
        //top:'60%'
    },
    addButtons : {
        flexDirection: 'row',
        marginTop: '20%'
    }
});

