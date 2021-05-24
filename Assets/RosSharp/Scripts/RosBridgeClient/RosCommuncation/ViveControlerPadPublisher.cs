using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace RosSharp.RosBridgeClient
{
    public class ViveControlerPadPublisher : Publisher<Messages.Geometry.Vector3>
    {
        public SteamVR_Input_Sources handType;
        public SteamVR_Action_Boolean UpDpadActionBoolean;
        public SteamVR_Action_Boolean DownDpadActionBoolean;
        public SteamVR_Action_Boolean LeftDpadActionBoolean;
        public SteamVR_Action_Boolean RightDpadActionBoolean;
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
            // Vector3 with (x:1=up and -1=down, y:1=left and -1=right)
            message = new Messages.Geometry.Vector3();
        }
        private void UpdateMessage()
        {
            if (GetUpPad())
            {
                print("Up " + handType);
                message.x = 1;
            }
            else if (GetDownPad())
            {
                print("Down " + handType);
                message.x = -1;
            }

            else if (GetLeftPad())
            {
                print("Left " + handType);
                message.y = 1;
            }
            else if (GetRightPad())
            {
                print("Right " + handType);
                message.y = -1;
            }
            else
            {
                message.x = 0;
                message.y = 0;
            }

            Publish(message);
        }
        public bool GetUpPad()
        {
            return UpDpadActionBoolean.GetState(handType);
        }
        public bool GetDownPad()
        {
            return DownDpadActionBoolean.GetState(handType);
        }
        public bool GetLeftPad()
        {
            return LeftDpadActionBoolean.GetState(handType);
        }
        public bool GetRightPad()
        {
            return RightDpadActionBoolean.GetState(handType);
        }

    }
}