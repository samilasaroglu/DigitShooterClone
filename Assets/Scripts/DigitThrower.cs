using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DigitThrower : MonoBehaviour
{
    public static DigitThrower instance;
    public float _currentRange, _currentFireRate;

    [SerializeField] private GameData _gameData;
    private bool _isFinish,_isLose,_isGameStart;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameStart, OnGameStart);
        EventManager.AddHandler(GameEvent.OnFinish, OnFinish);
        EventManager.AddHandler(GameEvent.OnLose, OnLose);
        EventManager.AddHandler(GameEvent.OnCollisionBarrel,OnCollisionBarrel );
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameStart, OnGameStart);
        EventManager.RemoveHandler(GameEvent.OnFinish, OnFinish);
        EventManager.RemoveHandler(GameEvent.OnLose, OnLose);
        EventManager.RemoveHandler(GameEvent.OnCollisionBarrel, OnCollisionBarrel);

    }

    private void Start()
    {
        StartCoroutine(DigitThrow());

        _currentRange = _gameData.Range;
        _currentFireRate = _gameData.FireRate;
    }

    void OnFinish()
    {
        _isFinish = true;
    }

    void OnLose()
    {
        _isLose = true;
    }

    void OnCollisionBarrel()
    {
        _isLose = true;
    }

    void OnGameStart()
    {
        _isGameStart = true;
    }


    IEnumerator DigitThrow()
    {
        if (!_isFinish && !_isLose)
        {
            if (_isGameStart)
            {
                GameObject Digit = ObjectPool.instance.GetDigitObject();
                Digit.transform.position = transform.position;
                Digit.transform.DOMoveZ(Digit.transform.position.z + _currentRange, _currentRange / 10).OnPlay(() => StartCoroutine(Destroy(Digit)));

            }

            yield return new WaitForSeconds(_currentFireRate);
            StartCoroutine(DigitThrow());
        }

    }

    IEnumerator Destroy(GameObject throwableDigit)
    {
        yield return new WaitForSeconds(_currentRange / 25);
        throwableDigit.SetActive(false);
    }
}
