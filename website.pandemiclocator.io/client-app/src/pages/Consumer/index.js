import React, { useState } from "react";

import Map from "../../components/Map/Map.jsx";
import Marker from "../../components/Map/Marker";
import { useEffect } from "react";

import api from '../../services/api';

export default function Consumer() {
    const [markers, setMarkers] = useState([]);

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
            type: 'current'
        });

        setMarkers([...markers, ...[current], ...response.data]);
    }

    return (
        <div>
            <h1>PANDEMIC LOCATOR</h1>
            <p>Este gráfico mostra os casos de COVID-19 num raio de 30 km</p>
            <Map
                zoom={10}
                center={{ lat: -26.9206069, lng: -49.0766607 }}
                events={{ onBoundsChangerd: () => {} }}
            >
                {markers.map((m, index) => (
                    <Marker
                        key={m.id}
                        title={m.title}
                        position={{ lat: m.lat, lng: m.lng }}
                        type={m.type}
                        events={{
                            onClick: () => window.alert(`${m.qtd ?? ''} ${m.title}`)
                        }}
                    />
                ))}
            </Map>
        </div>
    );
}
