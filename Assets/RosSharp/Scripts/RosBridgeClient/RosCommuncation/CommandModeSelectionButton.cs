using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient
{
    public class CommandModeSelectionButton : Publisher<Messages.Standard.Int32>
    {
        private Messages.Standard.Int32 message;
        public GameObject selectedModeFromButtons;
        
        public int selectedMode;
        /*
        public Button btn;
        public Button teleopButton;
        public Button autonomButton;
        */
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
            /*
            if (CommandMode())
            {
                print("Autonomous Mode");
                message.x = 1;
            }
            else
            {
                print("Manual Mode");
                message.x = 0;
            }
            */
            message.data = CommandMode();
            Publish(message);
        }
        public int CommandMode()
        {
            LineRendererSettings inputScript = selectedModeFromButtons.GetComponent<LineRendererSettings>();
            selectedMode = inputScript.selectedMode;
            return selectedMode;
        }


    }
}