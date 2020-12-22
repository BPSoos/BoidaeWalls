using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]

public class Farok : MonoBehaviour
{
    public float pointSpacing = 0.5f;    

    List<Vector2> _points;    

    LineRenderer _line;
    EdgeCollider2D _col;

    public Transform myHead;
    
    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _col = GetComponent<EdgeCollider2D>();

        _points = new List<Vector2>();
        SetPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(_points.Last(), myHead.position) > pointSpacing)
            SetPoint();

    }

    void SetPoint ()
    {
        if (_points.Count > 1)
            _col.points = _points.ToArray<Vector2>();

        _points.Add(myHead.position);

        _line.positionCount = _points.Count;
        _line.SetPosition(_points.Count -1, myHead.position);

    }
}
