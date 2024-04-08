import React, {useContext, useEffect, useState} from 'react';
import {ScrollView, StyleSheet, Text, TouchableWithoutFeedback, View, Image} from 'react-native';
import Footer from "./Footer";
import ProductInCart from "../components/ProductInCart";
import {RFPercentage, RFValue} from 'react-native-responsive-fontsize';
import {Context} from "../index";


export default function Cart({navigation}) {

    const {cart, order} = useContext(Context);

    const pay = async () => {
        if (cart.cart.length > 0) {
            navigation.navigate('payment')
            const menuItems = cart.getMenuItems()
            console.log(menuItems)
            await order.makeNewOrder(menuItems)
            cart.clearCart()
        }
    }


    const [cartPro, setCartPro] = useState([])

    useEffect(() => {
        return navigation.addListener('focus', async () => {
            const cartt = cart.cart
            setCartPro(cartt)
        });

    }, [navigation]);


    return (
        <View style={styles.container}>

            <View style={styles.main}>
                <Text style={styles.header}>корзина</Text>

                {cartPro.length === 0 ? (
                    <View style={styles.empty}>
                        <Text style={styles.emptyText}> Упс, корзина пуста...</Text>
                        <Image source={require('../assets/icons/emptyCart.png')} style={styles.cart}/>
                    </View>

                ) : (
                    <View>
                        <View style={styles.products}>
                            <ScrollView style={styles.scroll}>

                                {cartPro.map((product) => (
                                    <ProductInCart name={product.name} menuItemId={product.id} price={product.price}/>
                                ))}

                            </ScrollView>
                        </View>

                        <View style={styles.button}>
                            <TouchableWithoutFeedback onPress={pay}>
                                <Text style={styles.buttonText}> к оплате </Text>
                            </TouchableWithoutFeedback>
                        </View>
                    </View>
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
        alignItems: 'center',
    },
    main: {
        flex: 1,
    },
    header: {
        fontSize: RFPercentage(4),
        marginTop: '16%',
        fontFamily: 'MontserratAlternates',
        textAlign: 'center'
    },

    button: {
        backgroundColor: '#169366',
        height: RFValue(45),
        alignItems: 'center',
        justifyContent: 'center',
        borderRadius: 10,
        elevation: 3,
        marginTop: RFValue(20)
    },
    products: {
        height: '80%'
    },
    scroll: {
        height: '80%',

    },
    buttonText: {
        fontSize: RFValue(15),
        color: 'white',
        fontFamily: 'MontserratAlternates',
    },
    emptyText: {
        fontFamily: 'MontserratAlternates',
        fontSize: RFPercentage(3),
        marginTop: '80%'
    },
    cart: {
        width: RFValue(60),
        height: RFValue(60),
    },
    empty: {
        alignItems: 'center',
    }

});

