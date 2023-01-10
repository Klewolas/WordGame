using System;
using UnityEngine;
using UnityEngine.UI;

public class InputListener : MonoBehaviour
{
    [SerializeField] private InputField _inputField;

    public static event Action<Word> WordMatched;

    private void Start()
    {
        _inputField.onValueChanged.AddListener(delegate { ControlWords(); });
    }

    private void ControlWords()
    {
        foreach (var aliveWord in LevelSpawnManager.Instance.AliveWords)
        {
            if (String.Equals(aliveWord.AliveWord, _inputField.text, StringComparison.CurrentCultureIgnoreCase))
            {
                WordMatched?.Invoke(aliveWord);
                _inputField.text = "";
                break;
            }
        }
    }
}
