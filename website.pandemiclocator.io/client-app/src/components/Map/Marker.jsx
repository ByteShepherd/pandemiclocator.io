import { useEffect } from "react";
import useGoogleMapMarker from "./useGoogleMapMarker";
import death from '../../assets/death.png';
import current from '../../assets/current.png';
import incident from '../../assets/incident.png';



export default function Marker({
  position,
  type,
  maps,
  map,
  events,
  active = false,
  title
}) {
  const marker = useGoogleMapMarker({
    position,
    type,
    maps,
    map,
    events,
    title
  });

  useEffect(
    () => {
      marker &&
        (active ? marker.setIcon(current) : marker.setIcon(incident));
    },
    [active]
  );

  return null;
}
