﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Valve.VR;
public class LineRendererSettings : MonoBehaviour
{
    //Declare a LineRenderer to store the component attached to the GameObject. 
    [SerializeField] LineRenderer rend;

    //Settings for the LineRenderer are stored as a Vector3 array of points. Set up a V3 array to 
    //initialize in Start. 
    Vector3[] points;

    //declare the pannel to change
    public GameObject panel; 
    public Image img; 
    public Button btn;
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Action_Boolean showButtonMenuAction;
    public SteamVR_Behaviour_Pose controllerPose;
    public Canvas ButtonMenuVR;

    public Button teleopButton;
    public Button autonomButton;
    public int selectedMode;


    public bool buttonMenuState;
    public bool buttonMenuStatePrev;
    //Start is called before the first frame update
    void Start()
    {

        //get the LineRenderer attached to the gameobject.     
        rend = gameObject.GetComponent<LineRenderer>();   
        
        //initialize the LineRenderer    
        points = new Vector3[2];    

        //set the start point of the linerenderer to the position of the gameObject.     
        points[0] = Vector3.zero;    
        
        //set the end point 20 units away from the GO on the Z axis (pointing forward)    
        points[1] = transform.position + new Vector3(0, 0, 20);    
        
        //finally set the positions array on the LineRenderer to our new values    
        rend.SetPositions(points);    
        rend.enabled = true;

        // Hide the Button menu by default
        ButtonMenuVR.enabled = false;
        rend.enabled = false;
        //img = panel.GetComponent<Image>();

    }

    public LayerMask layerMask;
    public bool AlignLineRenderer(LineRenderer rend) {
        bool hitBtn = false;
        Ray ray;
        //ray = new Ray(transform.position, transform.forward); 
        ray = new Ray(controllerPose.transform.position, transform.forward);
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(ray, out hit, 20)) {
            print("hit something");
            //points[1] = transform.forward + new Vector3(0, 0, hit.distance);
            points[1] = transform.forward + new Vector3(0, 0, 20);
            rend.startColor = Color.red;
            rend.endColor = Color.red;

            btn = hit.collider.gameObject.GetComponent<Button>();
            hitBtn = true;

        } else { 
            points[1] = transform.forward + new Vector3(0, 0, 20);
            rend.startColor = Color.green;
            rend.endColor = Color.green;

            hitBtn = false;
        }
        if(btn != null)
        {
            print(btn.name);
        }
        
        rend.SetPositions(points);
        rend.material.color = rend.startColor;

        return hitBtn;
    }

    public void ColorChangeOnClick()
    {
        if (btn != null) { 
            if (btn.name == "teleoperationButton") {
                //btn.image.color = Color.red;
                teleopButton.image.color = Color.red;
                autonomButton.image.color = Color.white;

                selectedMode = 0;

            }
            else if (btn.name == "AutonomousButton") {
                //btn.image.color = Color.blue;
                teleopButton.image.color = Color.white;
                autonomButton.image.color = Color.blue;

                selectedMode = 1;
            }
            /*
            else if (btn.name == "green_button") {
                btn.image.color = Color.green; 
            }
            */
        }
    }

    public bool GetGrab() // 2
    {
        return grabAction.GetState(handType);
    }
    public bool GetButtonMenu()
    {
        return showButtonMenuAction.GetState(handType);
    }
    // Update is called once per frame
    void Update()
    {
        
        AlignLineRenderer(rend);

        buttonMenuState = GetButtonMenu();
        if (GetGrab())
        {
            print("Grab " + handType);
            
        }

        if (buttonMenuState != buttonMenuStatePrev && buttonMenuState == true)
        {
            
            print("Disp Menu");
            ButtonMenuVR.enabled = !ButtonMenuVR.enabled;
            rend.enabled = !rend.enabled;
        }
        buttonMenuStatePrev = buttonMenuState;
        //if (AlignLineRenderer(rend) && GetGrab()) {
        if (GetGrab() && (ButtonMenuVR.enabled == true)) {
            print("Button pushed");
            btn.onClick.Invoke(); 
        }
    }

    
}
