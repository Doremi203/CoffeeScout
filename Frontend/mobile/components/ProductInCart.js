import React, {useContext, useState} from 'react';
import {
    Image,
    StyleSheet,
    Text,
    TouchableOpacity,
    TouchableWithoutFeedback,
    View
} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import {Context} from "../index";


const ProductInCart = ({menuItemId, name, price}) => {
    const [quantity, setQuantity] = useState(1);
    const [isVisible, setIsVisible] = useState(true);
    const [totalPrice, setTotalPrice] = useState(price);
    const {cart} = useContext(Context);

    const decrementQuantity = () => {
        if (quantity > 1) {
            setQuantity(quantity - 1);
            setTotalPrice((quantity - 1) * price);
            cart.decreaseProductQuantity(menuItemId)
        }
    };

    const incrementQuantity = () => {
        setQuantity(quantity + 1);
        setTotalPrice((quantity + 1) * price);
        cart.increaseProductQuantity(menuItemId)
    };

    const handleDelete = () => {
        setIsVisible(false);
        cart.deleteProductFromCart(menuItemId);
    };

    return (
        <View>
            {isVisible && <View style={styles.square}>
                <Image source={require('../assets/icons/coffee.png')} style={styles.image}/>
                <View style={styles.labelcont}>
                    <Text style={styles.label}>{name}</Text>
                </View>
                <View style={styles.plusminus}>
                    <TouchableOpacity onPress={decrementQuantity} style={styles.button}>
                        <View style={styles.box}><Text style={styles.buttonText}>-</Text></View>
                    </TouchableOpacity>
                    <Text style={styles.quantity}>{quantity}</Text>
                    <TouchableOpacity onPress={incrementQuantity} style={styles.button}>
                        <View style={styles.box}><Text style={styles.buttonText}>+</Text></View>
                    </TouchableOpacity>
                </View>

                <TouchableWithoutFeedback onPress={handleDelete}>
                    <Image source={require('../assets/icons/trash8.png')} style={styles.trash}/>
                </TouchableWithoutFeedback>

                <Text style={styles.price}> {totalPrice} ₽</Text>

            </View>}
        </View>

    );
};

const styles = StyleSheet.create({
    square: {
        marginVertical: 10,
        width: RFValue(300),
        height: RFValue(115),
        backgroundColor: 'white',
        borderRadius: 6,
        shadowColor: 'grey',
        shadowOffset: {
            width: 2,
            height: 1,
        },
        shadowOpacity: 0.6,
        shadowRadius: 3.84,
        elevation: 5,
        flexDirection: 'row',
        justifyContent: 'space-between',
    },
    image: {
        marginLeft: 20,
        marginTop: 10,
        width: RFValue(90),
        height: RFValue(90)
    },
    label: {
        fontSize: RFPercentage(2.3),
        color: 'black',
        fontFamily: 'MontserratAlternates',
    },

    button: {
        paddingHorizontal: '17%',
    },
    buttonText: {
        fontSize: RFPercentage(4),
        color: '#169366',
        fontFamily: 'MontserratAlternates',
        bottom: RFValue(4)

    },
    quantity: {
        fontSize: RFPercentage(2.5),
        color: '#169366',
        marginTop: RFValue(3),
        fontFamily: 'MontserratAlternates',
        width: RFValue(28),
        textAlign: 'center',
    },
    plusminus: {
        flexDirection: 'row',
        flex: 1,
        top: RFValue(75),
        left: RFValue(-150),
        minWidth: RFValue(50),
    },
    box: {
        width: RFValue(27),
        height: RFValue(27),
        backgroundColor: '#e3e6e4',
        alignItems: 'center',
        borderRadius: 5,
        justifyContent: 'center'
    },
    trash: {
        width: RFValue(20),
        height: RFValue(20),
        left: RFValue(-35),
        top: RFValue(10)

    },
    priceAndTrash: {
        flexDirection: 'column',
        flex: 1
    },
    price: {
        top: RFValue(80),
        left: RFValue(-80),
        fontFamily: 'MontserratAlternates',
        fontSize: RFPercentage(2),
    },

    labelcont: {
        maxHeight: RFValue(60),
        flex: 1,
        minWidth: RFValue(150),
        top: RFValue(10)
    }

});

export default ProductInCart;
