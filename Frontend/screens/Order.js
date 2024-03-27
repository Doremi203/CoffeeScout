import React from 'react';
import {ScrollView, StyleSheet, Text, View} from 'react-native';
import {RFPercentage} from "react-native-responsive-fontsize";
import Footer from "./Footer";
import CoffeeShop from "../components/CoffeeShop";

export default function Order({navigation}) {
    return (
        <View style={styles.container}>
            <View style={styles.main}>
                <Text style={styles.header}> Выберите кофейню </Text>
                <View style={styles.shops}>
                    <ScrollView style={styles.scroll}>
                        <CoffeeShop navigation={navigation}/>
                        <CoffeeShop navigation={navigation}/>
                        <CoffeeShop navigation={navigation}/>
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
        flex: 1
    },

    shops : {
        height: '50%',
        //backgroundColor:'pink',
        alignItems:'center'
    },

    header: {
        fontSize: RFPercentage(4),
        marginTop: '15%',
        fontFamily: 'MontserratAlternates',
        //color: '#05704A',
    }
});

