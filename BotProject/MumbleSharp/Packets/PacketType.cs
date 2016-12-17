
namespace MumbleSharp.Packets
{
    public enum DenyType
    {
        // Operation denied for other reason, see reason field. 
        Text = 0,
        // Permissions were denied. 
        Permission = 1,
        // Cannot modify SuperUser. 
        SuperUser = 2,
        // Invalid channel name. 
        ChannelName = 3,
        // Text message too long. 
        TextTooLong = 4,
        // The flux capacitor was spelled wrong. 
        H9K = 5,
        // Operation not permitted in temporary channel. 
        TemporaryChannel = 6,
        // Operation requires certificate. 
        MissingCertificate = 7,
        // Invalid username. 
        UserName = 8,
        // Channel is full. 
        ChannelFull = 9,
        NestingLimit = 10,
    } 
    public enum PacketType
        :short
    {
        Version         = 0,
        UDPTunnel       = 1,
        Authenticate    = 2,
        Ping            = 3,
        Reject          = 4,
        ServerSync      = 5,
        ChannelRemove   = 6,
        ChannelState    = 7,
        UserRemove      = 8,
        UserState       = 9,
        BanList         = 10,
        TextMessage     = 11,
        PermissionDenied= 12,
        ACL             = 13,
        QueryUsers      = 14,
        CryptSetup      = 15,
        ContextActionAdd= 16,
        ContextAction   = 17,
        UserList        = 18,
        VoiceTarget     = 19,
        PermissionQuery = 20,
        CodecVersion    = 21,
        UserStats       = 22,
        RequestBlob     = 23,
        ServerConfig    = 24,
        SuggestConfig   = 25,
        Empty = 32767
    }
}
