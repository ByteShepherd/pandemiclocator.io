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
    setMarker(marker);
  }, [position,
    map,
    title,
    type,
    icon,
    events,
    maps.Marker]);

  function getIcon(type) {
    if (!type || type === mapIconEnum.default)
      return mapIcons.find(x => x.type === mapIconEnum.default).url;
    return mapIcons.find(x => x.type === type).url;
  }

  return marker;
}
