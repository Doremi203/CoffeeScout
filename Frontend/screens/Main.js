import React from 'react';
import {Dimensions, ScrollView, StyleSheet, Text, TextInput, View} from 'react-native';
import Icon from 'react-native-vector-icons/FontAwesome';
import Product from "../components/Product";
import Footer from "./Footer";
import {RFPercentage} from 'react-native-responsive-fontsize';

const windowWidth = Dimensions.get('window').width;
const windowHeight = Dimensions.get('window').height;

export default function Main({navigation}) {
    return (
        <View style={styles.container}>
            <View style={styles.main}>
                <View style={styles.head}>
                    <Text style={[styles.text, {left: '5.5%'}]}> привет, Майя! </Text>
                    <Text style={[styles.text, {marginTop: '2%', left: '5.5%', opacity: 0.62}]}> что хотите выпить сегодня? </Text>
                    <View style={styles.searchBox}>
                        <TextInput
                            style={styles.input}
                            placeholder="искать"
                        />
                        <Icon name="search" size={20} color="#000" style={styles.searchIcon}/>
                    </View>
                </View>

                <View style={styles.body}>
                    <Text style={[styles.header, {}]}> популярно сейчас</Text>

                    <ScrollView horizontal={true} style={styles.scrollCont}>
                        <View style={styles.pro}>
                            <Product navigation={navigation}/>
                            <Product navigation={navigation}/>
                            <Product navigation={navigation}/>
                            <Product navigation={navigation}/>
                        </View>
                    </ScrollView>


                    <Text style={[styles.header, {top: '1%'}]}> кофейни поблизости</Text>
                    <ScrollView horizontal={true} style={styles.scrollCont2}>

                    </ScrollView>

                    <Text style={[styles.header, {top: '1%'}]}> вы недавно заказывали</Text>

                    <ScrollView horizontal={true} style={styles.scrollCont3}>
                        <View style={styles.pro3}>
                            <Product navigation={navigation}/>
                            <Product navigation={navigation}/>
                            <Product navigation={navigation}/>
                            <Product navigation={navigation}/>
                        </View>
                    </ScrollView>
                </View>
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
    main : {
        flex: 1,
    },
    name: {
        fontSize: RFPercentage(5),
        //fontSize: RFValue(36, windowHeight),
        //position: 'absolute',
        top: 84,
        left: 38,
    },
    /*line: {
        // position: 'absolute',
        left: 0,
        right: 0,
        borderBottomWidth: 1,
        borderBottomColor: 'black'
    },*/
    text: {
        //fontSize: RFValue(20, windowHeight),
        fontSize: RFPercentage(2.5),
        //fontSize: 20,
        // position: 'absolute',
        fontFamily: 'MontserratAlternates',
    },
    /*out: {
        fontSize: 20,
        color: '#05704A',
        position:'absolute',
        top: 365,
        left: 148,
        fontWeight: 'bold'
    },*/
    searchBox: {
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: 'white',
        borderRadius: 20,
        paddingHorizontal: 10,
        width: '87%',
        height: '29%',
        //width: 349,
        //height : 48,
        //position: 'absolute',
        // top: 132,
        //top: '29%',
        left: 22,
        marginTop: '2%'
    },
    input: {
        flex: 1,
        height: 40,
        fontFamily: 'MontserratAlternates',
    },
    searchIcon: {
        marginLeft: 10,
    },

    header: {
        // position: 'absolute',
        left: 25,
        fontFamily: 'MontserratAlternatesSemiBold',
        fontSize: RFPercentage(2),
    },
    box: {
        width: 128,
        height: 128,
        backgroundColor: '#169366',
        borderRadius: 6,
        //position: 'absolute'
    },
    /*product: {
        //position: 'absolute',
        top: 250,
        left: 50
    },*/
    scrollCont: {
        //top:'25%' ,
        //top: 200,
       // height: '10%',
        //marginTop: '60%',
        height: '20%',
       // top: '37%',
        //backgroundColor: 'blue'
        marginTop:'2%'
    },
    scrollCont2: {
        //backgroundColor: 'green',
        height: '20%',
        marginTop: '2%'
    },
    scrollCont3: {
        height: '20%',
       // top: '45%',
        //top: 400,
       // height: 180,
        //backgroundColor: 'pink',
        marginTop: '3%'
    },
    pro: {
        flex: 1,
        justifyContent: 'space-between',
        flexDirection: 'row',
        marginLeft: 20,
        // top: '25%'
       // backgroundColor:'red',
       // height: '50%'

    },
    pro3: {
        flex: 1,
        justifyContent: 'space-between',
        flexDirection: 'row',
        marginLeft: 20,
        left: 0
    },

    head: {
        height: '18%',
        top:'12%',
       // backgroundColor: 'purple'
    },
    body: {
       // backgroundColor:'yellow',
        marginTop: '18%'
    }

});
