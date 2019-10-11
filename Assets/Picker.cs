using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Picker : MonoBehaviour
{
    public ControllerConnectionHandler _controllerConnectionHandler;
    public ModeControl mc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (_controllerConnectionHandler.IsControllerValid())
        {
            MLInputController controller = _controllerConnectionHandler.ConnectedController;
            if (controller.Type == MLInputControllerType.Control && controller.Touch1Active)
            {
                transform.localPosition = new Vector3(controller.Touch1PosAndForce.x, controller.Touch1PosAndForce.y, 0f);
            }
                    
        }
    }
}
