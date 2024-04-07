import {Image, StyleSheet, Text, TouchableWithoutFeedback, View} from "react-native";
import Icon from "react-native-vector-icons/FontAwesome";
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import React, {useContext, useState} from "react";
import {Context} from "../index";


export default function ProductInMenu({name, menuItemId}) {

    const {product} = useContext(Context);

    const [isLiked, setIsLiked] = useState(false);

    const like = async() => {
        //toggleLike();
        //const newLikeStatus = !isLiked;
        //setIsLiked(newLikeStatus);
        await product.likeProduct(menuItemId);
        //saveLikeStatus(newLikeStatus);
    }

    const {cart} = useContext(Context);

    const addToCart = () => {
        cart.addProductToCart(menuItemId, name);
    }

    console.log(cart.cart)
    return(
        <View style={styles.square}>
            <Text style={styles.label}> {name}</Text>

            <View style={styles.priceAndSize}>
                <Text style={styles.size}> 400 мл - </Text>
                <Text style={styles.price}> 230₽ </Text>
            </View>


            <TouchableWithoutFeedback onPress={like}>
                <Icon name={isLiked ? 'heart' : 'heart-o'} size={RFValue(30)} color="#000" style={styles.heart}/>
            </TouchableWithoutFeedback>


            <Image source={require('../assets/icons/coffee.png')} style={styles.image}/>


            <View style={styles.buttons}>

                <View style={styles.button}>
                    <TouchableWithoutFeedback onPress={addToCart}>
                        <Text style={styles.buttonText}> в корзину </Text>
                    </TouchableWithoutFeedback>
                </View>


            </View>


        </View>
    );
}



const styles = StyleSheet.create({
    container: {},
    square: {
        //flex : 1,
        marginVertical: 10,
        //flexDirection: 'row',
        width: RFValue(300),
        height: RFValue(150),
        backgroundColor: 'white',
        borderRadius: 6,
        shadowColor: 'grey',
        shadowOffset: {
            width: 2,
            height: 1,
        },
        shadowOpacity: 0.6,
        shadowRadius: 3.84,
        elevation: 5,
        //overflow: 'hidden'
    },

    routeButton: {
        borderColor: 'black',
        borderWidth: 1,
        width: RFValue(140),
        height: RFValue(35),
        borderRadius: 20,

        //top: '50%',
        // right: RFValue(350),
        //top : RFValue(-110),
        //  marginLeft: '2%'

    },
    routeText: {
        fontSize: RFValue(13),
        // marginTop: RFValue(5),
        //marginLeft: RFValue(28),
        fontFamily: 'MontserratAlternates',
        textAlign: 'center',
        marginTop: RFValue(8),
    },
    image: {
        // marginLeft: 20,
        //marginTop: 10,
        top: RFValue(-40),
        width: RFValue(90),
        height: RFValue(90),
        left: '60%'
        // right: RFValue(90)
    },
    label: {
        fontSize: RFPercentage(2.3),
        color: 'black',
        fontFamily: 'MontserratAlternatesMedium',
        marginTop: '2%'
        //marginTop: 20,
        //marginLeft: 20
    },

    priceAndSize: {
        //position: 'relative',
        flexDirection: 'row',
        //right: '50%',
        marginTop: '5%',
        marginLeft: '2%'
    },

    price: {
        fontFamily: 'MontserratAlternatesSemiBold',
        fontSize: RFValue(15),

    },
    size: {
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(15)
    },
    place: {
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(10),
        // marginTop: '2%',
        marginLeft: '2%'
    },

    heart: {

        left: '88%',
        top: RFValue(-50),
    },

    button: {
        backgroundColor: '#169366',
        height: RFValue(35),
        alignItems: 'center',
        justifyContent: 'center',
        borderRadius: 20,
        elevation: 3,
        // marginRight: 10,
        //marginLeft: 10,
        //backgroundColor: 'red',
        //height: 50,
        width: RFValue(140),
        //top : RFValue(-110),
        //  marginLeft: '2%',
        // marginTop: '3%'
        marginLeft: RFValue(20)
    },
    buttonText: {
        fontSize: RFValue(15),
        color: 'white',
        fontFamily: 'MontserratAlternates',
    },

    buttons: {
        flexDirection: 'row',
        top: RFValue(-80),
        justifyContent: 'space-between',
    }

});

