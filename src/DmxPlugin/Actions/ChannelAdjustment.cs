namespace Loupedeck.DmxPlugin
{
    using System;

    public class ChannelAdjustment : PluginDynamicAdjustment
    {
        private DmxPlugin plugin;

        public ChannelAdjustment()
            : base(displayName: "Channel", description: "Change specific channel value", groupName: "Channels", false)
        {
            this.MakeProfileAction("text;channel");
        }

        protected override Boolean OnLoad()
        {
            this.plugin = base.Plugin as DmxPlugin;

            return !(this.plugin is null);
        }

        protected override void ApplyAdjustment(String actionParameter, Int32 diff)
        {
            if (Int32.TryParse(actionParameter, out var i))
            {
                this.plugin.client.AddToChannel(i, diff);

                this.ActionImageChanged(actionParameter);
            }
        }

        protected override String GetAdjustmentValue(String actionParameter) => this.plugin.client.GetChannel(Int32.Parse(actionParameter)).ToString();
    }
}
