import React from 'react';
import {Dimensions, Image, StyleSheet, Text, TouchableWithoutFeedback, View} from 'react-native';
import {RFPercentage, RFValue} from 'react-native-responsive-fontsize';
import {Callout, Marker} from "react-native-maps";
import Icon from "react-native-vector-icons/FontAwesome";


export default function MarkerMap({index, marker}) {
    return (
            <Marker key={index} coordinate={marker}>
                <Callout>
                    <View style={styles.container}>

                        <View style={styles.head}>
                            <Text style={styles.header}>{marker.name}</Text>

                            <Icon name="info-circle" size={RFValue(20)} color="#05704A" style={styles.info}/>
                        </View>


                        <Text style={styles.h2}> Часы работы </Text>
                        <Text style={styles.text}> ПН-ВС: 8:00-22:00</Text>

                        <Text style={styles.h2}> Адрес </Text>
                        <Text style={styles.text}> ул.Виницкая, 8к4 </Text>



                    </View>
                </Callout>
            </Marker>

    );
};

const styles = StyleSheet.create({
    container: {
        //height: RFValue(100),
        height: RFValue(100),
        width : RFValue(150),
        flexWrap: 'wrap',
        flexDirection: 'column',
    },

    head : {
        flexDirection: 'row',
    },


    header : {
        fontSize: RFValue(13),
        fontFamily: 'MontserratAlternatesSemiBold',

    },
    h2: {
        fontSize: RFValue(10),
        fontFamily: 'MontserratAlternatesMedium',
        //color: 'grey',
        paddingTop: RFValue(5)
    },

    text : {
        fontSize: RFValue(10),
        fontFamily: 'MontserratAlternates',
       // marginTop: RFValue(10)
        //color: 'grey',
    },

    info : {
        width: RFValue(20),
        height: RFValue(20),
       // marginTop: RFValue(-30),
        //marginRight: '5%'
        //marginRight: '2%'
        //paddingRight: RFValue(1)
        marginLeft: RFValue(20)
    }


});

//export default Product;
