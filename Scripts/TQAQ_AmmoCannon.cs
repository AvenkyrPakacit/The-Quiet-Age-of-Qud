using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
    [Serializable]
    public class TQAQ_AmmoCannon : IPart
    {
        public string ProjectileObject;

        public TQAQ_AmmoCannon()
        {
//            Name = "TQAQ_AmmoCannon";
        }

        public override bool SameAs(IPart p)
        {
            return true;
        }

        public override void Register(GameObject Object, IEventRegistrar Registrar)
        {
            Registrar.Register("QueryEquippableList");
            Registrar.Register("GetProjectileObject");
            base.Register(Object, Registrar);
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "GetProjectileObject")
            {
                E.AddParameter("Projectile", GameObjectFactory.Factory.CreateObject(ProjectileObject));
                return true;
            }

            if (E.ID == "QueryEquippableList")
            {
                if ((E.GetParameter("EquippableList") as List<GameObject>).CleanContains(ParentObject)) return true;
                if ((E.GetParameter("SlotType") as string).Contains("TQAQ_AmmoCannon"))
                {
                    (E.GetParameter("EquippableList") as List<GameObject>).Add(ParentObject);
                    return true;
                }
                else
                {
                    return true;
                }
            }

            return true;
        }
    }
}