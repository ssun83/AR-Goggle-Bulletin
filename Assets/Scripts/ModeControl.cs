using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ModeControl : MonoBehaviour
{
    public ControllerConnectionHandler _controllerConnectionHandler;
    public enum userMode { draw, emoji};
    public userMode mode = userMode.draw;
    public bool isDrawing = true;
    public GameObject DrawTip;
    public GameObject emojiPos;
    public GameObject emoji;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        MLInput.OnTriggerDown += OnTriggerDown;
        MLInput.OnControllerButtonDown += OnButtonDown;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Event Handlers
    /// <summary>
    /// Handles the event for button down and cycles the raycast mode.
    /// </summary>
    /// <param name="controllerId">The id of the controller.</param>
    /// <param name="button">The button that is being pressed.</param>
    private void OnButtonDown(byte controllerId, MLInputControllerButton button)
    {
        if (_controllerConnectionHandler.IsControllerValid(controllerId))
        {
            if (button == MLInputControllerButton.Bumper)
            {
                if (mode == userMode.draw) {
                    mode = userMode.emoji;
                    DrawTip.SetActive(false);
                    emojiPos.SetActive(true);
                    isDrawing = false;
                } else {
                    mode = userMode.draw;
                    DrawTip.SetActive(true);
                    emojiPos.SetActive(false);
                    isDrawing = true;
                }
            }
        }
    }
    private void OnTriggerDown(byte controllerId, float value)
    {
        if (_controllerConnectionHandler.IsControllerValid(controllerId) && mode == userMode.emoji)
        {
            if (mode == userMode.emoji)
            {
                GameObject temp = Instantiate(emoji, emojiPos.transform.position, emojiPos.transform.rotation);
                temp.transform.SetParent(parent.transform);
            }
        }
    }

    #endregion
}
