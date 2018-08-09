using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSetUp : MonoBehaviour {

    private string[] _controllerNames;
    private int _index = 0;

	// Use this for initialization
	void Start () {
        _controllerNames = new string[4];
	}
	
	// Update is called once per frame
	void Update () {
        JoystickSetup(_controllerNames);
    }

    void JoystickSetup(string[] names) {
        if (Input.GetKeyDown(KeyCode.JoystickButton7) && _index < 4) {
            if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                names[_index] = "Joystick1";
            }
            else if (Input.GetKeyDown(KeyCode.Joystick2Button7))
            {
                names[_index] = "Joystick2";
            }
            else if (Input.GetKeyDown(KeyCode.Joystick3Button7))
            {
                names[_index] = "Joystick3";
            }
            else if (Input.GetKeyDown(KeyCode.Joystick4Button7))
            {
                names[_index] = "Joystick4";
            }
            else if (Input.GetKeyDown(KeyCode.Joystick5Button7))
            {
                names[_index] = "Joystick5";
            }
            else if (Input.GetKeyDown(KeyCode.Joystick6Button7))
            {
                names[_index] = "Joystick6";
            }
            else if (Input.GetKeyDown(KeyCode.Joystick7Button7))
            {
                names[_index] = "Joystick7";
            }
            _index++;
        }
    }

    public string GetController(int index) {
        if (_controllerNames[index] != null)
        {
            return _controllerNames[index];
        }
        else {
            return null;
        }
    }
}
