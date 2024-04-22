import React, {useContext, useEffect, useState} from 'react';
import "./MainPage.css"
import Button from 'react-bootstrap/Button';
import NewProduct from "../components/NewProduct";
import {Context} from "../index";
import {ToastContainer} from "react-toastify";
import {Product} from "../components/Product";
import {Order} from "../components/Order";
import {useNavigate} from "react-router-dom";

const MainPage = () => {

    const {cafe} = useContext(Context)

    const [menu, setMenu] = useState("")
    const [name, setName] = useState("")

    useEffect(() => {
        const fetchMenu = async () => {
            const menuu = await cafe.getMenu();
            setMenu(menuu);
            const info = await cafe.getInfo();
            console.log(info)
            setName(info.name)
        }

        fetchMenu();

    }, []);


    const {order} = useContext(Context)


    const [orders, setOrders] = useState([])

    useEffect(() => {
        const fetchOrders = async () => {
            const orderss = await order.getOrdersInProgress()
            setOrders(orderss)
        }

        fetchOrders()
    }, []);


    const navigate = useNavigate();

    const profile = () => {
        navigate('/profile')
    }


    return (
        <div>
            <h1> {name} </h1>
            <Button className={"profileBut"} onClick={profile}> Профиль </Button>
            <h2 className="menu"> Меню </h2>

            <div>
                <div
                    style={{
                        width: '100%',
                        overflowX: "scroll",
                        scrollBehavior: "smooth",
                    }}
                >
                    <div className="content-box">
                        {Array.isArray(menu) && menu.map((product) => (
                            <Product name={product.name} size={product.sizeInMl} price={product.price}
                                     type={product.beverageType.name} menuItemId={product.id}
                                     description={product.description} key={product.id}/>
                        ))}

                    </div>
                </div>
            </div>

            <NewProduct/>

            <h2 className="orders"> Заказы </h2>
            <div>
                {Array.isArray(orders) && orders.map((order) => (
                    <Order number={order.id} status={order.status} orderItems={order.orderItems} key={order.id}/>
                ))}
            </div>
            <ToastContainer/>
        </div>
    );
};

export default MainPage;
