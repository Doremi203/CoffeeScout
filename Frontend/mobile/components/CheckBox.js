import React, {useState} from 'react';
import {StyleSheet, Text, TouchableOpacity, View} from 'react-native';
import {Ionicons} from '@expo/vector-icons';
import {RFValue} from "react-native-responsive-fontsize"; // Assuming you have Ionicons installed

const Checkbox = () => {
    const [isChecked, setIsChecked] = useState(false);

    const toggleCheckbox = () => {
        setIsChecked(!isChecked);
    };

    return (
        <TouchableOpacity onPress={toggleCheckbox} style={{ flexDirection: 'row', alignItems: 'center', paddingVertical:'1%' }}>
            <Ionicons
                name={isChecked ? 'checkbox-outline' : 'square-outline'}
                size={24}
                color={isChecked ? 'blue' : 'black'}
            />
            <View>
                <Text style={styles.text}>корица</Text>
                <Text style={styles.price}> 10 p </Text>
            </View>

        </TouchableOpacity>
    );
};


const styles = StyleSheet.create({


    text: {
        fontSize: RFValue(15),
        fontFamily: 'MontserratAlternates',
        marginLeft: '2%'
    },

    price : {
        fontSize: RFValue(10),
        fontFamily: 'MontserratAlternates',
    }



});
export default Checkbox;
