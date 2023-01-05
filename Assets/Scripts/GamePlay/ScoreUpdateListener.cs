using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreUpdateListener : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private Sequence _sequence;

    void Start()
    {
        ScoreManager.ScoreIncreased += ScoreTextUpdate;
        _sequence = DOTween.Sequence();
    }

    private void OnDestroy()
    {
        ScoreManager.ScoreIncreased -= ScoreTextUpdate;
    }

    void ScoreTextUpdate(int score)
    {
        _sequence.Append(transform.DOScale(new Vector3(3f, 3f, 1), 0.3f)).Append(transform.DOScale(new Vector3(1,1,1), 0.3f));
        _sequence.Play();
        _scoreText.text = "Score : " + score;
    }
}
