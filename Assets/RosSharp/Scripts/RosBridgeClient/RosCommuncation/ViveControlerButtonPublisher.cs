using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace RosSharp.RosBridgeClient
{
    public class ViveControlerButtonPublisher : Publisher<Messages.Geometry.Vector3>
    {
        public SteamVR_Input_Sources handType;
        public SteamVR_Action_Boolean grabAction; 
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
            if (GetGrab())
            {
                print("Grab " + handType);
                message.x = 1;
            }
            else
            {
                message.x = 0;
            }
            
            Publish(message);
        }
        public bool GetGrab() // 2
        {
            return grabAction.GetState(handType);
        }


    }
}