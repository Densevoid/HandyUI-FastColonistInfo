using Harmony;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace HUI_FastColInfo.Patch
{
    [HarmonyPatch(typeof(ColonistBarColonistDrawer), "HandleClicks")]
    static class Patch_ColonistBarColonistDrawer_HandleClicks
    {
        static bool Prefix(Rect rect, Pawn colonist)
        {
            if (Event.current.type == EventType.MouseUp && Event.current.button == 1 && Mouse.IsOver(rect))
            {
                if (!WorldRendererUtility.WorldRenderedNow)
                {
                    Thing colonistOrCorpse;
                    if (colonist != null && colonist.Dead && colonist.Corpse != null && colonist.Corpse.SpawnedOrAnyParentSpawned)
                    {
                        colonistOrCorpse = colonist.Corpse;
                    }
                    else
                    {
                        colonistOrCorpse = colonist;
                    }

                    if (colonistOrCorpse.Map == Find.CurrentMap)
                    {
                        FCIUtility.MakeFCIFloatMenu(FCIUtility.ChooseMenuAction, colonistOrCorpse);
                    }
                }

                else
                {
                    if (colonist.IsCaravanMember())
                    {
                        Caravan caravan = colonist.GetCaravan();
                        FCIUtility.MakeFCIFloatMenu(FCIUtility.ChooseMenuAction, caravan);
                    }
                }
}
            return true;
        }
    }
}
