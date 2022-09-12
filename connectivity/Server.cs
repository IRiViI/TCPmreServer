using System.Net;
using System.Net.Sockets;

namespace Server {

    class Server {
        public static int port {get; private set;}
        public static Client client {get; private set;} = new Client();
        public delegate void PacketHandler(Packet _packet);
        public static Dictionary<int, PacketHandler> packetHandlers = new Dictionary<int, PacketHandler>();
        private static TcpListener? tcpListener;

        public Server(int _port){
            port = _port;
        }

        public void Start(){

            Console.WriteLine("Starting server...");

            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Console.WriteLine($"Server started on port {port}");
        }

        private static void TCPConnectCallback(IAsyncResult _result){
            if(tcpListener == null) throw new ArgumentNullException("No tcp client. not an argument exception but i'm to lazy to get a propper exception");
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
            Console.WriteLine($"Incoming connection from {_client.Client.RemoteEndPoint}...");

            if (client.tcp.socket == null){
                client.tcp.Connect(_client);
                Program.connected = true;
                Program.OnConnect();
                return;
            }
            
            Console.WriteLine($"{_client.Client.RemoteEndPoint} failed to connect: Server full");
        }
    }
}