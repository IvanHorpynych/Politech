using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZI
{
    class Proc
    {
        public string uri;
        public string host;
        public string login;
        public string pass;
        public string server;
        public string port;

        public Proc(string u, string h, string l, string p, string s, string pr)
        {
            uri = u;
            host = h;
            login = l;
            pass = p;
            server = s;
            port = pr;
        }
    }
}
