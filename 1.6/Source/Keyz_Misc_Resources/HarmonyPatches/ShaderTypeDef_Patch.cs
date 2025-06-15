using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace Keyz_Misc_Resources.HarmonyPatches;

[HarmonyPatch(typeof(ShaderTypeDef))]
public static class ShaderTypeDef_Patch
{
    public static Lazy<FieldInfo> ShaderIntFI = new Lazy<FieldInfo>(() => AccessTools.Field(typeof(ShaderTypeDef), "shaderInt"));

    [HarmonyPatch(nameof(ShaderTypeDef.Shader), MethodType.Getter)]
    [HarmonyPrefix]
    public static bool Shader(ShaderTypeDef __instance, ref Shader __result)
    {
        if (!__instance.HasModExtension<KMRDefModExtension>()) return true;

        if (ShaderIntFI.Value.GetValue(__instance) is null)
        {
            ShaderIntFI.Value.SetValue(__instance, Shaders.LoadShader(__instance.shaderPath));
        }

        return true;
    }
}
