using Microsoft.EntityFrameworkCore;
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
    public class KardexInventarioRepositorio : Repositorio<KardexInventario>, IKardexInventarioRepositorio
    {
        private readonly ApplicationDbContext _db;

        public KardexInventarioRepositorio(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }

        public async Task RegistrarKardex(int bodegaProductoId, string tipo, string detalle, int stockAnterior, int cantidad, string usuarioId)
        {
            //Registro de bodegaProducto y los datos del producto
            var bodegaProducto = await _db.BodegasProductos.Include(b => b.Producto).FirstOrDefaultAsync(b => b.Id == bodegaProductoId);
            if (tipo == "Entrada")
            {
                //Creo un nuevo registro de Kardex
                KardexInventario kardex = new KardexInventario();
                kardex.BodegaProductoId= bodegaProductoId;
                kardex.Tipo= tipo;
                kardex.Detalle= detalle;
                kardex.StockAnterior= stockAnterior;
                kardex.Cantidad= cantidad;
                //Por el include puedo obtener el costo
                kardex.Costo = bodegaProducto.Producto.Costo;
                kardex.Stock = stockAnterior + cantidad;
                kardex.Total = kardex.Stock * kardex.Costo;
                kardex.UsuarioAplicacionId= usuarioId;
                kardex.FechaRegistro = DateTime.Now;

                await _db.KardexInventarios.AddAsync(kardex);
                await _db.SaveChangesAsync();
            }
            if (tipo == "Salida")
            {
                //Creo un nuevo registro de Kardex
                KardexInventario kardex = new KardexInventario();
                kardex.BodegaProductoId = bodegaProductoId;
                kardex.Tipo = tipo;
                kardex.Detalle = detalle;
                kardex.StockAnterior = stockAnterior;
                kardex.Cantidad = cantidad;
                //Por el include puedo obtener el costo
                kardex.Costo = bodegaProducto.Producto.Costo;
                kardex.Stock = stockAnterior - cantidad;
                kardex.Total = kardex.Stock * kardex.Costo;
                kardex.UsuarioAplicacionId = usuarioId;
                kardex.FechaRegistro = DateTime.Now;

                await _db.KardexInventarios.AddAsync(kardex);
                await _db.SaveChangesAsync();

            }


        }
    }
}
