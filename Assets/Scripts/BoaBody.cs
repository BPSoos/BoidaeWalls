using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class BoaBody : MonoBehaviour
{
    
    public float pointSpacing = .5f;
    public Transform myHead;
    LineRenderer line;
    EdgeCollider2D col;
    CircleCollider2D head_col;

    private bool set_points = false;

    List<Vector2> points;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
        head_col = transform.parent.GetComponentInChildren<CircleCollider2D>();
        head_col.enabled = false;


        points = new List<Vector2>();
        myHead.position = new Vector3(Random.Range(-35, 35), Random.Range(-35, 35), 0);
        myHead.Rotate(Vector3.forward * Random.Range(0, 360));
        
        StartCoroutine(StartDrawing ());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!set_points)
            return;
        if (points.Count == 0)
            SetPoint();
        else if (Vector3.Distance(points.Last(), myHead.position) > pointSpacing)
            SetPoint();
    }

    void SetPoint (){
        if (points.Count > 1)
            col.points = points.ToArray<Vector2>();

        points.Add(myHead.position);

        line.positionCount = points.Count;
        line.SetPosition(points.Count -1, myHead.position);

    }

    IEnumerator StartDrawing ()
    {
        yield return new WaitForSeconds(2f);
        set_points = true;
        head_col.enabled = true;
    }
}
