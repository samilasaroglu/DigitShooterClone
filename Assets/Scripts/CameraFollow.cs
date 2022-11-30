using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    private bool _gameStart,_onLose,_onCollisionBarrel;

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
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameStart, OnGameStart);
    }

    private void LateUpdate()
    {
        if (_gameStart && !_onLose && !_onCollisionBarrel)
        {
            Vector3 pos = _target.transform.position;
            pos.x = 0;
            pos.y = 0;

            transform.position = pos + _offset;
        }
    }

    public void ChangeAngle()
    {
        _onCollisionBarrel = true;
        transform.DOMoveX(transform.position.x + 3, .3f);
        transform.DORotate(new Vector3(33, -15f, 0), .3f);
    }

    void OnGameStart()
    {
        _gameStart = true;
    }
}
