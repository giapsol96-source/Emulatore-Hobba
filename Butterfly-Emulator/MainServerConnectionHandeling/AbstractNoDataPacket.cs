using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedPacketLib.DataPackets;

namespace Butterfly.MainServerConnectionHandeling
{
    public abstract class AbstractNoDataPacket : ISendable
    {
        protected ClientOutgoingPacket packet;
        public AbstractNoDataPacket(ServerOpCode code)
        {
            this.packet = new ClientOutgoingPacket(code);
        }

        public AbstractOutgoingPacket getPacket()
        {
            return this.packet;
        }
    }
}
