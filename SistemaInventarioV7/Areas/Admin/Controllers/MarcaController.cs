using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using SistemaInventarioV7.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioV7.Modelos;
using SistemaInventarioV7.Utilidades;

namespace SistemaInventarioV7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public MarcaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get
        public async Task<IActionResult> Upsert(int? id)
        {
            Marca marca = new Marca();
            if (id == null)
            {
                marca.Estado = true;
                return View(marca);
            }
            marca = await _unidadTrabajo.Marca.Obtener(id.GetValueOrDefault());
            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }


        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Marca marca)
        {
            if (ModelState.IsValid)
            {
                if (marca.Id == 0)
                {
                    await _unidadTrabajo.Marca.Agregar(marca);
                    TempData[DS.Exitosa] = "Marca creada exitosamente";
                }
                else
                {
                   _unidadTrabajo.Marca.Actualizar(marca);
                    TempData[DS.Exitosa] = "Marca actualizada exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar Marca";
            return View(marca);
        }






        #region
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Marca.ObtenerTodos();
            return Json(new { data = todos });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var marcaDb = await _unidadTrabajo.Marca.Obtener(id);
            if (marcaDb == null)
            {
                return Json(new { success = false, message = "Error al eliminar la Marca, " });
            }
            _unidadTrabajo.Marca.Remover(marcaDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Marca eliminada exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Marca.ObtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(m => m.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                if (nombre != null)
                {
                    valor = lista.Any(m => m.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && m.Id != id);
                }
            }
            if (valor)
            {
                return Json(new {data = true });
            }
            return Json(new { success = false });

        }


        #endregion
    }
}
