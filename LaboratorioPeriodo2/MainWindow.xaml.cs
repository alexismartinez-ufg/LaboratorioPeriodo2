using LaboratorioPeriodo2.DAOs;
using LaboratorioPeriodo2.DTOs;
using System.Windows;

namespace LaboratorioPeriodo2
{
    public partial class MainWindow : Window
    {
        private DAOProducto daoProducto = new DAOProducto();

        public MainWindow()
        {
            InitializeComponent();
            CargarProductos();
        }

        private void CargarProductos()
        {
            try
            {
                dgProductos.ItemsSource = daoProducto.GetAllProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            var addEditProductoWindow = new AddOrEditProducto();
            var dialogResult = addEditProductoWindow.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                CargarProductos();
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem is DTOProducto productoSeleccionado)
            {
                var addEditProductoWindow = new AddOrEditProducto(productoSeleccionado);
                var dialogResult = addEditProductoWindow.ShowDialog();

                if (dialogResult.HasValue && dialogResult.Value)
                {
                    CargarProductos();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para editar.", "Selección", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem is DTOProducto productoSeleccionado)
            {
                var confirmResult = MessageBox.Show("¿Estás seguro de que quieres eliminar este producto?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        daoProducto.Delete(productoSeleccionado.CodigoProd);
                        MessageBox.Show("Producto eliminado con éxito.", "Eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarProductos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para eliminar.", "Selección", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}