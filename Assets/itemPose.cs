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
public class itemPose : Message
{
    [JsonIgnore]
    public const string RosMessageName = "master_project/itemPose";

    public String id { get; set; }
    public Pose pose { get; set; }

    public itemPose()
    {
        this.id = new String();
        this.pose = new Pose();
    }
}
}

