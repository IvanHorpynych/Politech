using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;

namespace Server
{
    public partial class MainForm : Form
    {
        private OleDbConnection AccessConnection;

        public class SocketPacket
	    {
		    // Constructor which takes a Socket and a client number
		    public SocketPacket( Socket socket, int clientNumber )
		    {
			    m_currentSocket = socket;
			    m_clientNumber  = clientNumber;
		    }

		    public Socket m_currentSocket;
		    public int m_clientNumber;

		    // Buffer to store the data sent by the client
		    public byte[] dataBuffer = new byte[ 1024 * 1024 ];
	    }

        public delegate void UpdateRichEditCallback(string text);
		public delegate void UpdateClientListCallback();
				
		public AsyncCallback pfnWorkerCallBack ;
		private  Socket m_mainSocket;

		// An ArrayList is used to keep track of worker sockets that are designed
		// to communicate with each connected client. Make it a synchronized ArrayList
		// For thread safety
		private System.Collections.ArrayList m_workerSocketList = ArrayList.Synchronized( new System.Collections.ArrayList() );

		// The following variable will keep track of the cumulative 
		// total number of clients connected at any time. Since multiple threads
		// can access this variable, modifying this variable should be done
		// in a thread safe manner
		private int m_clientCount = 0;

        public MainForm()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            // Display the local IP address on the GUI
			textBoxIP.Text = GetIP();
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            richTextBoxReceivedMsg.Clear();
        }

        private void buttonStartListen_Click(object sender, EventArgs e)
        {
            try
			{
				// Check the port value
				if( textBoxPort.Text == "" )
				{
					MessageBox.Show( "Please enter a Port Number" );
					return;
				}

                textBoxPort.Enabled = false;

				string portStr = textBoxPort.Text;
				int port = System.Convert.ToInt32( portStr );

				// Create the listening socket...
				m_mainSocket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

				IPEndPoint ipLocal = new IPEndPoint( IPAddress.Any, port );

				// Bind to local IP Address...
				m_mainSocket.Bind( ipLocal );

				// Start listening...
				m_mainSocket.Listen( 4 );

				// Create the call back for any client connections...
				m_mainSocket.BeginAccept( new AsyncCallback ( OnClientConnect ), null );
				
				UpdateControls( true );

                AccessConnection = new OleDbConnection( @"provider=Microsoft.Jet.OLEDB.4.0;data source=" + @"db.mdb" );
			}
			catch( SocketException se )
			{
				MessageBox.Show ( se.Message );
			}
        }

        private void UpdateControls( bool listening ) 
		{
			buttonStartListen.Enabled = !listening;
			buttonStopListen.Enabled = listening;
		}	
		
        // This is the call back function, which will be invoked when a client is connected
		public void OnClientConnect( IAsyncResult asyn )
		{
			try
			{
				// Here we complete/end the BeginAccept() asynchronous call
				// by calling EndAccept() - which returns the reference to
				// a new Socket object
				Socket workerSocket = m_mainSocket.EndAccept( asyn );

				// Now increment the client count for this client 
				// in a thread safe manner
				Interlocked.Increment( ref m_clientCount );
				
				// Add the workerSocket reference to our ArrayList
				m_workerSocketList.Add( workerSocket );

				// Send a welcome message to client
                XmlDocument doc = new XmlDocument();
                doc.LoadXml( "<package></package>" );

                // create XML packet - tag -2
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-2";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode message = doc.CreateElement( "message" );
                message.InnerText = "Welcome client " + m_clientCount;
                doc.DocumentElement.AppendChild( message ); 

				string msg = doc.InnerXml.ToString();
				SendMsgToClient( msg, m_clientCount );

				// Update the list box showing the list of clients (thread safe call)
				UpdateClientListControl();

				// Let the worker Socket do the further processing for the 
				// just connected client
				WaitForData( workerSocket, m_clientCount );
							
				// Since the main Socket is now free, it can go back and wait for
				// other clients who are attempting to connect
				m_mainSocket.BeginAccept( new AsyncCallback( OnClientConnect ), null );
			}
			catch( ObjectDisposedException )
			{
				System.Diagnostics.Debugger.Log( 0, "1", "\n OnClientConnection: Socket has been closed\n" );
			}
			catch( SocketException se )
			{
				MessageBox.Show( se.Message );
			}
		}

