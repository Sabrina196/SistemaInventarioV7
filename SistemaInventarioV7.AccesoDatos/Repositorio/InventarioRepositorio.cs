using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventarioV7.AccesoDatos.Data;
using SistemaInventarioV7.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioV7.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV7.AccesoDatos.Repositorio
{
    public class InventarioRepositorio : Repositorio<Inventario>, IInventarioRepositorio
    {
        private readonly ApplicationDbContext _db;

        public InventarioRepositorio(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Actualizar(Inventario inventario)
        {
            var InventarioBD = _db.Inventarios.FirstOrDefault(x => x.Id == inventario.Id);
            if (InventarioBD != null)
            {
                InventarioBD.BodegaId= inventario.BodegaId;
                InventarioBD.FechaFinal= inventario.FechaFinal;
                InventarioBD.Estado= inventario.Estado;

                _db.SaveChanges();
                
            }
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj)
        {
            if (obj == "Bodega")
            {
                return _db.Bodegas.Where(b => b.Estado == true).Select(b => new SelectListItem
                {
                    Text = b.Nombre,
                    Value = b.Id.ToString()
                });
            }
            return null;
        }
    }
}
