namespace Loupedeck.DmxPlugin
{
    using System;

    public class AllCommand : PluginDynamicCommand
    {
        private DmxPlugin plugin;

        public AllCommand()
            : base("All", "Change all Channels", "Colors")
        {
            this.MakeProfileAction("text;Values (separated by : )");
        }
        protected override Boolean OnLoad()
        {
            this.plugin = base.Plugin as DmxPlugin;

            return !(this.plugin is null);
        }

        protected override void RunCommand(String actionParameter)
        {
            var channelsValues = actionParameter.Split(':');

            for (var i = 0; i < 512 && i < channelsValues.Length; i++)
            {
                if (Int32.TryParse(channelsValues[i], out var value))
                {
                    this.ActionImageChanged(actionParameter);

                    this.plugin.client.SetChannel(i + 1, (Byte) value);
                }
            }
        }
    }
}
