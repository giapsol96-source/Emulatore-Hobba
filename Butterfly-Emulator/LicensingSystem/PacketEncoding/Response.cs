//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Butterfly.LicensingSystem.PacketEncoding
//{
//    class Response
//    {
//        private string packetHeader;
//        private string[] arguments;

//        public Response(string encryptedData, int packetID)
//        {
//            string decrypted = CryptoService.Decrypt(encryptedData, CryptoService.superdupersecretpasswordhash + packetID);

//            string[] parts = decrypted.Split(':');

//            if (CryptoService.GetMD5Cheksum(parts[0] + ":" + parts[1]) != parts[2])
//                throw new MalformedPacketException("Invalid checksum");

//            this.packetHeader = parts[0];

//            this.arguments = parts[1].Split(',');

//            for (int i = 0; i < arguments.Length; i++)
//            {
//                arguments[i] = CryptoService.HexString2Ascii(arguments[i]);
//            }
//        }

//        internal string PacketHeader
//        {
//            get
//            {
//                return packetHeader;
//            }
//        }

//        internal string GetArgument(int i)
//        {
//            return arguments[i];
//        }
//    }
//}
