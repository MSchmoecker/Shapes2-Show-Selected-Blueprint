using System.Collections.Generic;
using HarmonyLib;
using Localization;

namespace ShowSelectedBlueprint {
    [HarmonyPatch]
    public static class Patches {
        private static Dictionary<IBlueprint, string> blueprintToTitle = new Dictionary<IBlueprint, string>();

        [HarmonyPatch(typeof(HUDBlueprintDetails<IslandBlueprint>), nameof(HUDBlueprintDetails<IslandBlueprint>.SidePanel_GetTitle))]
        [HarmonyPatch(typeof(HUDBlueprintDetails<BuildingBlueprint>), nameof(HUDBlueprintDetails<BuildingBlueprint>.SidePanel_GetTitle))]
        [HarmonyPostfix]
        public static void HUDBlueprintDetails_GetTitle(HUDBlueprintDetails<IBlueprint> __instance, ref IText __result) {
            if (__instance.Blueprint != null && blueprintToTitle.TryGetValue(__instance.Blueprint, out string title)) {
                __result = new RawText(title);
            }
        }

        [HarmonyPatch(typeof(BlueprintLibraryEntry), MethodType.Constructor, new[] { typeof(string), typeof(string), typeof(IBlueprint) })]
        [HarmonyPostfix]
        public static void BlueprintLibraryEntry_Constructor(BlueprintLibraryEntry __instance) {
            if (__instance.Blueprint != null) {
                blueprintToTitle[__instance.Blueprint] = __instance.Title;
            }
        }

        [HarmonyPatch(typeof(BlueprintLibraryEntry), nameof(BlueprintLibraryEntry.SetBlueprint))]
        [HarmonyPostfix]
        public static void BlueprintLibraryEntry_SetBlueprint(BlueprintLibraryEntry __instance, IBlueprint blueprint) {
            if (blueprint != null) {
                blueprintToTitle[blueprint] = __instance.Title;
            }
        }

        [HarmonyPatch(typeof(BlueprintLibraryEntryBase), nameof(BlueprintLibraryEntryBase.Title), MethodType.Setter)]
        [HarmonyPostfix]
        public static void BlueprintLibraryEntryBase_Title(BlueprintLibraryEntryBase __instance, string value) {
            if (__instance is BlueprintLibraryEntry entry && entry.Blueprint != null) {
                blueprintToTitle[entry.Blueprint] = value;
            }
        }

        [HarmonyPatch(typeof(IslandBlueprint), nameof(IslandBlueprint.GetHashCode))]
        [HarmonyPrefix]
        public static bool IslandBlueprint_GetHashCode(IslandBlueprint __instance, ref int __result) {
            int hash = 17;
            hash = hash * 31 + __instance.Entries.GetHashCode();
            hash = hash * 31 + __instance.Icon.GetHashCode();
            hash = hash * 31 + __instance.ChunksCost.GetHashCode();
            hash = hash * 31 + __instance.BuildingCount.GetHashCode();
            __result = hash;
            return false;
        }

        [HarmonyPatch(typeof(BuildingBlueprint), nameof(BuildingBlueprint.GetHashCode))]
        [HarmonyPrefix]
        public static bool BuildingBlueprint_GetHashCode(BuildingBlueprint __instance, ref int __result) {
            int hash = 17;
            hash = hash * 31 + __instance.Entries.GetHashCode();
            hash = hash * 31 + __instance.Icon.GetHashCode();
            hash = hash * 31 + __instance.BinaryConfigDataVersion.GetHashCode();
            __result = hash;
            return false;
        }

        [HarmonyPatch(typeof(IslandBlueprint), nameof(IslandBlueprint.GenerateRotatedVariant))]
        [HarmonyPatch(typeof(IslandBlueprint), nameof(IslandBlueprint.GenerateMirroredVariantYAxis))]
        [HarmonyPatch(typeof(IslandBlueprint), nameof(IslandBlueprint.GenerateMirroredVariantXAxis))]
        [HarmonyPatch(typeof(BuildingBlueprint), nameof(BuildingBlueprint.GenerateRotatedVariant))]
        [HarmonyPatch(typeof(BuildingBlueprint), nameof(BuildingBlueprint.GenerateMirroredVariantYAxis))]
        [HarmonyPatch(typeof(BuildingBlueprint), nameof(BuildingBlueprint.GenerateMirroredVariantXAxis))]
        [HarmonyPostfix]
        public static void IslandBlueprint_GenerateVariant(IBlueprint __instance, IBlueprint __result) {
            if (__instance != null && __result != null && blueprintToTitle.TryGetValue(__instance, out string title)) {
                blueprintToTitle[__result] = title;
            }
        }
    }
}
