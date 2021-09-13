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
using Tarea2LaboratorioAplicada;
using Tarea2LaboratorioAplicada.BLL;
using Tarea2LaboratorioAplicada.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tarea2LaboratorioAplicada.Entidades;

namespace Tarea2LaboratorioAplicada
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Usuario usuario = new Usuario();
        private void IDButton_Click(object sender, RoutedEventArgs e)
        {
            var usuario = UsuarioBLL.Buscar(Utilidades.ToInt(IDTextBox.Text));



            if (usuario != null)
            {
                this.usuario = usuario;
            }
            else
            {
                this.usuario = new Usuario();
            }



            this.DataContext = this.usuario;
        }



        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }



        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;



            var paso = UsuarioBLL.Guardar(usuario);



            if (paso)
            {
                Limpiar();
                MessageBox.Show("Operacion Exitosa!", "Exito", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Operacion Fallida!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (RolesBLL.Eliminar(Utilidades.ToInt(IDTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("No fue posible eliminar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private bool Validar()
        {
            bool esValido = true;



            if (NombresTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Transaccion Fallida", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }



            return esValido;
        }




        private void Limpiar()
        {
            AliasTextBox.Clear();
            NombresTextBox.Clear();
            EmailTextBox.Clear();
            ClaveTextBox.Clear();
            ConfClaveTextBox.Clear();
            ActivoCheckBox.IsChecked = false;
            NivelCombo.Text = "";
        }

    }
}
