using Applicacion.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Applicacion.Models
{
    class Telefonos
    {
        public int numero { get; set; }
        public int id_pais { get; set; }
        public int id_persona { get; set; }
        public string viewTelefono
        {
            get 
            {
                Database db = new Database();
                Paises pais = db.getPais(id_pais);
                return pais!=null? string.Format("{0} : +{1} {2}", pais.iso, pais.codigo_telefono, numero): ""; 
            }
        }
    }
}
