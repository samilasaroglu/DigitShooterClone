using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainDigit : MonoBehaviour
{
    public static MainDigit instance;
    public int _currentStartCount;


    [SerializeField] private GameData _gameData;
    [SerializeField] private GameObject[] _numbersPrefab;
    [SerializeField] private GameObject _dollarPrefab,_numbersParent,_moneyEffect,_firstPlace,_dieEffect,_explosionParticle,_bestScoreObject;
    private bool isFinish,_isLose,_gameStart;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        _currentStartCount = _gameData.StartCount;

        SetNumbers();
        RotateRight();

        if(_gameData.BestScore > 0)
        {

            GameObject bestScoreObject =  Instantiate(_bestScoreObject);
            bestScoreObject.transform.position = new Vector3(-4, -.5f, _gameData.BestScore);
        }
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameStart, OnGameStart);
        EventManager.AddHandler(GameEvent.OnFinish, OnFinish);
        EventManager.AddHandler(GameEvent.OnLose, OnLose);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameStart, OnGameStart);
        EventManager.RemoveHandler(GameEvent.OnFinish, OnFinish);
        EventManager.RemoveHandler(GameEvent.OnLose, OnLose);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            Gate gateScript = other.GetComponent<Gate>();
            GateOperation(gateScript);

            other.transform.parent.transform.DOScale(new Vector3(0,1,1), .3f).OnComplete(() => other.transform.parent.gameObject.SetActive(false));
          
        }

        else if (other.CompareTag("Collectable"))
        {
            _currentStartCount += other.GetComponent<CollectableDigit>()._value;

            if(_currentStartCount < 0)
            {
                other.gameObject.SetActive(false);
                EventManager.Broadcast(GameEvent.OnLose);
                UIManager.instance.Lose();
                CameraFollow.instance.ChangeAngle();
            }
            else
            {
                other.gameObject.SetActive(false);
                SetNumbers();
                GameObject _explosionEffect = Instantiate(_explosionParticle,transform.position,transform.rotation);
                _explosionEffect.transform.SetParent(transform);
               transform.DORotate(new Vector3(0, 180, 0), .1f).OnComplete(() => transform.DORotate(transform.eulerAngles+ new Vector3(0, 180, 0), .1f));
            }

        }

        else if (other.CompareTag("Money"))
        {
            Instantiate(_moneyEffect,other.transform.position,other.transform.rotation);
            other.gameObject.SetActive(false);
            _gameData.Money += _gameData.Income;
            EventManager.Broadcast(GameEvent.OnCollectMoney);
        }

        else if (other.CompareTag("Barrel"))
        {
            other.gameObject.SetActive(false);

            Instantiate(_dieEffect, transform.position, transform.rotation);

            transform.parent.transform.DORotate(new Vector3(-110f, 0, 0), 1f);

            _isLose = true;


            BestScore(other.transform.position.z);
            EventManager.Broadcast(GameEvent.OnCollisionBarrel);
            UIManager.instance.OpenWinPanel();
            CameraFollow.instance.ChangeAngle();

        }

        else if (other.CompareTag("Finish"))
        {
            InvokeRepeating("DecreaseValue", .001f, .1f);
        }

        else if (other.CompareTag("Finish2"))
        {
            EventManager.Broadcast(GameEvent.OnFinish);
            UIManager.instance.OpenWinPanel();
        }
    }

    public void SetNumbers()
    {

        _dollarPrefab.SetActive(false);

        for (int i = 0; i < _numbersParent.transform.childCount; i++)
        {
            _numbersParent.transform.GetChild(i).gameObject.SetActive(false);
        }


        if (_currentStartCount < 10)
        {
            var Number = _numbersPrefab[_currentStartCount];
            Number.SetActive(true);
            Number.transform.position = transform.position;
            Number.transform.localPosition -= new Vector3(.3f, 0, 0);

            var Dollar = _dollarPrefab;
            Dollar.SetActive(true);
            Dollar.transform.localPosition = new Vector3(.3f, 0, 0);
        }
        else if (_currentStartCount < 100)
        {
            var Number = _numbersPrefab[_currentStartCount / 10];
            Number.SetActive(true);
            Number.transform.position = transform.position;
            Number.transform.localPosition -= new Vector3(.6f, 0, 0);


            var Number2 = _numbersPrefab[(_currentStartCount % 10) + 10];
            Number2.SetActive(true);
            Number2.transform.position = transform.position;

            var Dollar = _dollarPrefab;
            Dollar.SetActive(true);
            Dollar.transform.localPosition = new Vector3(.6f, 0, 0);
        }
        else if (_currentStartCount < 1000)
        {
            var Number = _numbersPrefab[_currentStartCount / 100];
            Number.SetActive(true);
            Number.transform.position = transform.position;
            Number.transform.localPosition -= new Vector3(.9f, 0, 0);

            var Number2 = _numbersPrefab[((_currentStartCount % 100) / 10) + 10];
            Number2.SetActive(true);
            Number2.transform.position = transform.position;
            Number2.transform.localPosition -= new Vector3(.3f, 0, 0);

            var Number3 = _numbersPrefab[((_currentStartCount % 100) % 10) + 20];
            Number3.SetActive(true);
            Number3.transform.position = transform.position;
            Number3.transform.localPosition += new Vector3(.3f, 0, 0);

            var Dollar = _dollarPrefab;
            Dollar.SetActive(true);
            Dollar.transform.localPosition = new Vector3(.9f, 0, 0);
        }
        else if (_currentStartCount < 10000)
        {
            var Number = _numbersPrefab[_currentStartCount / 1000];
            Number.SetActive(true);
            Number.transform.position = transform.position;
            Number.transform.localPosition -= new Vector3(1.2f, 0, 0);

            var Number2 = _numbersPrefab[((_currentStartCount % 1000) / 100) + 10];
            Number2.SetActive(true);
            Number2.transform.position = transform.position;
            Number2.transform.localPosition -= new Vector3(.6f, 0, 0);

            var Number3 = _numbersPrefab[((_currentStartCount % 1000) % 100 / 10) + 20];
            Number3.SetActive(true);
            Number3.transform.position = transform.position;

            var Number4 = _numbersPrefab[((_currentStartCount % 1000) % 100 % 10) + 30];
            Number4.SetActive(true);
            Number4.transform.position = transform.position;
            Number4.transform.localPosition += new Vector3(.6f, 0, 0);

            var Dollar = _dollarPrefab;
            Dollar.SetActive(true);
            Dollar.transform.localPosition = new Vector3(1.2f, 0, 0);
        }
    }

    void GateOperation(Gate gateScript)
    {
        if (gateScript.gateType == Gate.GateType.FireRate)
        {
            if (DigitThrower.instance._currentFireRate != 0.01f)
            {
                DigitThrower.instance._currentFireRate -= gateScript._gateValue * .01375f;

                if (DigitThrower.instance._currentFireRate < .05f)
                {
                    DigitThrower.instance._currentFireRate = .05f;
                }
                else if (DigitThrower.instance._currentFireRate > .6f)
                {
                    DigitThrower.instance._currentFireRate = .6f;
                }

            }

        }
        else if (gateScript.gateType == Gate.GateType.Range)
        {
            if (DigitThrower.instance._currentRange != 60)
            {
                DigitThrower.instance._currentRange += gateScript._gateValue * 1;

                if (DigitThrower.instance._currentRange > 60)
                {
                    DigitThrower.instance._currentRange = 60;
                }
                else if (DigitThrower.instance._currentRange < 20)
                {
                    DigitThrower.instance._currentRange = 20;
                }
            }
        }
    }

    void OnGameStart()
    {
        _gameStart = true;
    }

    void DecreaseValue()
    {
        if (!isFinish && !_isLose)
        {
            if (_currentStartCount > 0)
            {
                _currentStartCount--;
                SetNumbers();
            }
            else
            {
                transform.DORotate(new Vector3(-110f, 0, 0), 1f);
                EventManager.Broadcast(GameEvent.OnCollisionBarrel);
                CameraFollow.instance.ChangeAngle();
                UIManager.instance.OpenWinPanel();
                isFinish = true;
            }

        }

    }

    void OnLose()
    {
        _isLose = true;
        Instantiate(_dieEffect, transform.position, transform.rotation);
        transform.parent.transform.DORotate(new Vector3(-110f, 0, 0), 1f);

    }

    void OnFinish()
    {
        isFinish = true;
        transform.DOMove(_firstPlace.transform.position, 1.275f);
    }

    void RotateRight()
    {
        if (!_gameStart)
        {
            transform.DORotate(new Vector3(0, 0, -5f), .65f).OnComplete(() => RotateLeft());
        }
        else
        {
            transform.DORotate(new Vector3(0, 0, 0), .15f).OnComplete(() => Jump());
        }
    }

    void RotateLeft()
    {
        if (!_gameStart)
        {
            transform.DORotate(new Vector3(0, 0, 5f), .65f).OnComplete(() => RotateRight());
        }
        else
        {
            transform.DORotate(new Vector3(0, 0, 0), .15f).OnComplete(() => Jump());
        }
    }

    void Jump()
    {
        if (!isFinish && !_isLose &&_gameStart)
        {
            transform.DOMoveY(transform.position.y + .1f, .15f).OnComplete(() => Fall());
        }
    }

    void Fall()
    {
        if (!isFinish && !_isLose && _gameStart)
        {
            transform.DOMoveY(transform.position.y - .1f, .15f).OnComplete(() => Jump());
        }
    }

    void BestScore(float progress)
    {
        if(progress > _gameData.BestScore)
        {
            _gameData.BestScore = progress;
        }
    }

}
