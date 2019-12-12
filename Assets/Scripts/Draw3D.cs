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
    public GameObject palette;
    public List<GameObject> strokes;
    // Start is called before the first frame update
    void Start()
    {
        MLInput.OnTriggerUp += HandleOnTriggerUp;
        MLInput.OnTriggerDown += HandleOnTriggerDown;
        MLInput.OnControllerButtonDown += HandleOnButtonDown;
        strokes = new List<GameObject>();
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
            palette.SetActive(true);
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

            palette.SetActive(false);
            MLInputController controller = _controllerConnectionHandler.ConnectedController;
            temp = Instantiate(Line);
            activeLine = temp.GetComponent<Draw>();
            activeLine.SetMaterial(DrawTip.GetComponent<Renderer>().material);
            strokes.Add(temp);
            controller.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.Buzz, MLInputControllerFeedbackIntensity.Medium);
            key = true;
        }
    }

    private void HandleOnButtonDown(byte controllerId, MLInputControllerButton button)
    {
        MLInputController controller = _controllerConnectionHandler.ConnectedController;
        controller.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.ForceUp, MLInputControllerFeedbackIntensity.Low);
        if (controller != null && controller.Id == controllerId &&
            button == MLInputControllerButton.HomeTap && mc.isDrawing)
        {
            int count = strokes.Count;
            if (count > 0)
            {
                strokes.RemoveAt(count - 1);
                Destroy(strokes[count - 1]);
            }
        }
    }

    private void OnDestroy()
    {
        MLInput.OnTriggerUp -= HandleOnTriggerUp;
        MLInput.OnTriggerDown -= HandleOnTriggerDown;
        MLInput.OnControllerButtonDown -= HandleOnButtonDown;
    }
}
