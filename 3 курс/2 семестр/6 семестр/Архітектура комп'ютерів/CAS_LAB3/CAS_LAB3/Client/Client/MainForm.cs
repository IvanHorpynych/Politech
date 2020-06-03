using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Threading;

namespace Client
{
    public partial class MainForm : Form
    {
        byte[] m_dataBuffer = new byte [ 10 ];
		IAsyncResult m_result;
		public AsyncCallback m_pfnCallBack ;
		public Socket m_clientSocket;

        public class SocketPacket
		{
			public Socket thisSocket;
			public byte[] dataBuffer = new byte[ 1024 * 1024 ];
		}

        public MainForm()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            textBoxIP.Text = GetIP();
        }

        static public string EncodeTo64( string toEncode )
        {
            return Convert.ToBase64String( ASCIIEncoding.ASCII.GetBytes( toEncode ) );
        }

        static public string DecodeFrom64( string encodedData )
        {
          byte[] encodedDataAsBytes = Convert.FromBase64String( encodedData );
          return ASCIIEncoding.ASCII.GetString( encodedDataAsBytes );
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            richTextRxMessage.Clear();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
			if ( m_clientSocket != null )
			{
				m_clientSocket.Close();
				m_clientSocket = null;
			}		
			Close();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
			// See if we have text on the IP and Port text fields
			if( textBoxIP.Text == "" || textBoxPort.Text == "" )
            {
				MessageBox.Show( "IP Address and Port Number are required to connect to the Server\n" );
				return;
			}
			try
			{
				UpdateControls( false );

                textBoxIP.Enabled = false;
                textBoxPort.Enabled = false;

				// Create the socket instance
				m_clientSocket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
				
				// Get the remote IP address
				IPAddress ip = IPAddress.Parse( textBoxIP.Text );

				int iPortNo = Convert.ToInt16( textBoxPort.Text );

				// Create the end point 
				IPEndPoint ipEnd = new IPEndPoint( ip,iPortNo );

				// Connect to the remote host
				m_clientSocket.Connect( ipEnd );

				if( m_clientSocket.Connected )
                {
					
					UpdateControls( true );
					// Wait for data asynchronously 
					WaitForData();
				}
			}
			catch( SocketException se )
			{
				string str = "\nConnection failed, is the server running?\n" + se.Message;
				MessageBox.Show( str );
				UpdateControls( false );
			}	
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            textBoxPort.Enabled = true;
            textBoxIP.Enabled = true;

			if ( m_clientSocket != null )
			{
				m_clientSocket.Close();
				m_clientSocket = null;
				UpdateControls( false );
			}
        }

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
			try
			{
				string msg = textBoxQuery.Text;

                // Processing client request
                string[] query = msg.Split( ' ', '\r', '\n' );

                if( query.Length < 2 || query.Length > 4 )
                {
                    richTextRxMessage.Text += "ServerMsg: Invalid command syntax!\n";
                    return;
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml( "<package></package>" );

                if( query[ 0 ].ToUpper() == "DL" && query.Length == 2 )
                { 
                    // create XML packet - tag 0
                    XmlNode tag = doc.CreateElement( "tag" );
                    tag.InnerText = "0";
                    doc.DocumentElement.AppendChild( tag ); 

                    XmlNode filename = doc.CreateElement( "filename" );
                    filename.InnerText = query[ 1 ];
                    doc.DocumentElement.AppendChild( filename ); 
                }
                else
                    if(  query[ 0 ].ToUpper() == "UL" && query.Length == 3 )
                    {
                        // process upload parameters - look up for the file (query[1]) in *.exe directory
                        FileInfo fi = new FileInfo( Application.StartupPath + query[ 1 ] );
                        
                        if( fi.Exists )
                        {
                            // create XML packet - tag 1
                            XmlNode tag = doc.CreateElement( "tag" );
                            tag.InnerText = "1";
                            doc.DocumentElement.AppendChild( tag ); 

                            XmlNode filename = doc.CreateElement( "filename" );
                            filename.InnerText = query[ 1 ];
                            doc.DocumentElement.AppendChild( filename ); 

                            XmlNode file = doc.CreateElement( "file" );

                            // load file (query[1])
                            BinaryReader inBin = new BinaryReader( File.Open( fi.FullName, FileMode.Open ) );
                            byte[] tr = inBin.ReadBytes( Convert.ToInt32( fi.Length ) );
                            //String Content = Encoding.Default.GetString( tr );
                            inBin.Close();

                            // convert file content to the BASE64
                            file.InnerText = Convert.ToBase64String( tr );
                            doc.DocumentElement.AppendChild( file );

                            XmlNode destination = doc.CreateElement( "destination" );
                            destination.InnerText = query[ 2 ];
                            doc.DocumentElement.AppendChild( destination ); 
                        }
                        else
                        {
                            richTextRxMessage.Text += "ServerMsg: Invalid filename!\n"; 
                            return;
                        }
                    }
                    else
                        if( query[ 0 ].ToUpper() == "CR" && query.Length == 2 )
                        {
                            // create XML packet - tag 2
                            XmlNode tag = doc.CreateElement( "tag" );
                            tag.InnerText = "2";
                            doc.DocumentElement.AppendChild( tag ); 

                            XmlNode path = doc.CreateElement( "path" );
                            path.InnerText = query[ 1 ];
                            doc.DocumentElement.AppendChild( path ); 
                        }
                        else
                            if( query[ 0 ].ToUpper() == "DEL" && query.Length == 3 )
                            {
                                if( query[ 1 ].ToUpper() == "FL" || query[ 1 ].ToUpper() == "DIR" )
                                {
                                    // create XML packet - tag 3 or 4
                                    XmlNode tag = doc.CreateElement( "tag" );

                                    if( query[ 1 ].ToUpper() == "FL" )
                                    {
                                        tag.InnerText = "3";
                                    }
                                    else // if query[ 1 ].ToUpper() == "DIR"
                                    {
                                        tag.InnerText = "4";
                                    }

                                    doc.DocumentElement.AppendChild( tag ); 

                                    XmlNode type = doc.CreateElement( "type" );
                                    type.InnerText = query[ 1 ];
                                    doc.DocumentElement.AppendChild( type ); 

                                    XmlNode path = doc.CreateElement( "path" );
                                    path.InnerText = query[ 2 ];
                                    doc.DocumentElement.AppendChild( path ); 
                                }
                                else
                                {
                                    richTextRxMessage.Text += "ServerMsg: Invalid command syntax!\n"; 
                                    return;
                                }
                            }
                            else
                                if( query[ 0 ].ToUpper() == "RN" && query.Length == 3 )
                                {
                                    if( query[ 1 ] == query[ 2 ] )
                                    {
                                        richTextRxMessage.Text += "ServerMsg: Please, use different old and new names!\n"; 
                                        return;
                                    }

                                    // create XML packet - tag 5
                                    XmlNode tag = doc.CreateElement( "tag" );
                                    tag.InnerText = "5";
                                    doc.DocumentElement.AppendChild( tag ); 

                                    XmlNode old_name = doc.CreateElement( "old_name" );
                                    old_name.InnerText = query[ 1 ];
                                    doc.DocumentElement.AppendChild( old_name ); 

                                    XmlNode new_name = doc.CreateElement( "new_name" );
                                    new_name.InnerText = query[ 2 ];
                                    doc.DocumentElement.AppendChild( new_name ); 
                                }
                                else
                                    if( query[ 0 ].ToUpper() == "LS" && query.Length == 2 )
                                    {
                                        // create XML packet - tag 8
                                        XmlNode tag = doc.CreateElement( "tag" );
                                        tag.InnerText = "6";
                                        doc.DocumentElement.AppendChild( tag ); 

                                        XmlNode dir = doc.CreateElement( "dir" );
                                        dir.InnerText = query[1];
                                        doc.DocumentElement.AppendChild( dir ); 
                                    }
                                    else
                                    {
                                        richTextRxMessage.Text += "ServerMsg: Invalid command syntax!\n"; 
                                        return;
                                    }

				/* Following code sends bytes */
                byte[] byData = Encoding.ASCII.GetBytes( doc.InnerXml.ToString() );

                if( m_clientSocket != null )
                {
                    // fake vizualization
                    buttonClose.Enabled = false;
                    buttonDisconnect.Enabled = false;
                    buttonSendMessage.Enabled = false;
                    btnClear.Enabled = false;

                    progressBar.Value = 0;
                    progressBar.Minimum = 0;
                    progressBar.Maximum = ( int )( byData.Length / 1024 ) + 1;

                    for( int i = 0; i <= ( int )( byData.Length / 1024 ); i++ )
                    {
                        Thread.Sleep( 20 );
                        progressBar.Value++;
                        Application.DoEvents();
                    }

                    buttonClose.Enabled = true;
                    buttonDisconnect.Enabled = true;
                    buttonSendMessage.Enabled = true;
                    btnClear.Enabled = true;

                    // real sending
                    m_clientSocket.Send( byData );
                }
			}
			catch( SocketException se )
			{
				MessageBox.Show( se.Message );
			}
        }

