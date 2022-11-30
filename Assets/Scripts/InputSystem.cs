using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputSystem : MonoBehaviour, IDragHandler ,IPointerClickHandler
{
    public bool GameStart = false;

    private bool _isLose;




    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameStart, OnGameStart);
        EventManager.AddHandler(GameEvent.OnLose, OnLose);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameStart, OnGameStart);
        EventManager.RemoveHandler(GameEvent.OnLose, OnLose);
    }

    public void OnDrag(PointerEventData data)
    {
        if (!GameStart)
        {
            EventManager.Broadcast(GameEvent.OnGameStart);
            UIManager.instance.MenuOff();
        }
        else if(!_isLose)
        {
            MainDigitMovement.instance.MoveHorizontal(data.delta);
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (!GameStart)
        {
            EventManager.Broadcast(GameEvent.OnGameStart);
            UIManager.instance.MenuOff();
        }
    }

    private void OnGameStart()
    {
        GameStart = true;
    }

    private void OnLose()
    {
        _isLose = true;
    }
}
