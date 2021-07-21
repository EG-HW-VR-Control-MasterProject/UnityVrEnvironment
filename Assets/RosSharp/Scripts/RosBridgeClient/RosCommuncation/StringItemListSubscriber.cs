/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using UnityEngine;
using System.Collections.Generic;

namespace RosSharp.RosBridgeClient
{
    public class StringItemListSubscriber : Subscriber<Messages.Standard.String>
    {
        
        private bool isMessageReceived;
        public string StringItems;
        public List<string> items = new List<string>();
        protected override void Start()
        {
            base.Start();
        }

        private void Update()
        {
            if (isMessageReceived)
            {
                ProcessMessage();
            }
                
        }

        protected override void ReceiveMessage(Messages.Standard.String message)
        {
            StringItems = message.data;

            isMessageReceived = true;
        }
        private void ProcessMessage()
        {

            string[] splitArray = StringItems.Split(char.Parse("-"));
            items = new List<string>();
            for (int index = 0; index < splitArray.Length; index++)
            {
                items.Add(splitArray[index]);
            }
            isMessageReceived = false;
        }

    }
}