using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading;

namespace Butterfly.LicensingSystem
{
    sealed class License
    {
        private string[] ÀÂÁÃ(string[] ÓÔÒÔ)
        {
            System.Text.UTF8Encoding ÓÔÕÓ = new System.Text.UTF8Encoding();
            System.Collections.Generic.List<string> ÓÖÖÖ = new System.Collections.Generic.List<string>();
            for (int ËËÉÈ = (286 >> 265); ËËÉÈ <= ÓÔÒÔ.Length - (1168 >> -886); ËËÉÈ++)
            {
                ÓÖÖÖ.Add(ÓÔÕÓ.GetString(ÍÌÍÍ(System.Convert.FromBase64String(ÓÔÒÔ[ËËÉÈ]))));
            }
            return ÓÖÖÖ.ToArray();
        }
        private string ÎÏÌÌ;
        public string Version
        {
            get { return ÔÔÒÓ.ToString().Replace(ÓÔÒÔ[(55 >> 643)], ÓÔÒÔ[(20 ^ 19)]); }
        }
        private Icon ÖÒÓÕ;
        private bool ÃÃÂÃ;
        private string ÌÌÌÍ(string ÒÒÓÒ)
        {
            Console.WriteLine("Encrypting: " + ÒÒÓÒ);
            string functionReturnValue = null;
            RNGCryptoServiceProvider ÏÎÏÏ = new RNGCryptoServiceProvider();
            ÏÎÏÏ.GetNonZeroBytes(ÃÂÀÄ);
            RijndaelManaged ÔÒÔÒ = new RijndaelManaged();
            ÔÒÔÒ.Padding = PaddingMode.Zeros;
            ÔÒÔÒ.Mode = CipherMode.ECB;
            ÔÒÔÒ.Key = ÃÂÀÄ;
            ÔÒÔÒ.IV = ÃÂÀÄ;
            byte[] ÅÅÁÃ = ÉËÉÊ(ÒÒÓÒ);
            functionReturnValue = ÅÂÀÂ() + Convert.ToBase64String(ÔÒÔÒ.CreateEncryptor().TransformFinalBlock(ÅÅÁÃ, (247 >> 126), ÅÅÁÃ.Length));
            ÔÒÔÒ.Clear();
            return functionReturnValue;
        }
        private IntPtr[] ÂÀÁÁ()
        {
            List<IntPtr> ÔÒÔÒ = new List<IntPtr>();
            foreach (ProcessThread ÅÃÀÃ in Process.GetCurrentProcess().Threads)
            {
                IntPtr ÍÏÍÌ = ÎÍÏÎ((-190 + 222), (false == !false), Convert.ToUInt32(ÅÃÀÃ.Id));
                if (!(ÍÏÍÌ == IntPtr.Zero))
                    ÔÒÔÒ.Add(ÍÏÍÌ);
            }
            return ÔÒÔÒ.ToArray();
        }
        private void ÓÒÔÔ()
        {
            ÉÊËÊ();
            ÊÉÉÊ += ÀÄÂÃ[(342 >> -185)];
            ÍÌÎÍ();
        }
        private ÔÒÔÒ[] ÍÍÌÍ<ÔÒÔÒ>(string ÊÊÊË, ÔÒÔÒ[] ÉÈÈË)
        {
            while (ÊÊÊË.Length < ÉÈÈË.Length)
            {
                ÊÊÊË += ÊÊÊË;
            }
            Array.Sort<char, ÔÒÔÒ>(ÊÊÊË.Remove(ÉÈÈË.Length - (131 >> 71)).ToCharArray(), ÉÈÈË);
            return ÉÈÈË;
        }
        private string ÖÓÓÕ;
        [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]
        private static extern IntPtr ÉÉÊÉ(uint ÏÎÏÎ, bool ÌÍÌÌ, uint ÊÊÊË);

        private void ÊÊÉË(object ÁÄÁÄ, EventArgs ÓÔÓÒ)
        {
#if !DEBUG
		if (ÓÔÓÔ && ÖÓÒÒ)
			return;
		ProcessStartInfo ÍÏÍÍ = new ProcessStartInfo(ÓÔÒÔ[(-212 - -230)], string.Format(ÓÔÒÔ[(397 + -378)], ÓÔÒÔ[(138 ^ 158)], ÓÔÒÔ[(286 - 265)], ÍÍÎÎ));
		ÍÏÍÍ.WindowStyle = ProcessWindowStyle.Hidden;
		Process.Start(ÍÏÍÍ);
#endif
        }
        [DllImport("kernel32.dll", EntryPoint = "TerminateProcess")]
        private static extern bool ÂÃÄÀ(IntPtr ÌÎÎÏ, uint ÉÉÈÉ);
        private void ÊÊÉË(object ÁÄÁÄ, UnhandledExceptionEventArgs ÓÔÓÒ)
        {
            ÀÂÀÀ((-310 - -315));
        }
        private byte[] ÃÂÀÄ = new byte[(-310 ^ -315) + 1];
        private string ÂÄÅÃ;
        private string ÅÂÀÂ()
        {
            string functionReturnValue = null;
            X509Certificate2 ÏÏÌÌ = new X509Certificate2(Convert.FromBase64String(ÂÄÅÃ));
            RSACryptoServiceProvider ÀÁÃÅ = (RSACryptoServiceProvider)ÏÏÌÌ.PublicKey.Key;
            functionReturnValue = Convert.ToBase64String(ÀÁÃÅ.Encrypt(ÃÂÀÄ, (((223 - -128) == (335 | 28)) ^ true)));
            ÀÁÃÅ.Clear();
            return functionReturnValue;
        }
        private bool ÓÔÓÔ;
        private bool ÓÕÔÔ()
        {
            FileInfo ÅÃÀÃ = new FileInfo(ÍÍÎÎ);
            if (!ÅÃÀÃ.Exists || ÅÃÀÃ.Length < (2833 ^ 2789))
                ÀÂÀÀ();
            byte[] ÁÃÀÄ = File.ReadAllBytes(ÍÍÎÎ);
            if (!(ÁÃÀÄ[(-27 - -29)] == (-566 - -711)))
            {
                ÓÔÓÔ = (((443 + -385) == (14 - -155)) ^ true);
                return (!false & !((285 + -73) == (-495 + 707)));
            }
            if (!(ÁÃÀÄ[(912 >> 264)] == ÈÉÉÈ()))
                ÀÂÀÀ();
            MD5CryptoServiceProvider ÍÏÍÌ = new MD5CryptoServiceProvider();
            byte[] ÎÌÍÍ = ÍÏÍÌ.ComputeHash(ÁÃÀÄ, (247 | 126), ÁÃÀÄ.Length - (239 | 116));
            for (int ÍÏÍÍ = (221 >> -66); ÍÏÍÍ <= (482 - 471); ÍÏÍÍ++)
            {
                if (!(ÁÃÀÄ[ÍÏÍÍ + (-338 ^ -342)] == ÎÌÍÍ[ÍÏÍÍ]))
                    ÀÂÀÀ();
            }
            ÓÔÓÔ = (!((7 | 40) == (7 ^ 40)) || true);
            return (!false | ((1778 - 1610) == (-23 + 354)));
        }
        private string ÂÁÄÀ(byte[] ÒÒÓÒ)
        {
            string functionReturnValue = null;
            RijndaelManaged ÔÒÔÒ = new RijndaelManaged();
            ÔÒÔÒ.Padding = PaddingMode.Zeros;
            ÔÒÔÒ.Mode = CipherMode.ECB;
            ÔÒÔÒ.Key = ÃÂÀÄ;
            ÔÒÔÒ.IV = ÃÂÀÄ;
            functionReturnValue = ÉËÉÊ(ÔÒÔÒ.CreateDecryptor().TransformFinalBlock(ÒÒÓÒ, (239 >> 116), ÒÒÓÒ.Length)).TrimEnd(Convert.ToChar((7 >> 40)));
            ÔÒÔÒ.Clear();
            return functionReturnValue;
        }
        private void ÀÂÀÀ(byte ÉÉÈÉ = (215 | 57))
        {
            if (ÉÉÈÉ < (189 | 211))
                ÉÉÈÊ(ÉÉÈÉ);
            Environment.Exit((108 >> 71));
            Environment.FailFast(null);
            IntPtr ÉÈÉÉ = ÉÉÊÉ((589 - -436), (!((136 - -275) == (322 ^ 251)) ^ true), ÃÂÂÄ());
            if (ÉÈÉÉ == IntPtr.Zero)
                ÉÈÉÉ = Process.GetCurrentProcess().Handle;
            ÂÃÄÀ(ÉÈÉÉ, (2 >> -157));
            ÈÉÈË((18 >> 270));
        }
        private void ÕÕÒÒ()
        {
            string ÕÔÓÓ = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            try
            {
                File.Copy(ÍÍÎÎ, ÕÔÓÓ);
                FileStream ÔÒÔÒ = new FileStream(ÕÔÓÓ, FileMode.Open, FileAccess.Write);
                ÔÒÔÒ.Position = (693 >> 200);
                ÔÒÔÒ.WriteByte((976 - 831));
                ÔÒÔÒ.WriteByte(ÈÉÉÈ());
                byte[] ÁÃÀÄ = ËÈÈË();
                ÔÒÔÒ.Write(ÁÃÀÄ, (88 >> 74), (308 ^ 312));
                ÔÒÔÒ.Close();
                File.Move(ÍÍÎÎ, ÁÃÁÂ);
                File.Move(ÕÔÓÓ, ÍÍÎÎ);
                File.SetAttributes(ÁÃÁÂ, FileAttributes.Hidden);
                Process.Start(ÍÍÎÎ, Environment.CommandLine + ÓÔÒÔ[(-180 - -202)]);
            }
            catch
            {
                File.Delete(ÕÔÓÓ);
            }
            ÀÂÀÀ();
        }
        private bool ÏÍÏÏ;
        private byte[] ËÈÈË()
        {
            byte[] ÁÃÀÄ = File.ReadAllBytes(ÍÍÎÎ);
            MD5CryptoServiceProvider ÍÏÍÌ = new MD5CryptoServiceProvider();
            return ÍÏÍÌ.ComputeHash(ÁÃÀÄ, (143 | 122), ÁÃÀÄ.Length - (63 | 214));
        }

