import React, { useState } from "react";
import { useHistory } from 'react-router-dom';
import { FiPlus } from 'react-icons/fi';

import Map from "../../components/Map/Map";
import Marker from "../../components/Map/Marker";
import { useEffect } from "react";
import mapIcons from '../../util/mapIcons';
import mapIconEnum from '../../util/mapIconEnum';

import api from '../../services/api';

export default function HealthReport() {
    const [markers, setMarkers] = useState([]);
    const [deaths, setDeaths] = useState(0);
    const [suspects, setSuspects] = useState(0);
    const [confirmeds, setConfirmeds] = useState(0);
    const [healthys, setHealthys] = useState(0);

    const history = useHistory();

    useEffect(() => {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                const { latitude, longitude } = position.coords;
                getReportsNearBy(latitude, longitude);
            }, (err) => {
                console.log(err);
            }, {
            timeout: 30000,
        }
        );
    }, []);

    async function getReportsNearBy(lat, lng) {
        if (!lat && !lng)
            return;

        const response = await api.get(`HealthReport/GetReportsNearBy?latitude=${lat}&longitude=${lng}`);
        const current = response.data.data.push({
            quantity: 0,
            status: 4,
            source: 0,
            location: { latitude: lat, longitude: lng }
        });

        setMarkers([...markers, ...[current], ...response.data.data]);
    }

    useEffect(() => calculate(), [markers]);

    function calculate() {
        var deathsSum = markers.filter(x => x.status === mapIconEnum.death).reduce((sum, data) => {
            return sum + data.quantity;
        }, 0);

        var suspectsSum = markers.filter(x => x.status === mapIconEnum.suspect).reduce((sum, data) => {
            return sum + data.quantity;
        }, 0);

        var confirmedSum = markers.filter(x => x.status === mapIconEnum.confirmed).reduce((sum, data) => {
            return sum + data.quantity;
        }, 0);

        var healthySum = markers.filter(x => x.status === mapIconEnum.healthy).reduce((sum, data) => {
            return sum + data.quantity;
        }, 0);

        setDeaths(deathsSum);
        setSuspects(suspectsSum);
        setConfirmeds(confirmedSum);
        setHealthys(healthySum);
    }


    function getTitle(marker) {
        let title = '';
        switch (marker.status) {
            case 0:
                title = `${marker.quantity} saudáveis`
                break;
            case 1:
                title = `${marker.quantity} suspeitos`
                break;
            case 2:
                title = `${marker.quantity} confirmados`
                break;
            case 3:
                title = `${marker.quantity} mortes`
                break;
            case 4:
                title = `Você está aqui`
                break;
            default:
                break;
        }

        return title;
    }

    return (
        <div>
            <div className="pandemic-title">PANDEMIC LOCATOR</div>
            <p>Este mapa mostra os casos de COVID-19 num raio de 30 km</p>
            <div className="inline-box">
                <div>
                    <img align="left" src={mapIcons.find(x => x.status === mapIconEnum.healthy).url} alt="Saudáveis" />
                    <label>{healthys} saudáveis</label>
                </div>
                <div>
                    <img align="left" src={mapIcons.find(x => x.status === mapIconEnum.suspect).url} alt="Casos suspeitos" />
                    <label>{suspects} casos suspeitos</label>
                </div>
                <div>
                    <img align="left" src={mapIcons.find(x => x.status === mapIconEnum.confirmed).url} alt="Casos confirmados" />
                    <label>{confirmeds} casos confirmados</label>
                </div>
                <div>
                    <img align="left" src={mapIcons.find(x => x.status === mapIconEnum.death).url} alt="Mortes" />
                    <label>{deaths} mortes pelo COVID</label>
                </div>
            </div>

            <Map
                zoom={10}
                center={{ lat: -26.9206069, lng: -49.0766607 }}
                events={{ onBoundsChangerd: () => { } }}
            >
                {markers.filter(x => x.location).map((m, index) => (
                    <Marker
                        id={index}
                        key={index}
                        title={getTitle(m)}
                        position={{ lat: m.location.latitude, lng: m.location.longitude }}
                        type={m.status}
                        events={{
                            onClick: () => window.alert(`${index}`)
                        }}
                    />
                ))}
            </Map>

            <button onClick={() => history.push('/incidents/new')}>
                <FiPlus size={16} color="#e02041" />
                Relatar incidente
            </button>
        </div>
    );
}
