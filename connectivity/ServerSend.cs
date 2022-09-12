namespace Server{

    class ServerSend{

        private static void SendTCPData(Packet _packet){
            _packet.WriteLength();
            Client client = Server.client;
            client.tcp.SendData(_packet);
        }
        public static void TestMessage(int number, string message){
            using (Packet _packet = new Packet(1)){
                _packet.Write(number);
                _packet.Write(message);
                SendTCPData(_packet);
            }
        }

    }

}