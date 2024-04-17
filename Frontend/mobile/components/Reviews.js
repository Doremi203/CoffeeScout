import {
    Alert, Image,
    Modal,
    ScrollView,
    StyleSheet,
    Text,
    TouchableWithoutFeedback,
    View
} from "react-native";
import {RFValue} from "react-native-responsive-fontsize";
import React, {useContext, useEffect, useState} from "react";
import {Context} from "../index";
import Review from "./Review";

export default function Reviews({menuItemId}) {

    const {product} = useContext(Context)
    const [modalVisible, setModalVisible] = useState(false);

    const [reviews, setReviews] = useState([])
    useEffect(() => {
        const fetchReview = async () => {
            const reviewss = await product.getReviews(menuItemId)
            setReviews(reviewss)
        }

        fetchReview();

    }, []);

    return (
        <View style={styles.cont}>
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
                        <Text style={styles.header}> Отзывы </Text>
                        <TouchableWithoutFeedback onPress={() => setModalVisible(false)}>
                            <Image source={require('../assets/icons/close.png')} style={styles.image}/>
                        </TouchableWithoutFeedback>
                        <ScrollView style={{marginTop: RFValue(-15)}}>
                            {reviews && reviews.map((review) => (
                                <Review content={review.content} rating={review.rating}/>
                            ))}
                        </ScrollView>

                    </View>
                </View>
            </Modal>

            <TouchableWithoutFeedback onPress={() => setModalVisible(!modalVisible)}>
                <Text style={styles.reviews}> отзывы </Text>
            </TouchableWithoutFeedback>
        </View>
    );
}


const styles = StyleSheet.create({
    cont: {
        marginTop: RFValue(-42),
        marginLeft: RFValue(180)
    },
    centeredView: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        marginTop: 160,
        maxHeight: RFValue(500)
    },
    modalView: {
        margin: 20,
        backgroundColor: 'white',
        borderRadius: 20,
        padding: 15,
        shadowColor: '#000',
        shadowOffset: {
            width: 0,
            height: 2,
        },
        shadowOpacity: 0.25,
        shadowRadius: 4,
        elevation: 5,
        width: RFValue(300),
    },
    reviews: {
        fontFamily: 'MontserratAlternates',
        fontSize: RFValue(15),
        top: RFValue(-70),
        left: RFValue(-170),
        textDecorationLine: 'underline',
        height: RFValue(50),
        width: RFValue(100)
    },
    image: {
        width: RFValue(20),
        height: RFValue(20),
        left: '90%',
        top: RFValue(-20),
    },
    header: {
        fontSize: RFValue(20),
        fontFamily: 'MontserratAlternates',
        textAlign: 'center',
    }

});

