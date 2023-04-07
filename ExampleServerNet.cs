using System;
using Hkmp.Api.Server;
using Hkmp.Api.Server.Networking;
using Hkmp.Logging;
using Hkmp.Networking.Packet;

namespace ExampleAddon {
    /// <summary>
    /// Separate class that manages server-side networking of this example addon.
    /// </summary>
    public class ExampleServerNet {
        /// <summary>
        /// Constructor of this class.
        /// </summary>
        /// <param name="logger">The logger used to log to the ModLog.</param>
        /// <param name="addon">The server addon for which to register network sender and receiver.</param>
        /// <param name="netServer">The net server interface.</param>
        public ExampleServerNet(ILogger logger, ServerAddon addon, INetServer netServer) {
            // Get a network receiver for this addon, which we can use to register handlers for certain packet IDs
            // We need to provide the type of the incoming packet ID enum (in this case ClientPacketId), the addon
            // instance (passed through this constructor) and a method that is able to instantiate IPacketData
            // classes given a packet ID
            var netReceiver = netServer.GetNetworkReceiver<ServerPacketId>(addon, InstantiatePacket);

            // Get a network sender for this addon, which we can use to send packet data over the network.
            // We need to provide the type of the outgoing packet ID enum (in this case ServerPacketId) and
            // the addon instance (passed through this constructor).
            var netSender = netServer.GetNetworkSender<ClientPacketId>(addon);
            
            // Using the network receiver we register a handler for a specific enum value of the ClientPacketId
            // enum. We also provide the type of the packet data that we expect to get from this packet ID.
            // If this type does not match, the packet handler will throw an exception.
            netReceiver.RegisterPacketHandler<ServerPacketData>(
                ServerPacketId.PacketId1,
                (id, packetData) => {
                    // Get the float from the packet data
                    var someFloat = packetData.SomeFloat;
                    
                    // Log the player ID and the float value
                    logger.Info($"Received server packet data from ID {id}: {someFloat}");
                    
                    // Then send response data to the client by flooring the received float
                    netSender.SendSingleData(ClientPacketId.PacketId1, new ClientPacketData {
                        SomeUShort = (ushort) Math.Floor(someFloat)
                    }, id);
                }
            );
        }
        
        /// <summary>
        /// Method that provides the instantiation of IPacketData classes given a packet ID.
        /// </summary>
        /// <param name="packetId">The packet ID to instantiate a class for.</param>
        /// <returns>An instantiation of IPacketData.</returns>
        private static IPacketData InstantiatePacket(ServerPacketId packetId) {
            switch (packetId) {
                case ServerPacketId.PacketId1:
                    return new ServerPacketData();
            }

            return null;
        }
    }
}
