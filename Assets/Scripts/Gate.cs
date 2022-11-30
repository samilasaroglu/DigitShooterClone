using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Gate : MonoBehaviour
{
    public enum GateType
    {
        FireRate,
        Range
    }

    public GateType gateType;
    public int _gateValue;

    [SerializeField] private TextMeshPro _gateValueText,_gateTypeText;
    [SerializeField] private Color red, blue;
    [SerializeField] private bool _rightGate,_colorChanged;

    private void Start()
    {
        SetGateTypeText();
        SetGateValueText();
        ControlGateColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Throwable"))
        {
            MakeBiggerEffect();

            _gateValue += other.GetComponent<ThrowableDigit>().value;
            SetGateValueText();

            if(_gateValue >= 0 && !_colorChanged)
            {
                ChangeColor();
                _colorChanged = true;
            }

            other.gameObject.SetActive(false);
        }
    }

    void MakeBiggerEffect()
    {
        transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), .1f).OnComplete(() => transform.DOScale(new Vector3(1, 1, 1), .1f));
    }

    void ControlGateColor()
    {
        if (_rightGate)
        {
            if (_gateValue >= 0)
            {
                GetComponentInParent<MeshRenderer>().materials[1].color = blue;
            }
            else
            {
                GetComponentInParent<MeshRenderer>().materials[1].color = red; 
            }
        }
        else
        {
            if (_gateValue >= 0)
            {
                GetComponentInParent<MeshRenderer>().materials[2].color = blue;
            }
            else
            {
                GetComponentInParent<MeshRenderer>().materials[2].color = red;
            }
        }

    }

    void ChangeColor()
    {
        if (_rightGate)
        {
            GetComponentInParent<MeshRenderer>().materials[1].color = blue;
        }
        else
        {
            GetComponentInParent<MeshRenderer>().materials[2].color = blue;

        }
    }

    void SetGateValueText()
    {
        if (_gateValue >= 0)
        {
            _gateValueText.SetText("+" + _gateValue);

        }
        else
        {
            _gateValueText.SetText(_gateValue.ToString());
        }

    }

    void SetGateTypeText()
    {
        if(gateType == GateType.FireRate)
        {
            _gateTypeText.SetText("FIRERATE");
        }
        else if (gateType == GateType.Range)
        {
            _gateTypeText.SetText("RANGE");
        }
    }
}
