using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimeUpdateListener : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    private Sequence _sequence;

    private int _levelTime;

    IEnumerator Start()
    {
        _sequence = DOTween.Sequence();
        yield return new WaitUntil(() => LevelDataManager.Instance.IsInitialized);
        _levelTime = LevelDataManager.Instance.LevelData.LevelTime;
        StartCoroutine(TimeTextUpdate());
    }

    IEnumerator TimeTextUpdate()
    {
        while (true)
        {
            if (CheckCanCount())
            {
                if (_levelTime < 5)
                {
                    _sequence.Append(transform.DOScale(new Vector3(3f, 3f, 1), 0.3f)).Append(transform.DOScale(new Vector3(1,1,1), 0.3f));
                    _sequence.Play();
                }
                _timeText.text = "Time : " + _levelTime;
                _levelTime -= 1;

                if (_levelTime == 0)
                {
                    LevelStateManager.Instance.LevelLoseWithTime();
                }
            }
            
            yield return new WaitForSeconds(1f);
        }
    }

    private bool CheckCanCount()
    {
        return LevelStateManager.Instance.GameState == GameState.GamePlay;
    }
}
