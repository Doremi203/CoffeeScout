import {makeAutoObservable} from "mobx";
import {ActionSheetIOS, Alert, Linking, Platform} from "react-native";
import * as Location from "expo-location";
import {useEffect, useState} from "react";

export default class LocationStore {

    constructor() {
        makeAutoObservable(this);
    }

    location = {coords: {longitude: 0, latitude: 0}};

    async setLocation() {
        let {status} = await Location.requestForegroundPermissionsAsync();
        if (status !== 'granted') {
            Alert.alert('Permission to access location was denied');
            return;
        }

        let location = await Location.getCurrentPositionAsync({});
        console.log(location.coords.longitude);
        console.log(location.coords.latitude);
        this.location = location;
    }

    async getLocation() {
        return {latitude: this.location.coords.latitude, longitude: this.location.coords.longitude};
    }

    async getUrlYandex(latitude, longitude) {
        const [url, setUrl] = useState("");

        useEffect(() => {
            (async () => {
                let {status} = await Location.requestForegroundPermissionsAsync();
                if (status !== 'granted') {
                    Alert.alert('Permission to access location was denied');
                    return;
                }

                const coordinates = [
                    [this.location.latitude, this.location.longitude],
                    [latitude, longitude],
                ];


                let minLat = Number.POSITIVE_INFINITY;
                let minLon = Number.POSITIVE_INFINITY;
                let maxLat = Number.NEGATIVE_INFINITY;
                let maxLon = Number.NEGATIVE_INFINITY;


                coordinates.forEach(function (coordinates) {
                    minLat = Math.min(minLat, coordinates[0]);
                    minLon = Math.min(minLon, coordinates[1]);
                    maxLat = Math.max(maxLat, coordinates[0]);
                    maxLon = Math.max(maxLon, coordinates[1]);
                });


                const centerLat = (minLat + maxLat) / 2;
                const centerLon = (minLon + maxLon) / 2;


                if (this.location) {
                    setUrl("https://yandex.ru/maps/213/moscow/?indoorLevel=1&ll=" + centerLat + "%2C" + centerLon + "&mode=routes&rtext=" + latitude + "%2C" + longitude + "~" + this.location.coords.latitude + "%2" + this.location.coords.longitude + "&rtt=pd&ruri=~&z=17");
                }
            })();
        }, []);

        return url
    };

    async getUrlGoogle(latitude, longitude) {
        return "https://www.google.com/maps/dir/?api=1&destination=" + latitude + "," + longitude;
    }


    openMap(urlYandex, urlGoogle) {
        if (Platform.OS === 'ios') {
            ActionSheetIOS.showActionSheetWithOptions(
                {
                    options: ['Cancel', 'Яндекс карты', 'Google maps'],
                    cancelButtonIndex: 0,
                    userInterfaceStyle: 'dark',
                },
                async buttonIndex => {
                    if (buttonIndex === 1) {
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
}