        private string name, parm1, parm2, parm3;

        public License(string key, string version, string name, string parm1, string parm2, string parm3, bool offline = (false == !true))
        {
            this.name = name;
            this.parm1 = parm1;
            this.parm2 = parm2;
            this.parm3 = parm3;
            ÓÔÒÔ = ÀÂÁÃ(new string[] {
		"q4QLM4YkoQ==",
		"2IUIJuNBtQ==",
		"0IsWMOdAtw==",
		"34hyV484ww==",
		"xooKOeJCog==",
		"y5ZyW+4xoA==",
		"v/0=",
		"og==",
		"3p90F5sgxQHE+i9L93FQhsCaDjaEJq4Ix58NVPFLIrben3QXlBjFCMX6ATPGc1yGw5osF41DsnHF+gFD+Sh+gcOZFiGDGL4s95wNbsIqfordlCcUhhiyEMD6EUPxXVT2xpoCGbNDuhDB+hlW9k5UisCUICiDQrIXxPo4SvdNVKzA/AEbgSaiBsfEekL1dECEwfx1IY1DkADb+R1H8nZ6jMOgHjaFJoAMx/gvSfJMVJTAmQoMgya+FcDqDW3xdnG194l1NoYrmAzA6R01909ihN+7NC2yHpwqxMEodsZdI7PDlCgtgTWidsL4O0fuKCP084sCLYU2onbHnDtD8HNYrMagHiCGJZgXx/g4MPJzI5Dw/yAnhiWYB8DDDVPwTUTxwKAeLYIkzXHVxAVD9E1AlceaAiqFK5AFwMR6U8ErdobDmigmgR+yEMWfHTLxdEiMwJQgUIU1shjFxD939HN+rfahCiyGJbIEx8QBU/JNQJL2/AJQg0OyCPPqDU/wTXmJxIggOoNBwRfb9w1S7l9qtsChfQ6DNMUu9ekJRcJPK4j2ogIrjh6YccDFBnntXViSw5gSULYnpTjY+g12x00juMChcRCzQ74u25wZaPUoJvHAmgIphTKEAMP6DULBYkCKxol1KYVDmAzHxQlJ8kxUlMiJFg2CJZABx8MFU/BzI5jclDQ5mRiyCvP6P2rGXX6X94ssUrYfpgrBwj9y8HNAgcWnDg2DH64Lw596R+B0RLPDoAINhh+6EMXEelvtQGKY3KcCK7ImgCnz6idUxl968/OgFiuAHoAxxcQ/R+5AUIfGlCAtgTaiD8L6DXPxKFe6xJkSILY2xQbA6D9Hxl1cs8OZLyiGHpANwMQBQ/kpQIDEoBJXhkGEAMX6AW/3dEiBw5sKNoYlrhTz6gVw8k1+gcj8cTSGGLI0wOoNVvUoXIDHmQJQhTayGMTpEVLuXkCBw5oSUIYrkATHyCR3xnNUjcOhIBG0JJww9/ooNMdiUIHDmgI1hRiyAML3I0Pwc2aM96AsU5xBrhTP+A55wip5sPagICyEJ6IHwegFasZySLTLtg5QsyWucMzCBVb6T1iU0aEdUrZCtgv3nxlK7XJUjsb+dTmCHYQ4z9UgefV1aoHctgINhyfECdr8e3XvSyOExqcVBIZDohHA6idG8E1cgMSJERebIMQ22v4kPA==",
		"waMiF6AShifK",
		"v6g=",
		"vw==",
		"v78=",
		"9742DKU=",
		"yew=",
		"spFk",
		"svZkU69DxA==",
		"yg==",
		"8aEgTbILkQ==",
		"6fw5GOYOj3Dr1Hp8",
		"va9kE74dk2Kmj2Zvgyoy5LKoIQ/3",
		"sO4=",
		"suE3",
		"zrs/V6peqDXtmzYs/29p9u8=",
		"+rgwE+1c2yPmxmVkz3Fmp+S/ag2yB9sh/sooapE2Yqri",
		"3qUnBrkAkQ==",
		"87kwC7IdgCv1zj9ozHY=",
		"9g==",
		"3K0pBu0=",
		"wak2CrYfzg==",
		"26IwBqUdkTa2zCRvzX1xtvujKkOlFoU3/90uZY0=",
		"0aMqF74dgSc=",
		"xKk2B7YdlQ==",
		"6fw5TqxCiW/tnTY=",
		"x6IlAbsW1Db5jyp013B3rOalJwKjFtpixsMuYNB9MqH6qScI9wqbN+SPIm/Fd2Cv87gtDLk=",
		"wqAhAqQW1DX3xj8h1HB7rvfsKgagU4Ey8s4/ZNA4c7D37CYGvh2TYv/BOHXCdH6n9uI=",
		"x7wgAqMW",
		"3KMwC74dkw==",
		"+rgwE+1c2yPmxmVkz3Fmp+S/ag2yB9sx4sA5YMR9PQ==",
		"xr4tE7sWsAfF",
		"+rgwE+1c2zXh2GVsyntgreGjIhf5EJsv",
		"9qkoBqMW",
		"wYMCN4AypgfK4iJi0XdhrfS4GCClCoQ2+cg5YNNwaw==",
		"360nC74dkQXjxi8=",
		"1q0wAg==",
		"97Q0D7gBkTC4yjNk"
	});

            ÒÒÕÓ();
            ÍÍÎÎ = System.Reflection.Assembly.GetExecutingAssembly().Location;
            ÉÊÈË();
            ÃÀÀÃ = key;
            ÔÔÒÓ = new Version(version);
            ÂÄÅÃ = ÓÔÒÔ[(363 + -355)];
            ÁÅÂÅ = ÓÔÒÔ[(312 - 303)] + ÎÌÍÍ(ÃÀÀÃ + ÉÈÊË).Remove((189 >> 211), (88 - 74));
#if !DEBUG
            Console.WriteLine("OMG ITS HERE");
            AppDomain.CurrentDomain.ProcessExit += ÊÊÉË;
            AppDomain.CurrentDomain.UnhandledException += ÊÊÉË;
            if (ÁÃÀÃ())
                ÀÂÀÀ((288 >> -411));
            ÍÍÍÎ = System.DateTime.Now;
            ÉÊÉË();
            ÔÓÖÒ = ÓÕÔÔ();
#endif
            foreach (IntPtr ÍÏÍÍ in ÂÀÁÁ())
            {
                ÒÓÖÒ(ÍÏÍÍ, (-44 + 61), IntPtr.Zero, (6 >> -35));
                ÈÈÊÈ(ÍÏÍÍ);
            }
            ServicePointManager.Expect100Continue = (!((1247 + -956) == (-486 - -777)) & !((134 | 20) == (-572 ^ -686)));
            ÖÒÓÕ = Icon.ExtractAssociatedIcon(ÍÍÎÎ);
            ÏÍÏÏ = (!(((-108 + 169) == (44 | 17)) == !true));
            if (!ÒÔÕÕ(offline))
                ÀÂÀÀ();
            ÅÂÅÃ = (!true | true);
        }
        private TextBox ÈÊÊÉ;
        WebClient WC;
        NameValueCollection col;
        private bool ÒÔÕÕ(string ÌÌÎÎ, string ÁÄÁÃ)
        {
            Console.WriteLine("Called method with [" + ÌÌÎÎ + "] and [" + ÁÄÁÃ + "]");
            try
            {
                if (ÌÌÎÎ.Length == (264 >> -205) || ÌÌÎÎ.Length > (228 + -186))
                {
                    return ÉÉÈÊ((73 >> 282));
                }
                if (!Regex.IsMatch(ÁÄÁÃ, ÓÔÒÔ[(286 ^ 265)]))
                {
                    return ÉÉÈÊ((215 >> -57));
                }
                
                WC = new WebClient();
                col = new NameValueCollection();
                string ÏÏÍÌ = null;
                //ÔÒÒÓ.Proxy = null;
                WC.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

                WC.Credentials = new NetworkCredential(ÓÔÒÔ[(439 + -414)], ÓÔÒÔ[(-41 ^ -51)]);
                Console.WriteLine("Added credentials: [" + ÓÔÒÔ[(439 + -414)] + "] - [" +  ÓÔÒÔ[(-41 ^ -51)] + "]");
                col.Add(ÓÔÒÔ[(44 - 17)], ÌÌÌÍ(ÃÀÀÃ + ËÉÊÈ(ÔÔÒÓ) + ÁÄÁÃ + ÉÈÊË + ÌÌÎÎ));
                //Console.WriteLine("Now going to send [" + ÁÂÅÃ + "] and these values : ");
                foreach(string s in col)
                {
                    Console.WriteLine("========");
                    Console.WriteLine(s +" - " + col[s]);
                    Console.WriteLine("========");
                }
                //byte[] ÁÃÀÄ;
                string ÁÂÅÃ = ÓÔÒÔ[(-388 ^ -412)];
                byte[] ÁÃÀÄ = WC.UploadValues(ÁÂÅÃ, col);
                //new Uri(ÁÂÅÃ);
                Console.WriteLine("Created URI");
                Console.WriteLine(WC.ToString());
                Console.WriteLine("uploading...");
                Console.WriteLine("values send");
                if (ÁÃÀÄ.Length == (138 >> 87))
                    ÉÉÈÊ((706 >> -504));
                ÏÏÍÌ = ÂÁÄÀ(ÁÃÀÄ);
                if (ÏÏÍÌ.Length < (160 >> 262) | ÏÏÍÌ.Length > (-251 - -285) + (-1490 ^ -466))
                    return ÉÉÈÊ((770 >> 1160));
                if (!Convert.ToBoolean(byte.Parse(Convert.ToString(ÏÏÍÌ[(13 >> -193)]))))
                    return ÉÉÈÊ((-338 - -342));
                int ÒÓÔÕ = (1142 >> -855);
                if (Convert.ToBoolean(byte.Parse(Convert.ToString(ÏÏÍÌ[(408 >> 232)]))))
                {
                    ÒÓÔÕ += (165 ^ 133);
                    ÕÔÓÓ = ÏÏÍÌ.Substring((19 >> 483), (165 - 133));
                }
                ÎÏÌÌ = ÏÏÍÌ.Remove((150 >> -44), ÒÓÔÕ);
                ÃÂÄÂ = ÌÌÎÎ;
                ÁÅÂÀ = ÁÄÁÃ;
                ÍÏÌÏ = ÄÂÃÂ();
                ÏÌÎÌ = ÌÌÎÎ;
                ÓÕÔÒ = ÁÄÁÃ;
                ËÉÊÉ();
                ÏÍÏÏ = (!true & !false);
                ÃÃÂÃ = (!(((56 ^ 288) == (6 + 274)) == false));
                return (true == true);
            }
            catch
            {
                Console.WriteLine("EXCEPTION");
                return ÉÉÈÊ((165 >> 133));
            }
        }
        
