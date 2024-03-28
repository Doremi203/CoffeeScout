import React, {useEffect, useState} from 'react';
import {
    ActionSheetIOS,
    Alert,
    Image,
    Linking, Platform,
    StyleSheet,
    Text,
    TouchableWithoutFeedback,
    View
} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import * as Location from 'expo-location';

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

export default function CoffeeShopCard({navigation}) {

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

    return (
        <View style={styles.square}>
            <Image source={require('../assets/icons/coffee.png')} style={styles.image}/>
            <Text style={styles.label}>One Price Coffee</Text>

            <TouchableWithoutFeedback onPress={onPress}>
                <View style={styles.routeButton}>
                    <Text style={styles.routeText}> МАРШРУТ </Text>
                </View>
            </TouchableWithoutFeedback>


        </View>
    );
};


const styles = StyleSheet.create({
    container: {},
    square: {
        marginVertical: 10,
        flexDirection: 'row',
        width: RFValue(300),
        height: RFValue(115),
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
        width: RFValue(150),
        height: RFValue(40),
        borderRadius: 20,
        top: '20%',
        right: RFValue(130),

    },
    routeText: {
        fontSize: RFValue(13),
        marginTop: RFValue(11),
        marginLeft: RFValue(32),
        fontFamily: 'MontserratAlternates',
    },
    image: {
        marginLeft: 20,
        marginTop: 10,
        width: RFValue(90),
        height: RFValue(90)
    },
    label: {
        fontSize: RFPercentage(2.3),
        color: 'black',
        fontFamily: 'MontserratAlternates',
        marginTop: 20,
        marginLeft: 20
    },

});
