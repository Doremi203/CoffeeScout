import React, {useRef, useEffect} from 'react';
import {StyleSheet, TextInput, View} from "react-native";
import {RFPercentage} from "react-native-responsive-fontsize";
import Footer from "./Footer";
import Icon from "react-native-vector-icons/FontAwesome";

export default function SearchScreen({navigation}) {
    const searchInputRef = useRef(null);

    useEffect(() => {
        if (searchInputRef.current) {
            searchInputRef.current.focus();
        }
    }, []);

    return (
        <View style={styles.container}>
            <View style={styles.main}>

                <View style={styles.searchBox}>
                    <TextInput
                        ref={searchInputRef}
                        style={styles.input}
                        placeholder="искать"
                    />
                    <Icon name="search" size={20} color="#000" style={styles.searchIcon}/>
                </View>



            </View>
            <Footer navigation={navigation}/>
        </View>
    );
}


/*<View style={styles.searchBox}>
    <TextInput
        ref={searchInputRef}
        style={styles.input}
        placeholder="искать"
    />
</View>*/

const styles = StyleSheet.create({
    container: {
        flexDirection: 'column',
        minHeight: '100%',
    },
    main: {
        flex: 1,
        alignItems: 'center'
    },

    input: {
         flex: 1,
        //height: 40,
        fontFamily: 'MontserratAlternates',
        opacity: 0.5
    },

    searchBox: {
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: 'white',
        borderRadius: 20,
        paddingHorizontal: 10,
        width: '87%',
        height: '6%',
        //width: 349,
        //height : 48,
        //position: 'absolute',
        // top: 132,
        //top: '29%',
        //left: 22,
        marginTop: '15%'
    },

    searchIcon: {
        marginLeft: 10,
    },


});
