import React, {useContext, useEffect, useRef, useState} from 'react';
import {ScrollView, StyleSheet, TextInput, View} from "react-native";
import Footer from "./Footer";
import Icon from "react-native-vector-icons/FontAwesome";
import {Context} from "../index";
import ProductCard from "../components/ProductCard";
import {debounce} from 'lodash';

export default function SearchScreen({navigation}) {

    const {product} = useContext(Context)

    const searchInputRef = useRef(null);

    useEffect(() => {
        if (searchInputRef.current) {
            searchInputRef.current.focus();
        }
    }, []);

    const [query, setQuery] = useState('');
    const [suggestions, setSuggestions] = useState([]);


    const fetchSuggestions = debounce(async (word) => {
        try {
            const response = await product.search(word, 10);
            setSuggestions(response);
        } catch (error) {
            console.error('Failed to fetch suggestions:', error);
            return [];
        }

    }, 300);


    const handleSearch = async (text) => {
        setQuery(text);
        const lastWord = text.split(' ').pop();
        if (lastWord.length > 1) {
            fetchSuggestions(lastWord);
        } else {
            setSuggestions([]);
        }
    };


    return (
        <View style={styles.container}>
            <View style={styles.main}>
                <View style={styles.searchBox}>
                    <TextInput
                        ref={searchInputRef}
                        style={styles.input}
                        placeholder="искать"
                        value={query}
                        onChangeText={handleSearch}
                        autoCorrect={false}
                    />
                    <Icon name="search" size={20} color="#000" style={styles.searchIcon}/>
                </View>

                <ScrollView>
                    {suggestions && suggestions.map((product) => (
                        <ProductCard menuItemId={product.id} name={product.name} price={product.price}
                                     size={product.sizeInMl} cafe={product.cafe} key={product.id}/>
                    ))}
                </ScrollView>

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
        alignItems: 'center'
    },
    input: {
        flex: 1,
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
        marginTop: '15%'
    },
    searchIcon: {
        marginLeft: 10,
    },
});
