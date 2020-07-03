using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TTTserver
{
    class JugadorConectado
    {
        Usuario user { get; set; }
        TcpClient tcp { get; set; }

        public JugadorConectado(Usuario user, TcpClient tcp)
        {
            this.user = user;
            this.tcp = tcp;
        }

        public string getUserName()
        {
            return user.getName();
        }
    }
}
