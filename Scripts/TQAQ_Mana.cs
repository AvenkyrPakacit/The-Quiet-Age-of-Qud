using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Liquids;
using XRL.Rules;
using XRL.World;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

[Serializable]
[IsLiquid]
public class TQAQ_Mana : BaseLiquid
{
	public new const string ID = "tqaq_mana";

	[NonSerialized]
	public static List<string> Colors = new List<string>(4) { "B", "y", "Y", "b" };

	public TQAQ_Mana()
		: base("tqaq_mana")
	{
		Combustibility = -50;
		VaporObject = "SteamGas";
		Fluidity = 30;
		Evaporativity = 2;
		Cleansing = 5;
		PureElectricalConductivity = 0;
		MixedElectricalConductivity = 100;
		EnableCleaning = true;
		SlipperyWhenFrozen = true;
		SlipperySaveTargetBase = 5;
		SlipperySaveTargetScale = 0.3;
		SlipperySaveVs = "Ice Slip Move";
		SlipperyMessage = "{{C|=subject.T= =verb:slip= on the ice!}}";
		SlipperyParticle = "&C\u001a";
	}

	public override List<string> GetColors()
	{
		return Colors;
	}

	public override string GetColor()
	{
		return "O";
	}

	public override string GetName(LiquidVolume Liquid)
	{
		if (Liquid != null && Liquid.IsPureLiquid("tqaq_mana"))
		{
			return "{{extradimensional|pure mana}}";
		}
		return "{{extradimensional|mana}}";
	}

	public override string GetAdjective(LiquidVolume Liquid)
	{
		if (Liquid == null)
		{
			return "{{extradimensional|spiritual}}";
		}
		if (Liquid.ComponentLiquids["tqaq_mana"] > 0)
		{
			return "{{extradimensional|spiritual}}";
		}
		return null;
	}

	public override string GetWaterRitualName()
	{
		return "mana";
	}

	public override string GetSmearedAdjective(LiquidVolume Liquid)
	{
		return "{{extradimensional|soaked}}";
	}

	public override string GetSmearedName(LiquidVolume Liquid)
	{
		return "{{extradimensional|wet}}";
	}

	public override string GetStainedName(LiquidVolume Liquid)
	{
		return "{{extradimensional|soaked}}";
	}

	public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
	{
		Message.Compound("You are overcome by the mana!");
		if (Target.ApplyEffect(new Confused(Stat.Roll("3d6"), 1, 3)))
		{
			ExitInterface = true;
		}
		return true;
	}

	public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
	{
		if (eRender.ColorsVisible)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (num > 5 && num < 15)
			{
				eRender.ColorString = "&O";
			}
		}
		base.RenderSmearPrimary(Liquid, eRender, obj);
	}

	public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
	{
		if (eRender.ColorsVisible)
		{
			eRender.ColorString = "^Y" + eRender.ColorString;
		}
	}

	public override void BaseRenderPrimary(LiquidVolume Liquid)
	{
		Liquid.ParentObject.Render.ColorString = "&M^m";
		Liquid.ParentObject.Render.TileColor = "&Y";
		Liquid.ParentObject.Render.DetailColor = "O";
	}

	public override void BaseRenderSecondary(LiquidVolume Liquid)
	{
		Liquid.ParentObject.Render.ColorString += "&O";
	}

	public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
	{
		if (!Liquid.IsWadingDepth())
		{
			return;
		}
		if (Liquid.ParentObject.IsFrozen())
		{
			eRender.RenderString = "~";
			eRender.TileVariantColors("&y^M", "&Y", "O");
			return;
		}
		Render render = Liquid.ParentObject.Render;
		int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
		if (Stat.RandomCosmetic(1, 600) == 1)
		{
			eRender.RenderString = "~";
			eRender.TileVariantColors("&y^M", "&Y", "O");
		}
		if (Stat.RandomCosmetic(1, 60) == 1)
		{
			render.ColorString = "&M^m";
			render.TileColor = "&Y";
			render.DetailColor = "O";
			if (num < 15)
			{
				render.RenderString = "÷";
			}
			else if (num < 30)
			{
				render.RenderString = "~";
			}
			else if (num < 45)
			{
				render.RenderString = " ";
			}
			else
			{
				render.RenderString = "~";
			}
		}
	}

	public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
	{
		if (eRender.ColorsVisible)
		{
			eRender.ColorString += "&O";
		}
	}

	public override float GetValuePerDram()
	{
		return 0.01f;
	}

	public override float GetPureLiquidValueMultipler()
	{
		return 100f;
	}
}