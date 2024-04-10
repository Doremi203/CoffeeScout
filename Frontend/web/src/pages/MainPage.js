import React, {useContext, useEffect, useState} from 'react';
import "./MainPage.css"
import Button from 'react-bootstrap/Button';
import NewProduct from "../components/NewProduct";
import {Context} from "../index";
import {ToastContainer} from "react-toastify";
import {Product} from "../components/Product";
import {Order} from "../components/Order";


const MainPage = () => {

    const {cafe} = useContext(Context)

    const [menu, setMenu] = useState("")

    useEffect(() => {
        const fetchMenu = async () => {
            const menuu = await cafe.getMenu();
            setMenu(menuu);
        }

        fetchMenu();

    }, []);

    return (
        <div>
            <h1> Имя кофейни </h1>
            <Button className={"profileBut"}> Профиль </Button>

            <h2 className="menu"> Меню </h2>
            <div className="products">

                {Array.isArray(menu) && menu.map((product) => (
                    <Product name={product.name} size={product.sizeInMl} price={product.price}
                             type={product.beverageType.name} menuItemId={product.id}/>
                ))}

            </div>

            <NewProduct/>

            <h2 className="orders"> Заказы </h2>
            <div>
                <Order/>
                <Order/>
                <Order/>
            </div>

            <ToastContainer/>
        </div>
    );
};

export default MainPage;
