using System;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public int ComboCount { get; set; }
    
    private static ComboManager _instance;
    public static ComboManager Instance => _instance;
    public static event Action<int> ComboChanged;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }

        ComboCount = 1;
    }

    public void IncreaseCombo()
    {
        ComboCount++;
        ComboChanged?.Invoke(ComboCount);
    }

    public void ResetCombo()
    {
        ComboCount = 1;
        ComboChanged?.Invoke(ComboCount);
    }
}
