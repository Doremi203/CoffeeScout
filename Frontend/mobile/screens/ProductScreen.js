import React, {useContext, useEffect, useState} from 'react';
import {ScrollView, StyleSheet, Text, View} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import Footer from "./Footer";
import ProductCard from "../components/ProductCard";
import {Context} from "../index";

export default function ProductScreen({navigation, route}) {

    const name = route.params.type.name
    const description = route.params.type.description

    const typeId = route.params.type.id

    const {product} = useContext(Context);
    const {loc} = useContext(Context);

    const location = loc.getLocation();

    const [nearProducts, setNearProducts] = useState([]);

    useEffect(() => {
        const fetchProducts = async () => {
            const products = await product.getNearbyProducts(location._j.longitude, location._j.latitude, 10000, typeId);
            setNearProducts(products);

        };
        fetchProducts();
    }, []);


    return (
        <View style={styles.container}>
            <View style={styles.main}>
                <Text style={styles.header}> {name} </Text>
                <Text style={styles.description}>{description}</Text>
                <Text style={styles.toOrder}>закажите:</Text>
                <View style={styles.shops}>
                    <ScrollView style={styles.scroll}>
                        {nearProducts && nearProducts.map((product) => (
                            <ProductCard menuItemId={product.id} name={product.name} price={product.price}
                                         size={product.sizeInMl} cafe={product.cafe} key={product.id}/>
                        ))}
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
        flex: 1
    },
    header: {
        fontSize: RFPercentage(8),
        marginTop: '15%',
        fontFamily: 'MontserratAlternates',
    },
    description: {
        fontFamily: 'MontserratAlternates',
        marginLeft: '5%',
        width: '90%',
        marginTop: '4%',
        fontSize: RFPercentage(1.7),
    },
    toOrder: {
        fontFamily: 'MontserratAlternatesSemiBold',
        fontSize: RFPercentage(4),
        marginLeft: '5%',
        marginTop: '5%',
    },
    shops: {
        alignItems: 'center',
    },
    scroll: {
        height: '62%',
    },
    button: {
        backgroundColor: '#169366',
        height: RFValue(45),
        alignItems: 'center',
        justifyContent: 'center',
        borderRadius: 10,
        elevation: 3,
        marginRight: 10,
        marginLeft: 10
    },
    buttonText: {
        fontSize: RFValue(15),
        color: 'white',
        fontFamily: 'MontserratAlternates',
    }

});

