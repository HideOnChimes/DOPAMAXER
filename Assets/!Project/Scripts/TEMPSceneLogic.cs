using TMPro;
using UnityEngine;
using System.IO;

public class TEMPSceneLogic : MonoBehaviour
{
    [SerializeField]
    private TMP_Text dopamineAmount;
    public void OnDopamineClick()
    {
        SaveData.current.playerData.dopamine += 1;
        UpdateUI();
    }

    public void OnSaveClick()
    {
        SerializationManager.Save("test", SaveData.current);
    }

    public void UpdateUI()
    {
        dopamineAmount.text = $"Dopamines: {SaveData.current.playerData.dopamine}";
    }

    public void OnLoadClick()
    {
        SaveData.current = (SaveData)SerializationManager.Load(Path.Combine(Application.persistentDataPath, "saves", "test.sav"));
        UpdateUI();
    }
}
