using System.Collections.Generic;
using System;
using XRL.Names;
using XRL.World.AI;
using XRL.World.Parts;

namespace XRL.World.ObjectBuilders
{

    public class TQAQ_EldimorHeroTropical : IObjectBuilder
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
                GO.BoostStat("Ego", 3);
                GO.BoostStat("Intelligence", 3);
                if (!mutations.HasMutation("MentalMirror"))
                {
                    mutations.AddMutation(new World.Parts.Mutation.MentalMirror(), 5);
                }
            }
            else
            if (epithet == "the Denhead")
            {
                GO.BoostStat("Ego", 2);
                GO.BoostStat("Strength", 3);
                if (!mutations.HasMutation("TwoHearted"))
                {
                    mutations.AddMutation(new World.Parts.Mutation.TwoHearted(), 1);
                }
            }
            else
            if (epithet == "the Champion")
            {
                GO.BoostStat("Agility", 2);
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
                if (!mutations.HasMutation("Regeneration"))
                {
                    mutations.AddMutation(new World.Parts.Mutation.Regeneration(), 1);
                }
            }
            else
            if (epithet == "the Oracle")
            {
                GO.BoostStat("Ego", 3);
                GO.BoostStat("Intelligence", 2);
                if (!mutations.HasMutation("Precognition"))
                {
                    mutations.AddMutation(new World.Parts.Mutation.Precognition(), 1);
                }
            }
            else
            if (epithet == "the Shaman")
            {
                GO.BoostStat("Ego", 3);
                if (!mutations.HasMutation("WillForce"))
                {
                    mutations.AddMutation(new World.Parts.Mutation.WillForce(), 1);
                }
            }
            else
            if (epithet == "the Wicca")
            {
                GO.BoostStat("Ego", 3);
                if (!mutations.HasMutation("Pyrokinesis"))
                {
                    mutations.AddMutation(new World.Parts.Mutation.Pyrokinesis(), 4);
                }
            }
            else
            if (epithet == "the Pirate King")
            {
                GO.BoostStat("Ego", 1);
                GO.BoostStat("Willpower", 2);
                if (!mutations.HasMutation("Horns"))
                {
                    mutations.AddMutation(new World.Parts.Mutation.Horns(), 4);
                }
            }
            if (!title.IsNullOrEmpty())
            {
                if (title.Contains("Clan Greensplit"))
                {
                    GO.BoostStat("Agility", 3);
                    GO.BoostStat("Toughness", 2);
                }
                if (title.Contains("Clan Basin"))
                {
                    GO.BoostStat("Strength", 2);
                }
                if (title.Contains("Clan Riverland"))
                {
                    GO.BoostStat("Agility", 3);
                }
                if (title.Contains("Clan Yatyl"))
                {
                    GO.BoostStat("Intelligence", 3);
                }
            }
            GO.MultiplyStat("Hitpoints", 2);
        }

    }

}

namespace XRL.World.Parts
{

    [Serializable]
    public class TQAQ_EldimorHeroDrekirClanTropical : IPart
    {

        public bool Created;

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
                int peltasts = Rules.Stat.Random(0, 2);
                int fighters = Rules.Stat.Random(0, 4);
                int archers = Rules.Stat.Random(0, 2);
                int mages = Rules.Stat.Random(0, 1);
                int jezailers = Rules.Stat.Random(0, 2);
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
                    follower.SetAlliedLeader<AllyClan>(ParentObject);
                    cell.AddObject(follower);
                    follower.MakeActive();
                    emptyCells.Remove(cell);
                }
            }
            catch (Exception ex)
            {
                MetricsManager.LogError("TQAQ_EldimorHeroDrekirClanTropical setup", ex);
            }
            ParentObject.RemovePart(this);
            return base.HandleEvent(E);
        }

    }

}

namespace XRL.World.Parts
{

    [Serializable]
    public class TQAQ_EldimorHeroOrmerClanTropical : IPart
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
                int slingers = Rules.Stat.Random(0, 2);
                int warriors = Rules.Stat.Random(0, 4);
                int axethrowers = Rules.Stat.Random(0, 2);
                int mages = Rules.Stat.Random(0, 1);
                int cannoneer = Rules.Stat.Random(0, 2);
                List<string> followers = new List<string>(slingers + warriors + axethrowers + mages + cannoneer);
                for (int x = 0; x < slingers; x++)
                {
                    followers.Add("TQAQ_Ormer_Slinger_Temperate");
                }
                for (int x = 0; x < warriors; x++)
                {
                    followers.Add("TQAQ_Ormer_Warrior_Temperate");
                }
                for (int x = 0; x < axethrowers; x++)
                {
                    followers.Add("TQAQ_Ormer_Axe_Thrower_Temperate");
                }
                for (int x = 0; x < mages; x++)
                {
                    followers.Add("TQAQ_Ormer_Mage_Temperate");
                }
                for (int x = 0; x < cannoneer; x++)
                {
                    followers.Add("TQAQ_Ormer_Cannoneer_Temperate");
                }
                for (int i = 0, j = followers.Count; i < j; i++)
                {
                    Cell cell = emptyCells.GetRandomElement();
                    if (cell == null)
                    {
                        break;
                    }
                    GameObject follower = GameObject.Create(followers[i]);
                    follower.SetAlliedLeader<AllyClan>(ParentObject);
                    cell.AddObject(follower);
                    follower.MakeActive();
                    emptyCells.Remove(cell);
                }
            }
            catch (Exception ex)
            {
                MetricsManager.LogError("TQAQ_EldimorHeroOrmerClanTropical setup", ex);
            }
            ParentObject.RemovePart(this);
            return base.HandleEvent(E);
        }

    }

}