import React, {useContext, useState} from 'react';
import "./Product.css"
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import {Context} from "../index";


export const Product = ({name, size, price, type, menuItemId}) => {

    const {cafe} = useContext(Context)

    const [isVisible, setIsVisible] = useState(true);

    const handleDelete = async () => {
        setIsVisible(false);
        await cafe.deleteProduct(menuItemId)
    };


    return (
        <div>
            {isVisible && <Card style={{width: '14rem'}}>
                <Card.Img variant="top" src="./coffee.png" className="coffee"/>
                <Card.Body>
                    <Card.Title>{name}</Card.Title>
                    <Card.Text>
                        размер - {size} ml <br/> цена - {price} RUB <br/> тип напитка - {type} <br/>
                    </Card.Text>
                </Card.Body>
                <Button variant="secondary" className="button" onClick={handleDelete}>Удалить напиток</Button>
            </Card>}
        </div>
    );
}