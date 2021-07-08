using System;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace HUI_FastColInfo
{
	[StaticConstructorOnStartup]
	internal static class HarmonyPatches
	{
		static HarmonyPatches()
		{
			Harmony harmonyInstance = new Harmony("rimworld.densevoid.hui.fastcolinf");
			harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
		}
	}
}
