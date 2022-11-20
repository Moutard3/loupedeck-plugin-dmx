namespace Loupedeck.DmxPlugin
{
    using System;
    using System.Linq;

    using OpenDMX.NET;

    public class DmxClient
    {
        private readonly DmxController dmx = new DmxController();

        private readonly Byte[] channels = new Byte[9];

        public void Connect()
        {
            var devices = this.dmx.GetDevices();

            if (devices.Length > 0)
            {
                this.dmx.Open(devices.First().DeviceIndex);
            }
        }

        public void Disconnect() => this.dmx.Dispose();

        public void Send() => this.dmx.SetChannels(1, this.channels);

        public Byte GetChannel(Int32 channel) => channel < 1 || channel > this.channels.Length ? (Byte) 0 : this.channels[channel - 1];

        public void SetChannel(Int32 channel, Byte value)
        {
            if (channel < 1 || channel > this.channels.Length)
            {
                return;
            }

            this.channels[channel - 1] = value;

            this.Send();
        }

        public void AddToChannel(Int32 channel, Int32 value)
        {
            if (channel < 1 || channel > this.channels.Length)
            {
                return;
            }

            var currentValue = this.GetChannel(channel);

            var newValue = currentValue + value;

            if (newValue < 0)
            {
                newValue = 0;
            }
            else if (newValue > 255)
            {
                newValue = 255;
            }

            this.SetChannel(channel, (Byte) newValue);
        }
    }
}
