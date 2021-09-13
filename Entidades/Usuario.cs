using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea2LaboratorioAplicada.Entidades
{
    public class Usuario
    {
        [Key]
        public int id { get; set; }
        public string alias { get; set; }
       public string nombres { get; set; }
        public string email { get; set; }
        public string clave { get; set; }
        public float costo { get; set; }
        public string nivel { get; set; }


    }
}
