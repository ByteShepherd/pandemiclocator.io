import { useEffect, useState } from "react";
import mapIcons from '../../util/mapIcons';
import mapIconEnum from '../../util/mapIconEnum';

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
    
    if (type === mapIconEnum.current)
      createCentralCircle(map, marker);
    
    setMarker(marker);
  }, [position,
    map,
    title,
    type,
    icon,
    events,
    maps.Marker]);

  function getIcon(type) {
    return mapIcons.find(x => x.status === type).url;
  }

  function createCentralCircle(map, marker) {
    var circle = new maps.Circle({
      radius: 30000,
      fillColor: '#AA0000'
    });
    circle.setMap(map);
    circle.bindTo('center', marker, 'position');
  }

  return marker;
}
