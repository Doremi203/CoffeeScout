import React, {useEffect, useState} from 'react';
import Button from 'react-bootstrap/Button';
import './Time.css'

function Time({day, openingTime, closingTime}) {

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