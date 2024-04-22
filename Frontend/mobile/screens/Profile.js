import React, {useContext, useEffect, useState} from 'react';
import {StyleSheet, Text, TouchableWithoutFeedback, View, Image, ScrollView} from 'react-native';
import Footer from "./Footer";
import {RFValue} from "react-native-responsive-fontsize";
import OrderComponent from "../components/OrderComponent";
import {Context} from "../index";


export default function Profile({navigation}) {

    const {user} = useContext(Context);

    const [name, setName] = useState("")
    const [email, setEmail] = useState("")

    useEffect(() => {
        return navigation.addListener('focus', async () => {
            const name = await user.getName();
            setName(name);
            const email = await user.getEmail();
            setEmail(email);
        });

    }, [navigation]);


    const {order} = useContext(Context)

    const [orders1, setOrders1] = useState([])
    const [orders2, setOrders2] = useState([])
    const [orders3, setOrders3] = useState([])
    const [orders4, setOrders4] = useState([])

    useEffect(() => {
        const fetchOrders = async () => {
            const orderss1 = await order.getOrdersPending()
            setOrders1(orderss1)
            const orderss2 = await order.getOrdersInProgress()
            setOrders2(orderss2)
            const orderss3 = await order.getOrdersCompleted()
            setOrders3(orderss3)
            const orderss4 = await order.getOrdersCancelled()
            setOrders4(orderss4)
        }

        fetchOrders()
    }, []);


    return (
        <View style={styles.container}>
            <View style={styles.main}>

                <TouchableWithoutFeedback onPress={() => navigation.navigate('settings')}>
                    <View style={styles.profile}>
                        <Image source={require('../assets/icons/user.png')} style={styles.image}/>
                        <Text style={styles.name}>{name}</Text>
                        <Text style={styles.email}>{email}</Text>
                        <Image source={require('../assets/icons/right-arrow.png')} style={styles.arrow}/>
                    </View>
                </TouchableWithoutFeedback>

                <Text style={styles.historyHeader}> История заказов </Text>

                {orders1.length === 0 && orders2.length === 0 && orders3.length === 0 && orders4.length === 0 ? (
                    <View>
                        <Text style={styles.noorders}> у вас пока нет ни одного заказа :( </Text>
                    </View>
                ) : (
                    <ScrollView style={styles.scroll}>
                        {orders1 && orders1.map((order) => (
                            <OrderComponent navigation={navigation} status={order.status} number={order.id}
                                            items={order.orderItems} key={order.id} date={order.date}
                                            cafe={order.cafe}/>
                        ))}
                        {orders2 && orders2.map((order) => (
                            <OrderComponent navigation={navigation} status={order.status} number={order.id}
                                            items={order.orderItems} key={order.id} date={order.date}
                                            cafe={order.cafe}/>
                        ))}
                        {orders3 && orders3.map((order) => (
                            <OrderComponent navigation={navigation} status={order.status} number={order.id}
                                            items={order.orderItems} key={order.id} date={order.date}
                                            cafe={order.cafe}/>
                        ))}
                        {orders4 && orders4.map((order) => (
                            <OrderComponent navigation={navigation} status={order.status} number={order.id}
                                            items={order.orderItems} key={order.id} date={order.date}
                                            cafe={order.cafe}/>
                        ))}

                    </ScrollView>
                )}
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
    profile: {
        backgroundColor: '#E2E9E6',
        width: '90%',
        height: RFValue(100),
        top: '10%',
        borderRadius: 15,
    },

    image: {
        width: RFValue(70),
        height: RFValue(70),
        marginTop: RFValue(17),
        marginLeft: RFValue(15),
    },

    name: {
        fontSize: RFValue(22),
        fontFamily: 'MontserratAlternatesMedium',
        marginTop: RFValue(-60),
        marginLeft: RFValue(90),
    },
    email: {
        marginTop: RFValue(7),
        marginLeft: RFValue(91),
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(12),
    },
    arrow: {
        width: RFValue(15),
        height: RFValue(15),
        marginTop: RFValue(-35),
        right: RFValue(-290)
    },
    historyHeader: {
        fontSize: RFValue(20),
        fontFamily: 'MontserratAlternates',
        marginTop: '30%',
        right: '16%',
    },
    noorders : {
        fontFamily: 'MontserratAlternates',
        textAlign:'center',
        fontSize: RFValue(15),
        marginTop: RFValue(15)
    }

});