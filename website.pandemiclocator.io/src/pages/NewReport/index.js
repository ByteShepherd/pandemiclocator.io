import React, { useState, useEffect } from 'react';
import { Link, useHistory } from 'react-router-dom';
import './styles.css';
import { FiArrowLeft } from 'react-icons/fi';
import mapIconEnum from '../../util/mapIconEnum';
import sourceEnum from '../../util/sourceEnum';

import api from '../../services/api';

export default function NewReport() {
    const [latitude, setLatitude] = useState(0);
    const [longitude, setLongitude] = useState(0);
    const [quantity, setQuantity] = useState(1);
    const [status, setStatus] = useState(mapIconEnum.death);
    const [source, setSource] = useState(sourceEnum.self);

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
            status: parseInt(status),
            source: parseInt(source),
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
                    <select onChange={e => setStatus(e.target.value)}>
                        <option value={mapIconEnum.death}>Morte</option>
                        <option value={mapIconEnum.suspect}>Suspeita</option>
                        <option value={mapIconEnum.confirmed}>Confirmad</option>
                        <option value={mapIconEnum.healthy}>Saudável</option>
                    </select>
                    <select onChange={e => setSource(e.target.value)}>
                        <option value={sourceEnum.self}>Você</option>
                        <option value={sourceEnum.familiar}>Familiar</option>
                        <option value={sourceEnum.friend}>Amigo</option>
                        <option value={sourceEnum.coworker}>Colega do trabalho</option>
                        <option value={sourceEnum.stranger}>Pessoa desconhecida</option>
                    </select>
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