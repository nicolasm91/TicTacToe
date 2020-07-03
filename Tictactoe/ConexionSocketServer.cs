using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tictactoe
{
    static class ConexionSocketServer
    {
        static NetworkStream stream;
        static bool logged = false;
        static bool onQueue = false;
        public static Usuario usuarioLocal;

    static public void Conectar()
        {
            TcpClient socket = new TcpClient();
            
            // 127.0.0.1 : Localhost
            socket.Connect("127.0.0.1", 8000);
            stream = socket.GetStream();
            //Thread escucharServerTH = new Thread(escucharServer);
            //escucharServerTH.Start();


        }



        static public bool getStatusCola() // Returns queue status
        {
            return onQueue;
        }

        static public bool getLoggedStatus() // Returns login status
        {
            return logged;
        }

        public static void escucharServer() // Thread
        {
                        
            while (true)
            {
                byte[] msgCliente = recibirMensaje(stream);
                interpretarMensaje(msgCliente);
            }
        }

        private static void interpretarMensaje(byte[] msgCliente)
        {
            string txtMsg = System.Text.Encoding.ASCII.GetString(msgCliente);

            JObject mensaje = JObject.Parse(txtMsg);

            string mensajeRecibido = (string)mensaje["msg"];

            Console.WriteLine("Respuesta servidor: "+mensajeRecibido);

            // RESPUESTAS JSON:{msg: '' } -- SERVIDOR

            if (mensajeRecibido.Equals("noexiste"))
            {
                Console.WriteLine("print no existe");
            }else if (mensajeRecibido.Equals("existe"))
            {
                logged = true;
            }else if (mensajeRecibido.Equals("onserver"))
            {
                setUsuarioLocal(txtMsg);
            }
        }

        private static void setUsuarioLocal(string txtMsg)
        {
            usuarioLocal = JsonConvert.DeserializeObject<Usuario>(txtMsg);
        }

        private static byte[] recibirMensaje(NetworkStream stream)
        {
            byte[] bytesFrom = new byte[4]; // tamaño int

            stream.Read(bytesFrom, 0, bytesFrom.Length);

            int buffersize = BitConverter.ToInt32(bytesFrom, 0);

            bytesFrom = new byte[buffersize]; // tamaño mensaje

            stream.Read(bytesFrom, 0, bytesFrom.Length);

            return bytesFrom;
        }



        public static void Login(Usuario usuario)
        {
            // Enviar jugador
            string sendUsuario = JsonConvert.SerializeObject(usuario);
            Console.WriteLine(sendUsuario);
            var sendBytes = System.Text.Encoding.ASCII.GetBytes(sendUsuario);
            byte[] intBytes = BitConverter.GetBytes(sendBytes.Length);

            stream.Write(intBytes, 0, intBytes.Length);
            stream.Write(sendBytes, 0, sendBytes.Length);
            stream.Flush();

            // Recibir mensaje

            byte[] bytesFrom = new byte[4]; // tamaño int
            stream.Read(bytesFrom, 0, bytesFrom.Length);

            int buffersize = BitConverter.ToInt32(bytesFrom, 0);

            bytesFrom = new byte[buffersize]; // tamaño mensaje

            stream.Read(bytesFrom, 0, bytesFrom.Length);

            interpretarMensaje(bytesFrom);
            var msgRec = System.Text.Encoding.ASCII.GetString(bytesFrom);
            System.Console.WriteLine(msgRec);

            if (msgRec.Equals("{msg: 'noexiste' }"))
            {
                Console.WriteLine("nohagasnada");
            }
            else
            {
                byte[] msgCliente = recibirMensaje(stream);
                interpretarMensaje(msgCliente);                
            }

            // Iniciar thread de escucha
            Thread escucharServerTH = new Thread(escucharServer);
            escucharServerTH.Start();
        }


        public static void ponerseEnCola()
        {
            if (!onQueue)
            {
                string testmsg = @"{msg: 'queue' }";
                string sendMsg = JsonConvert.SerializeObject(testmsg);
                var sendBytes = System.Text.Encoding.ASCII.GetBytes(testmsg);

                byte[] intBytes = BitConverter.GetBytes(sendBytes.Length);

                stream.Write(intBytes, 0, intBytes.Length);
                stream.Write(sendBytes, 0, sendBytes.Length);
                stream.Flush();

                // Recibir mensaje
                /*byte[] bytesFrom = new byte[4]; // tamaño int
                stream.Read(bytesFrom, 0, bytesFrom.Length);

                int buffersize = BitConverter.ToInt32(bytesFrom, 0);

                bytesFrom = new byte[buffersize]; // tamaño mensaje

                stream.Read(bytesFrom, 0, bytesFrom.Length);
                var msgRec = System.Text.Encoding.ASCII.GetString(bytesFrom);
                System.Console.WriteLine(msgRec);*/
                onQueue = true;
            }         
        }

        public static void salirDeCola()
        {
            string testmsg = @"{msg: 'getmeout' }";
            string sendMsg = JsonConvert.SerializeObject(testmsg);
            var sendBytes = System.Text.Encoding.ASCII.GetBytes(testmsg);

            byte[] intBytes = BitConverter.GetBytes(sendBytes.Length);

            stream.Write(intBytes, 0, intBytes.Length);
            stream.Write(sendBytes, 0, sendBytes.Length);
            stream.Flush();

            // Recibir mensaje
            /*byte[] bytesFrom = new byte[4]; // tamaño int
            stream.Read(bytesFrom, 0, bytesFrom.Length);

            int buffersize = BitConverter.ToInt32(bytesFrom, 0);

            bytesFrom = new byte[buffersize]; // tamaño mensaje

            stream.Read(bytesFrom, 0, bytesFrom.Length);
            var msgRec = System.Text.Encoding.ASCII.GetString(bytesFrom);
            System.Console.WriteLine(msgRec);*/

            onQueue = false;
            //Console.WriteLine("fuera de cola - Cliente");
        }

        static public void generarJugada()
        {
            // Traer jugada del server
        }
    }
}
