using UnityEngine;

public partial class SaveData
{
    private PlayerData _playerData;
    public PlayerData playerData
    {
        get
        {
            return _playerData ??= new PlayerData();
        }
        set
        {
            _playerData = value;
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public int score;
}