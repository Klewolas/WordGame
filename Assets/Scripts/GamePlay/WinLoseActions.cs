using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseActions : MonoBehaviour
{
    [SerializeField] private GameObject uiObjectsParent;
    [SerializeField] private TMP_Text header;
    [SerializeField] private Image backGroundImage;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject restartButton;

    [SerializeField] private Color winColor;
    [SerializeField] private Color loseColor;

    [SerializeField] private float spawnDelayTime;

    void Start()
    {
        LevelStateManager.LevelWin += GameWinActions;
        LevelStateManager.LevelLose += GameLoseActions;
    }

    private void OnDestroy()
    {
        LevelStateManager.LevelWin -= GameWinActions;
        LevelStateManager.LevelLose -= GameLoseActions;
    }

    void GameWinActions()
    {
        StartCoroutine(UpdateUIForWin());
        if (PlayerDataManager.Instance.PlayerData.CurrentLevel >= PlayerDataManager.Instance.PlayerData.LastOpenedLevel)
        {
            PlayerDataManager.Instance.PlayerData.LastOpenedLevel++;
            PlayerDataManager.Instance.PlayerData.CurrentLevel = PlayerDataManager.Instance.PlayerData.LastOpenedLevel;
        }

        PlayerDataManager.Instance.PlayerData.CurrentLevel++;
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