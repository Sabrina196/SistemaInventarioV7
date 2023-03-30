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
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepositorio(ApplicationDbContext db):base(db)
        {
            _db= db;
        }


        public void Actualizar(Categoria categoria)
        {
            var CategoriaBD = _db.Categorias.FirstOrDefault(c => c.Id == categoria.Id);
            if (CategoriaBD != null)
            {
                CategoriaBD.Nombre= categoria.Nombre;
                CategoriaBD.Estado= categoria.Estado;
                _db.SaveChanges();
            }
        }
    }
}
