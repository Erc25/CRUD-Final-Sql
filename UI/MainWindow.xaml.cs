using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BIZ;
using DAL;

namespace UI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProductoBIZ productosBLL = new ProductoBIZ();
        public MainWindow()
        {
            InitializeComponent();
            CargarProductos();

        }

        private void CargarProductos()
        {
            List<Producto> productos = productosBLL.ObtenerProductos();
            dtgProductos.ItemsSource = productos;
        }

        private void AgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombre.Text;
            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Ingrese un precio válido.");
                return;
            }
            string descripcion = txtDescripcion.Text;

            Producto nuevoProducto = new Producto
            {
                Nombre = nombre,
                Precio = precio,
                Descripcion = descripcion
            };

            try
            {
                productosBLL.Insertar(nuevoProducto);
                CargarProductos(); // Recargar la lista de productos después de la inserción

                // Mostrar mensaje de éxito
                MessageBox.Show("El producto se ha insertado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error
                MessageBox.Show($"Error al insertar el producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProductos.SelectedItem != null)
            {
                Producto productoSeleccionado = (Producto)dtgProductos.SelectedItem;
                int idProducto = productoSeleccionado.Id;

                MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar este producto?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    productosBLL.Eliminar(idProducto);
                    CargarProductos(); // Recargar la lista de productos después de la eliminación
                    MessageBox.Show("El producto se ha eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProductos.SelectedItem != null)
            {
                Producto productoSeleccionado = (Producto)dtgProductos.SelectedItem;

                // Mostrar los datos del producto seleccionado en los TextBox
                txtNombre.Text = productoSeleccionado.Nombre;
                txtPrecio.Text = productoSeleccionado.Precio.ToString();
                txtDescripcion.Text = productoSeleccionado.Descripcion;
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para editar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProductos.SelectedItem != null)
            {
                Producto productoSeleccionado = (Producto)dtgProductos.SelectedItem;

                // Actualizar los datos del producto seleccionado con los datos de los TextBox
                productoSeleccionado.Nombre = txtNombre.Text;
                productoSeleccionado.Precio = decimal.Parse(txtPrecio.Text);
                productoSeleccionado.Descripcion = txtDescripcion.Text;

                // Llamar al método de la capa BLL para actualizar el producto en la base de datos
                productosBLL.Editar(productoSeleccionado);

                // Actualizar el DataGrid con los cambios
                CargarProductos();

                MessageBox.Show("Los cambios se han guardado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para guardar los cambios.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
