/*import React, {useEffect, useState} from 'react';
import MapView, {Callout, Marker, PROVIDER_GOOGLE} from 'react-native-maps';
import {StyleSheet, TextInput, View, Text} from 'react-native';
import Footer from "./Footer";
import MapViewDirections from "react-native-maps-directions";
import {Polyline} from "react-native-maps";
import * as Location from 'expo-location';

export default function MapScreen({navigation}) {
    const markers = [
        {
            latitude: 55.697695,
            longitude: 37.497517,
            latitudeDelta: 0.0922,
            longitudeDelta: 0.0421,
            //name : 'first'
        },
        {}
    ]


    return (
        <View style={styles.container}>
            <View style={styles.main}>
                <MapView style={styles.map} initialRegion={{
                    latitude: 55.7522,
                    longitude: 37.6156,
                    latitudeDelta: 0.0922,
                    longitudeDelta: 0.0421,
                }}
                         provider={PROVIDER_GOOGLE}
                         showsUserLocation>
                    {markers.map((marker, index) => (
                        <Marker key={index} coordinate={marker}>
                            <Callout>
                                <View>
                                    <Text>One Price Coffee</Text>
                                </View>
                            </Callout>
                        </Marker>
                    ))}
                </MapView>
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
    map: {
        width: '100%',
        height: '100%',
    },
});*/


import React, {useEffect, useState} from 'react';
import MapView, {Callout, Marker, PROVIDER_GOOGLE} from 'react-native-maps';
import {Alert, StyleSheet, Text, View} from 'react-native';
import Footer from './Footer';
import * as Location from 'expo-location';
import MarkerMap from "../components/MarkerMap";

export default function MapScreen({ navigation }) {
    const [userLocation, setUserLocation] = useState(null);
    const [errorMsg, setErrorMsg] = useState(null);
    const [markers, setMarkers] = useState([
        {
            latitude: 55.697695,
            longitude: 37.497517,
            latitudeDelta: 0.0922,
            longitudeDelta: 0.0421,
            name: 'One Price Coffee',
        },
        {
            latitude: 55.77695,
            longitude: 37.597517,
            latitudeDelta: 0.0922,
            longitudeDelta: 0.0421,
            name: 'One Price Coffee',
        },
        {
            latitude: 55.597695,
            longitude: 37.397517,
            latitudeDelta: 0.0922,
            longitudeDelta: 0.0421,
            name: 'One Price Coffee',
        },
        {
            latitude: 55.67695,
            longitude: 37.397517,
            latitudeDelta: 0.0922,
            longitudeDelta: 0.0421,
            name: 'One Price Coffee',
        },
        {
            latitude: 55.699762,
            longitude: 37.507090,
            latitudeDelta: 0.0922,
            longitudeDelta: 0.0421,
            name: 'One Price Coffee',
        },
        {
            latitude: 55.693642,
            longitude: 37.495993,
            latitudeDelta: 0.0922,
            longitudeDelta: 0.0421,
            name: 'One Price Coffee',
        },
        {
            latitude: 55.677628,
            longitude: 37.506460,
            latitudeDelta: 0.0922,
            longitudeDelta: 0.0421,
            name: 'One Price Coffee',
        },
        {
            latitude: 55.735181,
            longitude: 37.517988,
            latitudeDelta: 0.0922,
            longitudeDelta: 0.0421,
            name: 'One Price Coffee',
        },
        {
            latitude: 55.730651,
            longitude: 37.460493,
            latitudeDelta: 0.0922,
            longitudeDelta: 0.0421,
            name: 'One Price Coffee',
        },




    ]);

    useEffect(() => {
        (async () => {
            let { status } = await Location.requestForegroundPermissionsAsync();
            if (status !== 'granted') {
                setErrorMsg('Permission to access location was denied');
                Alert.alert('Permission to access location was denied');
                return;
            }

            let location = await Location.getCurrentPositionAsync({});
            setUserLocation(location.coords);
        })();
    }, []);

    return (
        <View style={styles.container}>
            <View style={styles.main}>
                {userLocation && (
                    <MapView
                        style={styles.map}
                        initialRegion={{
                            latitude: userLocation.latitude,
                            longitude: userLocation.longitude,
                            latitudeDelta: 0.0922,
                            longitudeDelta: 0.0421,
                        }}
                        provider={PROVIDER_GOOGLE}
                        showsUserLocation>
                        {markers.map((marker, index) => (
                            <MarkerMap index={index} marker={marker}/>
                        ))}
                    </MapView>
                )}
            </View>

            <Footer navigation={navigation} />
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
    map: {
        width: '100%',
        height: '100%',
    },
});

