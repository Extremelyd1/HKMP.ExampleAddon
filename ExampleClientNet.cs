using Hkmp;
using Hkmp.Api.Client;
using Hkmp.Networking.Packet;

namespace ExampleAddon {
    /// <summary>
    /// Separate class that manages client-side networking of this example addon.
    /// </summary>
    public class ExampleClientNet {
        /// <summary>
        /// Constructor of this class.
        /// </summary>
        /// <param name="logger">The logger used to log to the ModLog.</param>
        /// <param name="addon">The client addon for which to register network sender and receiver.</param>
        /// <param name="clientApi">The client API instance.</param>
        public ExampleClientNet(ILogger logger, ClientAddon addon, IClientApi clientApi) {
            // Get a network receiver for this addon, which we can use to register handlers for certain packet IDs
            // We need to provide the type of the incoming packet ID enum (in this case ServerPacketId), the addon
            // instance (passed through this constructor) and a method that is able to instantiate IPacketData
            // classes given a packet ID
            var netReceiver = clientApi.NetClient.GetNetworkReceiver<ClientPacketId>(addon, InstantiatePacket);

            // Get a network sender for this addon, which we can use to send packet data over the network.
            // We need to provide the type of the outgoing packet ID enum (in this case ClientPacketId) and
            // the addon instance (passed through this constructor).
            var netSender = clientApi.NetClient.GetNetworkSender<ServerPacketId>(addon);
            
            // Using the network receiver we register a handler for a specific enum value of the ServerPacketId
            // enum. We also provide the type of the packet data that we expect to get from this packet ID.
            // If this type does not match, the packet handler will throw an exception.
            netReceiver.RegisterPacketHandler<ClientPacketData>(
                ClientPacketId.PacketId1,
                packetData => {
                    logger.Info(this, $"Received client packet data: {packetData.SomeUShort}");
                }
            );

            // For this example we register the PlayerConnect event and send packet data containing a float
            // with the packet ID "PacketId1", which should be registered by the server addon to receive this data
            clientApi.ClientManager.PlayerConnectEvent += player => {
                logger.Info(this, "Player connected, sending PI to server");
                
                netSender.SendSingleData(ServerPacketId.PacketId1, new ServerPacketData {
                    SomeFloat = 3.141592f
                });
            };
        }

        /// <summary>
        /// Method that provides the instantiation of IPacketData classes given a packet ID.
        /// </summary>
        /// <param name="packetId">The packet ID to instantiate a class for.</param>
        /// <returns>An instantiation of IPacketData.</returns>
        private static IPacketData InstantiatePacket(ClientPacketId packetId) {
            switch (packetId) {
                case ClientPacketId.PacketId1:
                    return new ClientPacketData();
            }

            return null;
        }
    }
}
