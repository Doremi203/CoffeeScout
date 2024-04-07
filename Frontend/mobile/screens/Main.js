import React, {useContext, useEffect, useState} from 'react';
import {Dimensions, ScrollView, StyleSheet, Text, TouchableOpacity, View} from 'react-native';
import Icon from 'react-native-vector-icons/FontAwesome';
import Product from "../components/Product";
import Footer from "./Footer";
import {RFPercentage} from 'react-native-responsive-fontsize';
import {Context} from "../index";
import CoffeeShop from "../components/CoffeeShop";
import * as SecureStorage from "expo-secure-store";


const windowWidth = Dimensions.get('window').width;
const windowHeight = Dimensions.get('window').height;

export default function Main({navigation}) {


    const {product} = useContext(Context);
    const {cafe} = useContext(Context);
    const {user} = useContext(Context);

    const [favTypes, setFavTypes] = useState([]);

    useEffect(() => {

        return navigation.addListener('focus', async () => {
            const types = await product.getBeverageTypes();
            setFavTypes(types);
        });

    }, [navigation]);



    const [location, setLocation] = useState(null);
    user.getLocation().then(location => {
        setLocation(location);
        console.log('LOCATION')
        console.log(location.coords.longitude, location.coords.latitude);
    });




    const [cafes, setCafes] = useState([]);

    useEffect( async () => {

        async function fetchCafes() {
            const cafes = await cafe.getNearbyCafes(location.coords.longitude, location.coords.latitude, 10000);
            setCafes(cafes);
        }

        await fetchCafes();

    }, [location]);



    console.log('CAFES')
    console.log(cafes)






    const name = SecureStorage.getItem('username');
    console.log(name)

    return (

        <View style={styles.container}>
            <View style={styles.main}>
                <View style={styles.head}>
                    <Text style={[styles.text, {left: '5.5%'}]}> привет, {name}! </Text>
                    <Text style={[styles.text, {marginTop: '2%', left: '5.5%', opacity: 0.62}]}> что хотите выпить
                        сегодня? </Text>
                    <TouchableOpacity onPress={() => navigation.navigate('searchScreen')}>
                        <View style={styles.searchBox}>
                            <View>
                                <Text style={styles.input}> искать </Text>
                            </View>
                            <Icon name="search" size={20} color="#000" style={styles.searchIcon}/>
                        </View>
                    </TouchableOpacity>
                </View>

                <View style={styles.body}>
                    <Text style={[styles.header, {}]}> популярно сейчас</Text>

                    <ScrollView horizontal={true} style={styles.scrollCont}>
                        <View style={styles.pro}>
                            <Product name={'Капучино'} navigation={navigation}/>
                            <Product name={'Латте'} navigation={navigation}/>
                            <Product name={'Раф'} navigation={navigation}/>
                            <Product name={'Американо'} navigation={navigation}/>

                        </View>
                    </ScrollView>


                    <Text style={[styles.header, {top: '1%'}]}> кофейни поблизости</Text>
                    <ScrollView horizontal={true} style={styles.scrollCont2}>
                        <View style={styles.pro2}>
                            {cafes && cafes.map((cafe) => (
                                <CoffeeShop navigation={navigation} name={cafe.name}/>
                            ))}
                        </View>
                    </ScrollView>

                    <Text style={[styles.header, {top: '1%'}]}> любимое </Text>

                    <ScrollView horizontal={true} style={styles.scrollCont3}>
                        <View style={styles.pro3}>
                            {favTypes && favTypes.map((type) => (
                                <Product key={type.id} id={type.id} name={type.name} navigation={navigation}/>
                            ))}

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
    main: {
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
        height: '53%',
        //width: 349,
        //height : 48,
        //position: 'absolute',
        // top: 132,
        //top: '29%',
        left: 22,
        marginTop: '2%'
    },
    input: {
       // flex: 1,
        //height: 40,
        fontFamily: 'MontserratAlternates',
        opacity: 0.5
    },
    searchIcon: {
        marginLeft: '75%',
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
        height: '20%',
        marginTop: '2%'
    },
    scrollCont2: {
        height: '20%',
        marginTop: '2%'
    },
    scrollCont3: {
        height: '20%',
        marginTop: '3%',
    },
    pro: {
        flex: 1,
        justifyContent: 'space-between',
        flexDirection: 'row',
        marginLeft: 20,
    },
    pro2 : {
        flex: 1,
        justifyContent: 'space-between',
        flexDirection: 'row',
        marginLeft: 20,
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
        top: '12%',
        // backgroundColor: 'purple'
    },
    body: {
        // backgroundColor:'yellow',
        marginTop: '18%'
    }

});
