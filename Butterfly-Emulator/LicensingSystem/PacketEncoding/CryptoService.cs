//using System;
//using System.IO;
//using System.Security.Cryptography;
//using System.Text;

//namespace Butterfly.LicensingSystem.PacketEncoding
//{
//    class CryptoService
//    {
//        #region What's bellow this line is a secret
//        internal const string superdupersecretpasswordhash = "HMTym1m¤Om=0089k!ZN1)mjz#qUAealnzUFjUfUMolzF7QoacJtqruRuDtE&g69Ob6JpBZI7fbM77WMwbTxN1¤%edkYBTYGCv5Dbg=RWbGlq25HZ1i¤FHu7g?EmrahmzOjKbsLpccbMu2YwrV)TL5b%b(xjU7zv#t5URuFGp!!CF2Mxmi¤ObGTONoJe%BNoVYSa(dX(p";
//        #endregion

//        internal static string GetMD5Cheksum(string value)
//        {
//            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
//            byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
//            data = x.ComputeHash(data);
//            string ret = "";
//            for (int i = 0; i < data.Length; i++)
//                ret += data[i].ToString("x2").ToLower();

//            return ret;
//        }

//        private static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
//        {
//            MemoryStream ms = new MemoryStream();
//            Rijndael alg = Rijndael.Create();

//            alg.Key = Key;
//            alg.IV = IV;

//            CryptoStream cs = new CryptoStream(ms,
//               alg.CreateEncryptor(), CryptoStreamMode.Write);

//            cs.Write(clearData, 0, clearData.Length);
//            cs.Close();

//            byte[] encryptedData = ms.ToArray();
//            return encryptedData;
//        }

//        internal static string Encrypt(string clearText, string Password)
//        {
//            byte[] clearBytes =
//              System.Text.Encoding.Unicode.GetBytes(clearText);

//            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
//                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
//            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

//            byte[] encryptedData = Encrypt(clearBytes,
//                     pdb.GetBytes(32), pdb.GetBytes(16));

//            return Convert.ToBase64String(encryptedData);
//        }

//        private static byte[] Encrypt(byte[] clearData, string Password)
//        {
//            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
//                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
//            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

//            return Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16));
//        }

//        private static byte[] Decrypt(byte[] cipherData,
//                                    byte[] Key, byte[] IV)
//        {
//            MemoryStream ms = new MemoryStream();
//            Rijndael alg = Rijndael.Create();

//            alg.Key = Key;
//            alg.IV = IV;

//            CryptoStream cs = new CryptoStream(ms,
//                alg.CreateDecryptor(), CryptoStreamMode.Write);

//            cs.Write(cipherData, 0, cipherData.Length);
//            cs.Close();

//            byte[] decryptedData = ms.ToArray();
//            return decryptedData;
//        }

//        internal static string Decrypt(string cipherText, string Password)
//        {

//            byte[] cipherBytes = Convert.FromBase64String(cipherText);

//            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
//                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 
//            0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

//            byte[] decryptedData = Decrypt(cipherBytes,
//                pdb.GetBytes(32), pdb.GetBytes(16));

//            return System.Text.Encoding.Unicode.GetString(decryptedData);
//        }

//        private static byte[] Decrypt(byte[] cipherData, string Password)
//        {
//            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
//                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
//            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

//            return Decrypt(cipherData, pdb.GetBytes(32), pdb.GetBytes(16));
//        }

//        internal static string HexString2Ascii(string hexString)
//        {
//            StringBuilder sb = new StringBuilder();
//            for (int i = 0; i <= hexString.Length - 2; i += 2)
//            {
//                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
//            }
//            return sb.ToString();
//        }

//        internal static string GetHex(string text)
//        {
//            return BitConverter.ToString(System.Text.Encoding.Default.GetBytes(text)).Replace("-", string.Empty);
//        }
//    }
//}
