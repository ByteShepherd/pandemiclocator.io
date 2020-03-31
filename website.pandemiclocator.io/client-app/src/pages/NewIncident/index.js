import React, { useState, useEffect } from 'react';
import { Link, useHistory } from 'react-router-dom';
import './styles.css';
import { FiArrowLeft } from 'react-icons/fi';
import mapIconEnum from '../../util/mapIconEnum';
import Select from 'react-select';

import api from '../../services/api';

export default function NewIncident() {
    const [description, setDescription] = useState('');
    const [latitude, setLatitude] = useState(0);
    const [longitude, setLongitude] = useState(0);
    const [type, setType] = useState('');
    const [items] = useState([
        { label: 'Morte', value: mapIconEnum.death },
        { label: 'Suspeita', value: mapIconEnum.suspect },
        { label: 'Confirmado', value: mapIconEnum.confirmed }
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

    async function handleNewIncident(e) {
        e.preventDefault();

        const data = {
            type,
            lat: latitude,
            lng: longitude,
            title: description
        };
        
        try {
            await api.post('incident', data);

            history.push('/');
        } catch (err) {
            alert('Erro ao cadastrar incidente');
        }
    }

    return (
        <div className="new-incident-container">
            <div className="content">
                <section>
                    <div className="pandemic-title-black">PANDEMIC LOCATOR</div>
                    <h1>Cadastrar novo incidente</h1>
                    <p>Informe se é uma suspeita, caso confirmado ou morte por COVID-19</p>

                    <Link className="back-link" to="/">
                        <FiArrowLeft size={16} color="#e02041" />
                        Voltar para o início
                    </Link>
                </section>
                <form>
                    <div className="select">
                        <Select options={items} onChange={e => setType(e.value)} />
                    </div>
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
                    <textarea
                        placeholder="Descrição"
                        value={description}
                        onChange={e => setDescription(e.target.value)}
                    />
                    <button className="button" onClick={handleNewIncident}>Cadastrar</button>
                </form>
            </div>
        </div>
    )
}