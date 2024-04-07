import React, {useContext, useState} from 'react';
import {
    Image,
    StyleSheet,
    Text,
    TouchableOpacity,
    TouchableWithoutFeedback,
    TouchableWithoutFeedbackBase,
    View
} from 'react-native';
import {RFPercentage, RFValue} from "react-native-responsive-fontsize";
import {Context} from "../index";


const ProductInCart = ({ menuItemId, name, price}) => {
    const [quantity, setQuantity] = useState(1);
    const [isVisible, setIsVisible] = useState(true);
    const [totalPrice, setTotalPrice] = useState(price);
    const {cart} = useContext(Context);

    const decrementQuantity = () => {
        if (quantity > 1) {
            setQuantity(quantity - 1);
            setTotalPrice((quantity - 1) * price); // обновляем общую стоимость
            cart.decreaseProductQuantity(menuItemId)
            console.log(cart.cart)
        }
    };

    const incrementQuantity = () => {
        setQuantity(quantity + 1);
        setTotalPrice((quantity + 1) * price); //
        cart.increaseProductQuantity(menuItemId)
        console.log(cart.cart)
    };




    const handleDelete = () => {
        setIsVisible(false);
        cart.deleteProductFromCart(menuItemId);
        console.log(cart.cart)
    };





    return (
        <View>
            {isVisible && <View style={styles.square}>
                <Image source={require('../assets/icons/coffee.png')} style={styles.image} />
                <Text style={styles.label}>{name}</Text>
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
    container: {
    },
    square: {
        marginVertical: 10,
       // flexDirection: 'row',
       // width: windowWidth * 0.85,
       // height: windowHeight* 0.15,
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
       // marginTop: 20,
       // marginLeft: 20
        top: RFValue(-90),
        marginLeft: RFValue(120)
    },

    button: {
        paddingHorizontal: '17%',
    },
    buttonText: {
        fontSize: RFPercentage(4),
        //fontWeight: 'bold',
        color: '#169366',
        fontFamily: 'MontserratAlternates',
        //marginTop: RFValue(),
        bottom: RFValue(4)

    },
    quantity: {
        fontSize: RFPercentage(2.5),
        //marginHorizontal: RFValue(5),
        color: '#169366',
        marginTop: RFValue(3),
        fontFamily: 'MontserratAlternates',
       // backgroundColor:'pink',
        width: RFValue(28),
        textAlign:'center',
        //top: RFValue(10)


    },

    plusminus : {
        //flex: 1,
        flexDirection: 'row',
        //marginTop: '80%',
        //top:'70%',
        //right: RFValue(88),
        top: RFValue(-40),
        width: RFValue(50),
        marginLeft: RFValue(105),

    },
    box : {
        width: RFValue(27),
        height: RFValue(27),
        backgroundColor: '#e3e6e4',
        alignItems: 'center',
        borderRadius: 5,
        justifyContent: 'center'
    },
    trash : {
       //marginTop: 20,
       // left: 10,
        width: RFValue(20),
        height: RFValue(20),
        top: RFValue(-135),
        left: RFValue(270),

    },
    priceAndTrash: {
        flexDirection: 'column',

    },
    price: {
        //top: '30%',
        //right: 20,
        top: RFValue(-80),
        marginLeft: RFValue(250),
        fontFamily: 'MontserratAlternates',
        fontSize: RFPercentage(2),
    }

});

export default ProductInCart;
