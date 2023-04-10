using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV7.Modelos.Especificaciones
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }

        //Constructor
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize) //Redondea el número total de páginas 
            };
            //Agrega los elementos de la colección al final de la lista
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> entidad, int pageNumber, int pageSize)
        {
            var count = entidad.Count();
            var items = entidad.Skip((pageNumber -1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