		// Start waiting for data from the client
		public void WaitForData( Socket soc, int clientNumber )
		{
			try
			{
				if( pfnWorkerCallBack == null )
				{		
					// Specify the call back function which is to be 
					// invoked when there is any write activity by the 
					// connected client
					pfnWorkerCallBack = new AsyncCallback( OnDataReceived );
				}

				SocketPacket theSocPkt = new SocketPacket( soc, clientNumber );

				soc.BeginReceive( theSocPkt.dataBuffer, 0, theSocPkt.dataBuffer.Length, SocketFlags.None, pfnWorkerCallBack, theSocPkt );
			}
			catch( SocketException se )
			{
				MessageBox.Show ( se.Message );
			}
		}

		// This the call back function which will be invoked when the socket
		// detects any client writing of data on the stream
		public void OnDataReceived( IAsyncResult asyn )
		{
			SocketPacket socketData = ( SocketPacket )asyn.AsyncState ;
			try
			{
				// Complete the BeginReceive() asynchronous call by EndReceive() method
				// which will return the number of characters written to the stream 
				// by the client
				int iRx  = socketData.m_currentSocket.EndReceive( asyn );

				char[] chars = new char[ iRx ];

				// Extract the characters as a buffer
				Decoder d = Encoding.UTF8.GetDecoder();

				int charLen = d.GetChars( socketData.dataBuffer, 0, iRx, chars, 0 );

				String szData = new String( chars );
				
                string msg = "" + socketData.m_clientNumber + ": " + szData + "\r\n";
				
                AppendToRichEditControl( msg );

                // Now, task is to parse XML packet & make some actions
                // after that - return result to the client

                String _replyXML = Parse( szData );
                
				// Send back the reply to the client
				
                // Convert the reply to byte array
				byte[] byData = Encoding.ASCII.GetBytes( _replyXML );

				Socket workerSocket = ( Socket )socketData.m_currentSocket;
				
                workerSocket.Send( byData );
	
				// Continue the waiting for data on the Socket
				WaitForData( socketData.m_currentSocket, socketData.m_clientNumber );

			}
			catch( ObjectDisposedException )
			{
				System.Diagnostics.Debugger.Log( 0,"1","\nOnDataReceived: Socket has been closed\n" );
			}
			catch( SocketException se )
			{
				if( se.ErrorCode == 10054 ) // Error code for Connection reset by peer
				{	
					string msg = "Client " + socketData.m_clientNumber + " Disconnected" + "\n";
					AppendToRichEditControl( msg );

					// Remove the reference to the worker socket of the closed client
					// so that this object will get garbage collected
					m_workerSocketList[ socketData.m_clientNumber - 1 ] = null;
					UpdateClientListControl();
				}
				else
				{
					MessageBox.Show( se.Message );
				}
			}
		}

        String Parse( String _xmlData )
        {
            String result = "";
            XmlReader reader = XmlReader.Create( new StringReader( _xmlData ) );

            reader.ReadToFollowing( "tag" );
            int _tag = reader.ReadElementContentAsInt();

            switch( _tag )
            {
                case 0: result = DLParse( _xmlData );
                    break;
                case 1: result = ULParse( _xmlData );
                    break;
                case 2: result = CRParse( _xmlData );
                    break;
                case 3: result = DELParseFile( _xmlData );
                    break;
                case 4: result = DELParseDir( _xmlData );
                    break;
                case 5: result = RNParse( _xmlData );
                    break;
                case 6: result = LSParse( _xmlData );
                    break;
            }

            return result;
        }

        String DLParse( String _xmlData )
        {
            XmlReader reader = XmlReader.Create( new StringReader( _xmlData ) );

            reader.ReadToFollowing( "filename" );
            String _filename = reader.ReadElementContentAsString();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml( "<package></package>" );

            // Checking file existence
            if( CheckFile( _filename ) )
            {
                // create XML packet - tag 0
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "0";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode filename = doc.CreateElement( "filename" );
                filename.InnerText = Path.GetFileName( _filename );
                doc.DocumentElement.AppendChild( filename ); 

                XmlNode content = doc.CreateElement( "content" );
                content.InnerText = GetFile( _filename );
                doc.DocumentElement.AppendChild( content ); 
            }
            else
            {
                // create XML packet - tag -1
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-1";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode error = doc.CreateElement( "error" );
                error.InnerText = "File does not exist!";
                doc.DocumentElement.AppendChild( error ); 
            }

            return doc.InnerXml.ToString();
        }

