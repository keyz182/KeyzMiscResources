using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Verse;

namespace Keyz_Misc_Resources;

[StaticConstructorOnStartup]
public static class Materials
{
    private static Dictionary<string, Material> lookupMaterials;

    public static readonly Material ZoomMat = LoadMaterial(Path.Combine("Assets", "Shaders", "Unlit_ZoomShader.mat"));

    public static Material LoadMaterial(string materialName)
    {
        lookupMaterials ??= new Dictionary<string, Material>();
        if (!lookupMaterials.ContainsKey(materialName))
        {
            ModLog.Debug($"lookupMaterials: {lookupMaterials.ToList().Count}");
            lookupMaterials[materialName] = Keyz_Misc_ResourcesMod.mod.MainBundle.LoadAsset<Material>(materialName);
        }

        Material mat = lookupMaterials[materialName];
        if (mat == null)
        {
            ModLog.Warn($"Could not load material: {materialName}");
            return null;
        }

        if (mat != null)
        {
            ModLog.Debug($"Loaded Materials: {lookupMaterials.Count}");
        }

        return mat;
    }
}
