using System.IO;
using UnityEditor;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;


public class CreateAssetBundles
{
    [MenuItem("Assets/Get AssetBundle names")]
    static void GetNames()
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        foreach (var name in names)
        {
            Debug.Log("AssetBundle: " + name);
            var paths = AssetDatabase.GetAssetPathsFromAssetBundle(name);
            foreach (var path in paths)
            {
                Debug.Log("Path: " + path);
            }
        }
    }

    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string path = Path.Combine("..", "1.6", "Materials");

        if (!Directory.Exists(path)) Directory.CreateDirectory(path);


        string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle("keyzmiscresources");


        AssetBundleBuild[] bundles = { new() { assetBundleName = "keyzmiscresources_linux", assetNames = assetPaths } };
        BuildPipeline.BuildAssetBundles(path, bundles, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneLinux64);

        bundles[0] =
            new() { assetBundleName = "keyzmiscresources_mac", assetNames = assetPaths };
        BuildPipeline.BuildAssetBundles(path, bundles, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneOSX);


        bundles[0] =
            new() { assetBundleName = "keyzmiscresources_windows", assetNames = assetPaths };
        BuildPipeline.BuildAssetBundles(path, bundles, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
    }
}
