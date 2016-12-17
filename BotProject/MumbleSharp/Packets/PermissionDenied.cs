using System;
using ProtoBuf;

namespace MumbleSharp.Packets
{
    [ProtoContract]
    public class PermissionDenied
    {
 	// The denied permission when type is Permission.
        [ProtoMember(1, IsRequired = false)]
        public UInt32[] permission;
 	// channel_id for the channel where the permission was denied when type is 
 	// Permission. 
        [ProtoMember(2, IsRequired = false)]
        public UInt32[] channel_id;
 	// The user who was denied permissions, identified by session. 
        [ProtoMember(3, IsRequired = false)]
        public UInt32[] session; 
 	// Textual reason for the denial. 
        [ProtoMember(4, IsRequired = false)]
        public string reason;
 	// Type of the denial.
        [ProtoMember(5, IsRequired = false)]
        public DenyType deny;
 	// The name that is invalid when type is UserName. 
        [ProtoMember(6, IsRequired = false)]
        public string name;
    }
}
