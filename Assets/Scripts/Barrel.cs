using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class Barrel : MonoBehaviour
{
    [SerializeField] private int _barrelCount;
    [SerializeField] private TextMeshPro _barrelCountText;
    [SerializeField] private GameObject _money;


    private void Awake()
    {
        SetText();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Throwable"))
        {
            if(_barrelCount > 0)
            {
                _barrelCount -= other.GetComponent<ThrowableDigit>().value;
                SetText();

                if (_barrelCount < 1)
                {
                    _money.transform.SetParent(null);
                    gameObject.tag = "Untagged";
                    transform.DOScale(Vector3.zero, .25f);
                }

                other.gameObject.SetActive(false);
            }
        }
    }

    void SetText()
    {
        _barrelCountText.SetText(_barrelCount.ToString());
    }
}
