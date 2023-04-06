using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventarioV7.AccesoDatos.Data;
using SistemaInventarioV7.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioV7.Modelos;
using SistemaInventarioV7.Modelos.ViewModels;
using SistemaInventarioV7.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV7.AccesoDatos.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ProductoRepositorio(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Actualizar(Producto producto)
        {
            var productoBD = _db.Productos.FirstOrDefault(p => p.Id == producto.Id);
            if (productoBD != null)
            {
                //Verifico si actualizó la ImagenUrl para actualizar, en caso contrario se
                //conserva la imagen anterior
                if (producto.ImagenUrl != null)
                {
                    productoBD.ImagenUrl = producto.ImagenUrl;
                }

                productoBD.NumeroSerie= producto.NumeroSerie;
                productoBD.Descripcion=producto.Descripcion;
                productoBD.Precio = producto.Precio;
                productoBD.Costo=producto.Costo;
                productoBD.CategoriaId = producto.CategoriaId;
                productoBD.MarcaId=producto.MarcaId;  
                productoBD.PadreId=producto.PadreId;
                productoBD.Estado = producto.Estado;

                //Guardamos
                _db.SaveChanges();
            }
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropDownLista(string obj)
        {
            if (obj == "Categoria")
            {
                return _db.Categorias.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value= c.Id.ToString()
                });    
            }
            if (obj == "Marca")
            {
                return _db.Marcas.Where(m => m.Estado == true).Select(m => new SelectListItem
                {
                    Text = m.Nombre,
                    Value = m.Id.ToString()
                });
            }
            if (obj == "Producto")
            {
                return _db.Productos.Where(p => p.Estado == true).Select(p => new SelectListItem
                {
                    Text = p.Descripcion,
                    Value = p.Id.ToString()
                });
            }
            return null;
        }

        //Cargar Nombre y extensión de una imagen
        public async Task<string> CrearNombreImagenAsync(IFormFileCollection files, string webRootPath, string upload)
        {
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(files[0].FileName);
            string nombreImagen = $"{fileName}{extension}";

            using (var fileStream = new FileStream(Path.Combine(upload, nombreImagen), FileMode.Create))
            {
                //Se guarda la imagen física
                files[0].CopyTo(fileStream);
            }

            return await Task.FromResult(nombreImagen);

        }

    }
}
