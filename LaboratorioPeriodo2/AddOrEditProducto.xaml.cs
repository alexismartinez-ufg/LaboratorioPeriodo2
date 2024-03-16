using System;
using System.Windows;
using LaboratorioPeriodo2.DTOs;
using LaboratorioPeriodo2.DAOs;

namespace LaboratorioPeriodo2
{
    public partial class AddOrEditProducto : Window
    {
        private DTOProducto _producto = null;
        private DAOProducto _daoProducto = new DAOProducto();

        // Constructor para nuevo producto
        public AddOrEditProducto()
        {
            InitializeComponent();
            btnAgregarEditar.Content = "Agregar";
        }

        // Constructor para editar un producto existente
        public AddOrEditProducto(DTOProducto producto) : this()
        {
            _producto = producto;
            btnAgregarEditar.Content = "Editar";
            LoadProductoData();
        }

        private void LoadProductoData()
        {
            if (_producto != null)
            {
                txtCodigoProd.Text = _producto.CodigoProd.ToString();
                txtNombreProd.Text = _producto.NombreProd;
                txtNombreProv.Text = _producto.NombreProv;
                txtPrecioUnit.Text = _producto.PrecioUnit.ToString();
                txtUnidades.Text = _producto.Unidades.ToString();
                txtCodigoProd.IsEnabled = false; // Evitar edición del código de producto
            }
        }

        private void btnAgregarEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var producto = new DTOProducto
                {
                    CodigoProd = int.Parse(txtCodigoProd.Text),
                    NombreProd = txtNombreProd.Text,
                    NombreProv = txtNombreProv.Text,
                    PrecioUnit = decimal.Parse(txtPrecioUnit.Text),
                    Unidades = int.Parse(txtUnidades.Text)
                };

                if (_producto == null)
                {
                    // Agregar nuevo producto
                    _daoProducto.Add(producto);
                    MessageBox.Show("Producto agregado con éxito.");
                }
                else
                {
                    // Editar producto existente
                    producto.CodigoProd = _producto.CodigoProd; // Asegurarse de mantener el mismo código de producto
                    _daoProducto.Edit(producto);
                    MessageBox.Show("Producto editado con éxito.");
                }

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar agregar/editar el producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
