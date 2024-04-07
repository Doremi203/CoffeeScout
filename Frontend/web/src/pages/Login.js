
import React, {useContext, useEffect, useState} from 'react';
import {useNavigate} from "react-router-dom";
import Form from "react-bootstrap/Form";
import './Reg.css';
import Button from "react-bootstrap/Button";
import {Context} from "../index";

const LoginPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    let id = 0;

    let userExists = false;

    const navigate = useNavigate();

    const {user} = useContext(Context);

    const click = async () => {
        await fetch('http://localhost/api/v1/accounts/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                email : email,
                password : password
            })
        })
            .then(response => response.json())
            .then(data => console.log(data))
            .catch((error) => {
                console.error('Error:', error);
            });
        //console.log(email, password)
        //await user.login(email, password)
        //navigate('/main')
    };


    return (
        <div className="regPage">
            <main>

                <h2 className="reg"> Войти </h2>
                <Form className="form2">

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

                    <Button className="buttonLog" onClick={click}>Войти </Button>
                </Form>

                <div className="noAcc">
                    Нет аккаунта?
                    <a href="/reg" className="log">
                        {" "}
                        Зарегистрироваться{" "}
                    </a>
                </div>


            </main>
        </div>

    );
};

export default LoginPage;
