using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    void Start()
    {
        _button.onClick.AddListener(RestartGame);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
