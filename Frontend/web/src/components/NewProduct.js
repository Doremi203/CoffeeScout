import React, {useContext, useEffect, useState} from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from "react-bootstrap/Form";
import './NewProduct.css'
import {Context} from "../index";

function NewProduct() {
    const {cafe} = useContext(Context)

    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const [name, setName] = useState("")
    const [price, setPrice] = useState("")
    const [size, setSize] = useState("")
    const [typeId, setTypeId] = useState("")

    const addProduct = async () => {
        await cafe.addProduct(name, price, size, typeId)
        setShow(false);
    }

    const [types, setTypes] = useState([])
    useEffect(() => {
        const fetchTypes = async () => {
            const typess = await cafe.getTypes()
            setTypes(typess)
        }
        fetchTypes();
    }, []);

    return (
        <>
            <Button variant="primary" onClick={handleShow} className="buttonPro">
                Добавить напиток
            </Button>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Добавить напиток</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group>
                            <Form.Control
                                type="text"
                                placeholder="Название напитка"
                                className="boxPro"
                                value={name}
                                onChange={(e) => setName(e.target.value)}
                            />
                        </Form.Group>

                        <Form.Group>
                            <Form.Control
                                type="number"
                                placeholder="Размер"
                                className="boxPro"
                                value={size}
                                onChange={(e) => setSize(e.target.value)}
                            />
                        </Form.Group>

                        <Form.Group>
                            <Form.Control
                                type="number"
                                placeholder="Цена"
                                className="boxPro"
                                value={price}
                                onChange={(e) => setPrice(e.target.value)}
                            />
                        </Form.Group>

                        <Form.Group>
                            <Form.Select aria-label="Default select example"
                                         className="boxPro"
                                         value={typeId}
                                         onChange={(e) => setTypeId(e.target.value)}>
                                <option>Тип напитка</option>
                                {Array.isArray(types) && types.map((type) => (
                                    <option value={type.id}> {type.name} </option>
                                ))}

                            </Form.Select>
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Закрыть
                    </Button>
                    <Button variant="primary" onClick={addProduct}>
                        Добавить
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
}

export default NewProduct;