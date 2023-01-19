using System;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    private int _spentLife;
    
    private static LifeManager _instance;
    public static LifeManager Instance => _instance;
    
    public static event Action<int> LoseLife;

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
    }

    private void Start()
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
