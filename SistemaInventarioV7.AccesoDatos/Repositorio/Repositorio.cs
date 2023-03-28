using Microsoft.EntityFrameworkCore;
using SistemaInventarioV7.AccesoDatos.Data;
using SistemaInventarioV7.AccesoDatos.Migrations;
using SistemaInventarioV7.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV7.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db= db;
            this.dbSet= _db.Set<T>();
        }

        public async Task Agregar(T entidad)
        {
            //Este script se asemeja al insert into table
            await dbSet.AddAsync(entidad);
        }

        public async Task<T> Obtener(int id)
        {
            //Se asemeja al select * from (solo por Id)
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
           IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                //Se asemeja al select * from where
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    //Me va a incluir las propiedades de los objetos relacionados
                    //ejemplo "Categoria,Marca"
                    query = query.Include(incluirProp);
                }
                if (orderBy != null)
                {
                    query = orderBy(query);
                }
                if (!isTracking)
                {
                    query = query.AsNoTracking();
                }
                return await query.ToListAsync();
            }
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                //Se asemeja al select * from where
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    //Me va a incluir las propiedades de los objetos relacionados
                    //ejemplo "Categoria,Marca"
                    query = query.Include(incluirProp);
                }
                if (!isTracking)
                {
                    query = query.AsNoTracking();
                }

                //Retorna un solo elemento que trabaja con filtros
                return await query.FirstOrDefaultAsync();
            }
        }

        public void Remover(T entidad)
        {
            //Se asemeja a un Delete
            dbSet.Remove(entidad);
        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            //Elimina una lista de tipo de Objeto
            dbSet.RemoveRange(entidad);
        }
    }
}
