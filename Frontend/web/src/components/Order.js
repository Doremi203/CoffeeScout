import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import React from "react";
import "./Order.css"


export const Order = () => {
    return (
        <Card>
            <Card.Header> В процессе </Card.Header>
            <Card.Body>
                <Card.Title>Заказ №1</Card.Title>
                <div className="productsInOrder">
                    <Card.Text className="pro">
                        capuchino - 0.3 L <br/> latte - 0.3 L <br/> price - 400 RUB
                    </Card.Text>
                    <Card.Text className="pro">
                        capuchino - 0.3 L <br/> latte - 0.3 L <br/> price - 400 RUB
                    </Card.Text>
                    <Card.Text className="pro">
                        capuchino - 0.3 L <br/> latte - 0.3 L <br/> price - 400 RUB
                    </Card.Text>
                </div>

                <Button variant="primary">Изменить статус заказа</Button>
            </Card.Body>
        </Card>
    );
}