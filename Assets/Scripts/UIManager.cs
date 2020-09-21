using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _descPanel;
    [SerializeField]
    private Material _lrMaterial;
    private List<Transform> _targets = new List<Transform>();
    private List<LineRenderer> _lines = new List<LineRenderer>();

    private void Update()
    {        
        Clear();
        if (_targets.Count > 0)
        {
            for(int i=0; i<_targets.Count; i++)
            {
                if (_targets[i] != null)
                {   
                            DrawLine(_descPanel.transform.position, _targets[i].position, Color.red, i);
                }
            }
        }
    }

    public void AddLine(Transform GO)
    {                
        _targets.Add(GO);
    }

    public void ClearLines()
    {
        _targets.Clear();
        Clear();
    }

    private void Clear()
    {
        if (_lines.Count > 0)
        {
            for (int i = 0; i < _lines.Count; i++)
                Destroy(_lines[i]?.transform.gameObject);

            _lines.Clear();
        }
    }

    public void RemoveTarget(Transform GO)
    {
        _targets.Remove(GO);
        Clear();
    }
     
    void DrawLine(Vector3 start, Vector3 end, Color color, int id)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = _lrMaterial;
        lr.startColor = color;
        lr.startWidth = 0.02f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        _lines.Add(lr);
    }
}
