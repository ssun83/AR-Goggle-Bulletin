using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ModeControl : MonoBehaviour
{
    public ControllerConnectionHandler _controllerConnectionHandler;
    public enum userMode { draw, emoji, select};
    public userMode mode = userMode.draw;
    public bool isDrawing = true;
    public bool isEmoji = false;
    public GameObject DrawTip;
    public GameObject EmojiTip;
    public GameObject emojiCanvas;
    public GameObject Palette;
    public GameObject SelectionPad;
    public GameObject ActionPad;
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

        EmojiTip.GetComponent<SpriteRenderer>().sprite = emojiprfab[EmojiSelect.currentSelect].GetComponent<SpriteRenderer>().sprite; ;
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
                    EmojiTip.SetActive(true);
                    emojiCanvas.SetActive(true);
                    Palette.SetActive(false);
                    SelectionPad.SetActive(false);
                    ActionPad.SetActive(false);
                    isDrawing = false;
                    isEmoji = true;
                } else if (mode == userMode.emoji)
                {
                    mode = userMode.select;
                    DrawTip.SetActive(false);
                    EmojiTip.SetActive(false);
                    emojiCanvas.SetActive(false);
                    Palette.SetActive(false);
                    SelectionPad.SetActive(true);
                    ActionPad.SetActive(true);
                    isDrawing = false;
                    isEmoji = false;
                } else {
                    mode = userMode.draw;
                    DrawTip.SetActive(true);
                    EmojiTip.SetActive(false);
                    emojiCanvas.SetActive(false);
                    Palette.SetActive(true);
                    SelectionPad.SetActive(false);
                    ActionPad.SetActive(false);
                    isDrawing = true;
                    isEmoji = false;
                }
            }

            if (button == MLInputControllerButton.HomeTap && isEmoji)
            {
                int count = emojiList.Count;
                if (count > 0)
                {
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
                temp = Instantiate(emojiprfab[EmojiSelect.currentSelect], EmojiTip.transform.position, Quaternion.identity);
                isScaling = true;
                emojiList.Add(temp);
                tempPos = controller.Position;
                tempScale = temp.transform.localScale;
                controller.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.Buzz, MLInputControllerFeedbackIntensity.Medium);
                emojiCanvas.SetActive(false);
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
                emojiCanvas.SetActive(true);
                //temp.transform.SetParent(parent.transform);
            }

        }
    }

    #endregion
}
