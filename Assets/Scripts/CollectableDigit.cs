using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableDigit : MonoBehaviour
{
    public int _value;

    [SerializeField] private GameObject[] _numbersPrefab,_redNumbersPrefab;
    [SerializeField] private GameObject _numbersParent,_redNumbersParent,_dollar,_redDollar,_minus,_redMinus;
    [SerializeField] private bool _canWalk;

    private void Awake()
    {
        SetNumbers();

        if (_canWalk)
        {
            Walk();
        }
        else
        {
            RotateRight();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Throwable"))
        {
            transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), .1f).OnComplete(() => transform.DOScale(Vector3.one, .1f));
            _value += other.GetComponent<ThrowableDigit>().value;
            SetNumbers();
            other.gameObject.SetActive(false);

        }
    }

    void RotateRight()
    {
        transform.DORotate(new Vector3(0, 0, -5f), .55f).OnComplete(() => RotateLeft());
    }

    void RotateLeft()
    {
        transform.DORotate(new Vector3(0, 0, 5f), .55f).OnComplete(() => RotateRight());

    }

    void Walk()
    {
        transform.DOJump(transform.position + new Vector3(0, 0, .35f), .25f, 1, .35f).OnComplete(() => Walk());
    }

    void SetNumbers()
    {
        _dollar.SetActive(false);
        _minus.SetActive(false);
        _redDollar.SetActive(false);
        _redMinus.SetActive(false);

        for (int i = 0; i < _numbersParent.transform.childCount; i++)
        {
            _numbersParent.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < _redNumbersParent.transform.childCount; i++)
        {
            _redNumbersParent.transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_value > -100 && _value < -9)
        {
            var Minus = _redMinus;
            Minus.SetActive(true);
            Minus.transform.localPosition = new Vector3(-.9f, 0, 0);

            var Number = _redNumbersPrefab[Mathf.Abs(_value/10)];
            Number.SetActive(true);
            Number.transform.position = transform.position;
            Number.transform.localPosition -= new Vector3(.3f, 0, 0);

            var Number2 = _redNumbersPrefab[Mathf.Abs(_value%10)+10];
            Number2.SetActive(true);
            Number2.transform.position = transform.position;
            Number2.transform.localPosition += new Vector3(.3f, 0, 0);

            var Dollar = _redDollar;
            Dollar.SetActive(true);
            Dollar.transform.localPosition = new Vector3(.9f, 0, 0);
        }
        else if(_value > -10 && _value < 0)
        {
            var Minus = _redMinus;
            Minus.SetActive(true);
            Minus.transform.localPosition = new Vector3(-.6f,0,0);

            var Number = _redNumbersPrefab[Mathf.Abs(_value)];
            Number.SetActive(true);
            Number.transform.position = transform.position;

            var Dollar = _redDollar;
            Dollar.SetActive(true);
            Dollar.transform.localPosition = new Vector3(.6f, 0, 0);

        }
        else if (_value < 10)
        {
            var Number = _numbersPrefab[_value];
            Number.SetActive(true);
            Number.transform.position = transform.position;
            Number.transform.localPosition -= new Vector3(.3f, 0, 0);

            var Dollar = _dollar;
            Dollar.SetActive(true);
            Dollar.transform.localPosition = new Vector3(.3f, 0, 0);
        }
        else if (_value < 100)
        {
            var Number = _numbersPrefab[_value / 10];
            Number.SetActive(true);
            Number.transform.position = transform.position;
            Number.transform.localPosition -= new Vector3(.6f, 0, 0);


            var Number2 = _numbersPrefab[(_value % 10)+10];
            Number2.SetActive(true);
            Number2.transform.position = transform.position;


            var Dollar = _dollar;
            Dollar.SetActive(true);
            Dollar.transform.localPosition = new Vector3(.6f, 0, 0);
        }
        else if (_value < 1000)
        {
            var Number = _numbersPrefab[_value / 100];
            Number.SetActive(true);
            Number.transform.position = transform.position;
            Number.transform.localPosition -= new Vector3(.9f, 0, 0);

            var Number2 = _numbersPrefab[((_value % 100) / 10)+10];
            Number2.SetActive(true);
            Number2.transform.position = transform.position;
            Number2.transform.localPosition -= new Vector3(.3f, 0, 0);

            var Number3 = _numbersPrefab[((_value % 100) % 10)+20];
            Number3.SetActive(true);
            Number3.transform.position = transform.position;
            Number3.transform.localPosition += new Vector3(.3f, 0, 0);

            var Dollar = _dollar;
            Dollar.SetActive(true);
            Dollar.transform.localPosition = new Vector3(.9f, 0, 0);
        }
        else if (_value < 10000)
        {
            var Number = _numbersPrefab[_value / 1000];
            Number.SetActive(true);
            Number.transform.position = transform.position;
            Number.transform.localPosition -= new Vector3(1.2f, 0, 0);

            var Number2 = _numbersPrefab[((_value % 1000) / 100)+10];
            Number2.SetActive(true);
            Number2.transform.position = transform.position;
            Number2.transform.localPosition -= new Vector3(.6f, 0, 0);

            var Number3 = _numbersPrefab[((_value % 1000) % 100 / 10)+20];
            Number3.SetActive(true);
            Number3.transform.position = transform.position;

            var Number4 = _numbersPrefab[((_value % 1000) % 100 % 10)+30];
            Number4.SetActive(true);
            Number4.transform.position = transform.position;
            Number4.transform.localPosition += new Vector3(.6f, 0, 0);

            var Dollar = _dollar;
            Dollar.SetActive(true);
            Dollar.transform.localPosition = new Vector3(1.2f, 0, 0);
        }
    }


}
