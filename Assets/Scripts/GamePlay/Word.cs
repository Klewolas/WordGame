using TMPro;
using UnityEngine;

public class Word : MonoBehaviour
{
    public string AliveWord;
    [SerializeField] private TMP_Text _wordText;
    public float wordVelocity;
    
    private void LateUpdate()
    {
        var position = transform.position;
        transform.position = new Vector3(position.x, position.y - wordVelocity, position.z);
        if (transform.position.y < -6)
        {
            LevelSpawnManager.Instance.Pool.Release(this);
            ComboManager.Instance.ResetCombo();
            LifeManager.Instance.IncreaseSpentLife();
        }
    }

    public void SetText(string aliveWord)
    {
        AliveWord = aliveWord;
        _wordText.text = aliveWord;
    }
}