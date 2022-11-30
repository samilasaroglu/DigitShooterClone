using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameData _gameData;
    [SerializeField] private TextMeshProUGUI _moneyText,_levelText;
    [SerializeField] private GameObject _menuPanel,_losePanel,_winPanel,_incrementalPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        OnCollectMoney();
        SetLevelText();
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnCollectMoney, OnCollectMoney);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnCollectMoney, OnCollectMoney);
    }


    public void OpenWinPanel()
    {
        StartCoroutine(OnWinPanel());
    }

    public void MenuOff()
    {
        _menuPanel.SetActive(false);
    }

    public void OnCollectMoney()
    {
        _moneyText.SetText(_gameData.Money.ToString());

#if !UNITY_EDITOR
        SaveManager.SaveData(_gameData);
#endif
    }

    public void Lose()
    {
        StartCoroutine(OnLosePanel());
    }

    public void SetLevelText()
    {
        _levelText.SetText("Level "+_gameData.LevelIndex);
    }

    IEnumerator OnWinPanel()
    {
        yield return new WaitForSeconds(1.5f);

        _winPanel.SetActive(true);
        _incrementalPanel.SetActive(true);
    }

    IEnumerator OnLosePanel()
    {
        yield return new WaitForSeconds(1.5f);

        _losePanel.SetActive(true);
        _incrementalPanel.SetActive(true);
    }
}
