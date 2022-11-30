using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDigitMovement : MonoBehaviour
{
    public static MainDigitMovement instance;


    [SerializeField] private GameData _gameData;
    private float _sensivity, _limit, _speed;
    private bool _gameStart;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _sensivity = _gameData.Sensivity;
        _speed = _gameData.ForwardSpeed;
        _limit = 2f;
    }



    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameStart, OnGameStart);
        EventManager.AddHandler(GameEvent.OnFinish, OnFinish);
        EventManager.AddHandler(GameEvent.OnLose, OnLose);
        EventManager.AddHandler(GameEvent.OnCollisionBarrel, OnCollisionBarrel);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameStart, OnGameStart);
        EventManager.RemoveHandler(GameEvent.OnFinish, OnFinish);
        EventManager.RemoveHandler(GameEvent.OnLose, OnLose);
        EventManager.RemoveHandler(GameEvent.OnCollisionBarrel, OnCollisionBarrel);

    }

    void Update()
    {
        if (_gameStart)
        {
            MoveForward();
        }
    }


    void MoveForward()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void MoveHorizontal(Vector2 delta)
    {
        Vector3 tPos = transform.position;
        tPos.x = Mathf.Clamp(tPos.x + delta.x * _sensivity, -_limit, _limit);

        transform.position = tPos;
    }

    void OnGameStart()
    {
        _gameStart = true;
    }

    void OnLose()
    {
        SetZero();
    }

    void OnFinish()
    {
        SetZero();
    }

    void OnCollisionBarrel()
    {
        SetZero();
    }

    void SetZero()
    {
        _speed = 0;
        _sensivity = 0;
        _limit = 0;
    }
}
