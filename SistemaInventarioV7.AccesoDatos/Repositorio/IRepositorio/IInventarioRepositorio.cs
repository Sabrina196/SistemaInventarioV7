using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventarioV7.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV7.AccesoDatos.Repositorio.IRepositorio
{
    public interface IInventarioRepositorio: IRepositorio<Inventario>
    {
        void Actualizar(Inventario inventario);

        //Método para el ViewModel
        IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj);


    }
}
