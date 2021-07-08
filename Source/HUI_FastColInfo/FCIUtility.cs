using System;
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace HUI_FastColInfo
{
	public static class FCIUtility
	{
		public static void MakeFCIFloatMenu(Action<InspectTabBase> itemAction, ISelectable selObj)
		{
			bool flag = selObj is Thing;
			if (flag)
			{
				Find.Selector.ClearSelection();
				Find.Selector.Select(selObj, false, true);
			}
			else
			{
				bool flag2 = selObj is WorldObject;
				if (flag2)
				{
					Find.WorldSelector.ClearSelection();
					Find.WorldSelector.Select((WorldObject)selObj, false);
				}
			}
			IEnumerable<InspectTabBase> inspectTabs = selObj.GetInspectTabs();
			List<FloatMenuOption> list = new List<FloatMenuOption>();
			using (IEnumerator<InspectTabBase> enumerator = inspectTabs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					InspectTabBase tab = enumerator.Current;
					bool isVisible = tab.IsVisible;
					if (isVisible)
					{
						list.Add(new FloatMenuOption(Translator.Translate(tab.labelKey), delegate()
						{
							itemAction(tab);
						}, MenuOptionPriority.Default, null, null, 0f, null, null));
					}
				}
			}
			list.Reverse();
			Find.WindowStack.Add(new FloatMenu(list));
		}

		public static void ChooseMenuAction(InspectTabBase tab)
		{
			bool flag = tab is ITab;
			if (flag)
			{
				InspectPaneUtility.OpenTab(tab.GetType());
			}
			else
			{
				bool flag2 = tab is WITab;
				if (flag2)
				{
					Find.MainTabsRoot.EscapeCurrentTab(false);
					tab.OnOpen();
					Find.World.UI.inspectPane.OpenTabType = tab.GetType();
				}
			}
		}
	}
}
