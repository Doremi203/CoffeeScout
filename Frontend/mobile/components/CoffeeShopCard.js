import React, {useContext, useEffect, useState} from 'react';
import {
    ActionSheetIOS,
    Alert,
    Image,
    Linking, Platform,
    StyleSheet,
    Text, TouchableOpacity,
    TouchableWithoutFeedback,
    View
} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import * as Location from 'expo-location';
import Icon from "react-native-vector-icons/FontAwesome";
import {likeProduct} from "../http/productApi";
import {Context} from "../index";
import FontAwesome from "react-native-vector-icons/FontAwesome";
import FontAwesome5 from "react-native-vector-icons/FontAwesome5";
import {FontAwesomeIcon} from "@fortawesome/react-native-fontawesome";

import {faHeart} from '@fortawesome/free-regular-svg-icons';
import {faHeart as solidHeart} from '@fortawesome/free-solid-svg-icons';
import * as AsyncStorage from "expo-secure-store";


const getUrl = () => {
    const [location, setLocation] = useState(null);
    const [url, setUrl] = useState("");

    useEffect(() => {
        (async () => {
            let {status} = await Location.requestForegroundPermissionsAsync();
            if (status !== 'granted') {
                Alert.alert('Permission to access location was denied');
                return;
            }

            let location = await Location.getCurrentPositionAsync({});
            setLocation(location);

            console.log(location.coords.longitude)
            console.log(location.coords.latitude)

            if (location) {
                setUrl("https://yandex.ru/maps/213/moscow/?indoorLevel=1&ll=" + location.coords.longitude + "%2C" + location.coords.latitude + "&mode=routes&rtext=55.697182%2C37.500930~55.698969%2C37.499201&rtt=pd&ruri=~&z=17");
            }
        })();
    }, []);

    console.log(url)

    return url
};


//const supportedURL = "https://www.google.com/maps/dir/?api=1&destination=55.697695,37.497517\n";
//const supportedURL = "https://yandex.ru/maps/org/1723336082\n";
const supportedURL = "https://yandex.ru/maps/?mode=routes&rtext=~55.698969%2C37.499201&rtt=pd\n\n";


//<OpenURLButton url={url}></OpenURLButton>

export default function CoffeeShopCard({navigation, menuItemId, name, price}) {

    const urlYandex = getUrl();
    const urlGoogle = "https://www.google.com/maps/dir/?api=1&destination=55.697695,37.497517\n";


    const onPress = () => {
        if (Platform.OS === 'ios') {
            ActionSheetIOS.showActionSheetWithOptions(
                {
                    options: ['Cancel', 'Яндекс карты', 'Google maps'],
                    // destructiveButtonIndex: 2,
                    cancelButtonIndex: 0,
                    userInterfaceStyle: 'dark',
                },
                async buttonIndex => {
                    if (buttonIndex === 0) {
                        // cancel action
                    } else if (buttonIndex === 1) {
                        const supported = await Linking.canOpenURL(urlYandex);
                        if (supported) {
                            await Linking.openURL(urlYandex);
                        } else {
                            Alert.alert("Dont know how to open this URL:");
                        }
                    } else if (buttonIndex === 2) {
                        const supported = await Linking.canOpenURL(urlGoogle);
                        if (supported) {
                            await Linking.openURL(urlGoogle);
                        } else {
                            Alert.alert("Dont know how to open this URL:");
                        }
                    }
                },
            );
        } else {

            Alert.alert(
                'Choose Map App',
                'Which map application would you like to use?',
                [
                    {
                        text: 'Cancel',
                        style: 'cancel',
                    },
                    {
                        text: 'Yandex Maps',
                        onPress: async () => {
                            const supported = await Linking.canOpenURL(urlYandex);
                            if (supported) {
                                await Linking.openURL(urlYandex);
                            } else {
                                Alert.alert("Don't know how to open this URL:");
                            }
                        },
                    },
                    {
                        text: 'Google Maps',
                        onPress: async () => {
                            const supported = await Linking.canOpenURL(urlGoogle);
                            if (supported) {
                                await Linking.openURL(urlGoogle);
                            } else {
                                Alert.alert("Don't know how to open this URL:");
                            }
                        },
                    },
                ],
                {cancelable: false}
            );
        }
    }

    const {product} = useContext(Context);

    const [isLiked, setIsLiked] = useState(false);

    const like = async() => {
        //toggleLike();
        //const newLikeStatus = !isLiked;
        //setIsLiked(newLikeStatus);
        await product.likeProduct(menuItemId);
        //saveLikeStatus(newLikeStatus);
    }

   /* useEffect(() => {
        // Загрузка состояния лайка для данного продукта из AsyncStorage при монтировании компонента
        loadLikeStatus().then();
    }, []);

    const loadLikeStatus = async () => {
        try {
            // Получаем сохраненное состояние лайка для данного продукта из AsyncStorage
            const savedLikeStatus = AsyncStorage.getItem(`likeStatus_${menuItemId}`);
            if (savedLikeStatus !== null) {
                // Устанавливаем состояние лайка для данного продукта из AsyncStorage
                setIsLiked(savedLikeStatus === 'true');
            }
        } catch (error) {
            console.error('Ошибка загрузки состояния лайка:', error);
        }
    };

    const saveLikeStatus = async (status) => {
        try {
            // Сохраняем состояние лайка для данного продукта в AsyncStorage
            await AsyncStorage.setItem(`likeStatus_${menuItemId}`, status.toString());
        } catch (error) {
            console.error('Ошибка сохранения состояния лайка:', error);
        }
    };

    const toggleLike = () => {
        // Инвертируем состояние лайка
        const newLikeStatus = !isLiked;
        setIsLiked(newLikeStatus);
        // Сохраняем новое состояние лайка для данного продукта
        saveLikeStatus(newLikeStatus);
    };*/







    const {cart} = useContext(Context);

    const addToCart = () => {
        cart.addProductToCart(menuItemId, name, price);
    }

    console.log(cart.cart)




    return (


        <View style={styles.square}>
            <Text style={styles.label}> {name}</Text>
            <Text style={styles.place}> One Price Coffee</Text>

            <View style={styles.priceAndSize}>
                <Text style={styles.size}> 400 мл - </Text>
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

// <FontAwesome name='heart' size={30} color='#169366' style={styles.searchIcon} />

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
        top: RFValue(-80),
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
        top: RFValue(-70),
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
