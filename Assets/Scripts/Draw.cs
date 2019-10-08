using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    private LineRenderer lr;
    List<Vector3> points;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void UpdateLine(Vector3 pos) {
        if (points == null)
        {
            points = new List<Vector3>();
            SetPoint(pos);
            return;
        }
        if (Vector3.Distance(points[points.Count - 1], pos) > 0.01f)
        {
            SetPoint(pos);
        }
    }

    void SetPoint(Vector3 point) {
        points.Add(point);

        lr.positionCount = points.Count;
        lr.SetPosition(points.Count - 1, point);
    }
}

