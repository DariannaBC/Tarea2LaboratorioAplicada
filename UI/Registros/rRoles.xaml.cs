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

        public static bool Guardar(Roles roles)
        {
            if (!Existe(roles.RolId)) //Si no existe insertamos
            {
                return Insertar(roles);
            }
            else
            {
                return Modificar(roles);
            }
        }



        private static bool Insertar(Roles roles)
        {
            bool paso = false;
            Contexto contexto = new Contexto();



            try
            {
                //Agregar la entidad que se desea insertar al contexto
                contexto.Roles.Add(roles);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }



            return paso;
        }



        private static bool Modificar(Roles roles)
        {
            bool paso = false;
            Contexto contexto = new Contexto();



            try
            {
                //Marcar la entidad como modificada para que
                //El contexto sepa como proceder
                contexto.Entry(roles).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }



            return paso;
        }



        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();



            try
            {
                //Buscar la entidad que se desea eliminar
                var roles = contexto.Roles.Find(id);



                if (roles != null)
                {
                    contexto.Roles.Remove(roles); //Remover la entidad
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }



            return paso;
        }



        public static Roles Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Roles roles;



            try
            {
                roles = contexto.Roles.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return roles;
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
    }
}
