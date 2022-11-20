namespace Loupedeck.DmxPlugin
{
    using System;

    public class ChannelCommand : PluginDynamicCommand
    {
        private DmxPlugin plugin;

        public ChannelCommand()
            : base("Channel", "Change specific channel value", "Channels")
        {
            /*
            for (var i = 1; i <= 9; i++)
            {
                var actionParameter = i.ToString();

                this.AddParameter(actionParameter, $"Channel {i}", "Channels");
            }
            */

            this.MakeProfileAction("text;channel:value");
        }

        protected override Boolean OnLoad()
        {
            this.plugin = base.Plugin as DmxPlugin;

            return !(this.plugin is null);
        }

        protected override void RunCommand(String actionParameter)
        {
            var paramsArr = actionParameter.Split(':');

            if (Int32.TryParse(paramsArr[0], out var chann) && Int32.TryParse(paramsArr[1], out var val))
            {
                this.plugin.client.SetChannel(chann, (Byte) val);

                // inform Loupedeck that command display name and/or image has changed
                this.ActionImageChanged(actionParameter);
            }
        }
    }
}
