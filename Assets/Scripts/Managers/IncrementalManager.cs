using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncrementalManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private TextMeshProUGUI _startCountUpgradeMoneyText, _startCountLevelText,_bulletCountUpgradeMoneyText,_bulletCountLevelText;
    [SerializeField] private TextMeshProUGUI _incomeUpgradeMoneyText, _incomeLevelText,_fireRateUpgradeMoneyText,_fireRateLevelText,_rangeUpgradeMoneyText,_rangeLevelText;

    private void Awake()
    {
#if !UNITY_EDITOR
        SaveManager.LoadData(_gameData);
#endif
        SetIncrementalText();
    }


    public void UpgradeStartCount()
    {
        if(_gameData.Money >= _gameData.StartCountUpgradeMoney)
        {
            _gameData.Money -= _gameData.StartCountUpgradeMoney;
            _gameData.StartCountIncrementalLevel++;
            _gameData.StartCountUpgradeMoney += 150;
            _gameData.StartCount++;
            MainDigit.instance._currentStartCount = _gameData.StartCount;
            MainDigit.instance.SetNumbers();
            SetIncrementalText();
            UIManager.instance.OnCollectMoney();

#if !UNITY_EDITOR
        SaveManager.SaveData(_gameData);
#endif
        }
    }

    public void UpgradeBulletCount()
    {
        if(_gameData.Money >= _gameData.BulletCountUpgradeMoney)
        {
            _gameData.Money -= _gameData.BulletCountUpgradeMoney;
            _gameData.BulletCountIncrementalLevel++;
            _gameData.BulletCountUpgradeMoney += 150;
            _gameData.BulletCount++;
            ObjectPool.instance.CreatePool();
            SetIncrementalText();
            UIManager.instance.OnCollectMoney();

#if !UNITY_EDITOR
        SaveManager.SaveData(_gameData);
#endif
        }
    }

    public void UpgradeIncome()
    {
        if(_gameData.Money >= _gameData.IncomeUpgradeMoney)
        {
            _gameData.Money -= _gameData.IncomeUpgradeMoney;
            _gameData.IncomeIncrementalLevel++;
            _gameData.IncomeUpgradeMoney += 150;
            _gameData.Income += 25;
            SetIncrementalText();
            UIManager.instance.OnCollectMoney();

#if !UNITY_EDITOR
        SaveManager.SaveData(_gameData);
#endif
        }
    }

    public void UpgradeFireRate()
    {
        if(_gameData.Money >= _gameData.FireRateUpgradeMoney)
        {
            _gameData.Money -= _gameData.FireRateUpgradeMoney;
            _gameData.FireRateIncrementalLevel++;
            _gameData.FireRateUpgradeMoney += 150;

            if (_gameData.FireRate > 0.01f)
            {
                _gameData.FireRate -= .02f;
            }
            else
            {
                _gameData.FireRate = .01f;
            }

            SetIncrementalText();
            UIManager.instance.OnCollectMoney();

#if !UNITY_EDITOR
        SaveManager.SaveData(_gameData);
#endif
        }
    }

    public void UpgradeRange()
    {
        if(_gameData.Money >= _gameData.RangeUpgradeMoney)
        {
            _gameData.Money -= _gameData.RangeUpgradeMoney;
            _gameData.RangeIncrementalLevel++;
            _gameData.RangeUpgradeMoney += 150;

             if(_gameData.Range < 60)
            {
                _gameData.Range++;
            }
            else
            {
                _gameData.Range = 60;
            }

            SetIncrementalText();
            UIManager.instance.OnCollectMoney();

#if !UNITY_EDITOR
        SaveManager.SaveData(_gameData);
#endif
        }
    }

    void SetIncrementalText()
    {
        _startCountLevelText.SetText("Lv " + _gameData.StartCountIncrementalLevel);
        _startCountUpgradeMoneyText.SetText(_gameData.StartCountUpgradeMoney.ToString());

        _bulletCountLevelText.SetText("Lv " + _gameData.BulletCountIncrementalLevel);
        _bulletCountUpgradeMoneyText.SetText(_gameData.BulletCountUpgradeMoney.ToString());

        _incomeLevelText.SetText("Lv " + _gameData.IncomeIncrementalLevel);
        _incomeUpgradeMoneyText.SetText(_gameData.IncomeUpgradeMoney.ToString());

        _fireRateLevelText.SetText("Lv " + _gameData.FireRateIncrementalLevel);
        _fireRateUpgradeMoneyText.SetText(_gameData.FireRateUpgradeMoney.ToString());

        _rangeLevelText.SetText("Lv " + _gameData.RangeIncrementalLevel);
        _rangeUpgradeMoneyText.SetText(_gameData.RangeUpgradeMoney.ToString());
    }
}
