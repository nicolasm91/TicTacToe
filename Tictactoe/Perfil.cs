using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tictactoe
{
    public partial class Perfil : Form
    {


        public Perfil()
        {
            InitializeComponent();
            this.labelUserName.Text = "" + ConexionSocketServer.usuarioLocal.getName();
            this.labelScore.Text = ""+ConexionSocketServer.usuarioLocal.getScore();
            this.labelWinRate.Text = "" + ConexionSocketServer.usuarioLocal.calcularPorcentaje();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            
            if (!ConexionSocketServer.getStatusCola())
            {
                ConexionSocketServer.ponerseEnCola();
                BuscandoMatch b = new BuscandoMatch();
                b.Show();
            }
            else
            {
                MessageBox.Show("Ya estas en cola");
            }
            
            
        }
    }
}
