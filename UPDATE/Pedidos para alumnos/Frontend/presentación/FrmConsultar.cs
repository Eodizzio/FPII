using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backend.Dominio;
using Backend.Implementación;

namespace Frontend.presentación
{
    public partial class FrmConsultar : Form
    {
        private Servicio servicio;
       
        public FrmConsultar()
        {
            InitializeComponent();
            servicio = new Servicio();
        }

        private void FrmConsultar_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = new DateTime(2000,01,01);
            dtpHasta.Value = DateTime.Now;
            CargarCombo();
        }

        private async void CargarCombo()
        {
            cboClientes.DataSource = await servicio.GetClientes();
            cboClientes.SelectedIndex = -1;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            CargarDgv();
        }

        private async void CargarDgv()
        {
            int idCliente = 0;
            if(cboClientes.SelectedIndex != -1)
            {
                Cliente cliente = (Cliente)cboClientes.SelectedItem;
                idCliente = cliente.IdCliente;
            }
            DateTime desde = dtpDesde.Value;
            DateTime hasta = dtpHasta.Value;
            List<Pedido> pedidos = await servicio.GetPedidos(idCliente, desde, hasta);
            foreach(Pedido pedido in pedidos)
            {
                dgvPedidos.Rows.Add(new object[] {pedido.Codigo, pedido.Cliente, pedido.FechaEntrega.ToString("dd-MM-yyyy"), pedido.Entregado});
            }
            lblTotal.Text = $"Total de Pedidos: {dgvPedidos.Rows.Count}";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("¿Desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                this.Close();
        }

        private void dgvPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvPedidos.Rows[e.RowIndex];
            int codigo = (int)row.Cells["colCodigo"].Value;
            int opcion;
            if(dgvPedidos.CurrentCell.ColumnIndex == 4)
            {
                string entregado = row.Cells["colEntregado"].Value.ToString();
                opcion = 1;
                EntregarPedido(opcion, codigo, entregado);
            }
            if(dgvPedidos.CurrentCell.ColumnIndex == 5)
            {
                opcion = 2;
                EliminarPedido(opcion, codigo);
            }
        }

        private async void EliminarPedido(int opcion, int codigo)
        {
            if(DialogResult.Yes == MessageBox.Show("¿Desea eliminar el pedido?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            {
                await servicio.UpdatePedidos(codigo, opcion);
                MessageBox.Show("Pedido Eliminado", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private async void EntregarPedido(int opcion, int codigo, string entregado)
        {
            if (!entregado.Equals("S"))
            {
                await servicio.UpdatePedidos(codigo, opcion);
                MessageBox.Show("Pedido Entregado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dgvPedidos.Rows.Clear();
                CargarDgv();
            }
            else
                MessageBox.Show("Pedido ya fue entregado con anterioridad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
