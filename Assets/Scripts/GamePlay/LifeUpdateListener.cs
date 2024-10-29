using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class LifeUpdateListener : MonoBehaviour
{
    private LevelDataManager _levelDataManager;
    private LifeManager _lifeManager;
    
    [SerializeField] private TMP_Text _lifeText;
    private Sequence _sequence;

    private int _levelLife;

    [Inject]
    void Construct(LevelDataManager levelDataManager, LifeManager lifeManager)
    {
        _levelDataManager = levelDataManager;
        _lifeManager = lifeManager;
    }

    IEnumerator Start()
    {
        _sequence = DOTween.Sequence();
        yield return new WaitUntil(() => _levelDataManager.IsInitialized);
        _levelLife = _levelDataManager.LevelData.Life;
        
        _lifeText.text = "Life : " + _levelLife;

        _lifeManager.LoseLife += LifeTextUpdate;
    }

    private void OnDestroy()
    {
        _lifeManager.LoseLife -= LifeTextUpdate;
    }

    void LifeTextUpdate(int spentLife)
    {
        _sequence.Append(transform.DOScale(new Vector3(3f, 3f, 1), 0.3f)).Append(transform.DOScale(new Vector3(1, 1, 1), 0.3f));
        _sequence.Play();

        _lifeText.text = "Life : " + (_levelLife - spentLife);
    }
}