﻿using Hkmp.Api.Client;

namespace ExampleAddon {
    /// <summary>
    /// The class that extends the ClientAddon class as a starting point for the client addon.
    /// </summary>
    public class ExampleClientAddon : ClientAddon {
        /// <summary>
        /// The Initialize method should be overridden from the base class and can be used to start doing things
        /// with the API.
        /// </summary>
        public override void Initialize(IClientApi clientApi) {
            // The Logger variable in the ClientAddon base class can be used to log things to the ModLog
            Logger.Info("Initializing client-side example addon!");

            // We instantiate a new ExampleClientNet with the logger, this addon class and the client API
            new ExampleClientNet(Logger, this, clientApi);
        }

        /// <summary>
        /// The overridden Name property denotes the registered name of our addon. If we want to make use
        /// of networking features, this name should match the name of the ServerAddon.
        /// </summary>
        protected override string Name => "ExampleAddon";
        /// <summary>
        /// The overridden Version property denotes the registered version of our addon. If we want to make use
        /// of networking features, this version should match the version of the ServerAddon.
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
