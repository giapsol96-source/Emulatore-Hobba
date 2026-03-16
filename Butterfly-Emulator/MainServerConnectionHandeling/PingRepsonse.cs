using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Butterfly.MainServerConnectionHandeling
{
    public class PingRepsonse : AbstractNoDataPacket
    {
        public PingRepsonse() : base(SharedPacketLib.DataPackets.ServerOpCode.Ping_Response) { }
    }
}
