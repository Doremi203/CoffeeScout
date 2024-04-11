import Card from "react-bootstrap/Card";
import React, {useContext} from "react";
import "./Order.css"
import {Context} from "../index";
import Dropdown from 'react-bootstrap/Dropdown';
import {DropdownButton} from "react-bootstrap";


export const Order = ({number, status, orderItems}) => {

    const {order} = useContext(Context)

    const cancelOrder = async () => {
        await order.cancelOrder(number)
    }

    const completeOrder = async () => {
        await order.completeOrder(number)

    }


    return (
        <Card>
            <Card.Header> {status} </Card.Header>
            <Card.Body>
                <Card.Title>Заказ №{number}</Card.Title>
                <div className="productsInOrder">
                    <div>
                        {Array.isArray(orderItems) && orderItems.map((item) => (
                            <Card.Text className="pro">
                                имя напитка - размер ml - количество - 3
                            </Card.Text>
                        ))}
                    </div>
                    <div>
                        {Array.isArray(orderItems) && orderItems.map((item) => (
                            <Card.Text className="pro">
                                цена - 400 Rub
                            </Card.Text>
                        ))}
                    </div>

                </div>
                <Card.Text className="totalPrice"> Сумма заказа - 1000 RUB</Card.Text>

                {(status === 'Pending' || status === 'InProgress') &&
                    <DropdownButton id="dropdown-basic-button" title="Изменить статус заказа">
                        <Dropdown.Item onClick={cancelOrder}>Отменить заказ</Dropdown.Item>
                        <Dropdown.Item onClick={completeOrder}>Завершить заказ</Dropdown.Item>
                    </DropdownButton>}
            </Card.Body>
        </Card>
    );
}