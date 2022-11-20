namespace Loupedeck.DmxPlugin
{
    using System;
    using System.Threading.Tasks;

    // This class contains the plugin-level logic of the Loupedeck plugin.

    public class DmxPlugin : Plugin
    {
        // Gets a value indicating whether this is an Universal plugin or an Application plugin.
        public override Boolean UsesApplicationApiOnly => true;

        // Gets a value indicating whether this is an API-only plugin.
        public override Boolean HasNoApplication => true;

        public DmxClient client = new DmxClient();

        private Boolean isStopped = false;

        private async void TryConnect()
        {
            while (!this.isStopped)
            {
                if (!this.client.IsConnected())
                {
                    this.client = new DmxClient();
                    this.client.Connect();
                }

                await Task.Delay(5000);
            }
        }

        // This method is called when the plugin is loaded during the Loupedeck service start-up.
        public override void Load()
        {
            Task.Run(this.TryConnect);
        }

        // This method is called when the plugin is unloaded during the Loupedeck service shutdown.
        public override void Unload()
        {
            this.client.Disconnect();

            this.isStopped = true;
        }
    }
}
