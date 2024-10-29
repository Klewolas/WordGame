using System;
using UnityEngine;

public class ComboManager
{
    public int ComboCount { get; private set; } = 1;

    public event Action<int> ComboChanged;

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
