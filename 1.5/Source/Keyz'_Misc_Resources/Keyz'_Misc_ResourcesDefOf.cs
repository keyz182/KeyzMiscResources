using RimWorld;
using Verse;

namespace Keyz'_Misc_Resources;

[DefOf]
public static class Keyz'_Misc_ResourcesDefOf
{
    // Remember to annotate any Defs that require a DLC as needed e.g.
    // [MayRequireBiotech]
    // public static GeneDef YourPrefix_YourGeneDefName;
    
    static Keyz'_Misc_ResourcesDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(Keyz'_Misc_ResourcesDefOf));
}