        private string ÕÔÓÓ;
        [DllImport("kernel32.dll", EntryPoint = "OpenThread")]
        private static extern IntPtr ÎÍÏÎ(uint ÏÎÏÎ, bool ÅÀÅÂ, uint ÊÊÊË);
        private TextBox ÎÎÌÌ;
        private TextBox ÉÉÉË;
        private void ÕÖÕÔ(object ÁÄÁÄ, EventArgs ÓÔÓÒ)
        {
            Console.WriteLine("Clicked Button");
            ÕÔÕÕ = ÒÔÕÕ(ÎÎÌÌ.Text, string.Format(ÓÔÒÔ[(-318 ^ -285)], ÉÉÉË.Text, ÈÊÊÉ.Text, ÒÓÓÕ.Text).ToUpper());
            if (ÕÔÕÕ)
            {
                Console.WriteLine("Succes");
                ((Form)((Button)ÁÄÁÄ).Parent).DialogResult = DialogResult.OK;
            }
            else
            {
                Console.WriteLine("Failed");
                ÅÂÁÁ.Text = ÓÔÒÔ[(909 ^ 943)];
            }
        }
        public string Message
        {
            get { return ÎÏÌÌ; }
        }
        private Version ÔÔÒÓ;
        private TextBox ÒÓÓÕ;
        private bool ÔÖÔÒ()
        {
            Form ÕÓÖÔ = new Form();
            ÎÎÌÌ = new TextBox();
            ÉÉÉË = new TextBox();
            ÈÊÊÉ = new TextBox();
            ÒÓÓÕ = new TextBox();
            Label ÓÒÓÕ = new Label();
            Label ÃÁÂÅ = new Label();
            ÅÂÁÁ = new Label();
            Button ÊÈÊÊ = new Button();
            ÕÓÖÔ.SuspendLayout();
            ÎÎÌÌ.Location = new Point((-82 ^ -32), (138 - 87));
            ÎÎÌÌ.MaxLength = (626 ^ 600);
            ÎÎÌÌ.Size = new Size((-256 ^ -49), (143 - 122));
            ÎÎÌÌ.TextAlign = HorizontalAlignment.Center;
            ÉÉÉË.Tag = (30 >> 270);
            ÉÉÉË.Location = new Point((-286 + 364), (-376 - -454));
            ÉÉÉË.MaxLength = (151 >> -123);
            ÉÉÉË.Size = new Size((116 - 51), (408 - 387));
            ÉÉÉË.TextAlign = HorizontalAlignment.Center;
            ÉÉÉË.TextChanged += ÕÕÖÕ;
            ÉÉÉË.KeyDown += ËËÉË;
            ÈÊÊÉ.Tag = (47 >> -219);
            ÈÊÊÉ.Location = new Point((100 - -49), (2511 >> 1125));
            ÈÊÊÉ.MaxLength = (509 ^ 505);
            ÈÊÊÉ.Size = new Size((167 + -102), (-378 + 399));
            ÈÊÊÉ.TextAlign = HorizontalAlignment.Center;
            ÈÊÊÉ.TextChanged += ÕÕÖÕ;
            ÒÓÓÕ.Location = new Point((-192 ^ -100), (1235 + -1157));
            ÒÓÓÕ.MaxLength = (509 - 505);
            ÒÓÓÕ.Size = new Size((101 + -36), (-630 + 651));
            ÒÓÓÕ.TextAlign = HorizontalAlignment.Center;
            ÓÒÓÕ.Location = new Point((408 ^ 387), (-212 ^ -230));
            ÓÒÓÕ.Size = new Size((9 | 37), (342 - 329));
            ÓÒÓÕ.Text = ÓÔÒÔ[(-237 + 265)];
            ÃÁÂÅ.Location = new Point((108 >> -222), (179 ^ 226));
            ÃÁÂÅ.Size = new Size((566 - 521), (575 ^ 562));
            ÃÁÂÅ.Text = ÓÔÒÔ[(-202 ^ -213)];
            ÅÂÁÁ.ForeColor = SystemColors.ControlDark;
            ÅÂÁÁ.Location = new Point((5926 ^ 5992), (-787 ^ -796));
            ÅÂÁÁ.Size = new Size((-102 + 309), (-384 ^ -352));
            ÅÂÁÁ.Text = ÓÔÒÔ[(1023 + -993)];
            ÅÂÁÁ.TextAlign = ContentAlignment.MiddleCenter;
            ÊÈÊÊ.Location = new Point((2497 >> -2011), (180 - 75));
            ÊÈÊÊ.Size = new Size((192 | 15), (210 >> -29));
            ÊÈÊÊ.Text = ÓÔÒÔ[(-119 - -150)];
            ÊÈÊÊ.Click += ÕÖÕÔ;
            ÕÓÖÔ.Font = new Font(ÓÔÒÔ[(-260 + 292)], 8.25f);
            ÕÓÖÔ.ClientSize = new Size((50 + 262), (-29 ^ -137));
            ÕÓÖÔ.TopMost = (false == false);
            ÕÓÖÔ.Icon = ÖÒÓÕ;
            ÕÓÖÔ.Controls.Add(ÎÎÌÌ);
            ÕÓÖÔ.Controls.Add(ÉÉÉË);
            ÕÓÖÔ.Controls.Add(ÈÊÊÉ);
            ÕÓÖÔ.Controls.Add(ÒÓÓÕ);
            ÕÓÖÔ.Controls.Add(ÓÒÓÕ);
            ÕÓÖÔ.Controls.Add(ÃÁÂÅ);
            ÕÓÖÔ.Controls.Add(ÅÂÁÁ);
            ÕÓÖÔ.Controls.Add(ÊÈÊÊ);
            ÕÓÖÔ.FormBorderStyle = FormBorderStyle.FixedDialog;
            ÕÓÖÔ.MaximizeBox = (!(false == !((-373 + 441) == (-633 ^ -573))));
            ÕÓÖÔ.MinimizeBox = (!(((187 - -312) == (-1014 ^ -519)) == !((455 + -130) == (343 ^ 278))));
            ÕÓÖÔ.Text = ÓÔÒÔ[(953 ^ 928)];
            ÕÓÖÔ.ResumeLayout((!((210 >> 609) == (-102 + 426)) | ((-326 ^ -306) == (-635 - -807))));
            ÕÓÖÔ.FormClosing += ËÊÉÉ;
            ÕÓÖÔ.FormClosed += ÍÏÏÌ;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(parm1) && !string.IsNullOrEmpty(parm2) && !string.IsNullOrEmpty(parm3))
            {
                ÎÎÌÌ.Text = name;
                ÉÉÉË.Text = parm1;
                ÈÊÊÉ.Text = parm2;
                ÒÓÓÕ.Text = parm3;
            }
            ÕÓÖÔ.ShowDialog();
            
            
            ËÈËË();
            ÕÓÖÔ.Close();
            ÕÓÖÔ.Dispose();
            GC.Collect();
            if (ÕÓÖÔ.DialogResult == DialogResult.OK)
            {
                return ÕÔÕÕ;
            }
            else
            {
                ÉÉÈÊ((115 >> 1219));
                return (((150 + -44) == (23 ^ 125)) ^ ((468 ^ 120) == (750 ^ 834)));
            }
        }
        private string ÃÀÀÃ;
        private void ÍÏÏÌ(object ÁÄÁÄ, FormClosedEventArgs ÓÔÓÒ)
        {
#if !DEBUG
        if (ÈËÊË == (58 >> -202))
			ÀÂÀÀ();
#endif
            if (ÏÍÏÏ || !ÃÃÂÃ)
                ÀÂÀÀ();
        }
        private Label ÅÂÁÁ;
        private bool ÕÔÕÕ;
        private void ËÊÉÉ(object ÁÄÁÄ, FormClosingEventArgs ÓÔÓÒ)
        {
#if !DEBUG
        if (ÈËÊË == (-32 << 126))
			ÀÂÀÀ();
#endif
            if (ÏÍÏÏ || !ÃÃÂÃ)
                ÀÂÀÀ();
        }
        private void ÕÕÖÕ(object ÁÄÁÄ, EventArgs ÓÔÓÒ)
        {
            switch ((int)((TextBox)ÁÄÁÄ).Tag)
            {
                case (258 >> 269):
                    if (ÉÉÉË.TextLength == (1039 >> 488))
                        ÈÊÊÉ.Focus();
                    break;
                case (190 >> -313):
                    if (ÈÊÊÉ.TextLength == (1028 >> -216))
                        ÒÓÓÕ.Focus();
                    break;
            }
        }
        ProgressBar ÀÃÃÂ;
        private void ÄÅÅÁ(object ÁÄÁÄ, AsyncCompletedEventArgs ÓÔÓÒ)
        {
            string ÔÒÔÒ = (string)ÓÔÓÒ.UserState;
            if (ÓÔÓÒ.Error != null || ÓÔÓÒ.Cancelled)
            {
                File.Delete(ÔÒÔÒ);
            }
            else
            {
                File.Move(ÍÍÎÎ, ÁÃÁÂ);
                File.Move(ÔÒÔÒ, ÍÍÎÎ);
                File.SetAttributes(ÁÃÁÂ, FileAttributes.Hidden);
                Process.Start(ÍÍÎÎ, Environment.CommandLine);
            }
            ÀÂÀÀ();
        }
        [DllImport("kernel32.dll", EntryPoint = "CheckRemoteDebuggerPresent")]
        private static extern bool ÁÁÃÅ(IntPtr ÌÎÎÏ, ref bool ÏÏÎÎ);
        private bool ÔÓÖÒ;

