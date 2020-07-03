using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tictactoe
{
    class Tablero
    {
        private List<Jugada> casilleros;

        public Tablero()
        {
            casilleros = new List<Jugada>();
        }

        public bool checkPos(Jugada jug)
        {
            if (this.casilleros[jug.getPos()] != null)
            {
                return false;
            }
            return true;
        }

        public void setPos(Jugada jug)
        {
            this.casilleros.Insert(jug.getPos(),jug);
            Console.WriteLine("Jugador: "+ jug.jugadorName()+" || Jugada: "+jug.getPos());
            
        }
    }
}
