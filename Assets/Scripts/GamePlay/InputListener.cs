using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InputListener : MonoBehaviour
{
    private LevelSpawnManager _levelSpawnManager;
    
    [SerializeField] private InputField _inputField;

    public event Action<Word> WordMatched;

    [Inject]
    void Construct(LevelSpawnManager levelSpawnManager)
    {
        _levelSpawnManager = levelSpawnManager;
    }

    private void Start()
    {
        _inputField.onValueChanged.AddListener(delegate { ControlWords(); });
        
        _inputField.ActivateInputField();
    }

    private void ControlWords()
    {
        foreach (var aliveWord in _levelSpawnManager.AliveWords)
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
