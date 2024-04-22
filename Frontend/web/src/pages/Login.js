import React, {useContext, useState} from 'react';
import {useNavigate} from "react-router-dom";
import Form from "react-bootstrap/Form";
import './Login.css';
import Button from "react-bootstrap/Button";
import {Context} from "../index";
import {ToastContainer} from "react-toastify";

const LoginPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const navigate = useNavigate();

    const {user} = useContext(Context);

    const click = async () => {
        await user.login(email, password, navigate)
    }


    return (
        <div>
            <main>
                <h2 className="log"> Войти </h2>
                <Form>
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
                <ToastContainer/>
            </main>
        </div>

    );
};

export default LoginPage;
