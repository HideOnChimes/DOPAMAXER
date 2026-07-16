using TMPro;
using UnityEngine;

public class ManagerUI : MonoSingleton<ManagerUI>
{
    [SerializeField] private TMP_Text dopamineLabel;

    protected override void StartBehaviour()
    {
        ManagerGame.Instance.OnDopamineChanged += UpdateDopamine;
        UpdateDopamine(ManagerGame.Instance.Dopamine);
    }

    public void UpdateDopamine(float value) => dopamineLabel.text = $"Dopamine: {value}";
}
