import React, {useContext, useEffect, useState} from 'react';
import {ScrollView, StyleSheet, Text, TouchableOpacity, View} from 'react-native';
import Icon from 'react-native-vector-icons/FontAwesome';
import Product from "../components/Product";
import Footer from "./Footer";
import {RFPercentage, RFValue} from 'react-native-responsive-fontsize';
import {Context} from "../index";
import Cafe from "../components/Cafe";

export default function Main({navigation}) {

    const {product} = useContext(Context);
    const {cafe} = useContext(Context);
    const {user} = useContext(Context);

    const [favTypes, setFavTypes] = useState([]);
    const [types, setTypes] = useState([]);
    const [name, setName] = useState("")

    useEffect(() => {

        return navigation.addListener('focus', async () => {
            const typesFav = await product.getFavoredBeverageTypes();
            setFavTypes(typesFav);
            const name = await user.getName();
            setName(name);
            const typess = await product.getTypes();
            setTypes(typess)
        });

    }, [navigation]);


    const {loc} = useContext(Context);
    const location = loc.getLocation();


    const [cafes, setCafes] = useState([]);
    const [executed, setExecuted] = useState(false);

    useEffect(() => {
        const fetchCafes = async () => {
            if (!executed && location._j.latitude !== 0) {
                const cafes = await cafe.getNearbyCafes(location._j.longitude, location._j.latitude, 2000);
                setCafes(cafes);
                setExecuted(true);
            }
        };

        fetchCafes();

        return () => {
        };
    }, [location, executed]);


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
                    <Text style={[styles.header, {}]}> типы напитков </Text>

                    <ScrollView horizontal={true} style={styles.scrollCont}>
                        <View style={styles.pro}>
                            {types && types.map((type) => (
                                <Product name={type.name} navigation={navigation} type={type} key={type.id}/>
                            ))}


                        </View>
                    </ScrollView>


                    <Text style={[styles.header, {top: '1%'}]}> кофейни поблизости</Text>
                    <ScrollView horizontal={true} style={styles.scrollCont2}>
                        <View style={styles.pro2}>
                            {cafes && cafes.map((cafe) => (
                                <Cafe navigation={navigation} cafe={cafe} key={cafe.id}/>
                            ))}
                        </View>
                    </ScrollView>

                    <Text style={[styles.header, {top: '1%'}]}> любимое </Text>

                    {favTypes.length === 0 ? (
                        <View>
                            <Text style={styles.nofav}> вы пока ничего не добавили в любимое :(</Text>
                        </View>
                    ) : (
                        <ScrollView horizontal={true} style={styles.scrollCont3}>
                            <View style={styles.pro3}>
                                {favTypes && favTypes.map((type) => (
                                    <Product key={type.id} name={type.name} navigation={navigation} type={type}/>
                                ))}

                            </View>
                        </ScrollView>
                    )}
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
    text: {
        fontSize: RFPercentage(2.5),
        fontFamily: 'MontserratAlternates',
    },
    searchBox: {
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: 'white',
        borderRadius: 20,
        paddingHorizontal: 10,
        width: '87%',
        height: '53%',
        left: 22,
        marginTop: '2%'
    },
    input: {
        fontFamily: 'MontserratAlternates',
        opacity: 0.5
    },
    searchIcon: {
        marginLeft: '75%',
    },

    header: {
        left: 25,
        fontFamily: 'MontserratAlternatesSemiBold',
        fontSize: RFPercentage(2),
    },
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
    pro2: {
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
    },
    body: {
        marginTop: '18%'
    },
    image: {
        width: RFValue(50),
        height: RFValue(50),
        marginTop: RFValue(0),
        marginLeft: RFValue(260)
    },

    nofav: {
        fontSize: RFPercentage(2),
        fontFamily: 'MontserratAlternates',
        marginTop: RFValue(10),
        marginLeft: RFValue(20)
    }

});
