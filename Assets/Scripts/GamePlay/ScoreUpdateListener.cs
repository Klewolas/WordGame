using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoreUpdateListener : MonoBehaviour
{
    private ScoreManager _scoreManager;
    
    [SerializeField] private TMP_Text _scoreText;
    private Sequence _sequence;

    [Inject]
    void Construct(ScoreManager scoreManager)
    {
        _scoreManager = scoreManager;
    }
    
    void Start()
    {
        _scoreManager.ScoreIncreased += ScoreTextUpdate;
        _sequence = DOTween.Sequence();
    }

    private void OnDestroy()
    {
        _scoreManager.ScoreIncreased -= ScoreTextUpdate;
    }

    void ScoreTextUpdate(int score)
    {
        _sequence.Append(transform.DOScale(new Vector3(3f, 3f, 1), 0.3f)).Append(transform.DOScale(new Vector3(1,1,1), 0.3f));
        _sequence.Play();
        _scoreText.text = "Score : " + score;
    }
}
