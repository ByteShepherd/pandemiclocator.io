import { useEffect, useState } from "react";

export default function TransitLayer({ enabled, maps, map }) {
  const [transitLayer, setTransitLayer] = useState(false);
  useEffect(() => {
    setTransitLayer(new maps.TransitLayer());
  }, []);

  useEffect(
    () => {
      if (transitLayer) {
        enabled ? transitLayer.setMap(null) : transitLayer.setMap(null);
      }
    },
    [enabled]
  );

  return null;
}
