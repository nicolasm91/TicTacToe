using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTserver
{
    class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string password { get; set; }
        public int puntaje { get; set; }
        public int partidasjugadas { get; set; }
        public string msg { get; set; }

        public Usuario(string nombre, string password, int puntaje, int partidasjugadas)
        {
            //this.msg = msg;
            this.nombre = nombre;
            this.password = password;
            this.puntaje = puntaje;
            this.partidasjugadas = partidasjugadas;
        }

        public void setMsg(string msg)
        {
            this.msg = msg;
        }

        public string getName()
        {
            return this.nombre;
        }

        public string getpw()
        {
            return this.password;
        }

        public int getPuntaje()
        {
            return this.puntaje;
        }

        public int getPartidas()
        {
            return this.partidasjugadas;
        }

        public float calcularPorcentaje(int puntaje, int partidasjugadas)
        {
            float score = puntaje;
            float partidas = partidasjugadas;

            return (float)Math.Round((score / partidas), 3);
        }
    }
}
