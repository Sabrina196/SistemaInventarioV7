using SistemaInventarioV7.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV7.AccesoDatos.Repositorio.IRepositorio
{
    public interface ICategoriaRepositorio :IRepositorio<Categoria>
    {
        void Actualizar(Categoria categoria);
    }
}
