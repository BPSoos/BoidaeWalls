using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Boae : MonoBehaviour
{
    
    public float pointSpacing = .1f;
    public Transform myHead;
    LineRenderer line;
    EdgeCollider2D col;

    List<Vector2> points;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();

        points = new List<Vector2>();
        Application.targetFrameRate = 300;
        SetPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(points.Last(), myHead.position) > pointSpacing)
            SetPoint();
    }

    void SetPoint (){
        if (points.Count > 1)
            col.points = points.ToArray<Vector2>();

        points.Add(myHead.position);

        line.positionCount = points.Count;
        line.SetPosition(points.Count -1, myHead.position);

    }
}
