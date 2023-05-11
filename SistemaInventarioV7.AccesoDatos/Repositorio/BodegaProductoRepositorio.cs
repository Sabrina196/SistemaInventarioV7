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
    public class BodegaProductoRepositorio : Repositorio<BodegaProducto>, IBodegaProductoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public BodegaProductoRepositorio(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Actualizar(BodegaProducto bodegaProducto)
        {
            var bodegaProductoBD= _db.BodegasProductos.FirstOrDefault(bp => bp.Id == bodegaProducto.Id);
            if (bodegaProductoBD != null)
            {
                //Solo actualizamos la cantidad
                bodegaProductoBD.Cantidad = bodegaProducto.Cantidad;

                _db.SaveChanges();
            }
        }
    }
}
