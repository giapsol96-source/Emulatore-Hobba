//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Butterfly.LicensingSystem.PacketEncoding
//{
//    class Packet
//    {
//        private readonly int packetID;
//        private readonly string header;
//        private List<string> variables;

//        public Packet(int packetID, string header)
//        {
//            this.packetID = packetID;
//            this.header = header;
//            this.variables = new List<string>();
//        }

//        internal string GetString()
//        {
//            StringBuilder builder = new StringBuilder();
//            builder.Append(header + ":");

//            int i = 0;
//            foreach (string value in variables)
//            {
//                i++;
//                builder.Append(value);
//                if (i != variables.Count)
//                    builder.Append(',');
//            }

//            builder.Append(":" + CryptoService.GetMD5Cheksum(builder.ToString()));
//            //return builder.ToString();
//            string text = CryptoService.Encrypt(builder.ToString(), CryptoService.superdupersecretpasswordhash + packetID);
//            return text;
//        }

//        internal void Append(string value)
//        {
//            variables.Add(CryptoService.GetHex(value));
//        }

//        internal void Append(int value)
//        {
//            Append(value.ToString());
//        }
//    }
//}
