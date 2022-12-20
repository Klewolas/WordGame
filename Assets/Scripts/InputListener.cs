using System;
using UnityEngine;
using UnityEngine.UI;

public class InputListener : MonoBehaviour
{
    [SerializeField] private InputField _inputField;

    private void Start()
    {
        _inputField.onValueChanged.AddListener(delegate { ControlWords(); });
    }

    private void ControlWords()
    {
        foreach (var aliveWord in LevelSpawnManager.Instance.AliveWords)
        {
            if (String.Equals(aliveWord, _inputField.text, StringComparison.CurrentCultureIgnoreCase))
            {
                ScoreManager.Instance.IncreaseScore();
                LevelSpawnManager.Instance.AliveWords.Remove(aliveWord);
                _inputField.text = "";
                break;
            }
        }
    }
}
