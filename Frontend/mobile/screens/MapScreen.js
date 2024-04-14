import React, {useContext, useEffect, useState} from 'react';
import MapView, {PROVIDER_GOOGLE} from 'react-native-maps';
import {Alert, StyleSheet, View} from 'react-native';
import Footer from './Footer';
import * as Location from 'expo-location';
import MarkerMap from "../components/MarkerMap";
import {Context} from "../index";

export default function MapScreen({navigation}) {
    const [userLocation, setUserLocation] = useState(null);

    const {cafe} = useContext(Context)
    const {loc} = useContext(Context)


    const [cafes, setCafes] = useState([]);


    useEffect(() => {
        const fetchData = async () => {
            const cafes = await cafe.getNearbyCafes(loc.location.coords.longitude, loc.location.coords.latitude, 2000)
            setCafes(cafes)
        }
        fetchData()
    }, [])


    const [markers, setMarkers] = useState([]);

    useEffect(() => {
        setMarkers(cafes.map(cafe => ({
            latitude: cafe.location.latitude,
            longitude: cafe.location.longitude,
            latitudeDelta: 0.0922,
            longitudeDelta: 0.0421,
            name: cafe.name,
            id: cafe.id
        })));
    }, [cafes]);


    useEffect(() => {
        (async () => {
            let {status} = await Location.requestForegroundPermissionsAsync();
            if (status !== 'granted') {
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
                            <MarkerMap index={index} marker={marker} key={marker.id}/>
                        ))}
                    </MapView>
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
    },
    map: {
        width: '100%',
        height: '100%',
    },
});

