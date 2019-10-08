using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Draw3D : MonoBehaviour
{
    Draw activeLine;
    public GameObject Line;
    private bool key = false;
    public Transform DrawTip;
    public ControllerConnectionHandler _controllerConnectionHandler;
    GameObject temp;
    public GameObject parent;

    public ModeControl mc;
    // Start is called before the first frame update
    void Start()
    {
        MLInput.OnTriggerUp += OnTriggerUp;
        MLInput.OnTriggerDown += OnTriggerDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mc.isDrawing)
            return;

        if (activeLine != null && key == true) {
            activeLine.UpdateLine(DrawTip.transform.position);
        }
    }

    private void OnTriggerUp(byte controllerId, float value)
    {
        if (_controllerConnectionHandler.IsControllerValid(controllerId))
        {
            if (!mc.isDrawing)
                return;

            temp.transform.SetParent(parent.transform);
            temp = null;
            activeLine = null;
            key = false;
        }
    }

    private void OnTriggerDown(byte controllerId, float value)
    {
        if (_controllerConnectionHandler.IsControllerValid(controllerId))
        {
            if (!mc.isDrawing)
                return;

            temp = Instantiate(Line);
            activeLine = temp.GetComponent<Draw>();
            key = true;

            if (mc.mode == 0) {
                Instantiate(Line);
            }
        }
    }
}
