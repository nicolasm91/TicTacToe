using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tictactoe
{
    class Jugada
    {
        Usuario jugador;
        int pos;

        public Jugada(Usuario jugador, int pos)
        {
            this.jugador = jugador;
            this.pos = pos;
        }

        public int getPos()
        {
            return this.pos;
        }

        public String jugadorName()
        {
            return jugador.getName();
        }
    }
}
