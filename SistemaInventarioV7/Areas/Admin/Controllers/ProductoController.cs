using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaInventarioV7.AccesoDatos.Repositorio;
using SistemaInventarioV7.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioV7.Modelos;
using SistemaInventarioV7.Modelos.ViewModels;
using SistemaInventarioV7.Utilidades;
using System.Data;

namespace SistemaInventarioV7.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Inventario)]
    public class ProductoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get
        public async Task<IActionResult> Upsert(int? id)
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = _unidadTrabajo.Producto.ObtenerTodosDropDownLista("Categoria"),
                MarcaLista = _unidadTrabajo.Producto.ObtenerTodosDropDownLista("Marca"),
                PadreLista= _unidadTrabajo.Producto.ObtenerTodosDropDownLista("Producto")
            };

            if (id == null)
            {
                //Crear Nuevo Producto
                productoVM.Producto.Estado = true;
                return View(productoVM);
            }
            else
            {
                //Se Actualiza Producto
                productoVM.Producto = await _unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());
                if (productoVM.Producto == null)
                {
                    return NotFound();
                }
                return View(productoVM);
            }
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productoVM.Producto.Id == 0) //Crear nuevo Producto con la ruta
                {
                    
                    //Método para Nombre de Imagen
                    string upload = webRootPath + DS.ImagenRuta;
                    Task<string> nombreImagenTask = _unidadTrabajo.Producto.CrearNombreImagenAsync(files, webRootPath, upload);
                    string nombreImagen = await nombreImagenTask;

                    //Guardamos el nombre del archivo que se graba en el campo ImagenUrl de la tabla
                    productoVM.Producto.ImagenUrl = nombreImagen;
                    await _unidadTrabajo.Producto.Agregar(productoVM.Producto);
                }
                else
                {
                    //Actualizar Producto
                    var objProducto = await _unidadTrabajo.Producto.ObtenerPrimero(p => p.Id == productoVM.Producto.Id, isTracking: false);
                    //Valido si se esta cargando una nueva imagen
                    if (files.Count >0)
                    {
                        //Método para Nombre de Imagen
                        string upload = webRootPath + DS.ImagenRuta;
                        Task<string> nombreImagenTask = _unidadTrabajo.Producto.CrearNombreImagenAsync(files, webRootPath, upload);
                        string nombreImagen = await nombreImagenTask;

                        //Eliminamos la imagen anterior
                        var anteriorfile = Path.Combine(upload, objProducto.ImagenUrl);
                        if (System.IO.File.Exists(anteriorfile))
                        {
                            System.IO.File.Delete(anteriorfile);
                        }
                        productoVM.Producto.ImagenUrl = nombreImagen;
                    }
                    else
                    {
                        //En caso de que no se actualice la Imagen se mantiene la anterior
                        productoVM.Producto.ImagenUrl = objProducto.ImagenUrl;
                    }
                    _unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                }
                TempData[DS.Exitosa] = "Transacción Exitosa";
                await _unidadTrabajo.Guardar();
                return View("Index");
            }
            //Si el Modelo no válido lleno las listas
            productoVM.CategoriaLista = _unidadTrabajo.Producto.ObtenerTodosDropDownLista("Categoria");
            productoVM.MarcaLista = _unidadTrabajo.Producto.ObtenerTodosDropDownLista("Marca");
            productoVM.PadreLista = _unidadTrabajo.Producto.ObtenerTodosDropDownLista("Producto");
            return View(productoVM);
        }





        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Producto.ObtenerTodos(incluirPropiedades:"Categoria,Marca");
            return Json(new { data = todos });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var productoDb = await _unidadTrabajo.Producto.Obtener(id);
            if (productoDb == null)
            {
                return Json(new {success = false, message = "Error al eliminar el Producto"});
            }
            //Remover imagen
            string upload = _webHostEnvironment.WebRootPath + DS.ImagenRuta;
            var anteriorfile = Path.Combine(upload, productoDb.ImagenUrl);
            if (System.IO.File.Exists(anteriorfile))
            {
                System.IO.File.Delete(anteriorfile);
            }

            _unidadTrabajo.Producto.Remover(productoDb);
            await _unidadTrabajo.Guardar();
            return Json(new {success= true, message="Producto eliminado exitosamente"});
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Producto.ObtenerTodos();
            if (nombre != null) 
            {
                if (id == 0)
                {
                    valor = lista.Any(p => p.Descripcion.ToLower().Trim() == nombre.ToLower().Trim());
                }
                else
                {
                    valor = lista.Any(p => p.Descripcion.ToLower().Trim() == nombre.ToLower().Trim() && p.Id != id);
                }
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }

        #endregion
    }
}
