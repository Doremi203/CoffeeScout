import React, {useContext, useEffect, useState} from 'react';
import {ScrollView, StyleSheet, Text, TouchableWithoutFeedback, View} from "react-native";
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import Footer from "./Footer";
import ProductInMenu from "../components/ProductInMenu";
import {Context} from "../index";

export default function CafeScreen({navigation, route}) {

    const {cafe} = useContext(Context)

    const {cafeParam} = route.params;

    const {loc} = useContext(Context);
    const urlYandex = loc.getUrlYandex(cafeParam.location.latitude, cafeParam.location.longitude)._j;
    const urlGoogle = loc.getUrlGoogle(cafeParam.location.latitude, cafeParam.location.longitude)._j;

    const map = () => {
        loc.openMap(urlYandex, urlGoogle)
    }

    const [menu, setMenu] = useState([])
    const [info, setInfo] = useState([])
    const [time, setTime] = useState()
    useEffect(() => {
        const fetchMenu = async () => {
            const menuu = await cafe.getMenu(cafeParam.id);
            setMenu(menuu)
            const infoo = await cafe.getInfo(cafeParam.id);
            setInfo(infoo)
            const timee = await cafe.getTime(cafeParam.workingHours);
            setTime(timee)
        }

        fetchMenu();
    }, []);
    

    return (
        <View style={styles.container}>
            <View style={styles.main}>

                <Text style={styles.header}> {info.name} </Text>
                <Text style={styles.info}> Адрес: {info.address}</Text>
                <Text style={styles.info}> Сегодня работает до {time}</Text>

                <Text style={styles.menu}> меню </Text>

                <View style={styles.products}>
                    <ScrollView style={styles.scroll}>
                        {menu && menu.map((item) => (
                            <ProductInMenu name={item.name} menuItemId={item.id} price={item.price}
                                               size={item.sizeInMl} key={item.id} cafeId={cafeParam.id}/>
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
