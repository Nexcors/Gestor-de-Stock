using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestordeStock.Model
{
    internal abstract class BaseProductSupplier
    {
        private int id;
        private string nombre;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set { nombre = value; } }

        protected BaseProductSupplier(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        protected BaseProductSupplier(int id)
        {
            Id = id;
        }
        protected BaseProductSupplier(string nombre)
        {
            Nombre = nombre;
        }

        protected BaseProductSupplier()
        {
        }
    }
}
