using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class SelectContent : MonoBehaviour
{
    public Material mat;
    public List<GameObject> strokes;
    public List<GameObject> Emojis;

    public ControllerConnectionHandler _controllerConnectionHandler;
    public bool isSelecting = false;
    public GameObject strokestext;
    public GameObject Emojisstext;
    public GameObject controller;

    private bool isOperating = false;
    void Awake()
    {
        MLInput.OnTriggerDown += OnTriggerDown;
        MLInput.OnTriggerUp += OnTriggerUp;
        MLInput.OnControllerButtonDown += OnButtonDown;
    }
        // Start is called before the first frame update
    void Start()
    {
        strokes = new List<GameObject>();
        Emojis = new List<GameObject>();

    }

    private void OnDestroy()
    {
        MLInput.OnTriggerDown -= OnTriggerDown;
        MLInput.OnTriggerUp -= OnTriggerUp;
        MLInput.OnControllerButtonDown -= OnButtonDown;
    }

        // Update is called once per frame
    void Update()
    {
        strokestext.GetComponent<TextMesh>().text = strokes.Count + "";
        Emojisstext.GetComponent<TextMesh>().text = Emojis.Count + "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOperating)
            return;

        if (other.tag == "Stroke")
        {
            LineRenderer lr = other.transform.parent.GetComponent<LineRenderer>();
            SelectStatus ss = lr.GetComponent<SelectStatus>();
            if (lr.GetComponent<SelectStatus>().status == 0 && !ss.waiting)
            {
                ss.status = 1;
                ss.changeMat(mat);
                ss.waitTime = 1.5f;
                ss.waiting = true;
                strokes.Add(other.transform.parent.gameObject);
            }
            if (lr.GetComponent<SelectStatus>().status == 1 && !ss.waiting)
            {
                ss.status = 0;
                ss.restoreMat();
                ss.waitTime = 1.5f;
                ss.waiting = true;
                strokes.Remove(other.transform.parent.gameObject);
            }
        }

        if (other.tag == "Emoji")
        {
            SelectStatus_Emoji ss = other.GetComponent<SelectStatus_Emoji>();
            if (ss.status == 0 && !ss.waiting)
            {
                ss.status = 1;
                ss.SetAlpha(.5f);
                ss.waitTime = 1.5f;
                ss.waiting = true;
                Emojis.Add(other.gameObject);
            }
            if (ss.status == 1 && !ss.waiting)
            {
                ss.status = 0;
                ss.restoreAlpha();
                ss.waitTime = 1.5f;
                ss.waiting = true;
                Emojis.Remove(other.gameObject);
            }
        }
    }

    private void OnTriggerDown(byte controllerId, float value)
    {
        isOperating = true;

        if (_controllerConnectionHandler.IsControllerValid(controllerId))
        {
            if (ActionSelect.currentSelect == 1) {
                if (strokes.Count > 0)
                {
                    for (int i = 0; i < strokes.Count; i++)
                    {
                        Destroy(strokes[i]);
                    }
                }
                if (Emojis.Count > 0)
                {
                    for (int i = 0; i < Emojis.Count; i++)
                    {
                        Destroy(Emojis[i]);
                    }
                }
                strokes.Clear();
                Emojis.Clear();
            }

            if (ActionSelect.currentSelect == 0)
            {
                if (strokes.Count > 0)
                {
                    for (int i = 0; i < strokes.Count; i++)
                    {
                        strokes[i].transform.parent = controller.transform;
                    }
                }

                if (Emojis.Count > 0)
                {
                    for (int i = 0; i < Emojis.Count; i++)
                    {
                        Emojis[i].transform.parent = controller.transform;
                    }
                }
            }
        }
    }

    private void OnTriggerUp(byte controllerId, float value)
    {
        isOperating = false;

        if (_controllerConnectionHandler.IsControllerValid(controllerId))
        {
            if (strokes.Count > 0)
            {
                for (int i = 0; i < strokes.Count; i++)
                {
                    strokes[i].transform.parent = null;
                }
            }
            if (Emojis.Count > 0)
            {
                for (int i = 0; i < Emojis.Count; i++)
                {
                    Emojis[i].transform.parent = null;
                }
            }
        }
    }

    private void OnButtonDown(byte controllerId, MLInputControllerButton button)
    {
        if (_controllerConnectionHandler.IsControllerValid(controllerId))
        {
            if (button == MLInputControllerButton.HomeTap)
            {
                for (int i = 0; i < strokes.Count; i++)
                {
                    SelectStatus ss = strokes[i].GetComponent<SelectStatus>();
                    ss.restoreMat();
                }

                for (int i = 0; i < Emojis.Count; i++)
                {
                    SelectStatus_Emoji ss = Emojis[i].GetComponent<SelectStatus_Emoji>();
                    ss.restoreAlpha();
                }
                strokes.Clear();
                Emojis.Clear();
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < strokes.Count; i++)
        {
            SelectStatus ss = strokes[i].GetComponent<SelectStatus>();
            ss.restoreMat();
        }

        for (int i = 0; i < Emojis.Count; i++)
        {
            SelectStatus_Emoji ss = Emojis[i].GetComponent<SelectStatus_Emoji>();
            ss.restoreAlpha();
        }
        strokes.Clear();
        Emojis.Clear();
    }
}
