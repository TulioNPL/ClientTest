using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTest {

    class Program {

        static void Main(string[] args){
            Client();
        }

        public static void Client(){

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("192.168.100.66"), 46111);
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            try{

                socket.Connect(serverEndPoint);

                Console.WriteLine("Conectado em {0}", socket.RemoteEndPoint?.ToString());

                byte[] msg = Encoding.ASCII.GetBytes("(30,40)<EOF>");
                int byteSent = socket.Send(msg);

                byte[] msgReceived = new Byte[1024];

                int byteReceived = socket.Receive(msgReceived);

                Console.WriteLine("Mensagem recebida -> " + Encoding.ASCII.GetString(msgReceived, 0, byteReceived));

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

            } catch(Exception e){
                Console.WriteLine(e.ToString());
            }
        }
    }
}