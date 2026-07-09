using TMPro;
using UnityEngine;
using System.IO;

public class TEMPSceneLogic : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txtScoreAmount;
    [SerializeField]
    private TMP_Text txtSaveSystemVersion;

    private void Start()
    {
        UpdateUI();
        txtSaveSystemVersion.text = SaveData.current.saveSystemVersion;
    }

    public void OnDopamineClick()
    {
        SaveData.current.playerData.score += 1;
        UpdateUI();
    }

    public void OnSaveClick()
    {
        SerializationManager.Save<SaveData>("DEMO", SaveData.current);
    }

    public void UpdateUI()
    {
        txtScoreAmount.text = $"Score: {SaveData.current.playerData.score}";
    }

    public void OnLoadClick()
    {
        SaveData.current = SerializationManager.Load<SaveData>("DEMO");
        UpdateUI();
    }
}
