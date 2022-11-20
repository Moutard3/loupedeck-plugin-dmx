namespace Loupedeck.DmxPlugin
{
    using System;
    using System.Linq;
    using System.Threading;

    using OpenDMX.NET;

    // This class contains the plugin-level logic of the Loupedeck plugin.

    public class DmxPlugin : Plugin
    {
        // Gets a value indicating whether this is an Universal plugin or an Application plugin.
        public override Boolean UsesApplicationApiOnly => true;

        // Gets a value indicating whether this is an API-only plugin.
        public override Boolean HasNoApplication => true;

        public readonly DmxClient client = new DmxClient();

        // This method is called when the plugin is loaded during the Loupedeck service start-up.
        public override void Load()
        {
            this.client.Connect();
        }

        // This method is called when the plugin is unloaded during the Loupedeck service shutdown.
        public override void Unload()
        {
            this.client.Disconnect();
        }
    }
}
