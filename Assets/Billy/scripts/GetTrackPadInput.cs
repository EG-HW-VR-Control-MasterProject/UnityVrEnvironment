using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GetTrackPadInput : MonoBehaviour
{
    public float m_Sensitivity = 0.1f;
    public float m_MaxLinearSpeed = 1.0f;
    public float m_MaxAngularSpeed = 1.0f;
    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;

    private float m_LinearSpeed = 0.0f;
    private float m_AngularSpeed = 0.0f;

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        // Figure out mouvement orientation
        // If not moving
        if (m_MovePress.GetStateUp(SteamVR_Input_Sources.Any))
        {
            m_LinearSpeed = 0;
        }
        //if button pressed
        if (m_MovePress.state)
        {
            // Take the values
            m_LinearSpeed += m_MoveValue.axis.y * m_Sensitivity;
            m_AngularSpeed += m_MoveValue.axis.x * m_Sensitivity;
            // Limite the speeds
            m_LinearSpeed = Mathf.Clamp(m_LinearSpeed, -m_MaxLinearSpeed, m_MaxLinearSpeed);
            m_AngularSpeed = Mathf.Clamp(m_AngularSpeed, -m_MaxAngularSpeed, m_MaxAngularSpeed);

            print(m_MoveValue.axis.x);
            

        }
        
    }
}