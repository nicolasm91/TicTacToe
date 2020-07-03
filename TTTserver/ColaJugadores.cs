using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTserver
{
    static class ColaJugadores
    {
        static List<JugadorConectado> jugadores = new List<JugadorConectado>();

        public static void addJugador(JugadorConectado a)
        {
            jugadores.Add(a);
        }

        public static bool ifExistLista(JugadorConectado a)
        {
            return jugadores.Contains(a);
        }

        public static void removeJugador(JugadorConectado a)
        {
            var jugadoraRemover = jugadores.Single(r => r.getUserName().Equals(a.getUserName()));
            if (jugadoraRemover != null)
            {
                jugadores.Remove(jugadoraRemover);
            }
        }
        

        public static void printLista()
        {
            Console.WriteLine("======= Listado de jugadores en cola =======");
            foreach (JugadorConectado jug in jugadores)
            {
                Console.WriteLine("Jugador conectado: "+jug.getUserName());
            }
        }
    }
}
