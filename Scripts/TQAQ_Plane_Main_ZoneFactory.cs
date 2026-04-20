namespace XRL.World.ZoneFactories {
    public class TQAQ_Plane_Main_ZoneFactory : IZoneFactory {
        public override bool CanBuildZone(ZoneRequest Request) {
            // Use BuildZone only for the world map; otherwise, we use GenerateZone
            // and AddBlueprintsFor.
            return Request.IsWorldZone;
        }
        public override Zone BuildZone(ZoneRequest Request) {
            var zone = new Zone(80, 25);
            zone.ZoneID = Request.ZoneID;
            if (Request.IsWorldZone) {
                zone.ForeachCell(delegate(Cell c) {
                    c.AddObject("TerrainJungle");
                });
            }
            zone.DisplayName = "Raseke";
            return zone;
        }
        public override void AddBlueprintsFor(ZoneRequest Request) {
            // Normally we would use the fields of the ZoneRequest to figure out
            // what cell blueprint we should get. In this case, we just resort
            // to using the same blueprint for all cells.
            var cellBlueprint = Blueprint.CellBlueprintsByName["TQAQ_DefaultRasekeCell"];
            var levelBlueprint = cellBlueprint.LevelBlueprint[1, 1, 10];
            Request.Blueprints.Add(levelBlueprint);
        }
        public override void AfterBuildZone(Zone zone, ZoneManager zoneManager) {
            ZoneManager.PaintWalls(zone);
            ZoneManager.PaintWater(zone);
        }
    }
}