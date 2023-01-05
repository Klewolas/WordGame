using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public int ComboCount { get; set; }
    
    private static ComboManager _instance;
    public static ComboManager Instance => _instance;

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

    public void IncreaseCombo()
    {
        ComboCount++;
    }

    public void ResetCombo()
    {
        ComboCount = 1;
    }
}
