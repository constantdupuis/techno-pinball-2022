using System.Linq;
using UnityEditor;

namespace Mo.Helpers
{ 

#if UNITY_EDITOR
public static class SceneHelper
{
    public static string GetSceneAssetPath(SceneAsset sceneAsset)
    {
        return AssetDatabase.GetAssetPath(sceneAsset);
    }

    public static void EnableSceneInBuildSettings(SceneAsset sceneAsset, bool enable = true)
    {
        bool sceneFound = false;
        var scenePath = GetSceneAssetPath(sceneAsset);
        foreach (var buildSceneSetting in EditorBuildSettings.scenes)
        {
            if (buildSceneSetting.path == scenePath) 
            {
                sceneFound = true;
                buildSceneSetting.enabled = enable;
                break;
            }
        }
        if (!sceneFound)
        {
            var newSceneBuildSettings = new EditorBuildSettingsScene(scenePath, enable);
            EditorBuildSettings.scenes = EditorBuildSettings.scenes.Concat(new EditorBuildSettingsScene[] { newSceneBuildSettings }).ToArray();
        }
    }

    public static void EnableScenesInBuildSettings(SceneAsset[] scenesAsset, bool enable = true)
    {
        foreach (var sceneAsset in scenesAsset)
        {
            EnableSceneInBuildSettings(sceneAsset, enable);
        }
    }

    public static void RemovaAllScenesFromBuildSettings()
    {
        EditorBuildSettings.scenes = new EditorBuildSettingsScene[0];
    }
}
#endif

}

