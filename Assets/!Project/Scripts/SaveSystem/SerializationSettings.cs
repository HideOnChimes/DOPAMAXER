using UnityEngine;
using System.IO;

public class SerializationSettings : ScriptableObject
{
    [SerializeField] private bool usePersistentDataPath = true;
    [SerializeField] private bool useCryptography = false;
    public bool UseCryptography => useCryptography;
    [SerializeField] private string saveGameDirName = "saves";

    private static SerializationSettings _instance;
    public static SerializationSettings instance
    {
        get
        {
            _instance ??= Resources.Load<SerializationSettings>("SerializationSettings");

            if(_instance == null)
            {
                _instance = CreateInstance<SerializationSettings>();
                #if UNITY_EDITOR
                string folderPath = "Assets/Resources";
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                string assetPath = Path.Combine(folderPath, "SerializationSettings.asset");
                UnityEditor.AssetDatabase.CreateAsset(instance, assetPath);
                UnityEditor.AssetDatabase.SaveAssets();
                #endif
            }
            return _instance;
        }
    }

    public static string GetSaveDirectory()
    {
        string basePath = instance.usePersistentDataPath ? Application.persistentDataPath : Application.persistentDataPath; //TODO: Create an enum for persistentDataPath / dataPath / customPath, with custom editor to type custom save path

        return Path.Combine(basePath, instance.saveGameDirName);
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Assets/Create/Save System/Serialization Settings")]
    public static void CreateSettingsFile()
    {
        SerializationSettings asset = Resources.Load<SerializationSettings>("SerializationSettings");
        if (asset != null)
        {
            Debug.LogWarning("Project already contains a SerializationSettings.asset");
            UnityEditor.EditorApplication.delayCall += () =>
            {
                UnityEditor.EditorUtility.FocusProjectWindow();
                UnityEditor.Selection.activeObject = asset;
                UnityEditor.EditorGUIUtility.PingObject(asset);
            };

            return;
        }

        string clickedPath = "Assets";
        if(UnityEditor.Selection.activeObject != null)
        {
            clickedPath = UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeObject);
            if (!Directory.Exists(clickedPath))
            {
                clickedPath = Path.GetDirectoryName(clickedPath).Replace('\\', '/');
            }
        }

        string finalFolder = clickedPath;
        if (!finalFolder.EndsWith("Resources"))
        {
            finalFolder = Path.Combine(finalFolder, "Resources");

            if (!Directory.Exists(finalFolder))
            {
                Directory.CreateDirectory(finalFolder);
                UnityEditor.AssetDatabase.Refresh();
            }
        }

        string assetPath = Path.Combine(finalFolder, "SerializationSettings.asset");
        if (File.Exists(assetPath))
        {
            return;
        }

        SerializationSettings newAsset = CreateInstance<SerializationSettings>();
        UnityEditor.AssetDatabase.CreateAsset(newAsset, assetPath);
        UnityEditor.AssetDatabase.SaveAssets();

        UnityEditor.EditorUtility.FocusProjectWindow();
        UnityEditor.Selection.activeObject = newAsset;
    }

    private void OnEnable()
    {
        if (Application.isPlaying) return;
        UnityEditor.EditorApplication.delayCall += () =>
        {
            if (this == null) return;
            string path = UnityEditor.AssetDatabase.GetAssetPath(this);

            if (!string.IsNullOrEmpty(path) && !path.EndsWith("Resources/SerializationSettings.asset"))
            {
                UnityEditor.AssetDatabase.DeleteAsset(path);
            }
        };
    }
#endif
}
