using SistemaInventarioV7.AccesoDatos.Data;
using SistemaInventarioV7.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV7.AccesoDatos.Repositorio.IRepositorio
{
    public class MarcaRepositorio : Repositorio<Marca>, IMarcaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public MarcaRepositorio(ApplicationDbContext db):base(db)
        {
            _db= db;
        }


        public void Actualizar(Marca marca)
        {
            var MarcaBD = _db.Marcas.FirstOrDefault(m => m.Id == marca.Id);
            if (MarcaBD != null)
            {
                MarcaBD.Nombre = marca.Nombre;
                MarcaBD.Estado = marca.Estado;
                _db.SaveChanges();
            }
        }
    }
}
