using System;
using UnityEngine;

public class LifeManager
{
    private int _spentLife;
    
    public event Action<int> LoseLife;

    public LifeManager()
    {
        ResetLife();
    }

    private void ResetLife()
    {
        _spentLife = 0;
    }

    public void IncreaseSpentLife()
    {
        _spentLife++;
        LoseLife?.Invoke(_spentLife);
    }
}
