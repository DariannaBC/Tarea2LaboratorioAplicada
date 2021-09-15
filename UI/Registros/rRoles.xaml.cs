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
using System.Windows.Shapes;
using Tarea2LaboratorioAplicada;
using Tarea2LaboratorioAplicada.Entidades;
using Tarea2LaboratorioAplicada.BLL;
using Tarea2LaboratorioAplicada.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;



namespace Tarea2LaboratorioAplicada.UI.Registros
{
    /// <summary>
    /// Interaction logic for rRoles.xaml
    /// </summary>
    public partial class rRoles : Window
    {
        public rRoles()
        {
            InitializeComponent();
        }

        private void IDButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Limpiar()
        {
            DescripcionTextBox.Clear();
            IDTextBox.Clear();
        }

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void Almacenar_Click(object sender, RoutedEventArgs e)
        {
            Roles roles;
            bool paso = false;
            if (!Validar())
            {
                return;
            }
            roles = LlenarClase();
            paso = RolesBLL.Guardar(roles);

            if (!ExisteEnLaBaseDeDatos())
            {
                Limpiar();
                MessageBox.Show("Usuario guardado correctamente", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Limpiar();
                MessageBox.Show("Usuario modificado correctamente", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Borrar_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(IDTextBox.Text, out id);
            Limpiar();
            if (RolesBLL.Eliminar(id))
                MessageBox.Show("Usuario eliminado correctamente", "Proceso exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("ID no existente");
        }



        public static List<Roles> GetList(Expression<Func<Roles, bool>> criterio)
        {
            List<Roles> lista = new List<Roles>();
            Contexto contexto = new Contexto();



            try
            {
                //Obtener la lista y filtrarla segun el criterio recibido como parametro
                lista = contexto.Roles.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }



            return lista;
        }



        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;



            try
            {
                encontrado = contexto.Roles.Any(e => e.RolId == id);
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


        private Roles LlenarClase()
        {
            Roles roles = new Roles();
            roles.RolId = int.Parse(IDTextBox.Text);
            roles.Descripcion = DescripcionTextBox.Text;

            return roles;
        }

        private void LlenarCampos(Roles roles)
        {
            IDTextBox.Text = roles.RolId.ToString();
            DescripcionTextBox.Text = roles.Descripcion;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Roles roles = RolesBLL.Buscar(Utilidades.ToInt(IDTextBox.Text));

            return (roles != null);
        }

        private bool Validar()
        {
            bool paso = true;

            if (RolesBLL.ExisteDescripcion(DescripcionTextBox.Text))
            {
                MessageBox.Show("Mensaje repetido");
                DescripcionTextBox.Focus();
                paso = false;
            }

            return paso;
        }
    }
}
