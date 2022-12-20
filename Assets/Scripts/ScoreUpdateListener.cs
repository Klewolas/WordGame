using TMPro;
using UnityEngine;

public class ScoreUpdateListener : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    void Start()
    {
        ScoreManager.ScoreIncreased += ScoreTextUpdate;
    }

    private void OnDestroy()
    {
        ScoreManager.ScoreIncreased -= ScoreTextUpdate;
    }

    void ScoreTextUpdate(int score)
    {
        _scoreText.text = "Score : " + score;
    }
}
