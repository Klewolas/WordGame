using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimeUpdateListener : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    private Sequence _sequence;

    private int _cacheTime;

    void Start()
    {
        LevelStateManager.TimeUpdate += TimeTextUpdate;
        _sequence = DOTween.Sequence();
    }

    private void OnDestroy()
    {
        LevelStateManager.TimeUpdate -= TimeTextUpdate;
    }

    void TimeTextUpdate(int time)
    {
        if (_cacheTime == time)
            return;
        _cacheTime = time;
        if (time < 5)
        {
            _sequence.Append(transform.DOScale(new Vector3(3f, 3f, 1), 0.3f)).Append(transform.DOScale(new Vector3(1,1,1), 0.3f));
            _sequence.Play();
        }
        _timeText.text = "Time : " + time;
    }
}
