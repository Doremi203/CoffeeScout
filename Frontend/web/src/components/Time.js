import React, {useContext, useEffect, useState} from 'react';
import Button from 'react-bootstrap/Button';
import './Time.css'
import {Context} from "../index";

function Time({day, openingTime, closingTime, name, address, latitude, longitude,  hours}) {

    const {cafe} = useContext(Context)

    function addLeadingZero(value) {
        return value < 10 ? "0" + value : value;
    }

    const [openTime, setOpenTime] = useState('')
    const [closeTime, setCloseTime] = useState('')

    const days = ['Воскресенье', 'Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница', 'Суббота'];

    useEffect(() => {
        setOpenTime(addLeadingZero(openingTime.hour) + ':' + addLeadingZero(openingTime.minute))
        setCloseTime(addLeadingZero(closingTime.hour) + ':' + addLeadingZero(closingTime.minute))
    }, []);


    const [editableHours, setEditableHours] = useState(false);

    const saveHours = async () => {
        setEditableHours(false);
        let open = openTime.split(":");
        let hourOpen = parseInt(open[0], 10);
        let minuteOpen = parseInt(open[1], 10);
        let close = closeTime.split(":");
        let hourClose = parseInt(close[0], 10);
        let minuteClose = parseInt(close[1], 10);
        hours[day - 1].openingTime.hour = hourOpen
        hours[day - 1].openingTime.minute = minuteOpen
        hours[day - 1].closingTime.hour = hourClose
        hours[day - 1].closingTime.hour = minuteClose

        await cafe.changeInfo(name, address, latitude, longitude, hours)
    };

    const changeHours = () => {
        setEditableHours(true)
    }

    return (
        <div className="timeCont">
            <div className="day">
                <text> {days[day]} </text>
            </div>

            <input type="time" id="appt" name="appt" className="time" disabled={!editableHours} value={openTime}
                   onChange={(e) => setOpenTime(e.target.value)}
            />

            <input type="time" id="appt" name="appt" className="time" disabled={!editableHours} value={closeTime}
                   onChange={(e) => setCloseTime(e.target.value)}
                   style={{marginLeft: 30}}
            />

            {!editableHours &&
                <Button onClick={changeHours} className="changeTime"> Изменить</Button>}
            {editableHours &&
                <Button onClick={saveHours} className="changeTime"> Сохранить</Button>}

        </div>
    );
}

export default Time;