import React, {useState} from 'react';
import "./Profile.css"
import Button from 'react-bootstrap/Button';
import {useNavigate} from "react-router-dom";


const Profile = () => {

    const navigate = useNavigate();

    const back = () => {
        navigate('/main')
    }

    const [address, setAddress] = useState("Какой-то адрес");
    const [editableAddress, setEditableAddress] = useState(false);

    const save = async () => {
        setEditableAddress(false);
    };

    const change = () => {
        setEditableAddress(true)
    }


    return (
        <div>
            <Button className={"back"} onClick={back}> Назад </Button>
            <h1> Имя кофейни </h1>

            <div>
                <div className="address"> Адрес</div>
                <div className="cont">
                    <input
                        className="boxProf"
                        value={address}
                        onChange={(e) => setAddress(e.target.value)}
                        disabled={!editableAddress}
                    />
                    {!editableAddress &&
                        <Button className={"change"} onClick={change}> Изменить</Button>}
                    {editableAddress &&
                        <Button className={"change"} onClick={save}> Сохранить</Button>}
                </div>


                <div className="address"> Часы работы</div>
                <div className="cont">
                    <input
                        className="boxProf"
                        disabled={!editableAddress}
                    />
                    <Button className={"change"}> Изменить</Button>
                </div>
            </div>
        </div>
    );
};

export default Profile;
