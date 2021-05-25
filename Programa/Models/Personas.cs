using System;
using System.Collections.Generic;
using System.Text;

namespace Applicacion.Models
{
    class Personas
    {
        public int id_persona { get; set; }
        public String identificacion { get; set; }
        public String primer_nombre { get; set; }
        public String segundo_nombre { get; set; }
        public String primer_apellido { get; set; }
        public String segundo_apellido { get; set; }
        public int id_pais_origen { get; set; }
        public int id_tipo { get; set; }
    }
}
