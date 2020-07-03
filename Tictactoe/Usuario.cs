using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tictactoe
{
    class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string password { get; set; }
        public int puntaje { get; set; }
        public int partidasjugadas { get; set; }
        public string msg { get; set; }

        public Usuario(string nombre, string password)
        {
            this.msg = "login";
            this.nombre = nombre;
            this.password = password;
            
        }

        [JsonConstructor]
        public Usuario(string nombre, string password, int puntaje, int partidasjugadas)
        {            
            this.nombre = nombre;
            this.password = password;
            this.puntaje = puntaje;
            this.partidasjugadas = partidasjugadas;

        }

        public String getName()
        {
            return this.nombre;
        }

        public int getScore()
        {
            return this.puntaje;
        }

        public int getPartidasJugadas()
        {
            return this.partidasjugadas;
        }


        public float calcularPorcentaje()
        {
            float score = puntaje;
            float partidas = partidasjugadas;

            return (float)Math.Round((score / partidas),2);
        }

    }
}
