using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace KA4_server
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
       
        
        string LoadFromFile(string filename, string authName)
        {
            BinaryReader br = new BinaryReader(new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read));
            try
            {
                string str = br.ReadString();
                string[] sequ = str.Split('\n');
                for ( int i = 0; i<sequ.Length; i++ )
                {
                    //string[] dat = str.Split(';');
                    string[] dat = sequ[i].Split(';');
                    br.Close();
                    

                    if (dat[0] == authName)
                    {
                        br.Close();
                        return sequ[i];
                    }
                }
            }
            catch (Exception ex)
            {
                br.Close();
                return "";
            }
            br.Close();
            return "";
        }

        [WebMethod]
        public string Autorization(string name, string pass)
        {
            
            string str_reg = LoadFromFile(@"D:\registered.txt", name);
           
           
            if (str_reg == "")
                return "user isn't registered";
          
            string str = "";
            BinaryReader br = new BinaryReader(new FileStream(@"D:\active.txt", FileMode.OpenOrCreate, FileAccess.Read));
            try
            {
                str = br.ReadString();
                
                string[] data = str.Split(' ');
                if (data.Contains(name))
                {
                    br.Close();
                    return "user has already entered";
                }
            }
            catch(Exception ex)
            {
            }
            br.Close();
            User us = new User();
            
            string[] dat = str_reg.Split(';');
            us.UserName = dat[0];
            us.Password = dat[1];
           

            if (dat[1] != pass)
                return "wrong password! try again";

            if (dat[2] == "blocked")
                return "current user is blocked";

           
            
           
            str += name + " ";
            
            BinaryWriter bw = new BinaryWriter(new FileStream(@"D:\active.txt", FileMode.Truncate));
            bw.Write(str);
            bw.Close();
           
            

            return "hello, " + us.UserName + "!";
        }

        [WebMethod]
        public string Register(string smth)
        {

            string[] dat = smth.Split(';');
            string curr_us = LoadFromFile(@"D:\registered.txt", dat[0]);
            if (curr_us != "")
                return "This nickname is busy!";

            string str = "";
            string buf = "";
                BinaryReader br = new BinaryReader(new FileStream(@"D:\registered.txt", FileMode.OpenOrCreate, FileAccess.Read));
                try
                {
                   
                    while ((str = br.ReadString()) != null)
                        buf += str + "\n";
                }
                catch(Exception ex)
                {
                }
                br.Close();

                buf += smth + "user\n";
                BinaryWriter bw = new BinaryWriter(new FileStream(@"D:\registered.txt", FileMode.Truncate));
                bw.Write(buf);
                bw.Close();


            return "You are registered! Please enter using your login";
        }

        [WebMethod]
        public string GetStatus(string Authname)
        {
            string name = Authname;
            string str = LoadFromFile(@"D:\registered.txt", name);
            string[] dat = str.Split(';');
            return dat[2];
        }

        
        [WebMethod]
        public string GetRegUsers()
        {
            string str = "";
            string users = "";
            BinaryReader br = new BinaryReader(new FileStream(@"D:\registered.txt", FileMode.OpenOrCreate, FileAccess.Read));
            try
            {
                str = br.ReadString();
                string[] sequ = str.Split('\n');
                for (int i = 0; i < sequ.Length; i++)
                {
                    if (sequ[i] == "")
                        continue;
                    string[] dat = sequ[i].Split(';');
                    users += dat[0] + ";";
                }
            }
            catch (Exception ex)
            {
            }
            br.Close();
            return users;
        }

        [WebMethod]
        public void ChangeStatus(string name, string status)
        {
            string str;
            string buf = "";
            BinaryReader br = new BinaryReader(new FileStream(@"D:\registered.txt", FileMode.OpenOrCreate, FileAccess.Read));
            try
            {
                str = br.ReadString();
                string[] sequ = str.Split('\n');
                for (int i = 0; i < sequ.Length; i++)
                {
                    if (sequ[i] == "")
                        continue;
                    string[] dat = sequ[i].Split(';');
                    if (dat[0] == name)
                    
                        buf += dat[0] + ";" + dat[1] + ";" + status + "\n";
                    
                    else buf += sequ[i] + "\n";
                }
            }
            catch (Exception ex)
            {
            }
            br.Close();

            BinaryWriter bw = new BinaryWriter(new FileStream(@"D:\registered.txt", FileMode.Truncate));
            bw.Write(buf);
            bw.Close();

        }


        [WebMethod]
        public void AddMsg(string msg)
        {
            BinaryWriter bw = new BinaryWriter(new FileStream(@"D:\session.txt", FileMode.Append));
            //string buf = user + "    " + DateTime.Now.ToString() + "\n" + msg + "\n" + "-----------------------------------------------\n";
            bw.Write(msg);
            bw.Close();
        }


        [WebMethod]
        public string Refresh(string fname)
        {
            
            string buf = "";
          
            BinaryReader br = new BinaryReader(new FileStream(fname, FileMode.OpenOrCreate, FileAccess.Read));
            
            try
            {
                string str = "";

                while ((str = br.ReadString()) != null)
                {
                    buf += str;
                }
            }
            catch (Exception ex)
            {
                br.Close();
                return buf;
            }
            br.Close();
            return buf;
        }

        [WebMethod]
        public string refreshUsers()
        {
            string buf = "";
            string str = "";
            BinaryReader br = new BinaryReader(new FileStream(@"D:\active.txt", FileMode.OpenOrCreate, FileAccess.Read));
            try
            {
                
                str = br.ReadString();
               
            }
            catch (Exception ex)
            {
                br.Close();
                return buf;
            }
            br.Close();

            string[] dat = str.Split(' ','\t','\n');
            for ( int i = 0; i<dat.Length; i++)
                if ( dat[i] != "" )
                {
                    return str;
                }

            string msg = Refresh(@"D:\session.txt");

            BinaryWriter bw = new BinaryWriter(new FileStream(@"D:\session.txt", FileMode.Truncate));
            bw.Close();

            bw = new BinaryWriter(new FileStream(@"D:\journal.txt", FileMode.Truncate));
            bw.Close();

            bw = new BinaryWriter(new FileStream(@"D:\active.txt", FileMode.Truncate));
            bw.Close();

            bw = new BinaryWriter(new FileStream(@"D:\history.txt", FileMode.Append));
            bw.Write(msg);
            bw.Close();

            return "";

          
        }

        [WebMethod]
        public string GetHistory()
        {
            string buf = "";
            BinaryReader br = new BinaryReader(new FileStream(@"D:\history.txt", FileMode.OpenOrCreate, FileAccess.Read));
            try
            {
                string str = "";

                while ((str = br.ReadString()) != null)
                {
                    buf += str;
                }
            }
            catch (Exception ex)
            {
                //br.Close();
                //return buf;
            }
            br.Close();

            string[] sequ = buf.Split('\n');
            buf = "";
            for (int i = 0; i < sequ.Length; i+=3)
            {
                string[] dat = sequ[i].Split(' ');
                int j = 1;
                string temp = "";
                for (j = 1; j < dat.Length; j++)
                {
                    if (dat[j] != "")
                    {
                        temp += dat[j] + " 0:00:00";
                        break;
                    }
                }
               
                if ( temp == DateTime.Now.Date.ToString() )
                   buf += sequ[i] + "\n" + sequ[i + 1] + "\n" + sequ[i + 2] + "\n";
            }

            return buf + Refresh(@"D:\session.txt");
        }


        [WebMethod]
        public string UserLogOff(string name)
        {
            string buf = "";
            string ret = "";
            BinaryReader br = new BinaryReader(new FileStream(@"D:\active.txt", FileMode.OpenOrCreate, FileAccess.Read));
            try
            {
                string str = br.ReadString();

                string[] dat = str.Split(' ');

                for (int i = 0; i < dat.Length; i++)
                {
                    if (dat[i] != name)
                        buf += dat[i] + " ";
                }
            }
            catch (Exception ex)
            {
                //br.Close();
                //return ret;
            }
            br.Close();
           
            BinaryWriter bw = new BinaryWriter(new FileStream(@"D:\active.txt", FileMode.Truncate));
            bw.Write(buf);
            bw.Close();

            

            return buf;
        }

        [WebMethod]
        public void WriteDownToJourn(string sign)
        {
            BinaryWriter bw = new BinaryWriter(new FileStream(@"D:\journal.txt", FileMode.Append));
            bw.Write(sign);
            bw.Close();
        }


        [Serializable]
        class User
        {
            public string UserName;
            public string Password;
            public bool blocked;

            public User()
            {
            }

            public User(string name, string pass, bool isblock)
            {
                UserName = name;
                Password = pass;
                blocked = isblock;
            }
        }

        
    }
}
