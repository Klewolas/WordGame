using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LifeUpdateListener : MonoBehaviour
{
    [SerializeField] private TMP_Text _lifeText;
    private Sequence _sequence;

    private int _levelLife;

    IEnumerator Start()
    {
        _sequence = DOTween.Sequence();
        yield return new WaitUntil(() => LevelDataManager.Instance.IsInitialized);
        _levelLife = LevelDataManager.Instance.LevelData.Life;
        
        _lifeText.text = "Life : " + _levelLife;

        LifeManager.LoseLife += LifeTextUpdate;
    }

    private void OnDestroy()
    {
        LifeManager.LoseLife -= LifeTextUpdate;
    }

    void LifeTextUpdate(int spentLife)
    {
        _sequence.Append(transform.DOScale(new Vector3(3f, 3f, 1), 0.3f)).Append(transform.DOScale(new Vector3(1, 1, 1), 0.3f));
        _sequence.Play();

        _lifeText.text = "Life : " + (_levelLife - spentLife);
    }
}