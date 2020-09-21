using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Part : MonoBehaviour
{

    [SerializeField]
    private GameObject _partObject;
    [SerializeField]
    private int _partID;
    [SerializeField]
    private float _partFocusY;
    [SerializeField]
    private string _partLabelText;
    [SerializeField]
    private TMP_Text _labelUIText;
    [SerializeField]
    private string _partDescription;
    [SerializeField]
    private TMP_Text _descUIText;
    [SerializeField]
    private GameObject[] _partLights;
    [SerializeField]
    private Material[] _partMaterials;
    private Renderer _partRenderer;
    private bool _holdColor = false;
    private EventManager _eventManager;
    private UIManager _UIManager;

    private void Awake()
    {
        _eventManager = GameObject.Find("Camera").GetComponent<EventManager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _partObject = this.gameObject;
        _partRenderer = _partObject.GetComponent<Renderer>();
        if (_partRenderer == null)
            Debug.Log("Renderer is NULL on " + _partObject.name);
    }

    private void OnEnable()
    {
        EventManager.onCall += ActivatePart;
    }

    private void ActivatePart(int callID)
    {
        if (_partID == callID)
        {
            _holdColor = true;
            if (_partRenderer.material.HasProperty("_Color"))
                _partRenderer.material = _partMaterials[1];
            else
            {
                for (int i = 0; i < _partLights.Length; i++)
                {
                    _partLights[i].SetActive(true);
                }
            }
            _eventManager.Focus(_partFocusY);
            _labelUIText.text = _partLabelText;
            _descUIText.text = _partDescription;
            _UIManager.AddLine(transform.GetChild(0).transform);
        }
        else
        {
            _holdColor = false;
            if (_partRenderer.material.HasProperty("_Color"))
                _partRenderer.material = _partMaterials[0];
            else
            {
                for (int i = 0; i < _partLights.Length; i++)
                {
                    _partLights[i].SetActive(false);
                }
            }
        }
    }

    private void OnBecameVisible()
    {
        if (_holdColor)
            _UIManager.AddLine(transform.GetChild(0).transform);
    }

    private void OnBecameInvisible()
    {
        if (_holdColor)
            _UIManager.RemoveTarget(transform.GetChild(0).transform);
    }

    private void OnMouseEnter()
    {
        if (_holdColor == false)
        {
            if (_partRenderer.material.HasProperty("_Color"))
                _partRenderer.material.color = Color.yellow;
            else
            {
                for (int i = 0; i < _partLights.Length; i++)
                {
                    _partLights[i].SetActive(true);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        _UIManager.ClearLines();
        _eventManager.Call(_partID);
    }

    private void OnMouseExit()
    {
        if (_holdColor == false)
        {
            if (_partRenderer.material.HasProperty("_Color"))
                _partRenderer.material.color = Color.gray;
            else
            {
                for (int i = 0; i < _partLights.Length; i++)
                {
                    _partLights[i].SetActive(false);
                }
            }
        }
    }

    private void OnDisable()
    {
        EventManager.onCall -= ActivatePart;
    }
}
