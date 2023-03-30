using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV7.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nombre es Requerido")]
        [MaxLength(60, ErrorMessage = "Nombre debe ser máximo 60 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Estado es requerido")]
        public bool Estado { get; set; }
    }
}
