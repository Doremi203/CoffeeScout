import React, {useContext, useEffect, useState} from 'react';
import {ScrollView, StyleSheet, Text, TouchableWithoutFeedback, View} from "react-native";
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import Footer from "./Footer";
import ProductInMenu from "../components/ProductInMenu";
import {Context} from "../index";

export default function CafeScreen({navigation, route}) {

    const {cafe} = useContext(Context)

    const {cafeParam} = route.params;
    console.log(cafeParam)

    console.log(cafeParam.workingHours)
    console.log(cafeParam.location)

    const {loc} = useContext(Context);
    const urlYandex = loc.getUrlYandex(cafeParam.location.latitude, cafeParam.location.longitude)._j;
    const urlGoogle = loc.getUrlGoogle(cafeParam.location.latitude, cafeParam.location.longitude)._j;

    const map = () => {
        loc.openMap(urlYandex, urlGoogle)
    }

    const [menu, setMenu] = useState([])
    const [info, setInfo] = useState([])
    useEffect(() => {
        const fetchMenu = async() => {
            const menuu = await cafe.getMenu(cafeParam.id);
            setMenu(menuu)
            const infoo = await cafe.getInfo(cafeParam.id);
            setInfo(infoo)
        }

        fetchMenu();

    }, []);



   // let hours = days[info.workingHours[0]]




    /* {menu && menu.map((item) => (
                            <ProductInMenu name={'Капучино'} menuItemId={1}/>
                        ))} */

    return (
        <View style={styles.container}>
            <View style={styles.main}>

                <Text style={styles.header}> {cafeParam.name} </Text>
                <Text style={styles.info}> Адрес: {cafeParam.address}</Text>
                <Text style={styles.info}> Часы работы: ПН-ВС 8:00 - 22:00</Text>

                <Text style={styles.menu}> меню </Text>

                <View style={styles.products}>
                    <ScrollView style={styles.scroll}>
                        {menu && menu.map((item) => (
                        <ProductInMenu name={item.name} menuItemId={item.id} price={item.price} size={item.sizeInMl}/>
                        ))}
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
