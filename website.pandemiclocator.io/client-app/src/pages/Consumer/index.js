import React, { useState } from "react";

import Map from "../../components/Map/Map.jsx";
import Marker from "../../components/Map/Marker";
import { useEffect } from "react";
import mapIcons from '../../util/mapIcons';
import mapIconEnum from '../../util/mapIconEnum';

import api from '../../services/api';

export default function Consumer() {
    const [markers, setMarkers] = useState([]);
    const [deaths, setDeaths] = useState(0);
    const [suspects, setSuspects] = useState(0);
    const [confirmeds, setConfirmeds] = useState(0);

    useEffect(() => {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                const { latitude, longitude } = position.coords;
                loadIncidents(latitude, longitude);
            }, (err) => {
                console.log(err);
            }, {
            timeout: 30000,
        }
        );
    }, []);

    async function loadIncidents(lat, lng) {
        if (!lat && !lng)
            return;

        const response = await api.get(`pandemic?lat=${lat}&lng=${lng}`);

        const current = response.data.push({
            id: 1,
            title: 'Você está aqui',
            lat: lat,
            lng: lng,
            type: mapIconEnum.current
        });

        setMarkers([...markers, ...[current], ...response.data]);
    }

    useEffect(() => calculate(), [markers]);

    function calculate() {
        var deathsSum = markers.filter(x => x.type === mapIconEnum.death).reduce((sum, data) => {
            return sum + data.qtd;
        }, 0);

        var suspectsSum = markers.filter(x => x.type === mapIconEnum.suspect).reduce((sum, data) => {
            return sum + data.qtd;
        }, 0);

        var confirmedSum = markers.filter(x => x.type === mapIconEnum.confirmed).reduce((sum, data) => {
            return sum + data.qtd;
        }, 0);

        setDeaths(deathsSum);
        setSuspects(suspectsSum);
        setConfirmeds(confirmedSum);
    }

    return (
        <div>
            <h1>PANDEMIC LOCATOR</h1>
            <p>Este mapa mostra os casos de COVID-19 num raio de 30 km</p>
            <div className="inline-box">
                <div>
                    <img src={mapIcons.find(x => x.type === mapIconEnum.death).url} alt="Mortes" />
                    <label>{deaths} mortes pelo COVID</label>
                </div>
                <div>
                    <img src={mapIcons.find(x => x.type === mapIconEnum.suspect).url} alt="Casos suspeitos" />
                    <label>{suspects} casos suspeitos</label>
                </div>
                <div>
                    <img src={mapIcons.find(x => x.type === mapIconEnum.confirmed).url} alt="Casos confirmados" />
                    <label>{confirmeds} casos confirmados</label>
                </div>
            </div>
            
            <Map
                zoom={10}
                center={{ lat: -26.9206069, lng: -49.0766607 }}
                events={{ onBoundsChangerd: () => { } }}
            >
                {markers.map((m, index) => (
                    <Marker
                        key={index}
                        title={m.title}
                        position={{ lat: m.lat ?? 0, lng: m.lng ?? 0 }}
                        type={m.type}
                        events={{
                            onClick: () => window.alert(`${m.id}`)
                        }}
                    />
                ))}
            </Map>
        </div>
    );
}
