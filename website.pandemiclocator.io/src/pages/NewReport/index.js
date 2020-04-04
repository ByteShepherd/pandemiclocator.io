import React, { useState, useEffect } from 'react';
import { Link, useHistory } from 'react-router-dom';
import './styles.css';
import { FiArrowLeft } from 'react-icons/fi';
import mapIconEnum from '../../util/mapIconEnum';
import Select from 'react-select';

import api from '../../services/api';

export default function NewReport() {
    const [latitude, setLatitude] = useState(0);
    const [longitude, setLongitude] = useState(0);
    const [quantity, setQuantity] = useState(1);
    const [status, setStatus] = useState('');
    const [items] = useState([
        { label: 'Morte', value: mapIconEnum.death },
        { label: 'Suspeita', value: mapIconEnum.suspect },
        { label: 'Confirmado', value: mapIconEnum.confirmed },
        { label: 'Saudável', value: mapIconEnum.healthy }
    ]);

    const history = useHistory();

    useEffect(() => {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                const { latitude, longitude } = position.coords;
                setLatitude(latitude);
                setLongitude(longitude);
            }, (err) => {
                console.log(err);
            }, {
            timeout: 30000,
        });
    }, []);

    async function handleNewReport(e) {
        e.preventDefault();

        const data = {
            quantity,
            identifier: '1',
            status,
            source: 0,
            location: { latitude, longitude }
        };
        
        try {
            await api.post('HealthReport/NewWebReport', data);

            history.push('/reports');
        } catch (err) {
            alert('Erro ao reportar caso');
        }
    }

    return (
        <div className="new-incident-container">
            <div className="content">
                <section>
                    <div className="pandemic-title-black">PANDEMIC LOCATOR</div>
                    <h1>Reportar novo caso</h1>
                    <p>Informe o tipo do caso, a quantidade de pessoas e a localização</p>

                    <Link className="back-link" to="/reports">
                        <FiArrowLeft size={16} color="#e02041" />
                        Voltar para o início
                    </Link>
                </section>
                <form>
                    <div className="select">
                        <Select options={items} onChange={e => setStatus(e.value)} />
                    </div>
                    <input
                        type="number"
                        placeholder="Quantidade"
                        min={1}
                        value={quantity}
                        onChange={e => setQuantity(e.target.value)}
                    />
                    <input
                        type="text"
                        placeholder="Latitude"
                        value={latitude}
                        onChange={e => setLatitude(e.target.value)}
                    />
                    <input
                        type="text"
                        placeholder="Longitude"
                        value={longitude}
                        onChange={e => setLongitude(e.target.value)}
                    />
                    <button className="button" onClick={handleNewReport}>Cadastrar</button>
                </form>
            </div>
        </div>
    )
}