using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient
{
    public class ViveDropDownIndexPublisher : Publisher<Messages.Standard.Int32>
    {

        public Dropdown dropdown;
        private Messages.Standard.Int32 message;
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
            message = new Messages.Standard.Int32();
        }
        private void UpdateMessage()
        {

            message.data = dropdown.value;
            print(message.data);
            Publish(message);
        }
        


    }
}