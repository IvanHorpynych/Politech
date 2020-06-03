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

namespace ZI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        private void Start_Click(object sender, EventArgs e)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(tServer.Text);
            int port = int.Parse(tPort.Text);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            s.Connect(ipe);
            byte[] buf = AuthRequest("/", "localhost", "mc", "1111");
            s.Send(buf);
            byte[] rbuf = new byte[1024];
            int rs = s.Receive(rbuf);
            tAns.Text = "";
            for (int i = 0; i < rs; ++i)
            {
                tAns.Text += (char)rbuf[i];
            }
            buf = AuthRequest("/", "localhost", "mc", "1234");
            s.Send(buf);
            rbuf = new byte[1024];
            rs = s.Receive(rbuf);
            tbb.Text = "";
            for(int i = 0; i < rs; ++i)
            {
                tbb.Text += (char)rbuf[i];
            }
            s.Close();
        
        }

    }
}
