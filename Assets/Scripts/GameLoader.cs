using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Image _loadingBar;

    private void Start()
    {
        StartCoroutine(LoadingBarFill());
    }

    private IEnumerator LoadingBarFill()
    {
        int barFillComplete = 0;
        while (barFillComplete < 4)
        {
            yield return new WaitForSeconds(1f);
            _loadingBar.fillAmount += 0.25f;
            barFillComplete++;
        }

        yield return new WaitUntil(() => PlayerDataManager.Instance.IsPlayerDataReady);
        SceneManager.LoadScene("Menu");
    }

    private bool CheckManagersReady()
    {
        return PlayerDataManager.Instance.IsPlayerDataReady;
    }
}