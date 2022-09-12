
// dotnet publish -c release -r ubuntu.20.04-x64 --self-contained

namespace Server {
    class Program {

        private static Server server;
        private static int numberOfMessages = 10000;

        public static bool connected = false;

        static void Main(string[] args){
            server = new Server(4449);
            server.Start();
            
            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            
        }

        private static void MainThread(){
            Console.WriteLine($"Main thread started. Running at {60} ticks per second.");
            DateTime _nextLoop = DateTime.Now;
            GameLogic.Start();
            while (true){
                while (_nextLoop < DateTime.Now){
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(60);

                    if (_nextLoop > DateTime.Now){
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }

        public static void OnConnect(){
            for(int i = 0; i < numberOfMessages; i++){
                ServerSend.TestMessage(i, $"message number {i}");
            }
        }
    }
}