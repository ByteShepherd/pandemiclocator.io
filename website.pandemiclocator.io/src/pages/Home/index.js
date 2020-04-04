import React, { useState, useEffect } from "react";
import {useHistory} from 'react-router-dom';

export default function Home() {
    const [acessoCencedido, setAcesso] = useState(true);
    const history = useHistory();

    useEffect(() => {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                const { latitude, longitude } = position.coords;
                setAcesso(true);
                if (latitude && longitude)
                    history.push('/reports');
            }, (err) => {
                setAcesso(false);
            }, {
            timeout: 30000,
        }
        );
    }, []);

    function showPermissionMessage() {
        if (!acessoCencedido) {
            return (
                <div className="button">
                    Você não nos permitiu saber sua localização.
                </div>
            );
        }
    }

    return (
        <>
            <div className="pandemic-title">PANDEMIC LOCATOR</div>
            <p style={ {marginTop: '45px' }}>Precisamos saber sua localização, para procurarmos os casos de COVID-19 perto de você</p>

            {showPermissionMessage()}
        </>
    );
}
