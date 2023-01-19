using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    void Start()
    {
        _button.onClick.AddListener(Continue);
    }
    
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void Continue()
    {
        SceneManager.LoadScene("Game");
    }
}
