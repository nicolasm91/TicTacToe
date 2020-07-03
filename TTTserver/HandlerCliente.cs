using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TTTserver
{
    class HandlerCliente
    {
        TcpClient clientSocket;
        NetworkStream networkStream;
        Usuario usr;
        JugadorConectado jug;
        List<Usuario> UsuariosLogin;

        public void startClient(TcpClient clientSocket)
        {
            this.clientSocket = clientSocket;
            //Thread threadClient = new Thread(parseMsg);
            Thread threadClient = new Thread(listenMsg);
            threadClient.Start();
            generarUsuarios();
        }

        private void generarUsuarios()
        {
            UsuariosLogin = new List<Usuario> ();
            UsuariosLogin.Add(new Usuario("a", "a", 1500, 3000));
            UsuariosLogin.Add(new Usuario("b", "b", 1490, 9000));
            UsuariosLogin.Add(new Usuario("Maradona", "Maradona", 1510, 2000));
            UsuariosLogin.Add(new Usuario("Marcelo", "Marcelo", 1500, 1700));

        }


        private void listenMsg()
        {
            networkStream = clientSocket.GetStream();

            while (true)
            {
                byte[] msgCliente = recibirMensaje(networkStream);
                interpretarMensaje(msgCliente);
            }
        }
        
        private byte[] recibirMensaje(NetworkStream networkStream)
        {
            byte[] bytesFrom = new byte[4]; // tamaño int
            
            networkStream = clientSocket.GetStream();

            networkStream.Read(bytesFrom, 0, bytesFrom.Length);

            int buffersize = BitConverter.ToInt32(bytesFrom, 0);

            bytesFrom = new byte[buffersize]; // tamaño mensaje

            networkStream.Read(bytesFrom, 0, bytesFrom.Length);

            return bytesFrom;
        }
        
        private void enviarMensaje(string msg)
        {
            var sendBytes = System.Text.Encoding.ASCII.GetBytes(msg);

            byte[] intBytes = BitConverter.GetBytes(sendBytes.Length);

            networkStream.Write(intBytes, 0, intBytes.Length);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
        }

        private bool checkUser(Usuario user)
        {

            foreach (Usuario u in UsuariosLogin)
            {
                if (user.getName().Equals(u.getName()) && user.getpw().Equals(u.getpw()))
                {
                    usr = u;
                    usr.setMsg("onserver");
                    return true;
                }
            }
            return false;
        }

        private void interpretarMensaje(byte[] msgCliente)
        {
            string txtMsg = System.Text.Encoding.ASCII.GetString(msgCliente);

            JObject mensaje = JObject.Parse(txtMsg);

            Console.WriteLine("mensaje de cliente: "+(string)mensaje["msg"]);

            string mensajeRecibido = (string)mensaje["msg"];

            // LOGIN DE USUARIO
            if (mensajeRecibido.Equals("login"))
            {
                
                Usuario usrAux = JsonConvert.DeserializeObject<Usuario>(txtMsg);

                if (checkUser(usrAux))
                {
                    this.jug = new JugadorConectado(usr, clientSocket);

                    Console.WriteLine("Usuario recibido y logueado: " + usr.getName() + " || " + usr.getPuntaje() + " || " + usr.getPartidas());
                    enviarMensaje(@"{msg: 'existe' }");
                    string confirmarUsuario = JsonConvert.SerializeObject(usr);
                    enviarMensaje(confirmarUsuario);
                }
                else
                {
                    Console.WriteLine("Servidor - Usuario recibido no existe");
                    enviarMensaje(@"{msg: 'noexiste' }");
                }

                
            } // PONER EN COLA
            else if (mensajeRecibido.Equals("queue"))
            {
                System.Console.WriteLine("Usuario en cola");

                
                if (true) // ColaJugadores.ifExistLista(jug) // arreglar este check
                {
                    ColaJugadores.addJugador(jug);
                    ColaJugadores.printLista();
                    // Enviar mensaje
                    enviarMensaje(@"{msg: 'Usuario en cola' }");
                }             
               
            } // SALIR DE COLA
            else if(mensajeRecibido.Equals("getmeout"))
            {

                ColaJugadores.removeJugador(jug);
                ColaJugadores.printLista();
                enviarMensaje(@"{msg: 'Usuario removido de cola' }");
            }

            

        }
        
        // Método viejo
        private void parseMsg()
        {
            
            string dataFromClient = null;

            Byte[] sendBytes = null;
            string serverResponse = null;

            JObject mensaje = null;

            while (true)
            {
                byte[] bytesFrom = new byte[4];
                // Recibi mensaje
                NetworkStream networkStream = clientSocket.GetStream();

                networkStream.Read(bytesFrom, 0, bytesFrom.Length);

                int buffersize = BitConverter.ToInt32(bytesFrom, 0);

                bytesFrom = new byte[buffersize];

                networkStream.Read(bytesFrom, 0, bytesFrom.Length);

                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);

                mensaje = JObject.Parse(dataFromClient);

                Console.WriteLine((string)mensaje["msg"]);

                string mensajeRecibido = (string)mensaje["msg"];

                if (mensajeRecibido.Equals("login"))
                {
                    System.Console.WriteLine("Usuario logueado");
                    Usuario usr = JsonConvert.DeserializeObject<Usuario>(dataFromClient);
                    String data = usr.getName();
                    System.Console.WriteLine("Printear usuario json: " + data);

                    // Enviar mensaje
                    serverResponse = "logged in";
                    sendBytes = System.Text.Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                }
                else if (mensajeRecibido.Equals("queue"))
                {
                    System.Console.WriteLine("Usuario en cola");

                    // Enviar mensaje
                    serverResponse = "it works my boi";
                    sendBytes = System.Text.Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                }

                

            }

        }
    }
}