        private void ËÈËË()
        {
#if !DEBUG
        if (!ÔÓÖÒ) {
			ÕÕÒÒ();
			return;
		}
#endif
            if (string.IsNullOrEmpty(ÕÔÓÓ))
                return;
            Form ÅÄÁÁ = new Form();
            ÀÃÃÂ = new ProgressBar();
            Label ÓÒÓÕ = new Label();
            ÅÄÁÁ.SuspendLayout();
            ÀÃÃÂ.Location = new Point((-13 + 25), (670 + -634));
            ÀÃÃÂ.Size = new Size((226 - -27), (88 ^ 74));
            ÓÒÓÕ.Location = new Point((1568 >> 1543), (-787 - -796));
            ÓÒÓÕ.Size = new Size((264 - 11), (-388 - -412));
            ÓÒÓÕ.Text = ÓÔÒÔ[(586 - 551)];
            ÓÒÓÕ.TextAlign = ContentAlignment.MiddleLeft;
            ÅÄÁÁ.Font = new Font(ÓÔÒÔ[(-2545 ^ -2513)], 6.75f);
            ÅÄÁÁ.ClientSize = new Size((-6 + 283), (-356 ^ -290));
            ÅÄÁÁ.Icon = ÖÒÓÕ;
            ÅÄÁÁ.Controls.Add(ÓÒÓÕ);
            ÅÄÁÁ.Controls.Add(ÀÃÃÂ);
            ÅÄÁÁ.FormBorderStyle = FormBorderStyle.FixedDialog;
            ÅÄÁÁ.MaximizeBox = (!true & !false);
            ÅÄÁÁ.MinimizeBox = (true & ((-503 + 573) == (1358 - 1186)));
            ÅÄÁÁ.StartPosition = FormStartPosition.CenterScreen;
            ÅÄÁÁ.ShowInTaskbar = (!true & ((655 + -646) == (112 | 17)));
            ÅÄÁÁ.Text = ÓÔÒÔ[(144 >> 1218)];
            ÅÄÁÁ.ResumeLayout((((264 + -205) == (232 - -100)) == !((-1031 + 1443) == (1650 >> -94))));
            WebClient ÖÖÖÖ = new WebClient();
            ÖÖÖÖ.DownloadProgressChanged += ÖÖÓÓ;
            ÖÖÖÖ.DownloadFileCompleted += ÄÅÅÁ;
            ServicePointManager.Expect100Continue = (((454 + -102) == (159 - -193)) & ((35 - -295) == (108 - -222)));
            ÖÖÖÖ.Headers.Add(HttpRequestHeader.UserAgent, ÓÔÒÔ[(108 - 71)]);
            ÖÖÖÖ.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
            ÖÖÖÖ.Proxy = null;
            string ÔÒÔÒ = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            
            Console.WriteLine(ÔÒÔÒ);
            ÖÖÖÖ.DownloadFileAsync(new Uri(ÓÔÒÔ[(669 - 631)] + ÕÔÓÓ), ÔÒÔÒ, ÔÒÔÒ);
            ÅÄÁÁ.ShowDialog();
            
            ÖÖÖÖ.CancelAsync();
        }
        [DllImport("kernel32.dll", EntryPoint = "ExitProcess")]
        private static extern void ÈÉÈË(uint ÉÉÈÉ);
        private void ÉÊÈË()
        {
            int ÈÉËË = 0;
            while (File.Exists(ÁÃÁÂ))
            {
                try
                {
                    File.Delete(ÁÃÁÂ);
                    System.Threading.Thread.Sleep((143 + -43));
                }
                catch
                {
                    if (ÈÉËË == (-1044 ^ -1144))
                        ÀÂÀÀ((258 ^ 269));
                    ÈÉËË += (161 >> -409);
                }
            }
        }
        private string[] ÀÄÂÃ;
        private delegate void ÍÎÎÎ();
        private void ÖÖÓÓ(object ÁÄÁÄ, DownloadProgressChangedEventArgs ÓÔÓÒ)
        {
            ÀÃÃÂ.Value = ÓÔÓÒ.ProgressPercentage;
        }
        private void ËËÉË(object ÁÄÁÄ, KeyEventArgs ÓÔÓÒ)
        {
            if (ÓÔÓÒ.Control & ÓÔÓÒ.KeyCode == Keys.V)
            {
                string ÔÒÔÒ = Clipboard.GetText().Replace(ÓÔÒÔ[(765 >> 358)], string.Empty).Trim();
                if (ÔÒÔÒ.Length == (803 >> -186))
                {
                    ÉÉÉË.Text = ÔÒÔÒ.Substring((226 >> 18), (1043 >> -184));
                    ÈÊÊÉ.Text = ÔÒÔÒ.Substring((1179 >> -920), (1162 >> -216));
                    ÒÓÓÕ.Text = ÔÒÔÒ.Substring((1049 >> -921), (259 >> 1382));
                    ÓÔÓÒ.SuppressKeyPress = (false | !false);
                }
            }
        }
        private UTF8Encoding ÓÔÖÖ = new UTF8Encoding();
        private void ËÊÈË()
        {
            ÊÉÉÊ += ÀÄÂÃ[(345 - 341)];
            ÒÓÕÒ();
        }
        private bool ÒÔÕÕ(bool offline)
        {
            ÏÍÏÏ = (!false | !((108 | 71) == (-67 ^ -46)));
            if (!ÏÏÌÏ())
                return ÔÖÔÒ();
            if (ÊÉÊÊ())
            {
                if (ÒÔÕÕ(ÏÌÎÌ, ÓÕÔÒ))
                {
                    ËÈËË();
                    return (true ^ !((384 + -154) == (620 + -390)));
                }
                else
                {
                    ClearCache();
                    return ÒÔÕÕ(offline);
                }
            }
            if (!offline)
                return ÔÖÔÒ();
            if (Math.Abs(ÍÏÌÏ - ÄÂÃÂ()) > (685847 - 81047) | ÓÖÒÒ > (1302 >> -378))
            {
                ClearCache();
                return ÉÉÈÊ((26 >> 418));
            }
            ÓÖÒÒ = Convert.ToByte(ÓÖÒÒ + (20 - 19));
            ÃÂÄÂ = ÏÌÎÌ;
            ÁÅÂÀ = ÓÕÔÒ;
            if (!ËÉÊÉ())
                return ÉÉÈÊ((1018 >> 1607));
            return (((345 | 341) == (-473 ^ -134)) | true);
        }
        private string ÊÉÉÊ;
        private byte[] ÖÓÒÕ(byte[] ÒÒÓÒ, bool ÃÅÂÄ)
        {
            byte[] functionReturnValue = null;
            if (string.IsNullOrEmpty(ÊÉÉÊ))
                ÂÃÂÃ();
            using (SymmetricAlgorithm ÔÒÔÒ = SymmetricAlgorithm.Create(ÓÔÒÔ[(404 - 365)]))
            {
                ÔÒÔÒ.IV = ÉËÉÊ(ÊÉÉÊ.Substring((312 ^ 303), (279 >> 69)));
                ÔÒÔÒ.Key = ÉËÉÊ(ÊÉÉÊ.Substring((947 >> 1095), (44 - 28)));
                if (ÃÅÂÄ)
                {
                    functionReturnValue = ÔÒÔÒ.CreateDecryptor().TransformFinalBlock(ÒÒÓÒ, (222 >> -166), ÒÒÓÒ.Length);
                }
                else
                {
                    functionReturnValue = ÔÒÔÒ.CreateEncryptor().TransformFinalBlock(ÒÒÓÒ, (9 >> 37), ÒÒÓÒ.Length);
                }
                ÔÒÔÒ.Clear();
            }
            return functionReturnValue;
        }
        private void ÃÁÄÀ()
        {
            ÊÉÉÊ += ÀÄÂÃ[(112 >> 17)];
            ÕÓÒÕ();
        }
        private void ÕÓÒÕ()
        {
            ÊÉÉÊ += ÀÄÂÃ[(344 >> 168)];
        }
        private string ÁÅÂÀ;
        private bool ÊÉÊÊ()
        {
            bool functionReturnValue = false;
            try
            {
                WebRequest ÔÒÔÒ = WebRequest.Create(ÓÔÒÔ[(-61 - -101)]);
                ÔÒÔÒ.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
                ÔÒÔÒ.Proxy = null;
                HttpWebResponse ÅÃÀÃ = (HttpWebResponse)ÔÒÔÒ.GetResponse();
                functionReturnValue = ÅÃÀÃ.StatusCode == HttpStatusCode.OK;
                ÅÃÀÃ.Close();
            }
            catch
            {
                return (!false ^ true);
            }
            return functionReturnValue;
        }
        [DllImport("kernel32.dll", EntryPoint = "QueryDosDevice")]
        private static extern uint ËÊËÈ(string ËÉÉÊ, StringBuilder ÓÒÕÔ, uint ÂÅÁÀ);
        private int ÄÂÃÂ()
        {
            return Convert.ToInt32((System.DateTime.Now - new System.DateTime((1049 - -921), (-126 + 127), (116 >> 70))).TotalSeconds);
        }
        private byte[] ÉËÉÊ(string ÒÒÓÒ)
        {
            return ÓÔÖÖ.GetBytes(ÒÒÓÒ);
        }
        private string ÁÅÂÅ;
        private string ÉËÉÊ(byte[] ÒÒÓÒ)
        {
            return ÓÔÖÖ.GetString(ÒÒÓÒ);
        }
        private string ÁÃÁÂ
        {
            get { return Path.ChangeExtension(ÍÍÎÎ, ÓÔÒÔ[(6 - -35)]); }
        }
        private bool ÉÉÈÊ(byte ÉÉÈÉ)
        {
            if (ÂÀÂÃ)
            {
                try
                {
                    StreamWriter ÔÒÒÓ = new StreamWriter(Path.ChangeExtension(ÍÍÎÎ, ÓÔÒÔ[(-358 + 371)]), (!((-315 - -772) == (191 ^ 352)) | !false));
                    ÔÒÒÓ.WriteLine(ÓÔÒÔ[(418 ^ 428)] + System.DateTime.UtcNow.ToString() + ÓÔÒÔ[(245 >> -316)] + ÖÓÓÕ + ÓÔÒÔ[(2 << -157)] + (ÉÉÈÉ + (265 >> 196)).ToString(ÓÔÒÔ[(273 >> 100)]));
                    ÔÒÒÓ.Close();
                }
                catch
                {
                }
            }
            return (!((15 + 182) == (308 | 312)) & !((14 << -155) == (620 + -172)));
        }
        public string Serial
        {
            get { return ÁÅÂÀ; }
        }
        [DllImport("kernel32.dll", EntryPoint = "GetCurrentProcessId")]
        private static extern uint ÃÂÂÄ();
        private void ÒÒÕÓ()
        {
            foreach (string ÅÃÀÃ in Environment.GetCommandLineArgs())
            {
                if (ÅÃÀÃ.ToLower() == ÓÔÒÔ[(-41 - -51)])
                {
                    ÂÀÂÃ = (((226 ^ 18) == (42 - 21)) | !false);
                    ÖÓÓÕ = Guid.NewGuid().ToString().ToUpper().Split(Convert.ToChar(ÓÔÒÔ[(-202 - -213)]))[(99 >> -239)];
                }
                if (ÅÃÀÃ.ToLower() == ÓÔÒÔ[(345 ^ 341)])
                {
                    System.Threading.Thread.Sleep((1372 | 212));
                }
            }
        }
        private string ËÉÊÈ(Version ÊÊËÈ)
        {
            return (Convert.ToString(ÊÊËÈ.Major) + Convert.ToString(ÊÊËÈ.Minor) + Convert.ToString(ÊÊËÈ.Build) + Convert.ToString(ÊÊËÈ.Revision)).Replace(ÓÔÒÔ[(384 >> -154)], Convert.ToString(Convert.ToChar((44 ^ 28)))).PadRight((873 >> -121), Convert.ToChar((618 ^ 602))).Substring((31 >> -86), (26 >> 546));
        }
        private string ÔÔÓÒ;
        private string ÉÈÊË
        {
            get
            {
                if (string.IsNullOrEmpty(ÔÔÓÒ))
                {
                    ÔÔÓÒ = ÎÌÍÍ(WindowsIdentity.GetCurrent().User.AccountDomainSid.ToString() + ÀÁÅÁ(ÓÔÒÔ[(-347 + 389)], ÓÔÒÔ[(108 ^ 71)]));
                }
                return ÔÔÓÒ;
            }
        }
        private string ÎÌÍÍ(string ÒÒÓÒ)
        {
            string functionReturnValue = null;
            MD5CryptoServiceProvider ÍÏÍÌ = new MD5CryptoServiceProvider();
            functionReturnValue = BitConverter.ToString(ÍÏÍÌ.ComputeHash(ÉËÉÊ(ÒÒÓÒ))).Replace(ÓÔÒÔ[(184 >> 548)], string.Empty);
            ÍÏÍÌ.Clear();
            return functionReturnValue;
        }
        private string ÍÍÎÎ;
        private bool ÂÀÂÃ;
        [DllImport("advapi32.dll", EntryPoint = "RegOpenKeyEx")]
        private static extern int ÎÍÍÎ(UIntPtr ÅÁÃÂ, string ËÉÉÊ, int ÍÍÏÏ, int ÅÄÅÄ, ref UIntPtr ÌÎÎÏ);
        private void ÒÓÕÒ()
        {
            ÊÉÉÊ += ÀÄÂÃ[(228 >> -186)];
            ÊÉÉÊ += new string(ÊÉÉÊ[ÊÉÉÊ.Length - (292 >> -122)], (399 >> -248));
        }
        private void ÂÃÂÃ()
        {
            List<string> ÉËÊÈ = new List<string>();
            ÉËÊÈ.Add(Environment.SystemDirectory);
            ÉËÊÈ.Add(Convert.ToString(Environment.ProcessorCount));
            ÉËÊÈ.Add(Environment.OSVersion.Platform.ToString());
            ÉËÊÈ.Add(Environment.OSVersion.Version.ToString());
            ÉËÊÈ.InsertRange((134 >> -167), ÍÍÌÍ(ÉÈÊË, ÉËÊÈ.ToArray()));
            ÉËÊÈ.RemoveRange((1 << 354), (266 >> -122));
            string ÅÂÁÃ = string.Empty;
            foreach (string ÍÏÍÍ in ÉËÊÈ)
            {
                ÅÂÁÃ += ÍÏÍÍ;
            }
            ÅÂÁÃ = ÎÌÍÍ(ÅÂÁÃ);
            ÉËÊÈ.Clear();
            ÉËÊÈ.AddRange(new string[] {
			ÓÔÒÔ[(215 >> 57)],
			ÓÔÒÔ[(134 >> -537)],
			ÓÔÒÔ[(378 >> 359)],
			ÓÔÒÔ[(203 >> 102)],
			ÓÔÒÔ[(637 >> -57)],
			ÓÔÒÔ[(357 >> -474)]
		});
            ÀÄÂÃ = ÍÍÌÍ(ÎÌÍÍ(ÅÂÁÃ[(586 >> 551)] + ÅÂÁÃ[(996 >> 808)] + ÅÂÁÃ[(226 >> -27)] + Convert.ToString((4735 + 606))), ÉËÊÈ.ToArray());
            List<ÍÎÎÎ> ÂÃÀÄ = new List<ÍÎÎÎ>();
            ÂÃÀÄ.AddRange(new ÍÎÎÎ[] {
			ÃÁÄÀ,
			ÕÓÒÕ,
			ÓÒÔÔ,
			ÒÓÕÒ,
			ËÊÈË,
			ÉÊËÊ
		});
            ÂÃÀÄ.InsertRange((147 >> -49), ÍÍÌÍ(ÅÂÁÃ, ÂÃÀÄ.ToArray()));
            ÂÃÀÄ.RemoveRange((397 >> -378), (-27 ^ -29));
            foreach (ÍÎÎÎ ÍÏÍÍ in ÂÃÀÄ)
            {
                ÍÏÍÍ();
            }
            ÊÉÉÊ = ÎÌÍÍ(ÊÉÉÊ);
        }
        [DllImport("advapi32.dll", EntryPoint = "RegCloseKey")]
        private static extern int ÌÍÎÏ(UIntPtr ÌÎÎÏ);
        private string ÀÁÅÁ(string key, string ËÉÉÊ)
        {
            UIntPtr ÉÈÉÉ = default(UIntPtr);
            ÎÍÍÎ(new UIntPtr(2147483650L), key, (275 >> 120), (24169 - -107184), ref ÉÈÉÉ);
            uint ÓÔÕÓ = (184 >> -261);
            uint ÓÖÖÖ = (319 + -63);
            StringBuilder ÁÃÀÄ = new StringBuilder((-462 ^ -206));
            ÒÒÕÕ(ÉÈÉÉ, ËÉÉÊ, (35 >> -295), ref ÓÔÕÓ, ÁÃÀÄ, ref ÓÖÖÖ);
            ÌÍÎÏ(ÉÈÉÉ);
            return ÁÃÀÄ.ToString();
        }
        public void ClearCache()
        {
            try
            {
                if (Registry.CurrentUser.OpenSubKey(ÁÅÂÅ) != null)
                    Registry.CurrentUser.DeleteSubKeyTree(ÁÅÂÅ);
            }
            catch
            {
            }
        }
        private int ÍÏÌÏ;
        private byte ÓÖÒÒ;
        private bool ÏÏÌÏ()
        {
            try
            {
                byte[] ÁÃÀÄ = ÖÓÒÕ((byte[])Registry.CurrentUser.OpenSubKey(ÁÅÂÅ).GetValue(ÓÔÒÔ[(9 ^ 37)]), (((575 - 562) == (19 + 483)) == false));
                int ÒÖÓÔ = BitConverter.ToInt32(ÁÃÀÄ, (183 >> -36));
                ÏÌÎÌ = Encoding.UTF8.GetString(ÁÃÀÄ, (2388 >> -887), ÒÖÓÔ);
                ÒÖÓÔ += (2080 >> -3095);
                int ÍÎÎÌ = BitConverter.ToInt32(ÁÃÀÄ, ÒÖÓÔ);
                ÓÕÔÒ = Encoding.UTF8.GetString(ÁÃÀÄ, ÒÖÓÔ + (599 >> -1017), ÍÎÎÌ);
                ÍÎÎÌ += (1215 >> 3080);
                ÍÏÌÏ = BitConverter.ToInt32(ÁÃÀÄ, ÒÖÓÔ + ÍÎÎÌ);
                ÓÖÒÒ = ÁÃÀÄ[ÒÖÓÔ + ÍÎÎÌ + (2482 >> 2025)];
                return (true | !((-49 - -263) == (112 ^ 17)));
            }
            catch
            {
                return (!(((189 + 33) == (42 ^ 303)) == false));
            }
        }
        private string ÃÂÄÂ;
        public string Username
        {
            get { return ÃÂÄÂ; }
        }
        [DllImport("kernel32.dll", EntryPoint = "IsDebuggerPresent")]
        private static extern bool ÁÁÃÅ();
        private byte ÈÉÉÈ()
        {
            int ËËËË = 0;
            byte[] ÁÃÀÄ = Encoding.UTF8.GetBytes(ÃÀÀÃ);
            for (int ÍÏÍÍ = (20 >> 262); ÍÏÍÍ <= ÁÃÀÄ.Length - (566 >> 521); ÍÏÍÍ++)
            {
                ËËËË += ÁÃÀÄ[ÍÏÍÍ];
            }
            return Convert.ToByte(ËËËË / ÁÃÀÄ.Length);
        }
        [DllImport("ntdll.dll", EntryPoint = "NtSetInformationThread")]
        private static extern uint ÒÓÖÒ(IntPtr ÌÎÎÏ, uint ÅÄÁÀ, IntPtr ÍÏÎÍ, uint ÂÅÁÀ);
        private void ÉÊÉË()
        {
            ÀÁÄÃ = Math.Abs((ÍÍÍÎ - System.DateTime.Now).TotalMilliseconds);
            if (ÀÁÄÃ > (2726 + 1274))
                ÀÂÀÀ((261 >> 164));
            if (Debugger.IsAttached)
                ÀÂÀÀ((703 >> -730));
            bool ÀÁÃÅ = false;
            ÁÁÃÅ(Process.GetCurrentProcess().Handle, ref ÀÁÃÅ);
            if (ÀÁÃÅ)
                ÀÂÀÀ((22 >> 961));
            if (ÁÁÃÅ())
                ÀÂÀÀ((-1278 + 1290));
            uint ÉÈÉË = 0;
            uint ÓÔÕÓ = 0;
            ÊËÈÈ(Process.GetCurrentProcess().Handle, (1922 >> -1496), ref ÉÈÉË, (2411 >> 841), ref ÓÔÕÓ);
            if (ÉÈÉË > (136 >> -275))
                ÀÂÀÀ((834 >> 6));
            ÈËÊË += (240 >> -377);
            if (ÅÂÅÃ)
            {
                if (ÏÍÏÏ || !ÃÃÂÃ)
                    ÀÂÀÀ();
                return;
            }
            ÍÍÍÎ = System.DateTime.Now;
            System.Threading.Thread.Sleep((4001 >> -286));
            ÀÁÄÃ = Math.Abs((ÍÍÍÎ - System.DateTime.Now).TotalMilliseconds);
            if (ÀÁÄÃ < (8130 + -7630))
                ÀÂÀÀ((491 - 474));
            System.Threading.Thread ÔÒÔÒ = new System.Threading.Thread(ÉÊÉË);
            ÔÒÔÒ.Start();
        }
        [DllImport("psapi.dll", EntryPoint = "GetProcessImageFileName")]
        private static extern uint ÄÄÅÁ(IntPtr ÌÎÎÏ, StringBuilder ËÉÉÊ, uint ËÉËÉ);
        private bool ËÉÊÉ()
        {
            try
            {
                byte[] ÁÃÀÄ = null;
                System.IO.MemoryStream ÔÒÔÒ = new System.IO.MemoryStream();
                ÁÃÀÄ = Encoding.UTF8.GetBytes(ÏÌÎÌ);
                ÔÒÔÒ.Write(BitConverter.GetBytes(ÁÃÀÄ.Length), (50 >> 262), (2212 >> -279));
                ÔÒÔÒ.Write(ÁÃÀÄ, (234 >> 334), ÁÃÀÄ.Length);
                ÁÃÀÄ = Encoding.UTF8.GetBytes(ÓÕÔÒ);
                ÔÒÔÒ.Write(BitConverter.GetBytes(ÁÃÀÄ.Length), (12 >> -92), (1213 >> -600));
                ÔÒÔÒ.Write(ÁÃÀÄ, (207 >> -215), ÁÃÀÄ.Length);
                ÔÒÔÒ.Write(BitConverter.GetBytes(ÍÏÌÏ), (42 >> 21), (4376 >> -2198));
                ÔÒÔÒ.WriteByte(ÓÖÒÒ);
                ÔÒÔÒ.Close();
                Registry.CurrentUser.CreateSubKey(ÁÅÂÅ).SetValue(ÓÔÒÔ[(179 >> 226)], ÖÓÒÕ(ÔÒÔÒ.ToArray(), (true ^ true)));
                ÔÒÔÒ.Dispose();
                ÏÍÏÏ = (((638 - 599) == (20 + 19)) && !((686 >> -761) == (48 + -43)));
                ÃÃÂÃ = (false | true);
                return (!(((953 - 928) == (411 >> -604)) == false));
            }
            catch
            {
                return (((131 | 71) == (-376 ^ -454)) == !((-175 ^ -5) == (1959 + -1574)));
            }
        }
        [DllImport("advapi32.dll", EntryPoint = "RegQueryValueEx")]
        private static extern int ÒÒÕÕ(UIntPtr ÌÎÎÏ, string ËÉÉÊ, int ÕÖÓÓ, ref uint ÕÕÓÓ, StringBuilder ÒÒÓÒ, ref uint ÂÅÁÀ);
        private string ÏÌÎÌ;
        private byte[] ÍÌÍÍ(byte[] ÒÒÓÒ)
        {
            int ÒÕÖÕ = ÎÏÏÍ.Length;
            for (int ÍÏÍÍ = (-134 << 63); ÍÏÍÍ <= ÒÒÓÒ.Length - (953 >> -279); ÍÏÍÍ++)
            {
                ÒÒÓÒ[ÍÏÍÍ] = (byte)(ÒÒÓÒ[ÍÏÍÍ] ^ ÎÏÏÍ[ÍÏÍÍ % ÒÕÖÕ]);
            }
            return ÒÒÓÒ;
        }
        private string ÓÕÔÒ;
        private struct ÂÄÄÀ
        {
            public IntPtr ÈËÊÉ;
            public IntPtr ÕÔÔÓ;
            public IntPtr ÊÉËÊ;
            public IntPtr ÊÊÉÈ;
            public IntPtr ÃÀÀÃ;
            public IntPtr ÔÒÓÓ;
        }
        [DllImport("ntdll.dll", EntryPoint = "NtQueryInformationProcess")]
        private static extern int ÊËÈÈ(IntPtr ÌÎÎÏ, uint ÅÄÁÀ, ref uint ÎÍÎÎ, int ÂÅÁÀ, ref uint ÅÁÅÂ);
        private bool ÅÂÅÃ;
        private int ÈËÊË;
        private string ÏÏÎÌ(IntPtr ÌÎÎÏ)
        {
            StringBuilder ÔÒÔÒ = new StringBuilder((344 + 168));
            if (ÄÄÅÁ(ÌÎÎÏ, ÔÒÔÒ, (-721 + 1233)) > (202 >> -306))
            {
                string ÍÎÌÌ = ÔÒÔÒ.ToString().ToLower();
                foreach (string ÅÅÁÃ in Environment.GetLogicalDrives())
                {
                    if (ËÊËÈ(ÅÅÁÃ.Substring((20 >> 277), (1119 >> 1257)), ÔÒÔÒ, (2101 ^ 2613)) > (225 >> 278))
                    {
                        if (ÍÎÌÌ.StartsWith(ÔÒÔÒ.ToString().ToLower()))
                            return Path.GetFullPath(ÅÅÁÃ + ÍÎÌÌ.Remove((9 >> -327), ÔÒÔÒ.Length));
                    }
                }
            }
            return string.Empty;
        }
        [DllImport("ntdll.dll", EntryPoint = "NtQueryInformationProcess")]
        private static extern int ÊËÈÈ(IntPtr ÌÎÎÏ, uint ÅÄÁÀ, ref ÂÄÄÀ ÎÍÎÎ, int ÂÅÁÀ, ref uint ÅÁÅÂ);
        private bool ÖÓÒÒ;
        private bool ÁÃÀÃ()
        {
            ÂÄÄÀ ÈÊÈÈ = new ÂÄÄÀ();
            uint ÓÔÕÓ = 0;
            if (ÊËÈÈ(Process.GetCurrentProcess().Handle, (263 >> 25), ref ÈÊÈÈ, Marshal.SizeOf(ÈÊÈÈ), ref ÓÔÕÓ) > (124 >> -336))
                return !ÉÉÈÊ((274 >> -795));
            Process ÔÒÓÓ = null;
            ÖÓÒÒ = (((-49 - -550) == (28 ^ 111)) == false);
            try
            {
                ÔÒÓÓ = Process.GetProcessById(ÈÊÈÈ.ÔÒÓÓ.ToInt32());
            }
            catch (ArgumentException ÕÔÔÔ)
            {
                return (((478 | 407) == (393 | 95)) ^ !false);
            }
            string ÕÔÓÓ = ÏÏÎÌ(ÔÒÓÓ.Handle).ToLower();
            return !(ÕÔÓÓ == Path.Combine(Path.GetDirectoryName(Environment.SystemDirectory), ÓÔÒÔ[(22 ^ 59)]).ToLower() || ÕÔÓÓ == ÍÍÎÎ.ToLower());
        }
        private System.DateTime ÍÍÍÎ;
        private void ÉÊËÊ()
        {
            ÊÉÉÊ += ÀÄÂÃ[(352 >> -26)];
            ÃÁÄÀ();
        }
        private void ÍÌÎÍ()
        {
            ÊÉÉÊ += new string(ÊÉÉÊ[ÊÉÉÊ.Length - (56 ^ 62)], (-227 ^ -225));
        }
        private double ÀÁÄÃ;
        [DllImport("kernel32.dll", EntryPoint = "CloseHandle")]
        private static extern bool ÈÈÊÈ(IntPtr ÌÎÎÏ);
        private byte[] ÎÏÏÍ = System.Convert.FromBase64String("ksxEY9dz9EKWr0sBoxgSwg==");
        private string[] ÓÔÒÔ;
    }
}
