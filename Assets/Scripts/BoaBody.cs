using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class BoaBody : MonoBehaviour
{
    
    public float pointSpacing = .5f;
    public Transform myHead;
    public LineRenderer line;
    EdgeCollider2D _col;
    CircleCollider2D _headCol;

    private bool _setPoints = false;

    List<Vector2> _points;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        _col = GetComponent<EdgeCollider2D>();
        _headCol = transform.parent.GetComponentInChildren<CircleCollider2D>();
        _headCol.enabled = false;
        _points = new List<Vector2>();
        myHead.position = new Vector3(Random.Range(-35, 35), Random.Range(-35, 35), 0);
        myHead.Rotate(Vector3.forward * Random.Range(0, 360));
        
        StartCoroutine(StartDrawing ());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_setPoints)
            return;
        if (_points.Count == 0)
            SetPoint();
        else if (Vector3.Distance(_points.Last(), myHead.position) > pointSpacing)
            SetPoint();
    }

    void SetPoint (){
        if (_points.Count > 1)
            _col.points = _points.ToArray<Vector2>();

        _points.Add(myHead.position);

        line.positionCount = _points.Count;
        line.SetPosition(_points.Count -1, myHead.position);

    }

    IEnumerator StartDrawing ()
    {
        yield return new WaitForSeconds(2f);
        _setPoints = true;
        _headCol.enabled = true;
    }
}
