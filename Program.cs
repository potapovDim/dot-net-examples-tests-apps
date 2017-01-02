using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer
{
    class TCPHelper
    {
      private static TcpListener listener { get; set; }
      private static bool accet {get; set;}
      public static void StartServer(int port, bool _accet){
        accet = _accet;
        IPAddress adress = IPAddress.Parse("127.0.0.1");
        listener = new TcpListener(adress, port);
        listener.Start();
        Console.WriteLine("Server start on=  {0}:{1} ", adress, port);
      } 
      public static void Listen(){
        if(listener!=null &&  accet){
          while(true){
            Console.WriteLine("Listen for client");
            var clientTasc = listener.AcceptTcpClientAsync();
            if(clientTasc.Result!= null){
              Console.WriteLine("Client conected , wait for data");
              var client = clientTasc.Result;
              string message = "";

              while(message !=null && !message.StartsWith("quit")){
                byte [] data = Encoding.ASCII.GetBytes("Send next data: [enter 'quit' to terminate] ");
                client.GetStream().Write(data, 0, data.Length);
                byte [] buffer = new byte[1024];
                client.GetStream().Read(buffer, 0, buffer.Length);
                  
                message = Encoding.ASCII.GetString(buffer);
                Console.WriteLine(message);
              }
              Console.WriteLine("Connection close");
              client.GetStream().Dispose();
            }
          }
        }
      }
    }
    class Server
    {
        public static void Main(string[] args)
        {
            TCPHelper.StartServer(5678, true);  
            TCPHelper.Listen();
        }
    }
}
