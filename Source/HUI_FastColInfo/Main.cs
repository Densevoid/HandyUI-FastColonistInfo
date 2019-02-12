using System;
using System.Collections.Generic;
using Harmony;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Verse.Sound;

namespace HUI_FastColInfo
{
    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            HarmonyInstance harmony = HarmonyInstance.Create("rimworld.densevoid.hui.fastcolinf");
            harmony.PatchAll();
        }
    }

    public static class FCIUtility
    {
        public unsafe static void MakeFCIFloatMenu(Action<InspectTabBase> itemAction, ISelectable selObj)
        {
            if (selObj is Thing)
            {
                Find.Selector.ClearSelection();
                Find.Selector.Select(selObj, false, true);
            }
            else if (selObj is WorldObject)
            {
                Find.WorldSelector.ClearSelection();
                Find.WorldSelector.Select((WorldObject)selObj, false);
            }

            IEnumerable<InspectTabBase> tabsList = selObj.GetInspectTabs();
            List<FloatMenuOption> floatMenuOptions = new List<FloatMenuOption>();
            foreach (InspectTabBase tab in tabsList)
            {
                if (tab.IsVisible)
                {
                    floatMenuOptions.Add(new FloatMenuOption(tab.labelKey.Translate(), () => itemAction(tab)));
                }
            }
            floatMenuOptions.Reverse();
            Find.WindowStack.Add(new FloatMenu(floatMenuOptions));
        }

        public static void ChooseMenuAction(InspectTabBase tab)
        {
            tab.OnOpen();
            if (tab is ITab)
            {
                MainTabWindow_Inspect pane = (MainTabWindow_Inspect)Find.MainTabsRoot.OpenTab.TabWindow;
                pane.OpenTabType = tab.GetType();
            }
            else if (tab is WITab)
            {
                Find.World.UI.inspectPane.OpenTabType = tab.GetType();
            }
        }
    }

}