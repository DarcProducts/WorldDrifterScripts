using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDetector : MonoBehaviour
{
    private bool connected = false;

    IEnumerator CheckForControllers()
    {
        while (true)
        {
            var controllers = Input.GetJoystickNames();
            if (!connected && controllers.Length > 0)
            {
                connected = true;
                Debug.Log("Connected");
            }
            else if (connected && controllers.Length == 0)
            {
                connected = false;
                Debug.Log("Disconnected");
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void Awake()
    {
        StartCoroutine(CheckForControllers());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1)) //B button press
        {
            Debug.Log("B button pressed");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton8)) //left thumb down press
        {
            Debug.Log("left thumb button pressed!");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton9)) //right thumb press
        {
            Debug.Log("right thumb button pressed!");
        }
    }
}
