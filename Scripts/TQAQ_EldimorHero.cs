using System.Collections.Generic;
using System;
using XRL.Names;
using XRL.World.AI;
using XRL.World.Parts;

namespace XRL.World.ObjectBuilders
{

    public class TQAQ_EldimorHero : IObjectBuilder
    {

        public string ForceTitle;
        public string ForceName;

        public override void Initialize()
        {
            ForceTitle = null;
            ForceName = null;
        }

        public override void Apply(GameObject GO, string Context)
        {
            Dictionary<string, string> ctx = new Dictionary<string, string>();
            if (!ForceTitle.IsNullOrEmpty())
            {
                ctx["*Position*"] = ForceTitle;
            }
            string epithet = NameMaker.MakeEpithet(
                For: GO,
                Special: "Hero",
                SpecialFaildown: true,
                NamingContext: ctx
            );
            string title = NameMaker.MakeTitle(
                For: GO,
                Special: "Hero",
                SpecialFaildown: true,
                NamingContext: ctx
            );
            string name = GO.GiveProperName(
                Name: ForceName,
                Force: !ForceName.IsNullOrEmpty(),
                Special: "Hero",
                SpecialFaildown: true,
                NamingContext: ctx
            ) ?? GO.ShortDisplayName;
            GO.RequirePart<DisplayNameColor>().SetColorByPriority("M", DescriptionBuilder.PRIORITY_HIGH);
            if (!epithet.IsNullOrEmpty())
            {
                GO.RequirePart<Epithets>().AddEpithet(epithet, DescriptionBuilder.ORDER_ADJUST_VERY_EARLY);
            }
            if (!title.IsNullOrEmpty())
            {
                GO.RequirePart<Titles>().AddTitle(title, DescriptionBuilder.ORDER_ADJUST_VERY_EARLY);
            }
            Mutations mutations = GO.RequirePart<Mutations>();
            if (epithet == "the Seer")
            {
                GO.BoostStat("Intelligence", 3);
                if (!mutations.HasMutation("LightManipulation"))
                {
                    mutations.AddMutation(new World.Parts.Mutation.LightManipulation(), 5);
                }
            }
            else
            if (epithet == "the Denhead")
            {
                GO.BoostStat("Strength", 3);
            }
            else
            if (epithet == "the Champion")
            {
                GO.BoostStat("Strength", 3);
                if (!mutations.HasMutation("AdrenalControl2"))
                {
                    mutations.AddMutation(new World.Parts.Mutation.AdrenalControl2(), 1);
                }
            }
            else
            if (epithet == "the Hiker")
            {
                GO.BoostStat("Intelligence", 3);
            }
            else
            if (epithet == "the Oracle")
            {
                GO.BoostStat("Intelligence", 3);
            }
            else
            if (epithet == "the Shaman")
            {
                GO.BoostStat("Ego", 3);
                if (!mutations.HasMutation("Syphon Vim"))
                {
                    mutations.AddMutation(new World.Parts.Mutation.LifeDrain(), 1);
                }
            }
            else
            if (epithet == "the Wicca")
            {
                GO.BoostStat("Ego", 3);
                if (!mutations.HasMutation("ElectricalGeneration"))
                {
                    mutations.AddMutation(new World.Parts.Mutation.ElectricalGeneration(), 4);
                }
            }
            else
            if (epithet == "the Pirate King")
            {
                GO.BoostStat("Ego", 1);
                GO.BoostStat("Willpower", 2);
            }
            if (!title.IsNullOrEmpty())
            {
                if (title.Contains("Clan Hotur"))
                {
                    GO.BoostStat("Strength", 1);
                }
                if (title.Contains("Clan Ibex"))
                {
                    mutations.AddMutation(new World.Parts.Mutation.Horns(), 2);
                }
                if (title.Contains("Clan Sol"))
                {
                    if (!mutations.HasMutation("PhotosyntheticSkin"))
                    {
                        mutations.AddMutation(new World.Parts.Mutation.PhotosyntheticSkin(), 4);
                    }
                    var clan = GO.GetPart<TQAQ_EldimorHeroDrekirClanTemperate>();
                    if (clan != null)
                    {
                        clan.Photosynthetic = true;
                    }
                }
                if (title.Contains("Clan Whitetongue"))
                {
                    GO.BoostStat("Intelligence", 1);
                    GO.BoostStat("Ego", 1);
                    GO.BoostStat("Willpower", 1);
                }
                if (title.Contains("Clan Yr"))
                {
                    GO.BoostStat("MoveSpeed", -0.75);
                }
                if (title.Contains("Clan Mnim"))
                {
                    GO.BoostStat("Toughness", 1);
                }
            }
            GO.MultiplyStat("Hitpoints", 2);
        }

    }

}

namespace XRL.World.Parts
{

    [Serializable]
    public class TQAQ_EldimorHeroDrekirClanTemperate : IPart
    {

        public bool Created;
        [NonSerialized] public bool Photosynthetic;

        public override bool SameAs(IPart p)
        {
            return false;
        }

        public override bool WantEvent(int ID, int cascade)
        {
            return
                base.WantEvent(ID, cascade)
                || ID == EnteredCellEvent.ID
            ;
        }

        public override bool HandleEvent(EnteredCellEvent E)
        {
            try
            {
                List<Cell> emptyCells = Event.NewCellList();
                foreach (Cell cell in ParentObject.CurrentCell.GetAdjacentCells(4))
                {
                    if (cell.IsEmptyOfSolid())
                    {
                        emptyCells.Add(cell);
                    }
                }
                int peltasts = Rules.Stat.Random(1, 2);
                int fighters = Rules.Stat.Random(2, 4);
                int archers = Rules.Stat.Random(1, 2);
                int mages = 95.in100() ? 1 : 0;
                int jezailers = Rules.Stat.Random(1, 2);
                List<string> followers = new List<string>(peltasts + fighters + archers + mages + jezailers);
                for (int x = 0; x < peltasts; x++)
                {
                    followers.Add("TQAQ_Drekir_Peltast_Temperate");
                }
                for (int x = 0; x < fighters; x++)
                {
                    followers.Add("TQAQ_Drekir_Fighter_Temperate");
                }
                for (int x = 0; x < archers; x++)
                {
                    followers.Add("TQAQ_Drekir_Archer_Temperate");
                }
                for (int x = 0; x < mages; x++)
                {
                    followers.Add("TQAQ_Drekir_Mage_Temperate");
                }
                for (int x = 0; x < jezailers; x++)
                {
                    followers.Add("TQAQ_Drekir_Jezailer_Temperate");
                }
                for (int i = 0, j = followers.Count; i < j; i++)
                {
                    Cell cell = emptyCells.GetRandomElement();
                    if (cell == null)
                    {
                        break;
                    }
                    GameObject follower = GameObject.Create(followers[i]);
                    if (Photosynthetic)
                    {
                        var mutations = follower.RequirePart<Mutations>();
                        if (!mutations.HasMutation("PhotosyntheticSkin"))
                        {
                            mutations.AddMutation(new XRL.World.Parts.Mutation.PhotosyntheticSkin(), 1);
                        }
                    }
                    follower.SetAlliedLeader<AllyClan>(ParentObject);
                    cell.AddObject(follower);
                    follower.MakeActive();
                    emptyCells.Remove(cell);
                }
            }
            catch (Exception ex)
            {
                MetricsManager.LogError("TQAQ_EldimorHeroDrekirClanTemperate setup", ex);
            }
            ParentObject.RemovePart(this);
            return base.HandleEvent(E);
        }

    }

}