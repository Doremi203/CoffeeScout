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

    useEffect(() => {
        const fetchMenu = async () => {
            const menuu = await cafe.getMenu();
            setMenu(menuu);
        }

        fetchMenu();

    }, []);


    const {order} = useContext(Context)

    const [orders1, setOrders1] = useState([])
    const [orders2, setOrders2] = useState([])
    const [orders3, setOrders3] = useState([])
    const [orders4, setOrders4] = useState([])

    useEffect(() => {
        const fetchOrders = async () => {
            const orderss1 = await order.getOrdersPending()
            setOrders1(orderss1)
            const orderss2 = await order.getOrdersInProgress()
            setOrders2(orderss2)
            const orderss3 = await order.getOrdersCompleted()
            setOrders3(orderss3)
            const orderss4 = await order.getOrdersCancelled()
            setOrders4(orderss4)
        }

        fetchOrders()
    }, []);


    const navigate = useNavigate();

    const profile = () => {
        navigate('/profile')
    }


    return (
        <div>
            <h1> Имя кофейни </h1>
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
                                     type={product.beverageType.name} menuItemId={product.id}/>
                        ))}

                    </div>
                </div>
            </div>

            <NewProduct/>

            <h2 className="orders"> Заказы </h2>
            <div>
                {Array.isArray(orders1) && orders1.map((order) => (
                    <Order number={order.id} status={order.status} orderItems={order.orderItems}/>
                ))}

                {Array.isArray(orders2) && orders2.map((order) => (
                    <Order number={order.id} status={order.status} orderItems={order.orderItems}/>
                ))}

                {Array.isArray(orders3) && orders3.map((order) => (
                    <Order number={order.id} status={order.status} orderItems={order.orderItems}/>
                ))}

                {Array.isArray(orders4) && orders4.map((order) => (
                    <Order number={order.id} status={order.status} orderItems={order.orderItems}/>
                ))}

            </div>
            <ToastContainer/>
        </div>
    );
};

export default MainPage;
