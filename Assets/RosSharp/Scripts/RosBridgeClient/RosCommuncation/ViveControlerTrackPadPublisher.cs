using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace RosSharp.RosBridgeClient
{
    public class ViveControlerTrackPadPublisher : Publisher<Messages.Geometry.Vector3>
    {
        public SteamVR_Input_Sources handType;
        public float m_Sensitivity = 0.1f;
        public float m_MaxLinearSpeed = 1f;
        public float m_MaxAngularSpeed = 1f;
        public SteamVR_Action_Boolean TouchTrackPadActionBoolean;
        public SteamVR_Action_Vector2 TrackPadActionVector2;

        private float m_LinearSpeed = 0.0f;
        private float m_AngularSpeed = 0.0f;
        private Messages.Geometry.Vector3 message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void InitializeMessage()
        {
            message = new Messages.Geometry.Vector3();
        }
        private void UpdateMessage()
        {
            // If not moving
            if (TouchTrackPadActionBoolean.GetStateUp(handType))
            {
                m_LinearSpeed = 0;
                m_AngularSpeed = 0;

            }
            //if button pressed
            if (TouchTrackPadActionBoolean.GetState(handType))
            {
                // Take the values
                m_LinearSpeed = TrackPadActionVector2.GetAxis(handType).y * m_Sensitivity;
                m_AngularSpeed = TrackPadActionVector2.GetAxis(handType).x * m_Sensitivity;
                // Limite the speeds
                m_LinearSpeed = Mathf.Clamp(m_LinearSpeed, -m_MaxLinearSpeed, m_MaxLinearSpeed)*5;
                m_AngularSpeed = -Mathf.Clamp(m_AngularSpeed, -m_MaxAngularSpeed, m_MaxAngularSpeed)*5;
                print(m_AngularSpeed);
            }
            message.x = m_LinearSpeed;
            message.y = m_AngularSpeed;
            Publish(message);
        }
        
    }
}