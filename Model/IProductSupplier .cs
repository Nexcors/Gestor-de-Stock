using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GestordeStock.Model
{
    internal interface IProductSupplier
    {
        void Agregar();
        void Eliminar();
        void Actualizar();
        DataTable Listar();
        DataTable Listar_unico(string NOMBRE);

    }
}