        public void OnDataReceived( IAsyncResult asyn )
		{
			try
			{
				SocketPacket theSockId = ( SocketPacket )asyn.AsyncState ;

				int iRx  = theSockId.thisSocket.EndReceive( asyn );
				char[] chars = new char[ iRx ];

				Decoder d = Encoding.UTF8.GetDecoder();

				int charLen = d.GetChars( theSockId.dataBuffer, 0, iRx, chars, 0 );

				String szData = new String( chars );

                if( szData[ szData.Length - 1 ] == '>' )
                {
                    String _reply = Parse( szData );

                    szData = "ServerMsg: " + _reply + "\n";

                    // Now, task is to parse the server reply and to make some actions

				    richTextRxMessage.AppendText( szData );
                }

				WaitForData();
			}
			catch( ObjectDisposedException )
			{
				Debugger.Log( 0, "1", "\nOnDataReceived: Socket has been closed\n" );
			}
			catch( SocketException se )
			{
				MessageBox.Show( se.Message );
			}
		}

        public String Parse( String _xmlData )
        {
            String result = "";
            XmlReader reader = XmlReader.Create( new StringReader( _xmlData ) );

            reader.ReadToFollowing( "tag" );
            int _tag = reader.ReadElementContentAsInt();

            switch( _tag )
            {
                case 0: result = DLParse( _xmlData );
                    break;
                case 1: result = LSParse( _xmlData );
                    break;
                case -1: result = ErrorParse( _xmlData );
                    break;
                case -2: result = MessageParse( _xmlData );
                    break;
            }

            return result;
        }

