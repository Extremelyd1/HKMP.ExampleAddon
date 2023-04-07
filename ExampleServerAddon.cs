using Hkmp.Api.Server;

namespace ExampleAddon {
    /// <summary>
    /// The class that extends the ServerAddon class as a starting point for the server addon.
    /// </summary>
    public class ExampleServerAddon : ServerAddon {
        /// <summary>
        /// The Initialize method should be overridden from the base class and can be used to start doing things
        /// with the API.
        /// </summary>
        public override void Initialize(IServerApi serverApi) {
            // The Logger variable in the ServerAddon base class can be used to log things to the ModLog
            Logger.Info("Initializing server-side example addon!");

            // We instantiate a new ExampleClientNet with the logger, this addon class and the server API
            new ExampleServerNet(Logger, this, serverApi.NetServer);
        }

        /// <summary>
        /// The overridden Name property denotes the registered name of our addon. If we want to make use
        /// of networking features, this name should match the name of the ClientAddon.
        /// </summary>
        protected override string Name => "ExampleAddon";
        /// <summary>
        /// The overridden Version property denotes the registered version of our addon. If we want to make use
        /// of networking features, this version should match the version of the ClientAddon.
        /// </summary>
        protected override string Version => "0.0.1";
        /// <summary>
        /// The overridden NeedsNetwork property denotes whether this addon wants to use the network. If false,
        /// any calls to the networking methods of the API will throw an exception. If true, we can request
        /// network receivers and senders to use the network.
        /// </summary>
        public override bool NeedsNetwork => true;
    }
}
