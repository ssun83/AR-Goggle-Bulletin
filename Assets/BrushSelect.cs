﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using DG.Tweening;

public class BrushSelect : MonoBehaviour
{
    public static int currentSelect = 0;
    public GameObject DrawTip;
    public Draw3D draw3d;
    public int id;
    private Vector3 initialScale;
    public ControllerConnectionHandler _controllerConnectionHandler;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "picker")
        {
            MLInputController controller = _controllerConnectionHandler.ConnectedController;
            currentSelect = id;
            transform.DOLocalMoveZ(-0.5f, .3f);
            transform.DOScale(initialScale * 1.2f, .3f);
            DrawTip.GetComponent<Renderer>().material = draw3d.MatList[currentSelect];
            MLInputControllerFeedbackIntensity intensity = (MLInputControllerFeedbackIntensity)((int)(.8f));
            controller.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.Buzz, intensity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "picker")
        {
            transform.DOScale(initialScale, .3f);
            transform.DOLocalMoveZ(0f, .3f);
        }
    }
}