        public String DLParse( String _xmlData )
        {
            XmlReader reader = XmlReader.Create( new StringReader( _xmlData ) );
            reader.ReadToFollowing( "filename" );
            String _filename = reader.ReadElementContentAsString();

            reader = XmlReader.Create( new StringReader( _xmlData ) );
            reader.ReadToFollowing( "content" );
            String _content = reader.ReadElementContentAsString();

            saveFileDialog.FileName = _filename;

            DialogResult r = this.saveFileDialog.ShowDialog( this );
            if( r != DialogResult.OK )
            {
                return "SaveDialog can not be shown!";
            }

            FileStream filestream = File.Open( saveFileDialog.FileName, FileMode.Create, FileAccess.Write );
            BinaryWriter streamwriter = new BinaryWriter( filestream );

            streamwriter.Write( Convert.FromBase64String( _content ) );
            streamwriter.Flush();
            
            filestream.Close();

            return "File has been successfully downloaded!";
        }

        public String ErrorParse( String _xmlData )
        {
           XmlReader reader = XmlReader.Create( new StringReader( _xmlData ) );

            reader.ReadToFollowing( "error" );
            return reader.ReadElementContentAsString();
        }

        public String LSParse( String _xmlData )
        {
           XmlReader reader = XmlReader.Create( new StringReader( _xmlData ) );

            reader.ReadToFollowing( "content" );

            String _content = "\n" + reader.ReadElementContentAsString();

            _content = _content.Substring( 0, _content.Length - 1 );

            return _content;
        }

        public String MessageParse( String _xmlData )
        {
           XmlReader reader = XmlReader.Create( new StringReader( _xmlData ) );

            reader.ReadToFollowing( "message" );
            return reader.ReadElementContentAsString();
        }
	
		private void UpdateControls( bool connected ) 
		{
			buttonConnect.Enabled = !connected;
			buttonDisconnect.Enabled = connected;
			string connectStatus = connected? "Connected" : "Not Connected";
			textBoxConnectStatus.Text = connectStatus;
		}

        public void WaitForData()
		{
			try
			{
				if( m_pfnCallBack == null ) 
				{
					m_pfnCallBack = new AsyncCallback( OnDataReceived );
				}

				SocketPacket theSocPkt = new SocketPacket();

				theSocPkt.thisSocket = m_clientSocket;

				// Start listening to the data asynchronously
				m_result = m_clientSocket.BeginReceive( theSocPkt.dataBuffer, 0, theSocPkt.dataBuffer.Length, SocketFlags.None, m_pfnCallBack, theSocPkt );
			}
			catch( SocketException se )
			{
				MessageBox.Show( se.Message );
			}
		}

       String GetIP()
	   {	   
	   		String strHostName = Dns.GetHostName();
		
		   	// Find host by name
		   	IPHostEntry iphostentry = Dns.GetHostByName( strHostName );
		
		   	// Grab the first IP addresses
		   	String IPStr = "";

		   	foreach( IPAddress ipaddress in iphostentry.AddressList )
            {
		        IPStr = ipaddress.ToString();
		   		return IPStr;
		   	}

		   	return IPStr;
	   }
    }
}