using UnityEngine;

[System.Serializable]
public partial class SaveData
{
    private static SaveData _current;
    public static SaveData current => _current ??= new SaveData();
}
