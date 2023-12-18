using RecetasSLN.datos;
using RecetasSLN.datos.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecetasSLN.presentación
{
    public partial class FrmConsultar : Form
    {
       
        public FrmConsultar()
        {
            InitializeComponent();
            
        }

        private void FrmConsultar_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Now.AddDays(-30);
            CargarCombo();
        }

        private void CargarCombo()
        {
            //Completar...
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //Completar...
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Completar...
        }

        private void dgvPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Completar...
        }
    }
}
