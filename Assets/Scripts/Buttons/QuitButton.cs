using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    void Start()
    {
        _button.onClick.AddListener(GoToMenu);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}