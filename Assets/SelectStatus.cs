using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStatus : MonoBehaviour
{
    public int status;
    public LineRenderer lr;
    private Material mat;
    public bool waiting = false;
    public float waitTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        status = 0;
        mat = lr.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting)
        {
            waitTime -= Time.deltaTime;
            if (waitTime < 0f)
            {
                waiting = false;
            }
        }
    }

    public void changeMat(Material Mat) {
        lr.GetComponent<Renderer>().material = Mat;
    }

    public void restoreMat() {
        lr.GetComponent<Renderer>().material = mat;
    }
}
