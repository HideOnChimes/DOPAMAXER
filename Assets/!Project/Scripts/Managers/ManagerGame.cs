using UnityEngine;

public class ManagerGame : MonoSingleton<ManagerGame>
{
    public event System.Action<float> OnDopamineChanged;

    public float Dopamine => SaveData.current.playerData.dopamine;

    public void AddDopamine(float amount)
    {
        SaveData.current.playerData.dopamine += amount;
        OnDopamineChanged?.Invoke(Dopamine);
    }
}
