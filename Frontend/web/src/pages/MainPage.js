import React, {useState} from 'react';
import "./MainPage.css"
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';


const Product = ({name, size, price}) => {
    return(
        <Card style={{ width: '14rem' }}>
            <Card.Img variant="top" src="./coffee.png" className="coffee"/>
            <Card.Body>
                <Card.Title>{name}</Card.Title>
                <Card.Text>
                    size - {size} L <br/> price - {price} RUB <br/>
                </Card.Text>
            </Card.Body>
        </Card>
    );
}

const Order = () => {
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


// Главный компонент страницы с двумя разделами
const MainPage = () => {
    return (
        <div>
            <h1>One Price Coffee</h1>

            <Button className={"profileBut"}> Профиль </Button>

            <h2 className="menu"> Меню </h2>
            <div className="products">
                <Product name={'capuchino'} size={0.3} price={200}/>
                <Product name={'latte'} size={0.3} price={200}/>
            </div>

            <Button variant="primary" className="button">Добавить товар</Button>
            <Button variant="primary" className="button">Удалить товар</Button>

            <h2 className="orders"> Заказы </h2>
            <div>
                <Order />
                <Order />
                <Order />
            </div>

        </div>
    );
};

export default MainPage;