        String ULParse( String _xmlData )
        {
            XmlReader reader = XmlReader.Create( new StringReader( _xmlData ) );

            reader.ReadToFollowing( "filename" );
            String _filename = reader.ReadElementContentAsString();

            reader = XmlReader.Create( new StringReader( _xmlData ) );
            reader.ReadToFollowing( "file" );
            String _file = reader.ReadElementContentAsString();

            reader = XmlReader.Create( new StringReader( _xmlData ) );
            reader.ReadToFollowing( "destination" );
            String _destination = reader.ReadElementContentAsString();

            if( _destination[ _destination.Length - 1 ] != '/' )
            {
                _destination += "/";
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml( "<package></package>" );

            // Uploading file to DB

            // Checking file existence
            if( !CheckFile( _destination + Path.GetFileName( _filename ) ) )
            {
                AddFile( _destination, Path.GetFileName( _filename ), Encoding.ASCII.GetBytes( _file ) ); 
                
                // create XML packet - tag -2
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-2";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode msg = doc.CreateElement( "message" );
                msg.InnerText = "File has been successfully added!";
                doc.DocumentElement.AppendChild( msg ); 
            }
            else
            {
                // create XML packet - tag -1
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-1";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode error = doc.CreateElement( "error" );
                error.InnerText = "File already exists or the path is invalid!";
                doc.DocumentElement.AppendChild( error ); 
            }

            return doc.InnerXml.ToString();
        }

        public void AddFile( String _filepath, String _filename, byte [] buf )
        {
            AccessConnection.Open();

            // Getting dir ID
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "SELECT code FROM Folders WHERE folder_path='" + _filepath + "'";
            cmd.Connection = AccessConnection;

            OleDbDataReader dr = cmd.ExecuteReader();

            dr.Read();
            int _dirCode = Convert.ToInt16( dr[ "code" ] );
            dr.Close();

            // Inserting new file with dir ID, content(_file), filename, filepath+filename as 'filepath'
            cmd.CommandText = "INSERT INTO Files ( folder_id, content, filepath, filename ) VALUES( @folder_id, @content, @filepath, @filename )";
            cmd.Connection = AccessConnection;

            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
            dataAdapter.InsertCommand = cmd;

            cmd.Parameters.Add( "@folder_id", OleDbType.Integer, 50 ).Value = _dirCode;
            cmd.Parameters.Add( "@content", OleDbType.Binary, buf.Length ).Value = buf;
            cmd.Parameters.Add( "@filepath", OleDbType.VarChar, ( _filepath + _filename ).Length ).Value = _filepath + _filename;
            cmd.Parameters.Add( "@filename", OleDbType.VarChar, ( _filename ).Length ).Value = _filename;
            
            cmd.ExecuteNonQuery();
            
            AccessConnection.Close();
        }

        String CRParse( String _xmlData )
        {
            XmlReader reader = XmlReader.Create( new StringReader( _xmlData ) );

            reader.ReadToFollowing( "path" );
            String _path = reader.ReadElementContentAsString();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml( "<package></package>" );
            
            if( _path[ _path.Length - 1 ] != '/' )
            {
                _path += "/";
            }

            if( CheckPathes( _path ) )
            {
                // Now creating new dir in the this parent dir         
                CreateDir( GetDirName( _path ), _path );

                // create XML packet - tag -2
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-2";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode msg = doc.CreateElement( "message" );
                msg.InnerText = "New dir has been created successfully!";
                doc.DocumentElement.AppendChild( msg ); 
            }
            else
            {
                // create XML packet - tag -1
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-1";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode error = doc.CreateElement( "error" );
                error.InnerText = "Invalid dir path, or may be directory already exists!";
                doc.DocumentElement.AppendChild( error ); 
            }

            return doc.InnerXml.ToString();
        }

        String DELParseFile( String _xmlData )
        {
            XmlReader reader = XmlReader.Create( new StringReader( _xmlData ) );

            reader.ReadToFollowing( "path" );
            String _path = reader.ReadElementContentAsString();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml( "<package></package>" );

            // Checking file existence
            if( CheckFile( _path ) )
            {
                DeleteFile( _path );

                // create XML packet - tag -2
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-2";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode msg = doc.CreateElement( "message" );
                msg.InnerText = "File has been successfully deleted!";
                doc.DocumentElement.AppendChild( msg ); 
            }
            else
            {
                // create XML packet - tag -1
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-1";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode error = doc.CreateElement( "error" );
                error.InnerText = "File does not exist!";
                doc.DocumentElement.AppendChild( error ); 
            }

            return doc.InnerXml.ToString();
        }

        String DELParseDir( String _xmlData )
        {
            XmlReader reader = XmlReader.Create( new StringReader( _xmlData ) );

            reader.ReadToFollowing( "path" );
            String _path = reader.ReadElementContentAsString();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml( "<package></package>" );

            if( _path == "/" )
            {
                // create XML packet - tag -1
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-1";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode error = doc.CreateElement( "error" );
                error.InnerText = "Invalid dir!";
                doc.DocumentElement.AppendChild( error ); 

                return doc.InnerXml.ToString();
            }
            
            if( _path[ _path.Length - 1 ] != '/' )
            {
                _path += "/";
            }

            if( CheckDir( _path ) )
            {
                // Now deleting given dir and all internal content
                DeleteDir( GetDirName( _path ), _path );

                // create XML packet - tag -2
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-2";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode msg = doc.CreateElement( "message" );
                msg.InnerText = "Dir has been deleted successfully!";
                doc.DocumentElement.AppendChild( msg ); 
            }
            else
            {
                // create XML packet - tag -1
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-1";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode error = doc.CreateElement( "error" );
                error.InnerText = "Dir does not exists!";
                doc.DocumentElement.AppendChild( error ); 
            }

            return doc.InnerXml.ToString();
        }

        String RNParse( String _xmlData )
        {
            XmlReader reader = XmlReader.Create( new StringReader( _xmlData ) );

            reader.ReadToFollowing( "old_name" );
            String _oldname = reader.ReadElementContentAsString();

            reader = XmlReader.Create( new StringReader( _xmlData ) );
            reader.ReadToFollowing( "new_name" );
            String _newname = reader.ReadElementContentAsString();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml( "<package></package>" );

            // Checking file existence
            if( CheckFile( _oldname ) )
            {
                RenameFile( _oldname, _newname );

                // create XML packet - tag -2
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-2";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode msg = doc.CreateElement( "message" );
                msg.InnerText = "File has been successfully renamed!";
                doc.DocumentElement.AppendChild( msg ); 
            }
            else
            {
                // create XML packet - tag -1
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-1";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode error = doc.CreateElement( "error" );
                error.InnerText = "File does not exist!";
                doc.DocumentElement.AppendChild( error ); 
            }

            return doc.InnerXml.ToString();
        }

        String LSParse( String _xmlData )
        {
            XmlReader reader = XmlReader.Create( new StringReader( _xmlData ) );

            reader.ReadToFollowing( "dir" );
            String _dir = reader.ReadElementContentAsString();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml( "<package></package>" );

            String content = "";

            AccessConnection.Open();

            OleDbCommand cmd = new OleDbCommand();
            
            if( _dir[ _dir.Length - 1 ] != '/' )
            {
                _dir += "/";
            }

            cmd.CommandText = "SELECT Code FROM Folders WHERE INSTR( folder_path, '" + _dir + "' ) = 1 AND folder_path <> '" + _dir + "'";
            cmd.Connection = AccessConnection;

            OleDbDataReader dr = cmd.ExecuteReader();

            List<int> _dirCodes = new List<int>();

            while( dr.Read() )
            {
                _dirCodes.Add( ( int )dr[ "code" ] );
            }

            dr.Close();
            
            foreach( int p in _dirCodes) 
            {
                OleDbCommand _cmd = new OleDbCommand();
                _cmd.CommandText = "SELECT folder_name FROM Folders WHERE Code = " + p.ToString();
                _cmd.Connection = AccessConnection;

                OleDbDataReader _dr = _cmd.ExecuteReader();

                if( _dr.Read() )
                {
                    content += _dr[ "folder_name" ].ToString() + "/\n";
                }
            }

            cmd.CommandText = "select code from Folders where folder_path='" + _dir + "'";
            cmd.Connection = AccessConnection;

            dr = cmd.ExecuteReader();

            int _dirCode = 0;

            if( dr.Read() )
            {
                _dirCode = Convert.ToInt16( dr[ "code" ] );
            }
            else
            {
                // create XML packet - tag -1
                XmlNode tag = doc.CreateElement( "tag" );
                tag.InnerText = "-1";
                doc.DocumentElement.AppendChild( tag ); 

                XmlNode error = doc.CreateElement( "error" );
                error.InnerText = "Invalid DIR!";
                doc.DocumentElement.AppendChild( error ); 
                
                dr.Close();
                AccessConnection.Close();
                
                return doc.InnerXml.ToString();
            }

            dr.Close();

            cmd.CommandText = "select filename from Files where folder_id=" + _dirCode;
            dr = cmd.ExecuteReader();

            while( dr.Read() )
            {
                content += dr[ "filename" ].ToString() + "\n";
            }

            dr.Close();

            AccessConnection.Close();

            // create XML packet - tag 1
            XmlNode tagXML = doc.CreateElement( "tag" );
            tagXML.InnerText = "1";
            doc.DocumentElement.AppendChild( tagXML ); 

            XmlNode contentXML = doc.CreateElement( "content" );
            contentXML.InnerText = content;
            doc.DocumentElement.AppendChild( contentXML ); 

            return doc.InnerXml.ToString();
        }

        public void RenameFile( String _file, String _newName )
        {
            List<string> w_Array = _newName.Split( '/' ).ToList();
            w_Array.RemoveAll( delegate( string obj ) { return obj == ""; } );

            String _newDir = "/";

            for( int i =0; i < w_Array.Count - 1; i++ )
            {
                _newDir += w_Array[ i ] + "/";
            }

            // If _newDir does not exists - create it!
            if( CheckPathes( _newDir ) )
            {
                // Now creating new dir      
                CreateDir( GetDirName( _newDir ), _newDir );
            }

            // Get _newDir ID
            AccessConnection.Open();

            OleDbCommand cmd = new OleDbCommand();

            cmd.CommandText = "SELECT code FROM Folders WHERE folder_path='" + _newDir + "'";
            cmd.Connection = AccessConnection;

            OleDbDataReader dr = cmd.ExecuteReader();

            dr.Read();
            int _dirCode = Convert.ToInt16( dr[ "code" ] );
            dr.Close();

            // UPDATE old file record with _newDir ID, _newName, Path.GetFileName( _newName ) )
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
            
            cmd.Parameters.Clear();
            cmd.Parameters.Add( "@folder_id", OleDbType.Integer, 50 ).Value = _dirCode;
            cmd.CommandText = "UPDATE Files SET folder_id = @folder_id WHERE filepath = '" + _file + "'";
            dataAdapter.InsertCommand = cmd;
            cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            cmd.Parameters.Add( "@filename", OleDbType.VarChar, Path.GetFileName( _newName ).Length ).Value = Path.GetFileName( _newName );
            cmd.CommandText = "UPDATE Files SET filename = @filename WHERE filepath = '" + _file + "'";
            dataAdapter.InsertCommand = cmd;
            cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            cmd.Parameters.Add( "@filepath", OleDbType.VarChar, _newName.Length ).Value = _newName;
            cmd.CommandText = "UPDATE Files SET filepath = @filepath WHERE filepath = '" + _file + "'";
            dataAdapter.InsertCommand = cmd;
            cmd.ExecuteNonQuery();

            AccessConnection.Close();
        }

        public void DeleteDir( String _dirName, String _dirPath )
        {
            AccessConnection.Open();
            
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "SELECT Code FROM Folders WHERE INSTR( folder_path, '" + _dirPath + "' ) = 1";
            cmd.Connection = AccessConnection;

            OleDbDataReader dr = cmd.ExecuteReader();

            while( dr.Read() )
            {
                OleDbCommand _cmd = new OleDbCommand();
                _cmd.CommandText = "DELETE FROM Folders WHERE Code = " + dr[ "code" ];
                _cmd.Connection = AccessConnection;

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter.InsertCommand = _cmd;

                _cmd.ExecuteNonQuery();
            }

            dr.Close();

            cmd.CommandText = "SELECT filepath FROM Files WHERE INSTR( filepath, '" + _dirPath + "' ) = 1";
            cmd.Connection = AccessConnection;

            dr = cmd.ExecuteReader();

            while( dr.Read() )
            {
                OleDbCommand _cmd = new OleDbCommand();
                _cmd.CommandText = "DELETE FROM Files WHERE filepath = '" + dr[ "filepath" ].ToString() + "'";
                _cmd.Connection = AccessConnection;

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter.InsertCommand = _cmd;

                _cmd.ExecuteNonQuery();
            }

            dr.Close();

            AccessConnection.Close();
        }

        public void DeleteFile( String _file )
        {
           AccessConnection.Open();

           OleDbCommand cmd = new OleDbCommand();
           cmd.CommandText = "DELETE FROM Files WHERE filepath = '" + _file + "'";
           cmd.Connection = AccessConnection;

           OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
           dataAdapter.InsertCommand = cmd;

           cmd.ExecuteNonQuery();

           AccessConnection.Close();
        }

        // Create dir command implementation
        public void CreateDir( String folder_name, String folder_path )
        {
            AccessConnection.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "INSERT INTO Folders ( folder_name, folder_path ) VALUES ( '" + folder_name + "', '" + folder_path + "' )";
            cmd.Connection = AccessConnection;

            OleDbDataReader dr = cmd.ExecuteReader();

            dr.Close();
            AccessConnection.Close();
            return;
        }

        // Getting dir_name from the query dir_path
        public String GetDirName( String query )
        {
            if( query[ query.Length - 1 ] != '/' )
            {
                query += "/";
            }

            List<string> w_Array = query.Split( '/' ).ToList();
            w_Array.RemoveAll( delegate( string obj ) { return obj == ""; } );

            return w_Array[ w_Array.Count - 1 ];
        }

        // Checking if file exists
        public Boolean CheckFile( String file )
        {
            if( AccessConnection == null )
            {
                return false;
            }

            AccessConnection.Open();

            OleDbCommand cmd = new OleDbCommand();

            if( file == "" )
            {
                return false;
            }

            cmd.CommandText = "SELECT code FROM Files WHERE filepath = '" + file + "'";
            cmd.Connection = AccessConnection;

            OleDbDataReader dr = cmd.ExecuteReader();

            if( dr.Read() )
            {
                dr.Close();
                AccessConnection.Close();
                return true;
            }
            else
            {
                dr.Close();
                AccessConnection.Close();
                return false;
            }
        }

        public String GetFile( String _filepath )
        {
            String buf = null;

            AccessConnection.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "SELECT content FROM Files WHERE filepath = '" + _filepath + "'";
            cmd.Connection = AccessConnection;
            OleDbDataReader dr = cmd.ExecuteReader();

            int bufferSize = 1024 * 1024;
            byte[] buffer = new byte[ bufferSize ];
            long startindex = 0;

            if( dr.Read() ) 
            {
                long retval = dr.GetBytes( 0, startindex, buffer, 0, bufferSize );

                while( retval == bufferSize ) 
                {
                    buf += Encoding.Default.GetString( buffer, 0, ( int )retval );
                    startindex += bufferSize;
                    retval = dr.GetBytes( 0, startindex, buffer, 0, bufferSize );
                }

                buf += Encoding.Default.GetString( buffer, 0, ( int )retval );
            }

            dr.Close();
            AccessConnection.Close();
            return buf;
        }

        public Boolean CheckPathes( String Path )
        {
            if( Path == "" )
            {
                return false;
            }

            // Lets generate the parent directory path and check if the parent directory exists  
            
            // Checking if dir does not already exists
            if( !CheckDir( Path ) && CheckDir( GetParent( Path ) ) )
            {
                return true;
            }

            return false;
        }

        // Getting parent dir for the query dir_path
        public String GetParent( String query )
        {
            if( query[ query.Length - 1 ] != '/' )
            {
                query += "/";
            }

            List<string> w_Array = query.Split( '/' ).ToList();
            w_Array.RemoveAll( delegate( string obj ) { return obj == ""; } );

            String parent_path = "/";

            for( int i = 0; i < w_Array.Count - 1 ; i++ )
            {
                parent_path += w_Array[ i ] + "/";
            }

            return parent_path;
        }

        // Checking if dir exists
        public Boolean CheckDir( String dir )
        {
            if( dir[ dir.Length - 1 ] != '/' )
            {
                dir += "/";
            }

            AccessConnection.Open();

            OleDbCommand cmd = new OleDbCommand();

            if( dir == "" )
            {
                return false;
            }

            cmd.CommandText = "SELECT code FROM Folders WHERE folder_path='" + dir + "'";
            cmd.Connection = AccessConnection;

            OleDbDataReader dr = cmd.ExecuteReader();

            if( dr.Read() )
            {
                dr.Close();
                AccessConnection.Close();
                return true;
            }
            else
            {
                dr.Close();
                AccessConnection.Close();
                return false;
            }
        }

		// This method could be called by either the main thread or any of the
		// worker threads
		private void AppendToRichEditControl( string msg ) 
		{
			// Check to see if this method is called from a thread 
			// other than the one created the control
			if( InvokeRequired ) 
			{
				// We cannot update the GUI on this thread.
				// All GUI controls are to be updated by the main (GUI) thread.
				// Hence we will use the invoke method on the control which will
				// be called when the Main thread is free
				// Do UI update on UI thread
				object[] pList = { msg };

				richTextBoxReceivedMsg.BeginInvoke( new UpdateRichEditCallback( OnUpdateRichEdit ), pList );
			}
			else
			{
				// This is the main thread which created this control, hence update it directly 
				OnUpdateRichEdit( msg );
			}
		}

		// This UpdateRichEdit will be run back on the UI thread
		// ( using System.EventHandler signature so we don't need to define a new
		// delegate type here )
		private void OnUpdateRichEdit( string msg ) 
		{
			richTextBoxReceivedMsg.AppendText( msg );
		}

		private void UpdateClientListControl() 
		{
			if ( InvokeRequired ) // Is this called from a thread other than the one created the control
			{
				// We cannot update the GUI on this thread.
				// All GUI controls are to be updated by the main (GUI) thread.
				// Hence we will use the invoke method on the control which will
				// be called when the Main thread is free
				// Do UI update on UI thread
				listBoxClientList.BeginInvoke( new UpdateClientListCallback( UpdateClientList ), null );
			}
			else
			{
				// This is the main thread which created this control, hence update it
				// directly 
				UpdateClientList();
			}
		}

        private void buttonClose_Click(object sender, EventArgs e)
        {
            CloseSockets();
			Close();
        }

        void CloseSockets()
		{
			if( m_mainSocket != null )
			{
				m_mainSocket.Close();
			}

			Socket workerSocket = null;

			for( int i = 0; i < m_workerSocketList.Count; i++ )
			{
				workerSocket = ( Socket )m_workerSocketList[ i ];

				if( workerSocket != null )
				{
					workerSocket.Close();
					workerSocket = null;
				}
			}	
		}

        // Update the list of clients that is displayed
		void UpdateClientList()
		{
			listBoxClientList.Items.Clear();

			for( int i = 0; i < m_workerSocketList.Count; i++ )
			{
				string clientKey = Convert.ToString( i + 1 );

				Socket workerSocket = ( Socket )m_workerSocketList[ i ];

				if( workerSocket != null )
				{
					if( workerSocket.Connected )
					{
						listBoxClientList.Items.Add( clientKey );
					}
				}
			}
		}

		void SendMsgToClient( string msg, int clientNumber )
		{
			// Convert the reply to byte array
			byte[] byData = Encoding.ASCII.GetBytes( msg );

			Socket workerSocket = ( Socket )m_workerSocketList[ clientNumber - 1 ];
			workerSocket.Send( byData );
		}

        private void buttonSendMsg_Click(object sender, EventArgs e)
        {
            try
			{
				string msg = richTextBoxSendMsg.Text;

				msg = "Server Msg: " + msg + "\n";

				byte[] byData = Encoding.ASCII.GetBytes( msg );

				Socket workerSocket = null;
				
                for( int i = 0; i < m_workerSocketList.Count; i++ )
				{
					workerSocket = ( Socket )m_workerSocketList[ i ];

                    if( workerSocket!= null )
					{
						if( workerSocket.Connected )
						{
							workerSocket.Send( byData );
						}
					}
				}
			}
			catch( SocketException se )
			{
				MessageBox.Show( se.Message );
			}
        }

        private void buttonStopListen_Click(object sender, EventArgs e)
        {
            textBoxPort.Enabled = true;
            CloseSockets();			
			UpdateControls( false );
        }
    }
}