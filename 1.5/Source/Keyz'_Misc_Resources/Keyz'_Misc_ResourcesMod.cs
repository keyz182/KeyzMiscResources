using Verse;
using UnityEngine;
using HarmonyLib;

namespace Keyz'_Misc_Resources;

public class Keyz'_Misc_ResourcesMod : Mod
{
    public static Settings settings;

    public Keyz'_Misc_ResourcesMod(ModContentPack content) : base(content)
    {
        Log.Message("Hello world from Keyz' Misc Resources");

        // initialize settings
        settings = GetSettings<Settings>();
#if DEBUG
        Harmony.DEBUG = true;
#endif
        Harmony harmony = new Harmony("keyz182.rimworld.Keyz'_Misc_Resources.main");	
        harmony.PatchAll();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        settings.DoWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "Keyz' Misc Resources_SettingsCategory".Translate();
    }
}
