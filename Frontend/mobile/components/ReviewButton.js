import {
    Alert,
    Modal,
    Pressable,
    StyleSheet,
    Text,
    TextInput,
    TouchableWithoutFeedback,
    View
} from "react-native";
import {RFValue} from "react-native-responsive-fontsize";
import React, {useContext, useState} from "react";
import {Context} from "../index";


export default function ReviewButton({menuItemId}) {

    const {product} = useContext(Context)

    const [modalVisible, setModalVisible] = useState(false);

    const [review, setReview] = useState('')
    const [rating, setRating] = useState()

    const leaveReview = async () => {
        setModalVisible(false)
        await product.leaveReview(menuItemId, review, rating)
        setRating()
        setReview('')
    }

    return (
        <View style={styles.routeButton}>

            <Modal
                animationType="slide"
                transparent={true}
                visible={modalVisible}
                onRequestClose={() => {
                    Alert.alert('Modal has been closed.');
                    setModalVisible(!modalVisible);
                }}>
                <View style={styles.centeredView}>
                    <View style={styles.modalView}>
                        <View style={styles.review}>

                            <TextInput style={styles.input}
                                       placeholder="Введите отзыв"
                                       placeholderTextColor="gray"
                                       onChangeText={text => setReview(text)}
                            />
                        </View>
                        <View style={styles.rating}>

                            <TextInput style={styles.input}
                                       placeholder={'Введите оценку от 1 до 5'}
                                       placeholderTextColor="gray"
                                       keyboardType="numeric"
                                       onChangeText={text => setRating(text)}
                            />
                        </View>
                        <Pressable
                            style={[styles.button, styles.buttonClose]}
                            onPress={() => setModalVisible(!modalVisible)}>
                            <Text style={styles.textStyle} onPress={leaveReview}>Оставить отзыв</Text>
                        </Pressable>
                    </View>
                </View>
            </Modal>

            <TouchableWithoutFeedback onPress={() => setModalVisible(!modalVisible)}>
                <Text style={styles.routeText}> Оставить отзыв </Text>
            </TouchableWithoutFeedback>

        </View>
    );
}


const styles = StyleSheet.create({
    routeButton: {
        borderColor: 'black',
        borderWidth: 1,
        width: RFValue(150),
        height: RFValue(35),
        borderRadius: 20,
        marginTop: RFValue(-42),
        marginLeft: RFValue(180)
    },
    routeText: {
        fontSize: RFValue(13),
        fontFamily: 'MontserratAlternates',
        textAlign: 'center',
        marginTop: RFValue(8),
    },


    centeredView: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        marginTop: 22,
    },
    modalView: {
        margin: 20,
        backgroundColor: 'white',
        borderRadius: 20,
        padding: 35,
        alignItems: 'center',
        shadowColor: '#000',
        shadowOffset: {
            width: 0,
            height: 2,
        },
        shadowOpacity: 0.25,
        shadowRadius: 4,
        elevation: 5,
        width: RFValue(300),
        height: RFValue(200)
    },
    button: {
        borderRadius: 20,
        padding: 10,
        elevation: 2,
        marginTop: RFValue(5)
    },
    buttonClose: {
        backgroundColor: '#169366',
    },
    textStyle: {
        color: 'white',
        fontWeight: 'bold',
        textAlign: 'center',
    },

    input: {
        height: RFValue(55),
        width: RFValue(280),
        borderWidth: 1,
        paddingLeft: 10,
        fontFamily: 'MontserratAlternates',
    },

    review: {},
    rating: {
        marginTop: RFValue(15)
    }

});

