
using Backend.Dominio;
using Backend.Implementacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrontEnd.Presentacion
{
    public partial class Frm_Alta : Form
    {
        private Servicio servicio;
        private Factura factura;

        public Frm_Alta()
        {
            InitializeComponent();
            servicio = new Servicio();
        }




        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(dgvDetalles.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar al menos un producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                RegistrarFactura();
                Limpiar();
                Habilitar(true);
            }
        }

        private void Habilitar(bool x)
        {
            dtpFecha.Enabled = x;
            cboForma.Enabled = x;
            txtCliente.Enabled = x;
        }

        private void Limpiar()
        {
            dtpFecha.Value = DateTime.Now;
            cboForma.SelectedIndex = -1;
            cboProducto.SelectedIndex = -1;
            txtCliente.Text = "";
            nudCantidad.Value = 1;
            dgvDetalles.Rows.Clear();
        }

        private async void RegistrarFactura()
        {
            factura.Total = CalcularTotal();
            int? nroFactura = await servicio.Save(factura);
            if(nroFactura != 0)
            {
                MessageBox.Show($"Factura {nroFactura} ingresada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                factura = new Factura();
            }
            else
                MessageBox.Show("No se pudo ingresar la factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();

            }
            else
            {
                return;
            }
        }

        private void Frm_Alta_Presupuesto_Load(object sender, EventArgs e)
        {
            CargarCombo();
            factura = new Factura();
        }

        private async void CargarCombo()
        {
            cboProducto.DataSource = await servicio.GetProductos();
            cboProducto.SelectedIndex = -1;
            List<FormaPago> formas = new List<FormaPago>();
            formas.Add(new FormaPago(1, "Efectivo"));
            formas.Add(new FormaPago(2, "Tarjeta de Crédito"));
            formas.Add(new FormaPago(3, "Tarjeta de Débito"));
            cboForma.DataSource = formas;
            cboForma.SelectedIndex = -1;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                Habilitar(false);
                CargarDgv();
                cboProducto.SelectedIndex = -1;
                nudCantidad.Value = 1;
                lblTotal.Text = $"Total $: {CalcularTotal()}";
            }
        }

        private decimal CalcularTotal()
        {
            decimal total = 0;
            foreach(DataGridViewRow fila in dgvDetalles.Rows)
            {
                decimal subtotal = (decimal)fila.Cells["colSubTotal"].Value;
                total += subtotal;
            }
            return total;
        }

        private void CargarDgv()
        {
            Producto producto = (Producto)cboProducto.SelectedItem;
            FormaPago forma = (FormaPago)cboForma.SelectedItem;
            if (!ExisteProductoEnGrilla(producto.Nombre))
            {
                factura.Fecha = dtpFecha.Value;
                factura.Cliente = txtCliente.Text;
                factura.formaPago = forma;
                DetalleFactura detalle = new DetalleFactura();
                detalle.oProducto = producto;
                detalle.Cantidad = (int)nudCantidad.Value;
                factura.AddDetalle(detalle);
                decimal subtotal = CalcularSubTotal(forma, detalle);
                dgvDetalles.Rows.Add(new object[] { producto.idProducto, producto.Nombre, producto.Precio, detalle.Cantidad, subtotal });
            }
        }

        private decimal CalcularSubTotal(FormaPago forma, DetalleFactura detalle)
        {
            decimal subtotal = 0;
            if (forma.formaPago.Equals("Tarjeta de Crédito"))
            {
                subtotal = (detalle.Cantidad * detalle.oProducto.Precio) * 120 / 100;
            }
            else
                subtotal = detalle.Cantidad * detalle.oProducto.Precio;
            return subtotal;
        }

        private bool Validar()
        {
            
            if (dtpFecha.Value > DateTime.Now)
            {
                MessageBox.Show("La fecha no puede ser mayor a la actual", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (txtCliente.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if(cboForma.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una forma de pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if(cboProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private bool ExisteProductoEnGrilla(string text)
        {
            foreach (DataGridViewRow fila in dgvDetalles.Rows)
            {
                if (fila.Cells["producto"].Value.Equals(text))
                    return true;
            }
            return false;
        }

       
        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 5)
            {
                factura.RemoveDetalle(e.RowIndex);
                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
                lblTotal.Text = $"Total $: {CalcularTotal()}";
            }
        }
    }
}
