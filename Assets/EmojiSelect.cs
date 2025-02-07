﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using DG.Tweening;

public class EmojiSelect : MonoBehaviour
{
    public ControllerConnectionHandler _controllerConnectionHandler;
    public static int currentSelect = 0;
    public int id;
    private Vector3 initialScale;
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
            transform.DOLocalMoveZ(-0.3f, .3f);
            transform.DOScale(initialScale * 1.2f, .3f);
            controller.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.Buzz, MLInputControllerFeedbackIntensity.Low);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "picker")
        {
            MLInputController controller = _controllerConnectionHandler.ConnectedController;
            transform.DOLocalMoveZ(0f, .3f);
            transform.DOScale(initialScale, .3f);
        }
    }
}
