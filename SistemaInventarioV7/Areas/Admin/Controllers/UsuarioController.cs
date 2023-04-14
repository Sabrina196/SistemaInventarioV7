using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaInventarioV7.AccesoDatos.Data;
using SistemaInventarioV7.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioV7.Utilidades;
using System.Data;

namespace SistemaInventarioV7.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin)]
    public class UsuarioController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly ApplicationDbContext _db;

        public UsuarioController(IUnidadTrabajo unidadTrabajo, ApplicationDbContext db)
        {
            _unidadTrabajo = unidadTrabajo;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }





        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var usuarioLista = await _unidadTrabajo.UsuarioAplicacion.ObtenerTodos();
            var UserRole = await _db.UserRoles.ToListAsync(); //Usuarios por Rol
            var roles = await _db.Roles.ToListAsync(); //Todos los roles creados

            foreach (var usuario in usuarioLista)
            {
                var roleId = UserRole.FirstOrDefault(u => u.UserId == usuario.Id).RoleId;
                usuario.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }
            return Json(new { data = usuarioLista });
        }

        [HttpPost]
        public async Task<IActionResult> BloquearDesbloquear([FromBody] string id)
        {
            var usuario = await _unidadTrabajo.UsuarioAplicacion.ObtenerPrimero(u => u.Id == id);
            if (usuario == null)
            {
                return Json(new { success = false, message = "Erro de Usuario" });
            }
            if (usuario != null && usuario.LockoutEnd > DateTime.Now)
            {
                //Usuario Bloqueado
                usuario.LockoutEnd= DateTime.Now;
            }
            else
            {
                usuario.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Operación Exitosa" });
        }

        #endregion
    }
}
