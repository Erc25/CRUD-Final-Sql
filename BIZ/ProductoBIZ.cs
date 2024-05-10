using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class ProductoBIZ
    {
        private Producto productosDAL = new Producto();

        public List<Producto> ObtenerProductos()
        {
            return productosDAL.ObtenerProductos();
        }

        public void Insertar(Producto producto)
        {
            productosDAL.Insertar(producto);
        }

        public void Eliminar(int id)
        {
            productosDAL.Eliminar(id);
        }

        public void Editar(Producto producto)
        {
            productosDAL.Editar(producto);
        }
    }
}
