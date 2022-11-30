using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/Game Data")]
public class GameData : ScriptableObject
{

    [Header("Player Controls")]
    public float ForwardSpeed = 5f;
    public float Sensivity = 0.005f;


    [Header("InGame Settings")]
    public int TotalLevelCount;
    public int LevelIndex;
    public int Money;
    public int StartCount;
    public int BulletCount;
    public int Income;
    public float FireRate;
    public float Range;
    public float BestScore;


    [Header("Incremental Settings")]

    public int IncomeUpgradeMoney;
    public int IncomeIncrementalLevel;
    public int StartCountUpgradeMoney;
    public int StartCountIncrementalLevel;
    public int BulletCountUpgradeMoney;
    public int BulletCountIncrementalLevel;
    public int FireRateUpgradeMoney;
    public int FireRateIncrementalLevel;
    public int RangeUpgradeMoney;
    public int RangeIncrementalLevel;


}
