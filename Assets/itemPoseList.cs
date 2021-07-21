/*
This message class is generated automatically with 'SimpleMessageGenerator' of ROS#
*/

using Newtonsoft.Json;
using RosSharp.RosBridgeClient.Messages.Geometry;
using RosSharp.RosBridgeClient.Messages.Navigation;
using RosSharp.RosBridgeClient.Messages.Sensor;
using RosSharp.RosBridgeClient.Messages.Standard;
using RosSharp.RosBridgeClient.Messages.Actionlib;

namespace RosSharp.RosBridgeClient.Messages
{
    public class itemPoseList : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "master_project/itemPoseList";

        public itemPose[] item_pose_list;

        public itemPoseList()
        {
            this.item_pose_list = new itemPose[0];
        }

        public itemPoseList(itemPose[] item_pose_list)
        {
            this.item_pose_list = item_pose_list;
        }
    }
}