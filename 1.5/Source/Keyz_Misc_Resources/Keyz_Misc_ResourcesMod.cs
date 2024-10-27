using System.IO;
using System.Runtime.InteropServices;
using Verse;
using UnityEngine;
using HarmonyLib;

namespace Keyz_Misc_Resources;

public class Keyz_Misc_ResourcesMod : Mod
{
    public static Keyz_Misc_ResourcesMod mod;

    public Keyz_Misc_ResourcesMod(ModContentPack content) : base(content)
    {
        mod = this;

#if DEBUG
        Harmony.DEBUG = true;
#endif
        Harmony harmony = new Harmony("keyz182.rimworld.Keyz'_Misc_Resources.main");
        harmony.PatchAll();
    }

    public AssetBundle bundleInt;

    public AssetBundle MainBundle
    {
        get
        {
            if(bundleInt != null) return bundleInt;

            string text = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                text = "StandaloneOSX";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                text = "StandaloneWindows64";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                text = "StandaloneLinux64";
            }

            string bundlePath = Path.Combine(Content.RootDir, $@"Materials\{text}\keyzmiscresources");
            Log.Message("Bundle Path: " + bundlePath);

            AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);

            if (bundle == null)
            {
                ModLog.Error("Failed to load bundle at path: " + bundlePath);
            }

            foreach (string allAssetName in bundle.GetAllAssetNames())
            {
                ModLog.Debug($"[{allAssetName}]");
            }

            bundleInt = bundle;

            return bundleInt;
        }
    }
}
