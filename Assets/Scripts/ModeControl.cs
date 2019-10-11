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
    public GameObject emojiCanvas;
    public GameObject Palette;
    public GameObject[] emoji;
    public GameObject[] emojiprfab;
    public Material[] Brushes;
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

    private void OnDestroy()
    {
        MLInput.OnTriggerDown -= OnTriggerDown;
        MLInput.OnControllerButtonDown -= OnButtonDown;
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
                    emojiCanvas.SetActive(true);
                    Palette.SetActive(false);
                    isDrawing = false;
                } else {
                    mode = userMode.draw;
                    DrawTip.SetActive(true);
                    emojiCanvas.SetActive(false);
                    Palette.SetActive(true);
                    isDrawing = true;
                }
            }
        }
    }
    private void OnTriggerDown(byte controllerId, float value)
    {
        if (_controllerConnectionHandler.IsControllerValid(controllerId) && mode == userMode.emoji)
        {
            if (mode == userMode.emoji && EmojiSelect.currentSelect != -1)
            {
                GameObject temp = Instantiate(emojiprfab[EmojiSelect.currentSelect], emoji[EmojiSelect.currentSelect].transform.position, Quaternion.identity);
                //temp.transform.SetParent(parent.transform);
            }

        }
    }

    #endregion
}
