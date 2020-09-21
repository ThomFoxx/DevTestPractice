using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Turntable : MonoBehaviour
{

    private bool _holdTable = false;
    [SerializeField]
    [Range(0, 20)]
    private int _rotateSpeed;
    [SerializeField]
    private float _targetRotY;
    private Quaternion _targetRot;
    private Quaternion _currentRot;
    [SerializeField]
    private TMP_Text _labelUIText;
    [SerializeField]
    private TMP_Text _descUIText;

    private void Awake()
    {
        _labelUIText.text = transform.GetChild(0).name;
        _descUIText.text = null;
    }

    private void OnEnable()
    {
        EventManager.onCall += PartCalled;
        EventManager.onFocus += PartFocus;
    }

    private void Update()
    {
        if(!_holdTable)
        {
            transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
        }
        else if(_holdTable)
        {
            _currentRot = transform.rotation;
            transform.rotation = Quaternion.RotateTowards(_currentRot, _targetRot, _rotateSpeed * Time.deltaTime*2);
        }
    }

    private void PartCalled(int callID)
    {
        if (callID == 99)
        {
            _holdTable = false;
            _labelUIText.text = transform.GetChild(0).name;
            _descUIText.text = null;
        }
        else
            _holdTable = true;
    }

    private void PartFocus(float YRot)
    {
        _targetRotY = YRot;
        _targetRot = Quaternion.Euler(0, _targetRotY, 0);
        
    }
    
    private void OnDisable()
    {
        EventManager.onCall -= PartCalled;
        EventManager.onFocus -= PartFocus;
    }
}
