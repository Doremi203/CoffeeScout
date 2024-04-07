
import React, {useContext, useEffect, useState} from 'react';
import {useNavigate} from "react-router-dom";
import Form from "react-bootstrap/Form";
import './Reg.css';
import Button from "react-bootstrap/Button";
import {Context} from "../index";
import {ToastContainer} from "react-toastify";

const RegistrationPage = () => {
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    let id = 0;

    let userExists = false;

    const navigate = useNavigate();

    const {user} = useContext(Context);

    const click = async () => {
        await user.registration(name, email, password)
        navigate('/main')
    };



    /*const [users, setUsers] = useState([]);
    useEffect(() => {
        setUsers([]);
        fetch("http://localhost:5000/api/user/auth")
            .then((result) => result.json())
            .then((data) => setUsers(data));
    }, []);

    console.log(users);*/

    return (
        <div className="regPage">
            <main>

                <h2 className="reg"> Регистрация </h2>
                <Form className="form">
                    <Form.Group className="mb-3" controlId="formBasicEmail">

                        <Form.Control
                            type="name"
                            placeholder="coffee shop name"
                            className="box"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                        />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formBasicEmail">

                        <Form.Control
                            type="email"
                            placeholder="email"
                            className="box"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formBasicPassword">

                        <Form.Control
                            type="password"
                            placeholder="password"
                            className="box"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </Form.Group>

                    <Button className="buttonLog" onClick={click}> Зарегистироваться</Button>

                </Form>

                <div className="noAcc">
                    Есть аккаунт?
                    <a href="/login" className="log">
                        {" "}
                        Войти{" "}
                    </a>
                </div>


            </main>
        </div>

    );
};

export default RegistrationPage;
