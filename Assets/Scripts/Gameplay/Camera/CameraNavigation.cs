using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNavigation : MonoBehaviour
{
    public float panSpeed = 30f;
    float FoV;
    public float FovMin;
    public float FovMax;

    //To get the main camera's field of view
    void Start()
    {
        FoV = Camera.main.fieldOfView;
    }

    
    void Update() //This are the initialization and the bindings of the controls which link them to the translation below.
    {
        //Below are the inputs needed to allow the camera to move. It gets the vector direction and multiply with speed to allow its speed to scale.
        //Also, the last parameters move the object gobally.
       if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * panSpeed, Space.World); // Move the object gobally in the forward direction 
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * Time.deltaTime * panSpeed, Space.World); // Move the object gobally in the backward direction 
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * panSpeed, Space.World); // Move the object gobally in the left direction 
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * panSpeed, Space.World);  // Move the object gobally in the right direction
        } else if (Input.GetKeyDown(KeyCode.Minus))
        {
            float resultantView = FoV + 5; // Get FoV for references.
            if (resultantView <= FovMax) // This prevent the camera's FoV from over shooting the max variable set by me.
                //Which clamps its zooming capabilities.
            {
                FoV += 5;
                Camera.main.fieldOfView = FoV; 
            }
        } else if (Input.GetKeyDown(KeyCode.Equals)) // Using the (=) sign to zoom out by reducing its FoV for every press by 5
        {
            float resultantView = FoV - 5; //Clamping to prevent the camera from zooming out too much.
            if (resultantView >= FovMin)
            {
                FoV -= 5; // this detuct 5 from the Var FoV when this (=) is pressed, this is a self assignment operator.
                Camera.main.fieldOfView = FoV;
            }
        }

       
       
    }
}
