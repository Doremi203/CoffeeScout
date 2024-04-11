import React, {useContext} from 'react';
import {ScrollView, StyleSheet, Text, TouchableWithoutFeedback, View} from "react-native";
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import Footer from "./Footer";
import ProductInMenu from "../components/ProductInMenu";
import {Context} from "../index";

export default function CafeScreen({navigation, route}) {

    const {cafe} = route.params;
    console.log(cafe)

    const {loc} = useContext(Context);
    const urlYandex = loc.getUrlYandex(cafe.location.latitude, cafe.location.longitude)._j;
    const urlGoogle = loc.getUrlGoogle(cafe.location.latitude, cafe.location.longitude)._j;

    const map = () => {
        loc.openMap(urlYandex, urlGoogle)
    }

    return (
        <View style={styles.container}>
            <View style={styles.main}>

                <Text style={styles.header}> {cafe.name} </Text>
                <Text style={styles.info}> Адрес: ул.Виницкая 8к4</Text>
                <Text style={styles.info}> Часы работы: ПН-ВС 8:00 - 22:00</Text>

                <Text style={styles.menu}> меню </Text>

                <View style={styles.products}>
                    <ScrollView style={styles.scroll}>
                        <ProductInMenu name={'Капучино'} menuItemId={1}/>
                        <ProductInMenu name={'Латте'} menuItemId={1}/>
                        <ProductInMenu name={'Латте'} menuItemId={1}/>
                    </ScrollView>
                </View>

                <View style={styles.routeButton}>
                    <TouchableWithoutFeedback onPress={map}>
                        <Text style={styles.routeText}> МАРШРУТ </Text>
                    </TouchableWithoutFeedback>
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

    header: {
        fontSize: RFPercentage(5),
        marginTop: '15%',
        fontFamily: 'MontserratAlternatesSemiBold',
    },
    menu: {
        fontSize: RFPercentage(8),
        fontFamily: 'MontserratAlternates',
    },

    products: {
        alignItems: 'center'
    },

    routeButton: {
        borderColor: 'black',
        borderWidth: 1,
        width: RFValue(200),
        height: RFValue(40),
        borderRadius: 20,
        marginTop: '10%',
        marginLeft: '20%'

    },
    routeText: {
        fontSize: RFValue(13),
        fontFamily: 'MontserratAlternates',
        textAlign: 'center',
        marginTop: RFValue(8),
    },

    info: {
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(15),
        marginLeft: RFValue(15)
    },
    scroll: {
        height: '50%',
    },

});
