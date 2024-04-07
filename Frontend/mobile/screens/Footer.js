import React from 'react';
import {Pressable, StyleSheet, View} from 'react-native';
import Icon from "react-native-vector-icons/FontAwesome";


export default function Footer({navigation}) {
    return (
        <View style={styles.NavContainer}>
            <View style={styles.NavBar}>

                <Pressable onPress={() => navigation.navigate('main')}>
                    <Icon name="home" size={30} color="#000" style={styles.searchIcon} />
                </Pressable>

                <Pressable onPress={() => navigation.navigate('map')}>
                    <Icon name="map-marker" size={30} color="#000" style={styles.searchIcon} />
                </Pressable>

                <Pressable onPress={() => navigation.navigate('cart')}>
                    <Icon name="shopping-cart" size={30} color="#000" style={styles.searchIcon} />
                </Pressable>

                <Pressable onPress={() => navigation.navigate('profile')}>
                    <Icon name="user" size={30} color="#000" style={styles.searchIcon} />
                </Pressable>



            </View>

        </View>
    );
}


const styles = StyleSheet.create({
   /* footer: {
        backgroundColor: 'red',
        //position: 'absolute',
       // top: 814,
        width: '100%',
        height: 59,
        alignItems: 'center',
        bottom: 20

    },*/
    text: {
        fontSize: 16,
        color: '#333',
    },
   /* container: {
        flex: 1,
        alignItems: 'center',
        justifyContent: 'center'
    },*/
    NavContainer : {
        //position: 'absolute',
        // top: 814,
        //top: 700,
       /* marginBottom: '10%',
        width: '100%',
        height: 59,
        alignItems: 'center',*/
        //bottom: 20
       // flex: 1,
        //justifyContent: 'flex-end',
        width: '100%',
        height: '7%',
        alignItems: 'center',
        //paddingBottom: 10,
        //backgroundColor: 'white',
    },
    NavBar: {
        flexDirection:'row',
        width: '90%',
        justifyContent: 'space-between',
        padding: 10,
        paddingHorizontal:20
    },
    navText: {
        flexDirection:'row',
        width: '90%',
        justifyContent: 'space-between',
        paddingHorizontal: 10
    }
});


