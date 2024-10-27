using RimWorld;
using Verse;

namespace Keyz_Misc_Resources;

[DefOf]
public static class Keyz_Misc_ResourcesDefOf
{
    public static ShaderTypeDef KMR_LiquidMetal;
    public static ShaderTypeDef KMR_LiquidMetalSimplex;
    public static ShaderTypeDef KMR_LiquidMetalCracked;
    public static ShaderTypeDef KMR_BlackHole;
    public static ShaderTypeDef KMR_Shield;
    public static ShaderTypeDef KMR_Swirl;
    public static ShaderTypeDef KMR_ZoomShader;

    static Keyz_Misc_ResourcesDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(Keyz_Misc_ResourcesDefOf));
}
