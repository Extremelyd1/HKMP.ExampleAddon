using Hkmp.Networking.Packet;

namespace ExampleAddon {
    /// <summary>
    /// Example class implementing the IPacketData interface, which represents data that is sent to the client.
    /// </summary>
    public class ClientPacketData : IPacketData {
        /// <summary>
        /// Denote whether this packet data should be handled as reliable data. This means that it will try to
        /// resend the data if the packet it was in is not acknowledged by the receiving end.
        /// </summary>
        public bool IsReliable => true;
        /// <summary>
        /// Denote whether this data can be disregarded if a newer version of the data is also included in the packet.
        /// This is useful for cases where the data contains a state that completely overwrites earlier entries.
        /// </summary>
        public bool DropReliableDataIfNewerExists => true;
        
        /// <summary>
        /// This is the actual unsigned short we are transmitting.
        /// </summary>
        public ushort SomeUShort { get; set; }
        
        /// <summary>
        /// The method that handles writing the data from this class into the given packet.
        /// </summary>
        /// <param name="packet">The packet interface for writing data.</param>
        public void WriteData(IPacket packet) {
            packet.Write(SomeUShort);
        }

        /// <summary>
        /// The method that handles reading the data from the given packet into this class.
        /// </summary>
        /// <param name="packet">The packet interface for reading data.</param>
        public void ReadData(IPacket packet) {
            SomeUShort = packet.ReadUShort();
        }
    }
    
    /// <summary>
    /// Example class implementing the IPacketData interface, which represents data that is sent to the server.
    /// </summary>
    public class ServerPacketData : IPacketData {
        /// <summary>
        /// Denote whether this packet data should be handled as reliable data. This means that it will try to
        /// resend the data if the packet it was in is not acknowledged by the receiving end.
        /// </summary>
        public bool IsReliable => true;
        /// <summary>
        /// Denote whether this data can be disregarded if a newer version of the data is also included in the packet.
        /// This is useful for cases where the data contains a state that completely overwrites earlier entries.
        /// </summary>
        public bool DropReliableDataIfNewerExists => true;
        
        /// <summary>
        /// This is the actual float we are transmitting.
        /// </summary>
        public float SomeFloat { get; set; }
        
        /// <summary>
        /// The method that handles writing the data from this class into the given packet.
        /// </summary>
        /// <param name="packet">The packet interface for writing data.</param>
        public void WriteData(IPacket packet) {
            packet.Write(SomeFloat);
        }

        /// <summary>
        /// The method that handles reading the data from the given packet into this class.
        /// </summary>
        /// <param name="packet">The packet interface for reading data.</param>
        public void ReadData(IPacket packet) {
            SomeFloat = packet.ReadFloat();
        }
    }
    
    /// <summary>
    /// The enum class representing packet IDs for server to client communication.
    /// </summary>
    public enum ClientPacketId {
        PacketId1
    }
    
    /// <summary>
    /// The enum class representing packet IDs for client to server communication.
    /// </summary>
    public enum ServerPacketId {
        PacketId1
    }
}
