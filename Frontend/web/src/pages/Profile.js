import React, {useContext, useEffect, useState} from 'react';
import "./Profile.css"
import Button from 'react-bootstrap/Button';
import {useNavigate} from "react-router-dom";
import {Context} from "../index";
import Time from "../components/Time";


const Profile = () => {
    const {cafe} = useContext(Context)

    const navigate = useNavigate();

    const back = () => {
        navigate('/main')
    }

    const [address, setAddress] = useState('');
    const [latitude, setLatitude] = useState('');
    const [longitude, setLongitude] = useState('');
    const [hours, setHours] = useState([]);
    const [name, setName] = useState('');
    useEffect(() => {
        const fetchInfo = async () => {
            const info = await cafe.getInfo();
            setAddress(info.address)
            setLatitude(info.location.latitude)
            setLongitude(info.location.longitude)
            setHours(info.workingHours)
            setName(info.name)
        }

        fetchInfo().then();
    }, []);


    const [editableAddress, setEditableAddress] = useState(false);

    const saveAddress = async () => {
        setEditableAddress(false);
        await cafe.changeInfo(name, address, latitude, longitude, hours)
    };

    const changeAddress = () => {
        setEditableAddress(true)
    }

    const [editableName, setEditableName] = useState(false);

    const saveName = async () => {
        setEditableName(false);
        await cafe.changeInfo(name, address, latitude, longitude, hours)
    };

    const changeName = async () => {
        setEditableName(true)
    }


    return (
        <div>
            <Button className={"back"} onClick={back}> Назад </Button>
            <div className="changeName">
                <input
                    className="nameBox"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    disabled={!editableName}
                />
                {!editableName &&
                    <Button className="buttonName" onClick={changeName}> Изменить</Button>}
                {editableName &&
                    <Button className="buttonName" onClick={saveName}> Сохранить</Button>}
            </div>


            <div>
                <div className="address"> Адрес</div>
                <div className="cont">
                    <input
                        className="boxProf"
                        value={address}
                        onChange={(e) => setAddress(e.target.value)}
                        disabled={!editableAddress}
                    />

                </div>


                <div>
                    <input
                        className="boxCoord"
                        value={latitude}
                        onChange={(e) => setLatitude(e.target.value)}
                        disabled={!editableAddress}
                    />
                    <input
                        className="boxCoord"
                        value={longitude}
                        onChange={(e) => setLongitude(e.target.value)}
                        disabled={!editableAddress}
                    />

                </div>

                {!editableAddress &&
                    <Button className={"change"} onClick={changeAddress}> Изменить</Button>}
                {editableAddress &&
                    <Button className={"change"} onClick={saveAddress}> Сохранить</Button>}


                <div className="address"> Часы работы</div>
                <div>
                    <text className="headers" style={{marginLeft: 20}}> День недели</text>
                    <text className="headers" style={{marginLeft: 50}}> Время открытия</text>
                    <text className="headers" style={{marginLeft: 26}}> Время закрытия</text>
                </div>

                {Array.isArray(hours) && hours.map((day) => (
                    <Time day={day.dayOfWeek} openingTime={day.openingTime} closingTime={day.closingTime}
                    name={name} address={address} latitude={latitude} longitude={longitude} id={day.id} hours={hours}/>
                ))}

            </div>
        </div>
    );
};

export default Profile;
