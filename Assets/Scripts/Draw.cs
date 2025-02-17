﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public LineRenderer lr;
    public TextMesh text;
    List<Vector3> points;
    CapsuleCollider capsule;
    Rigidbody rb;
    GameObject temp;
    // Start is called before the first frame update

    public void UpdateLine(Vector3 pos) {
        if (points == null)
        {
            text.transform.position = pos;
            text.text = pos + "";
            points = new List<Vector3>();
            SetPoint(pos);
            return;
        }
        if (Vector3.Distance(points[points.Count - 1], pos) > 0.005f)
        {
            SetPoint(pos);
            temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            temp.transform.localScale = Vector3.one * .005f;
            temp.transform.position = pos;
            temp.GetComponent<MeshRenderer>().enabled = false;
            temp.tag = "Stroke";
            rb = temp.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            temp.transform.SetParent(this.transform);
        }
    }

    public void SetMaterial(Material mat) {
        lr.GetComponent<Renderer>().material = mat;
    }

    void SetPoint(Vector3 point) {
        points.Add(point);

        lr.positionCount = points.Count;
        lr.SetPosition(points.Count - 1, point);
    }
}

