using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SerializationManager
{
    public static bool Save<T>(string saveName, T saveData)
    {
        string saveDataDirectory = SerializationSettings.GetSaveDirectory();

        if (!Directory.Exists(saveDataDirectory))
        {
            Directory.CreateDirectory(saveDataDirectory);
        }

        string path = Path.Combine(saveDataDirectory, $"{saveName}.sav");

        string json = JsonConvert.SerializeObject(saveData, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});

        if (SerializationSettings.instance.UseCryptography) json = EncryptionUtility.EncryptString(json);
        File.WriteAllText(path, json);

        return true;
    }

    public static T Load<T>(string saveName, string path = null)
    {
        path ??= SerializationSettings.GetSaveDirectory();
        path = Path.Combine(path, $"{saveName}.sav");
        if (!File.Exists(path))
        {
            return default;
        }

        try
        {
            string data = File.ReadAllText(path);
            if (SerializationSettings.instance.UseCryptography) data = EncryptionUtility.DecryptString(data);
            T save = JsonConvert.DeserializeObject<T>(data);
            return save;
        }
        catch
        {
            return default;
        }
        
    }
}
