using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GameLoader : MonoBehaviour
{
    private PlayerDataManager _playerDataManager;
    
    [SerializeField] private Image _loadingBar;

    [Inject]
    void Construct(PlayerDataManager playerDataManager)
    {
        _playerDataManager = playerDataManager;
    }
    
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

        yield return new WaitUntil(() => _playerDataManager.IsPlayerDataReady);
        SceneManager.LoadScene("Menu");
    }

    private bool CheckManagersReady()
    {
        return _playerDataManager.IsPlayerDataReady;
    }
}