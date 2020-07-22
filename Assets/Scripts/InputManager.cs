using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputData inputData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WriteInputData();
    }

    void WriteInputData()
    {
        //inputData.isPressed = Input.GetMouseButtonDown(0);
        //inputData.isHeld = Input.GetMouseButton(0);
        //inputData.isReleased = Input.GetMouseButtonUp(0);
    }
}
