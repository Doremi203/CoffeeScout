import {
    StyleSheet,
    Text,
    View
} from "react-native";
import {RFValue} from "react-native-responsive-fontsize";
import React from "react";

export default function Review({content, rating}) {
    return (
        <View style={styles.cont}>
            <Text style={styles.text}>{content}</Text>
            <Text style={styles.mark}>Оценка: {rating}</Text>
        </View>
    );
}


const styles = StyleSheet.create({
    text: {
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(17)
    },
    cont: {
        width: '100%',
        padding: RFValue(13),
        borderRadius: 15,
        backgroundColor: '#E2E9E6',
        marginVertical: RFValue(10)
    },
    mark: {
        fontFamily: 'MontserratAlternatesMedium',
        fontSize: RFValue(17)
    },

});

