using TMPro;
using UnityEngine;
using Zenject;

public class Word : MonoBehaviour
{
    private ComboManager _comboManager;
    private LevelSpawnManager _levelSpawnManager;
    private LifeManager _lifeManager;
    
    [Inject]
    void Construct(ComboManager comboManager, LevelSpawnManager levelSpawnManager, LifeManager lifeManager)
    {
        _comboManager = comboManager;
        _levelSpawnManager = levelSpawnManager;
        _lifeManager = lifeManager;

        //TODO - When this line deleted words instantiate with 196 scale. Try to fix this. (Is about IInstantiator InstantiatePrefab.
        transform.localScale = new Vector3(1, 1, 1);
    }
    
    public string AliveWord;
    [SerializeField] private TMP_Text _wordText;
    public float wordVelocity;
    
    private void LateUpdate()
    {
        var position = transform.position;
        transform.position = new Vector3(position.x, position.y - wordVelocity, position.z);
        if (transform.position.y < -6)
        {
            _levelSpawnManager.Pool.Release(this);
            _comboManager.ResetCombo();
            _lifeManager.IncreaseSpentLife();
        }
    }

    public void SetText(string aliveWord)
    {
        AliveWord = aliveWord;
        _wordText.text = aliveWord;
    }
}