using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManager
{
    private const bool USE_CRYPTOGRAPHY = false;
    public static bool Save(string saveName, object saveData)
    {
        string saveDataDirectory = Path.Combine(Application.persistentDataPath, "saves");

        BinaryFormatter formatter = GetBinaryFormatter();

        if(!Directory.Exists(saveDataDirectory))
        {
            Directory.CreateDirectory(saveDataDirectory);
        }

        string path = Path.Combine(saveDataDirectory, $"{saveName}.sav");

        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();
        return true;
    }

    public static object Load(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogError($"Failed to load file at {path}");
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        return formatter;
    }
}
