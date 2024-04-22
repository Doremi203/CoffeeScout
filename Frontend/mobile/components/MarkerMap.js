import React, {useContext, useEffect, useState} from 'react';
import {StyleSheet, Text, View} from 'react-native';
import {RFValue} from 'react-native-responsive-fontsize';
import {Callout, Marker} from "react-native-maps";
import {Context} from "../index";


export default function MarkerMap({index, marker, address, workingHours}) {
    const {cafe} = useContext(Context)
    const [time, setTime] = useState()

    useEffect(() => {
        const fetchTime = async () => {
            const timee = await cafe.getTime(workingHours)
            setTime(timee)
        }

        fetchTime();

    }, []);


    return (
        <Marker key={index} coordinate={marker}>
            <Callout>
                <View style={styles.container}>

                    <View style={styles.head}>
                        <Text style={styles.header}>{marker.name}</Text>
                    </View>

                    <Text style={styles.text}> Работает до {time}</Text>
                    <Text style={styles.h2}> Адрес </Text>
                    <Text style={styles.text}> {address} </Text>

                </View>
            </Callout>
        </Marker>

    );
};

const styles = StyleSheet.create({
    container: {
        flexWrap: 'wrap',
        flexDirection: 'column',
    },
    head: {
        flexDirection: 'row',
    },
    header: {
        fontSize: RFValue(13),
        fontFamily: 'MontserratAlternatesSemiBold',
    },
    h2: {
        fontSize: RFValue(10),
        fontFamily: 'MontserratAlternatesMedium',
        paddingTop: RFValue(5)
    },
    text: {
        fontSize: RFValue(10),
        fontFamily: 'MontserratAlternates',
    },
});

