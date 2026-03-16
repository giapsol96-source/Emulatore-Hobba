using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedPacketLib.Data_Format.Data_Exception;

namespace Butterfly.MainServerConnectionHandeling
{
    public class QueryResult : AbstractNoDataPacket
    {

        public QueryResult(bool p) : base(SharedPacketLib.DataPackets.ServerOpCode.Query_Result)
        {
            base.packet.WriteBoolean(p);
        }

    }
}
