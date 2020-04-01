import useGoogleMapMarker from "./useGoogleMapMarker";

export default function Marker({
  position,
  type,
  maps,
  map,
  events,
  title
}) {
  useGoogleMapMarker({
    position,
    type,
    maps,
    map,
    events,
    title
  });

  return null;
}
