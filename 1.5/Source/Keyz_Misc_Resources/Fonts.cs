using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Verse;

namespace Keyz_Misc_Resources;

[StaticConstructorOnStartup]
public static class Fonts
{
    private static AssetBundle bundleInt;
    private static Dictionary<string, Font> lookupFonts;

    public static readonly Font OptimusPrinceps = LoadFont(Path.Combine("Assets", "Fonts", "OptimusPrinceps.ttf"));
    public static readonly Font OptimusPrincepsSemiBold = LoadFont(Path.Combine("Assets", "Fonts", "OptimusPrinceps.ttf"));

    public static AssetBundle AssetBundle
    {
        get
        {
            if (bundleInt != null)
            {
                return bundleInt;
            }

            bundleInt = Keyz_Misc_ResourcesMod.mod.MainBundle;
            ModLog.Debug($"bundleInt: {bundleInt.name}");

            return bundleInt;
        }
    }

    public static Font LoadFont(string fontName)
    {
        lookupFonts ??= new Dictionary<string, Font>();
        if (!lookupFonts.ContainsKey(fontName))
        {
            ModLog.Debug($"lookupFonts: {lookupFonts.ToList().Count}");
            lookupFonts[fontName] = AssetBundle.LoadAsset<Font>(fontName);
        }

        Font font = lookupFonts[fontName];
        if (font == null)
        {
            ModLog.Warn($"Could not load font: {fontName}");
            return (Font) Resources.Load("Fonts/Arial_medium");
        }

        if (font != null)
        {
            ModLog.Debug($"Loaded font: {lookupFonts.Count}");
        }

        return font;
    }
}
