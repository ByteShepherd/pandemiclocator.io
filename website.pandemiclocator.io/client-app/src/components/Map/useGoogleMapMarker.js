import { useEffect, useState } from "react";

const eventMapping = {
  onClick: "click",
  onDoubleClick: "dblclick"
};

export default function useGoogleMapMarker({
  position,
  type,
  maps,
  map,
  events,
  title
}) {
  const [marker, setMarker] = useState();
  const icon = getIcon(type);
  
  useEffect(() => {
    const marker = new maps.Marker({
      position,
      map,
      title,
      type,
      icon
    });
    Object.keys(events).forEach(eventName =>
      marker.addListener(eventMapping[eventName], events[eventName])
    );
    setMarker(marker);
  }, []);

  function getIcon(type) {
    // http://kml4earth.appspot.com/icons.html
    switch (type) {
      case 'current':
        // return 'http://maps.google.com/mapfiles/kml/paddle/blu-stars.png';
        return 'https://maps.google.com/mapfiles/kml/pal3/icon31.png';
      case 'death':
        // return 'http://maps.google.com/mapfiles/kml/shapes/church.png';
        return 'https://maps.google.com/mapfiles/kml/pal3/icon39.png';
      case 'incident':
        // return 'http://maps.google.com/mapfiles/kml/shapes/caution.png';
        return 'https://maps.google.com/mapfiles/kml/pal3/icon37.png';
      default:
        return 'https://maps.google.com/mapfiles/kml/pal3/icon37.png';
    }
  }

  return marker;
}
