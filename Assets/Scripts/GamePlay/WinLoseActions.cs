using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinLoseActions : MonoBehaviour
{
    private PlayerDataManager _playerDataManager;
    private LevelStateManager _levelStateManager;
    
    [SerializeField] private GameObject uiObjectsParent;
    [SerializeField] private TMP_Text header;
    [SerializeField] private Image backGroundImage;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject restartButton;

    [SerializeField] private Color winColor;
    [SerializeField] private Color loseColor;

    [SerializeField] private float spawnDelayTime;

    [Inject]
    void Construct(PlayerDataManager playerDataManager, LevelStateManager levelStateManager)
    {
        _playerDataManager = playerDataManager;
        _levelStateManager = levelStateManager;
    }

    void Start()
    {
        _levelStateManager.LevelWin += GameWinActions;
        _levelStateManager.LevelLose += GameLoseActions;
    }

    private void OnDestroy()
    {
        _levelStateManager.LevelWin -= GameWinActions;
        _levelStateManager.LevelLose -= GameLoseActions;
    }

    void GameWinActions()
    {
        StartCoroutine(UpdateUIForWin());
        if (_playerDataManager.PlayerData.CurrentLevel >= _playerDataManager.PlayerData.LastOpenedLevel)
        {
            _playerDataManager.PlayerData.LastOpenedLevel++;
            _playerDataManager.PlayerData.CurrentLevel = _playerDataManager.PlayerData.LastOpenedLevel;
        }

        _playerDataManager.PlayerData.CurrentLevel++;
    }

    void GameLoseActions()
    {
        StartCoroutine(UpdateUIForLose());
    }

    IEnumerator UpdateUIForWin()
    {
        yield return new WaitForSeconds(spawnDelayTime);
        header.text = "Congratulations!!";
        backGroundImage.color = winColor;
        uiObjectsParent.SetActive(true);
    }

    IEnumerator UpdateUIForLose()
    {
        yield return new WaitForSeconds(spawnDelayTime);
        header.text = "You Lose!!";
        backGroundImage.color = loseColor;
        continueButton.SetActive(false);
        uiObjectsParent.SetActive(true);
    }
}