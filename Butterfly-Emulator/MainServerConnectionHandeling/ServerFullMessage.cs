using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Butterfly.MainServerConnectionHandeling
{
    public class ServerFullMessage : AbstractNoDataPacket
    {
        public ServerFullMessage() : base(SharedPacketLib.DataPackets.ServerOpCode.Server_Full) { }
    }
}
