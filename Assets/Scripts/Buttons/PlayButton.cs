using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    void Start()
    {
        _button.onClick.AddListener(PlayGame);
    }
    
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
}
