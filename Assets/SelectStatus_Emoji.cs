using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStatus_Emoji : MonoBehaviour
{
    public int status;
    public SpriteRenderer sr;
    public bool waiting = false;
    public float waitTime = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        status = 0;
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

    public void SetAlpha(float alpha)
    {
        sr.color = new Color(1f, 1f, 1f, alpha);
    }

    public void restoreAlpha()
    {
        sr.color = new Color(1f, 1f, 1f, 1f);
    }
}
