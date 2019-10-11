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
    public Material[] MatList;
    // Start is called before the first frame update
    void Start()
    {
        MLInput.OnTriggerUp += HandleOnTriggerUp;
        MLInput.OnTriggerDown += HandleOnTriggerDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mc.isDrawing)
            return;

        if (activeLine != null && key == true) {
            activeLine.UpdateLine(DrawTip.transform.position);
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    temp = Instantiate(Line);
        //    activeLine = temp.GetComponent<Draw>();
        //    key = true;
        //}

        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    temp = null;
        //    activeLine = null;
        //    key = false;
        //}
    }

    private void HandleOnTriggerUp(byte controllerId, float value)
    {
        if (_controllerConnectionHandler.IsControllerValid(controllerId))
        {
            if (!mc.isDrawing)
                return;

            //temp.transform.SetParent(parent.transform);
            temp = null;
            activeLine = null;
            key = false;
        }
    }

    private void HandleOnTriggerDown(byte controllerId, float value)
    {
        if (_controllerConnectionHandler.IsControllerValid(controllerId))
        {
            if (!mc.isDrawing)
                return;

            temp = Instantiate(Line);
            activeLine = temp.GetComponent<Draw>();
            activeLine.SetMaterial(DrawTip.GetComponent<Renderer>().material);
            key = true;
        }
    }

    private void OnDestroy()
    {
        MLInput.OnTriggerUp -= HandleOnTriggerUp;
        MLInput.OnTriggerDown -= HandleOnTriggerDown;
    }
}
