using System;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace HUI_FastColInfo.Patch
{
	[HarmonyPatch(typeof(ColonistBarColonistDrawer), "HandleClicks")]
	internal static class Patch_ColonistBarColonistDrawer_HandleClicks
	{
		private static bool Prefix(Rect rect, Pawn colonist)
		{
			bool flag = Event.current.type == EventType.MouseUp && Event.current.button == 1 && Mouse.IsOver(rect);
			if (flag)
			{
				bool flag2 = !WorldRendererUtility.WorldRenderedNow;
				if (flag2)
				{
					bool flag3 = colonist != null && colonist.Dead && colonist.Corpse != null && colonist.Corpse.SpawnedOrAnyParentSpawned;
					Thing thing;
					if (flag3)
					{
						thing = colonist.Corpse;
					}
					else
					{
						thing = colonist;
					}
					bool flag4 = thing.Map == Find.CurrentMap;
					if (flag4)
					{
						FCIUtility.MakeFCIFloatMenu(new Action<InspectTabBase>(FCIUtility.ChooseMenuAction), thing);
					}
				}
				else
				{
					bool flag5 = colonist.IsCaravanMember();
					if (flag5)
					{
						Caravan caravan = colonist.GetCaravan();
						FCIUtility.MakeFCIFloatMenu(new Action<InspectTabBase>(FCIUtility.ChooseMenuAction), caravan);
					}
				}
			}
			return true;
		}
	}
}
