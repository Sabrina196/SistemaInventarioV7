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
    public class InventarioDetalleRepositorio : Repositorio<InventarioDetalle>, IInventarioDetalleRepositorio
    {
        private readonly ApplicationDbContext _db;

        public InventarioDetalleRepositorio(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }

        public void Actualizar(InventarioDetalle inventarioDetalle)
        {
            var inventarioDetalleBD = _db.InventariosDetalles.FirstOrDefault(x => x.Id == inventarioDetalle.Id);
            if (inventarioDetalleBD != null)
            {
                inventarioDetalleBD.StockAnterior = inventarioDetalle.StockAnterior;

                _db.SaveChanges();
            }
        
        }
    }
}
