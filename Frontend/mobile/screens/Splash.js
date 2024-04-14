import React from 'react';
import {StyleSheet, View, Image} from 'react-native';


export default function Splash() {

    return (
        <View style={styles.container}>
            <Image source={require('../assets/icons/splash2.jpg')} style={styles.image}/>
        </View>
    );
}


const styles = StyleSheet.create({
    image: {
        width: '100%',
        height: '100%'
    }
});