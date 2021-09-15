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
            int id;
            Usuario usuario = new Usuario();
            int.TryParse(IDTextBox .Text, out id);



           



            usuario = UsuarioBLL.Buscar(id);



            if (usuario != null)
            {
                MessageBox.Show("Persona Encotrada");
                LlenarCampos(usuario);
            }
            else
            {
                MessageBox.Show("Persona no Encontrada");
            }



            this.DataContext = this.usuario;
        }



        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }



        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario;
            bool paso = false;
            if (!Validar())
            {
                return;
            }
            usuario = LlenarClase();
            paso = UsuarioBLL.Guardar(usuario);



            if (paso)
            {
                Limpiar();
                MessageBox.Show("Transaccion exitosa", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Limpiar();
                MessageBox.Show("Transaccion fallida", "Fallo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
           
            int id;
            int.TryParse(IDTextBox.Text, out id);
            Limpiar();
            if (UsuarioBLL.Eliminar(id))
                MessageBox.Show("Usuario eliminado correctamente", "Proceso exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
            else
               MessageBox.Show( "ID no existente");
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

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;



            try
            {
                encontrado = contexto.Usuario.Any(e => e.id == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }



            return encontrado;
        }


        private Usuario LlenarClase()
        {
            Usuario usuario = new Usuario();
            usuario.id = int.Parse(IDTextBox.Text);
            usuario.nombres = NombresTextBox.Text;
            usuario.alias = AliasTextBox.Text;
            usuario.clave = ClaveTextBox.Text;
            usuario.email = EmailTextBox.Text;
            usuario.costo = int.Parse(CostoTextBox.Text);

            return usuario;
        }

        private void LlenarCampos(Usuario usuario)
        {
            IDTextBox.Text = usuario.id.ToString();
            NombresTextBox.Text = usuario.nombres;
            AliasTextBox.Text = usuario.alias;
            ClaveTextBox.Text = usuario.clave;
            ConfClaveTextBox.Text = usuario.clave;
            EmailTextBox.Text = usuario.email;
            CostoTextBox.Text = usuario.costo.ToString();
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Usuario usuario = UsuarioBLL.Buscar(Utilidades.ToInt(IDTextBox.Text));

            return (usuario != null);
        }


        private bool Validar()
        {
            bool paso = true;

            if (UsuarioBLL.ExisteAlias(AliasTextBox.Text))
            {
                MessageBox.Show("Mensaje repetido");
                NombresTextBox.Focus();
                paso = false;
            }

            return paso;
        }
    }
}
    

