using XRL.World.ZoneFactories;

namespace XRL.World.ZoneFactories {
    public class TQAQ_Plane_Main_ZoneFactory : BlueprintZoneFactory {
        public override bool CanBuildZone(ZoneRequest Request) {
            // Use BuildZone only for the world map; otherwise, we use GenerateZone
            // and AddBlueprintsFor.
            return Request.IsWorldZone;
        }
        public override Zone BuildZone(ZoneRequest Request) {
            var zone = new Zone(80, 25);
            zone.ZoneID = Request.ZoneID;
            if (Request.IsWorldZone) {
            zone.loadMap("TQAQ_Raseke_WorldMap.rpm");
            }
            zone.DisplayName = "Raseke";
            return zone;
        }
        public override void AfterBuildZone(Zone zone, ZoneManager zoneManager) {
            ZoneManager.PaintWalls(zone);
            ZoneManager.PaintWater(zone);
        }
    }
}