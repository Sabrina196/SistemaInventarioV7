using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventarioV7.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV7.AccesoDatos.Repositorio.IRepositorio
{
    public interface IProductoRepositorio :IRepositorio<Producto>
    {
        void Actualizar(Producto producto);       

        //Método para el ViewModels
        IEnumerable<SelectListItem> ObtenerTodosDropDownLista(string obj);

        //Método para Nombre y Extensión de Imagen
        Task<string> CrearNombreImagenAsync(IFormFileCollection files, string webRootPath, string upload);
    }

   
}
