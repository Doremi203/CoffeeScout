import React, {useContext, useEffect, useState} from 'react';
import {
    Image,
    StyleSheet,
    Text,
    TouchableWithoutFeedback,
    View
} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import Icon from "react-native-vector-icons/FontAwesome";
import {Context} from "../index";

export default function ProductCard({menuItemId, name, price, size, cafe}) {

    const {loc} = useContext(Context);

    const urlYandex = loc.getUrlYandex(cafe.location.latitude, cafe.location.longitude)._j;
    const urlGoogle = loc.getUrlGoogle(cafe.location.latitude, cafe.location.longitude)._j;

    const onPress = () => {
        loc.openMap(urlYandex, urlGoogle)
    }

    const {product} = useContext(Context);

    const [isLiked, setIsLiked] = useState(false);

    useEffect(() => {
        const fetchLiked = async () => {
            const liked = await product.isProductLiked(menuItemId);
            setIsLiked(liked);
        };

        fetchLiked();
    }, []);

    const like = async () => {
        console.log(menuItemId)
        if (isLiked) {
            await product.dislikeMenuItem(menuItemId);
        } else {
            await product.likeProduct(menuItemId);
        }
        setIsLiked(!isLiked);
    }

    const {cart} = useContext(Context);

    const addToCart = () => {
        cart.addProductToCart(menuItemId, name, price);
    }

    return (
        <View style={styles.square}>
            <Text style={styles.label}> {name}</Text>
            <Text style={styles.place}> {cafe.name}</Text>

            <View style={styles.priceAndSize}>
                <Text style={styles.size}> {size} мл - </Text>
                <Text style={styles.price}> {price}₽ </Text>
            </View>

            <TouchableWithoutFeedback onPress={like}>
                <Icon name={isLiked ? 'heart' : 'heart-o'} size={RFValue(30)} color="#000" style={styles.heart}/>
            </TouchableWithoutFeedback>

            <Image source={require('../assets/icons/coffee.png')} style={styles.image}/>

            <View style={styles.buttons}>
                <View style={styles.routeButton}>
                    <TouchableWithoutFeedback onPress={onPress}>
                        <Text style={styles.routeText}> МАРШРУТ </Text>
                    </TouchableWithoutFeedback>
                </View>

                <View style={styles.button}>
                    <TouchableWithoutFeedback onPress={addToCart}>
                        <Text style={styles.buttonText}> в корзину </Text>
                    </TouchableWithoutFeedback>
                </View>
            </View>
        </View>
    );
};


const styles = StyleSheet.create({
    square: {
        marginVertical: 10,
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
    },

    routeButton: {
        borderColor: 'black',
        borderWidth: 1,
        width: RFValue(140),
        height: RFValue(35),
        borderRadius: 20,

    },
    routeText: {
        fontSize: RFValue(13),
        fontFamily: 'MontserratAlternates',
        textAlign: 'center',
        marginTop: RFValue(8),
    },
    image: {
        top: RFValue(-80),
        width: RFValue(90),
        height: RFValue(90),
        left: '60%'
    },
    label: {
        fontSize: RFPercentage(2.3),
        color: 'black',
        fontFamily: 'MontserratAlternatesMedium',
        marginTop: '2%'
    },

    priceAndSize: {
        flexDirection: 'row',
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
        marginLeft: '2%'
    },

    heart: {
        left: '88%',
        top: RFValue(-70),
    },

    button: {
        backgroundColor: '#169366',
        height: RFValue(35),
        alignItems: 'center',
        justifyContent: 'center',
        borderRadius: 20,
        elevation: 3,
        width: RFValue(140),
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
