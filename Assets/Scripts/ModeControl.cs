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
    public List<GameObject> emojiList;
    private bool isScaling = false;
    private Vector3 tempPos;
    private Vector3 tempScale;
    private GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        MLInput.OnTriggerDown += OnTriggerDown;
        MLInput.OnTriggerUp += OnTriggerUp;
        MLInput.OnControllerButtonDown += OnButtonDown;

        emojiList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isScaling && temp != null) {
            temp.transform.localScale = tempScale + Vector3.one * (_controllerConnectionHandler.ConnectedController.Position.z - tempPos.z) * .03f;
        }
    }

    private void OnDestroy()
    {
        MLInput.OnTriggerDown -= OnTriggerDown;
        MLInput.OnTriggerUp -= OnTriggerUp;
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

            if (button == MLInputControllerButton.HomeTap && !isDrawing) {
                int count = emojiList.Count;
                if (count > 0) {
                    Destroy(emojiList[count - 1]);
                    emojiList.RemoveAt(count - 1);
                }
            }
        }
    }
    private void OnTriggerDown(byte controllerId, float value)
    {
        if (_controllerConnectionHandler.IsControllerValid(controllerId) && mode == userMode.emoji)
        {
            if (mode == userMode.emoji && !isScaling)
            {
                MLInputController controller = _controllerConnectionHandler.ConnectedController;
                temp = Instantiate(emojiprfab[EmojiSelect.currentSelect], emoji[EmojiSelect.currentSelect].transform.position, Quaternion.identity);
                isScaling = true;
                emojiList.Add(temp);
                tempPos = controller.Position;
                tempScale = temp.transform.localScale;
                //temp.transform.SetParent(parent.transform);
            }

        }
    }

    private void OnTriggerUp(byte controllerId, float value)
    {
        if (_controllerConnectionHandler.IsControllerValid(controllerId) && mode == userMode.emoji)
        {
            if (mode == userMode.emoji)
            {
                isScaling = false;
                temp = null;
                //temp.transform.SetParent(parent.transform);
            }

        }
    }

    #endregion
}
